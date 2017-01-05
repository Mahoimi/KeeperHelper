using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;
using KeeperHelper.Utils;

namespace KeeperHelper
{
    public class MainProcess : SingletonMonoBehaviour<MainProcess>
    {
        public UIManager UIManager = null;

        #region Monobehaviour
        public override void Awake()
        {
            base.Awake();

            SetupProject();

            // Call ManualAwake functions
            Assert.IsNotNull(UIManager);
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