using UnityEngine;
using UnityEngine.Assertions;
using KeeperHelper.Utils;

namespace KeeperHelper
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        #region UI References
        [Header("UI References")]
        [SerializeField]
        private BackButtonUI m_backButtonUI = null;

        [SerializeField]
        private MainMenuUI m_mainMenuUI = null;

        [SerializeField]
        private QuestSelectionUI m_questSelectionUI = null;
        #endregion

        #region HistoryCallbacks
        private HistoryCallback m_newGameButtonHistoryCallback = null;
        #endregion
        #endregion

        #region Functions
        #region Init
        public void ManualAwake()
        {
            // Check missing refs
            Assert.IsNotNull(m_backButtonUI);
            Assert.IsNotNull(m_mainMenuUI);
            Assert.IsNotNull(m_questSelectionUI);

            // Assign HistoryCallbacks
            m_newGameButtonHistoryCallback = NewGameButtonHistoryCallback;

            // Call ManualAwakes
            m_backButtonUI.ManualAwake();
            m_mainMenuUI.ManualAwake();
            m_questSelectionUI.ManualAwake();
        }
        #endregion

        #region Flow
        public void OnNewGameButtonClick()
        {
            m_mainMenuUI.HideMenu();
            m_questSelectionUI.ShowMenu();

            MainProcess.History.HistoryPush(m_newGameButtonHistoryCallback);
        }
        #endregion

        #region HistoryCallbacks
        private void NewGameButtonHistoryCallback()
        {
            m_questSelectionUI.HideMenu();
            m_mainMenuUI.ShowMenu();
        }
        #endregion
        #endregion
    }
}