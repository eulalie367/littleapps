using System;
using System.Collections.Generic;
using System.Threading;

using System.Diagnostics;
using Library_ThreadedWorkerQueue.WorkItems;
using Library_ThreadedWorkerQueue.Management;

namespace Library_ThreadedWorkerQueue.ThreadQueues
{
	/// <summary>
	/// This class enables the custom management of asyncronse processing of customer work items.
    /// 
    /// It also supports the waitall method that allows the class to block until the current queue of worker items has completed.
    /// This however, is a potential memory leak, if this feature of the class is going to be used then it should be turned off. In order
    /// to wait on all workeritems that have been processed, then it is nessecary to hold a pointer to the waithandle of that worker item,
    /// therefore the is the potential for a minor memory leak.
	/// </summary>
	public class ThreadedQueue
	{
        private WorkItemQueue m_WorkQueue;
        private WaitCallback oWaitCallback;

        private int m_ItemsProcessing;
        private int m_NoParallelThreads;
        
        private object m_Lock_WorkItem = new object();

        /// <summary>
        /// Configures how many concurrent threads can be dispatching the workerqueue, be warned, you construct
        /// many of these classes in your application, all configured with larger numbers. Note: you only get ~20 threads per processor
        /// anyhow.
        /// 
        /// Then you will create contention between each of these objects and the threadpool, especially,
        /// if you then start blocking waiting for workitems to be completed.
        /// 
        /// I generally use values between 1-10, depending on why i am multithreading.
        /// 
        /// If a value is entered greater than the possible number of threads the computer can support, then
        /// it is set to the maximum for that computer.
        /// </summary>
        /// <param name="NoParallelThreads">Configures how many concurrent threads can be dispatching the workerqueue</param>
		public ThreadedQueue(int NoParallelThreads)
        {
            #region Configure Thread Count
            int iWorkerThreads = 0;
            int iCompletionPortThreads = 0;

            ThreadPool.GetMaxThreads(out iWorkerThreads, out iCompletionPortThreads);

            m_NoParallelThreads = NoParallelThreads > iWorkerThreads ? iWorkerThreads : NoParallelThreads;

            Debug.WriteLine(String.Format("PrioritisedWorkerQueue.Constr: Requested:{0} Availible:{1}", NoParallelThreads, m_NoParallelThreads)); 
            #endregion
			
            m_WorkQueue = new WorkItemQueue();
			m_ItemsProcessing = 0;            

            oWaitCallback = new WaitCallback(InvokeWaitHandleDelegate);
            
            if (ThreadedQueueMonitor.Instance.ThreadPoolCount != null)
                ThreadedQueueMonitor.Instance.ThreadPoolCount.Increment();
		}

        ~ThreadedQueue()
        {
            if(ThreadedQueueMonitor.Instance.ThreadPoolCount!=null)
                ThreadedQueueMonitor.Instance.ThreadPoolCount.Decrement();
        }

        /// <summary>
        /// Place a new waitcallback pointer to you method on the queue. Null will be passed as
        /// the parameter to the method.
        /// </summary>
        /// <param name="Method">WaitCallback to your method void METHOD(object)</param>
        public void QueueUserWorkItem(WaitCallback Method)
        {
            QueueUserWorkItem(Method, null);
        }

        /// <summary>
        /// Place a new waitcallback pointer to you method on the queue. The second parameter will be passed
        /// to your method.
        /// </summary>
        /// <param name="Method">WaitCallback to your method void METHOD(object)</param>
        /// <param name="State">Parameter that will be passed to your method as object</param>
        public void QueueUserWorkItem(WaitCallback Method, Object State)
        {
            QueueUserWorkItem(Method, State, ThreadPriority.Normal);
        }

