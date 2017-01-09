using UnityEngine.Assertions;
using KeeperHelper.Utils;

namespace KeeperHelper
{
    public class MainProcess : SingletonMonoBehaviour<MainProcess>
    {
        #region Static
        public static HistoryJournal History = new HistoryJournal();
        public static LocalizationProcess Localization = new LocalizationProcess();
        #endregion

        public UIManager UIManager = null;

        #region Monobehaviour
        public override void Awake()
        {
            base.Awake();

            SetupProject();
            
            // Check missing refs
            Assert.IsNotNull(UIManager);

            // Call ManualAwake functions
            Localization.Initialization();
            UIManager.ManualAwake();
        }
        #endregion
        
        private void SetupProject()
        {
            Assert.raiseExceptions = true;
            LogHandler.RegisterLogHandler();
        }
    }
}