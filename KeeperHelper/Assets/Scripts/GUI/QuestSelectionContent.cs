using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using KeeperHelper.Utils;

namespace KeeperHelper
{
    public class QuestSelectionContent : MonoBehaviour
    {
        [SerializeField]
        private LocalizedText m_LocalizedText = null;

        private Quest m_quest = null;

        public void Initialize(Quest quest)
        {
            m_quest = quest;
            m_LocalizedText.UpdateKey(m_quest.NameLocId);
        }

        public void OnQuestSelected()
        {
            MainProcess.Instance.UIManager.OnQuestSelected(m_quest);
        }
    }
}