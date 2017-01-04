using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace KeeperHelper
{
    public class UIManager : MonoBehaviour
    {
        #region UI References
        [Header("UI References")]
        [SerializeField]
        private MainMenuUI m_mainMenuUI = null;

        [SerializeField]
        private QuestSelectionUI m_questSelectionUI = null;
        #endregion

        #region Init
        public void ManualAwake()
        {
            Assert.IsNotNull(m_mainMenuUI);
            Assert.IsNotNull(m_questSelectionUI);

            m_mainMenuUI.ManualAwake();
            m_questSelectionUI.ManualAwake();
        }
        #endregion
    }
}