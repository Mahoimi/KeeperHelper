using UnityEngine;
using UnityEngine.Assertions;

namespace KeeperHelper
{
    public class QuestSelectionUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform m_contentTransform = null;

        [SerializeField]
        private GameObject m_questSelectionContent = null;

        private QuestSelectionContent[] m_questSelectionContents = null;

        #region Init
        public void ManualAwake()
        {
            CreateUI();
        }

        private void CreateUI()
        {
            Assert.IsNotNull(m_contentTransform);
            Assert.IsNotNull(m_questSelectionContent);

            // Load all quests
            Quest[] quests = Quest.GetAllQuests();

            m_questSelectionContents = new QuestSelectionContent[quests.Length];

            for (int i = 0; i < quests.Length; i++)
            {
                // Instantiate content
                GameObject go = Instantiate(m_questSelectionContent);
                go.transform.SetParent(m_contentTransform, false);

                // Assign quest data to content
                QuestSelectionContent content = go.GetComponent<QuestSelectionContent>();
                Assert.IsNotNull(content);
                content.Initialize(quests[i]);
                m_questSelectionContents[i] = content;
            }
        }
        #endregion

        public void ShowMenu()
        {
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            gameObject.SetActive(false);
        }
    }
}