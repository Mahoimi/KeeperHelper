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
        private OptionsUI m_optionUI = null;

        [SerializeField]
        private MainMenuUI m_mainMenuUI = null;

        [SerializeField]
        private QuestSelectionUI m_questSelectionUI = null;

        [SerializeField]
        private VariantSelectionUI m_variantSelectionUI = null;
        #endregion

        #region HistoryCallbacks
        private HistoryCallback m_newGameButtonHistoryCallback = null;
        private HistoryCallback m_questSelectedHistoryCallback = null;
        private HistoryCallback m_historyAnswerHistoryCallback = null;
        #endregion
        #endregion

        #region Functions
        #region Init
        public void ManualAwake()
        {
            // Check missing refs
            Assert.IsNotNull(m_backButtonUI);
            Assert.IsNotNull(m_optionUI);
            Assert.IsNotNull(m_mainMenuUI);
            Assert.IsNotNull(m_questSelectionUI);
            Assert.IsNotNull(m_variantSelectionUI);

            // Assign HistoryCallbacks
            m_newGameButtonHistoryCallback = NewGameButtonHistoryCallback;
            m_questSelectedHistoryCallback = QuestSelectedHistoryCallback;
            m_historyAnswerHistoryCallback = HistoryAnswerHistoryCallback;

            // Call ManualAwakes
            m_backButtonUI.ManualAwake();
            m_optionUI.ManualAwake();
            m_mainMenuUI.ManualAwake();
            m_questSelectionUI.ManualAwake();
            m_variantSelectionUI.ManualAwake();
        }
        #endregion

        #region Flow
        public void OnNewGameButtonClick()
        {
            m_mainMenuUI.HideMenu();
            m_questSelectionUI.ShowMenu();

            MainProcess.History.HistoryPush(m_newGameButtonHistoryCallback);
        }

        public void OnQuestSelected(Quest quest)
        {
            m_questSelectionUI.HideMenu();
            m_variantSelectionUI.ShowMenu();
            m_variantSelectionUI.SetQuest(quest);
            m_variantSelectionUI.SetQuestHistoryQuestion(0);

            MainProcess.History.HistoryPush(m_questSelectedHistoryCallback);
        }

        public void OnHistoryAnswerClick()
        {
            MainProcess.History.HistoryPush(m_historyAnswerHistoryCallback);
        }

        public void OnFinalQuestionAnswered()
        {

        }
        #endregion

        #region HistoryCallbacks
        private void NewGameButtonHistoryCallback()
        {
            m_questSelectionUI.HideMenu();
            m_mainMenuUI.ShowMenu();
        }

        private void QuestSelectedHistoryCallback()
        {
            m_variantSelectionUI.HideMenu();
            m_questSelectionUI.ShowMenu();
        }

        private void HistoryAnswerHistoryCallback()
        {
            m_variantSelectionUI.SetQuestHistoryQuestion(m_variantSelectionUI.CurrentQuestionNumber-1);
        }
        #endregion
        #endregion
    }
}