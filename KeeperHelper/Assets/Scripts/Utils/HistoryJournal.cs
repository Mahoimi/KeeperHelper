using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper.Utils
{
    public delegate void HistoryCallback();

	public class HistoryJournal 
	{
        private struct HistoryEntry
        {
            public string Journal;
            public HistoryCallback Callback;
        }

        private List<HistoryEntry> m_historyCallbacks = new List<HistoryEntry>();
        private List<IHistoryListener> m_historyListeners = new List<IHistoryListener>();
        
        public bool HasHistory { get { return m_historyCallbacks.Count > 0; } }

        public void HistoryPush(HistoryCallback callback, string journal = null)
        {
            HistoryEntry entry;
            entry.Journal = journal;
            entry.Callback = callback;
            m_historyCallbacks.Add(entry);

            // Inform listeners
            for (int i = 0; i < m_historyListeners.Count; i++)
            {
                m_historyListeners[i].OnHistoryPush();
            }
        }

        public void HistoryPop()
        {
            HistoryEntry entry = m_historyCallbacks[m_historyCallbacks.Count - 1];
            m_historyCallbacks.RemoveAt(m_historyCallbacks.Count - 1);
            entry.Callback();

            // Inform listeners
            for (int i = 0; i < m_historyListeners.Count; i++)
            {
                m_historyListeners[i].OnHistoryPop();
            }
        }

        public void RegisterListener(IHistoryListener listener)
        {
            if (!m_historyListeners.Contains(listener))
            {
                m_historyListeners.Add(listener);
            }
            else
            {
                Debug.LogWarning("Trying to register a listener twice.");
            }
        }

        public void UnregisterListener(IHistoryListener listener)
        {
            if (m_historyListeners.Contains(listener))
            {
                m_historyListeners.Remove(listener);
            }
            else
            {
                Debug.LogWarning("Trying to unregister an unregistered listener.");
            }
        }
	}

    public interface IHistoryListener
    {
        void OnHistoryPush();
        void OnHistoryPop();
    }
}