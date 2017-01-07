using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace KeeperHelper
{
    public class QuestSelectionContent : MonoBehaviour
    {
        [SerializeField]
        private Text m_textValue = null;

        private Quest m_quest = null;

        public void Initialize(Quest quest)
        {
            m_quest = quest;
            m_textValue.text = m_quest.NameLocId;
        }

        public void OnQuestSelected()
        {
            MainProcess.Instance.UIManager.OnQuestSelected(m_quest);
        }
    }
}