using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace KeeperHelper
{
    public class OptionsUI : MonoBehaviour
    {
        private Dropdown m_languageDropdown = null;

        #region Init
        public void ManualAwake()
        {
            m_languageDropdown = GetComponentInChildren<Dropdown>();
            Assert.IsNotNull(m_languageDropdown);

            SetLanguagesDropdown();
            m_languageDropdown.onValueChanged.AddListener(OnSelectLanguage);
            ShowMenu();
        }
        #endregion

        #region Set UI
        public void ShowMenu()
        {
            gameObject.SetActive(true);
        }

        public void HideMenu()
        {
            gameObject.SetActive(false);
        }

        public void SetLanguagesDropdown()
        {
            m_languageDropdown.ClearOptions();

            foreach(SystemLanguage language in MainProcess.Localization.m_supportedLanguages){
                string l_languageName = MainProcess.Localization.GetLanguageName(language.ToString());
                m_languageDropdown.options.Add(new Dropdown.OptionData() { text = l_languageName });
            }
            m_languageDropdown.value = MainProcess.Localization.GetCurrentLanguageIndex();
            m_languageDropdown.RefreshShownValue();            
        }

        #endregion

        #region UI Events
        public void OnSelectLanguage(int index)
        {
            MainProcess.Localization.SetLanguage(index);
            SetLanguagesDropdown();
        }
        #endregion
    }
}
