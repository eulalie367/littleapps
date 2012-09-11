using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;


namespace Library_ThreadedWorkerQueue.WorkItems
{
	/// <summary>
	/// Summary description for prioritisedArrayList.
	/// </summary>
	public class WorkItemQueue
	{
        private object m_Lock_WorkItem_AddRemove;

        private List<WorkItem> m_WorkItems;
        private List<ManualResetEvent> m_MREs;  
        
        public WorkItemQueue()            
        {
            m_Lock_WorkItem_AddRemove = new object();

            m_WorkItems = new List<WorkItem>();
            m_MREs = new List<ManualResetEvent>();
        }

        /// <summary>
        /// Removes the next highest priority work item from the queue.
        /// </summary>
        /// <returns></returns>
        public WorkItem Dequeue()
		{
            lock (m_Lock_WorkItem_AddRemove)
            {
                if (m_WorkItems.Count > 0)
                {
                    WorkItem o = m_WorkItems[m_WorkItems.Count - 1];
                    m_WorkItems.RemoveAt(m_WorkItems.Count - 1);
                    return o;
                }

                return null;
            }
		}

        /// <summary>
        /// Adds items to the work queue.
        /// 
        /// If the unique for the item already exists, then the work item is discarded.
        /// 
        /// If the priority is greater than previous items in the queue, then the new work item is
        /// inserted prior to the other items.
        /// 
        /// This code maintains the sorted order of the workitems.
        /// </summary>
        /// <param name="oNewItem"></param>
        public void AddPrioritised(WorkItem oNewItem)
		{
            lock(m_Lock_WorkItem_AddRemove)
            {
                foreach (WorkItem oItem in m_WorkItems)
                    if (oItem.Key.ToString() == oNewItem.Key.ToString())
                    {
                        Debug.WriteLine("PrioritisedArrayList: Key Already Queued: Skipped Request");
                        return;
                    }

                m_MREs.Add(oNewItem.MRE);

                int i = m_WorkItems.BinarySearch(oNewItem);

                if (i < 0)
                    m_WorkItems.Insert(~i, oNewItem);
                else
                    m_WorkItems.Insert(i, oNewItem);
            }
		}

        public List<ManualResetEvent> ManualResets
        {
            get { return m_MREs; }
        }

        public int Count
        {
            get { return m_WorkItems.Count; }
        }
	}
}