        /// <summary>
        /// Place a new waitcallback pointer to you method on the queue. The second parameter will be passed
        /// to your method.
        /// 
        /// The priority is used to sort the pending workitems, prior to them being placed on the threadpool,
        /// once they are being executed that are running with the same priority.
        /// </summary>
        /// <param name="Method">WaitCallback to your method void METHOD(object)</param>
        /// <param name="State">Parameter that will be passed to your method as object</param>
        /// <param name="Priority">System.Threading.ThreadPriority of this method</param>
        public void QueueUserWorkItem(WaitCallback Method, Object State, ThreadPriority Priority)
        {
            QueueUserWorkItem(Method, State, Priority, Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Place a new waitcallback pointer to you method on the queue. The second parameter will be passed
        /// to your method.
        /// 
        /// The priority is used to sort the pending workitems, prior to them being placed on the threadpool,
        /// once they are being executed that are running with the same priority.
        /// 
        /// If a key is shared between 2 workitems, and one is already on the queue when another is attempted to be
        /// placed on the queue, then the second one isnt place upon the queue.
        /// 
        /// This is a shortcutting piece of logic.
        /// </summary>
        /// <param name="Method">WaitCallback to your method void METHOD(object)</param>
        /// <param name="State">Parameter that will be passed to your method as object</param>
        /// <param name="Priority">System.Threading.ThreadPriority of this method</param>
        /// <param name="ItemKey">Unique identifier for the work item</param>
        public void QueueUserWorkItem(WaitCallback Method, Object State, ThreadPriority Priority, object ItemKey)
		{
            WorkItem oPThreadWorkItem = new WorkItem(Method, State, Priority, ItemKey);

            QueueUserWorkItem(oPThreadWorkItem);		
		}

        /// <summary>
        /// Private method that queues the work item, and ensures that a thread is executing to process the queue.
        /// 
        /// This method starts the dispatching of the work, however, subsequent invocations will be spawned by the 
        /// threadpool itself.
        /// </summary>
        /// <param name="oPThreadWorkItem"></param>
        private void QueueUserWorkItem(WorkItem oPThreadWorkItem)
        {
            lock (m_Lock_WorkItem)
            {
                m_WorkQueue.AddPrioritised(oPThreadWorkItem);
            }

            if (m_ItemsProcessing < m_NoParallelThreads) //Spawn another thread.
                SpawnWorkThread(null);	
        }

		/// <summary>
        /// This is the method is used from both <see cref="QueueUserWorkItem"/> and the <see cref="AsyncCallback"/>.
        /// 
        /// If from the QueueUserWorkItem(), then the parameter is null, and it is not nessecary to call EndInvoke.
        /// 
        /// If it is from a AsyncCallback then it is nessecary to call end invoke, tidy up the manual resetevents
        /// 
        /// Both code paths then spawn another workitem asycronusly on the threadpool
		/// </summary>
        /// <param name="AsyncResult">IAsyncResult from the previous Async invokation on the threadpool, or null if
        /// from  <see cref="QueueUserWorkItem"/></param>
		private void SpawnWorkThread(IAsyncResult AsyncResult)
		{
			lock(m_Lock_WorkItem)
			{
				if(AsyncResult!=null)
				{
                    oWaitCallback.EndInvoke(AsyncResult);

                    m_ItemsProcessing--;

                    #region Removes the ManualReset from the collection, indicating it is processed.
                    WorkItem oPThreadWorkItem = AsyncResult.AsyncState as WorkItem;

                    if (oPThreadWorkItem != null)
                    {
                        m_WorkQueue.ManualResets.Remove(oPThreadWorkItem.MRE);
                    } 
                    #endregion
                   
                    if (ThreadedQueueMonitor.Instance.ThreadPoolThreadCount != null)
                        ThreadedQueueMonitor.Instance.ThreadPoolThreadCount.Decrement();
				}

				// oWorker item will be null if the queue was empty
				if(m_WorkQueue.Count>0) 
				{
					m_ItemsProcessing++;

					//Gets the next piece of work to perform
					WorkItem oWorkItem = m_WorkQueue.Dequeue();

                    if (oWorkItem != null)
                    {
                        //Hooks up the callback to this method.		
                        AsyncCallback oAsyncCallback = new AsyncCallback(this.SpawnWorkThread);

                        //Invokes the work on the threadpool					
                        oWaitCallback.BeginInvoke(oWorkItem, oAsyncCallback, oWorkItem);

                        if (ThreadedQueueMonitor.Instance.ThreadPoolThreadCount != null)
                            ThreadedQueueMonitor.Instance.ThreadPoolThreadCount.Increment();
                    }
				}

                //Debug.WriteLine(String.Format("SpawnWorkThread Length:{0} InUse:{1} DateTime:{2}", m_WorkQueue.Count, m_ItemsProcessing, DateTime.Now.ToString()));
			}
		}

        /// <summary>
        /// Can only be called by <see cref="SpawnWorkThread"/> it is where the pointer to the method 
        /// is invoked, and the parameter is passed to the method.
        /// 
        /// Importantly, this is where reset event is set, to indicate that work has been completed.
        /// </summary>
        /// <param name="Item"></param>
        private void InvokeWaitHandleDelegate(object Item)            
        {
            try
            {
                WorkItem oWorkItem = Item as WorkItem;
                
                try
                {
                    oWorkItem.Method.Invoke(oWorkItem.State);
                }
                catch (Exception Ex)
                {
                    Debug.WriteLine("InvokeWaitHandleDelegate.Ex.2:" + Ex.ToString());
                }
                finally
                {                    
                    oWorkItem.MRE.Set(); //ManualResetEvent is set, in order to flag it is completed.
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine("InvokeWaitHandleDelegate.Ex.1:" + Ex.ToString());
            }
        }
	

        /// <summary>
        /// This causes the work queue to block, until all the currently queued workitems have been processed. 
        /// It takes a snapshot of the pending items, and waits for them to process.
        /// 
        /// This code will wait for all pending items to be processed, then it will unblock
        ///         
        /// </summary>
        /// <returns>The number of remaining worker items</returns>
        public int WaitAll()
        {
            lock (this)
            {
                //This collection is changed by many threads, it must therefore be copied before it is enumerated.
                List<ManualResetEvent> oCopy = new List<ManualResetEvent>(m_WorkQueue.ManualResets);

                foreach (ManualResetEvent oMR in oCopy)                
                    oMR.WaitOne();                                    

                return m_WorkQueue.ManualResets.Count;
            }            
        }

        public WorkItemQueue WorkQueue
        {
            get { return m_WorkQueue; }            
        }
	}
}
