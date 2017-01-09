using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    public class OptionsUI : MonoBehaviour
    {
        #region Init
        public void ManualAwake()
        {
            ShowMenu();
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

        public void OnSwitchLanguageClick()
        {
            MainProcess.Localization.SwitchLanguage();
        }
    }
}
