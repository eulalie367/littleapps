using System;
using System.Threading;

namespace Library_ThreadedWorkerQueue.WorkItems
{
	/// <summary>
	/// This class is a represents a piece of work to be performed.
    /// 
    /// It is a workeritem that will be sorted & executed
	/// </summary>
	public class WorkItem:IComparable<WorkItem>
	{
		private WaitCallback m_Method;
		private Object m_ObjectState;
		private ThreadPriority m_Priority;
		private ManualResetEvent m_MRE;
        private object m_ItemKey;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method">The method that will be invoked on the threadpool</param>
        /// <param name="state">The object that will be passed as a parameter to your method</param>
        /// <param name="priority">Thread Priority, used to order the pending workitems</param>
        /// <param name="ItemKey">Key to represent the uniqueness of this workitem, used to reduce the amount of redunent work on the workerqueue</param>
		public WorkItem(WaitCallback method, Object state, ThreadPriority priority,object ItemKey)
		{
			m_Method = method;
			m_ObjectState = state;
			m_Priority = priority;            
            m_ItemKey = ItemKey;

            m_MRE = new ManualResetEvent(false);
		}

		public ThreadPriority Priority{ get{return m_Priority;}}
		public WaitCallback Method{     get{return m_Method;}}
		public Object State{            get{return m_ObjectState;}}
        public Object Key {             get{return m_ItemKey; } }
		public ManualResetEvent MRE{    get{return m_MRE;} }

        #region IComparable<PThreadWorkItem> Members

        public int CompareTo(WorkItem other)
        {            
            return this.Priority.CompareTo(other.Priority);
        }

        #endregion
    }
}
