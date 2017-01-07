using System.Collections;
using System.Collections.Generic;
using System.IO;
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

            for (int i = 0; i < quests.Length; i++)
            {
                // Instantiate content
                GameObject go = Instantiate(m_questSelectionContent);
                go.transform.SetParent(m_contentTransform, false);

                // Assign quest data to content
                QuestSelectionContent content = go.GetComponent<QuestSelectionContent>();
                Assert.IsNotNull(content);
                content.Initialize(quests[i]);
            }
        }
    }
}