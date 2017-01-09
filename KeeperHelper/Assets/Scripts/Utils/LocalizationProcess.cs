using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace KeeperHelper.Utils
{
    public class LocalizationProcess
    {
        #region Variables
        #region Constant
        private const string c_languagesFolderPath = "TextFiles/Languages";
        private const SystemLanguage c_defaultLanguage = SystemLanguage.English;
        #endregion

        public List<SystemLanguage> m_supportedLanguages = new List<SystemLanguage>();
        public SystemLanguage m_currentLanguage = c_defaultLanguage;
        #endregion

        #region Initialization
        public void Initialization()
        {
            GetSupportedLanguages();
            PrintSupportedLanguages();
            GetSystemLanguage();
            LoadCurrentLanguage();
        }
        #endregion

        #region Supported Languages
        // TODO : Set the avalaible language list int a txt file ? Another cvs ?
        public void GetSupportedLanguages()
        {
            TextAsset[]  l_languagesAssets = Resources.LoadAll<TextAsset>(c_languagesFolderPath);
            foreach(TextAsset language in l_languagesAssets)
            {
                //Debug.Log(" [Localization] Text Asset " + language.name);
                SystemLanguage l_Systemlanguage;
                if(ParseToSystemLanguage(language.name, out l_Systemlanguage))
                {
                    //Debug.Log("[Localization] System Language " + language.name);
                    m_supportedLanguages.Add(l_Systemlanguage);       
                }

                Resources.UnloadAsset(language);
            }
        }

        public bool ParseToSystemLanguage(string p_language, out SystemLanguage p_SystemLanguage)
        {
            try
            {
                p_SystemLanguage = (SystemLanguage)Enum.Parse(typeof(SystemLanguage), p_language);
                return true;
            }
            catch (ArgumentException exception)
            {
                Debug.Log("[Localization] " + exception.Message);
                p_SystemLanguage = SystemLanguage.English;
                return false;
            }
        }

        public void PrintSupportedLanguages()
        {
            string l_msg = "[Localization] Supported languages : ";
            foreach (SystemLanguage language in m_supportedLanguages)
                l_msg += language + " | ";

            Debug.Log(l_msg);
        }

        public bool IsSupported(SystemLanguage p_language)
        {
            return m_supportedLanguages.Exists(x => x == p_language);
        }

        #endregion

        #region Current Language
        public void GetSystemLanguage()
        { 
            SystemLanguage l_language = Application.systemLanguage;
            if (!IsSupported(l_language))
                l_language = c_defaultLanguage;

            m_currentLanguage = l_language;

            Debug.Log("[Localization] System : " + Application.systemLanguage + " | App : " + m_currentLanguage);
        }

        public void LoadCurrentLanguage()
        {
            string l_LanguagePath = Path.Combine(c_languagesFolderPath, "" + m_currentLanguage);
            string[][] data = CSVReader.Read(l_LanguagePath, ';');
            Debug.Log("[Localization] Load " + l_LanguagePath);
        }

        #endregion
    }
}
