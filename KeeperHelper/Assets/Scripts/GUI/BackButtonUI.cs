using UnityEngine;
using KeeperHelper.Utils;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace KeeperHelper
{
	public class BackButtonUI : MonoBehaviour, IHistoryListener
	{
        private Button m_button = null;

        public void ManualAwake()
        {
            m_button = GetComponent<Button>();
            Assert.IsNotNull(m_button);

            m_button.onClick.AddListener(MainProcess.History.HistoryPop);

            MainProcess.History.RegisterListener(this);
        }

        public void OnHistoryPop()
        {
            if (!MainProcess.History.HasHistory)
            {
                gameObject.SetActive(false);
            }
        }

        public void OnHistoryPush()
        {
            gameObject.SetActive(true);
        }

        public void AddOnClickListener(UnityAction callback)
        {
            m_button.onClick.AddListener(callback);
        }
    }
}