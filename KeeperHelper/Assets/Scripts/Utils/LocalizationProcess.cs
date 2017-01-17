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
        public Dictionary<string, string> m_localizedString = new Dictionary<string, string>();
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
        // TODO : Set the avalaible language list in a txt file ? Another cvs ?
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

        #region Language selection
        public void GetSystemLanguage()
        { 
            SystemLanguage l_language = Application.systemLanguage;
            if (!IsSupported(l_language))
                l_language = c_defaultLanguage;

            m_currentLanguage = l_language;

            Debug.Log("[Localization] System : " + Application.systemLanguage + " | App : " + m_currentLanguage);
        }

        public void SwitchLanguage()
        {
            int index = m_supportedLanguages.FindIndex(x => x == m_currentLanguage);
            int nextIndex = (index == m_supportedLanguages.Count - 1) ? 0 : (index + 1);
            m_currentLanguage = m_supportedLanguages[nextIndex];
            LoadCurrentLanguage();
            RefreshLocalizedTexts();
        }

        public void SetLanguage(int index)
        {
            m_currentLanguage = m_supportedLanguages[index];
            LoadCurrentLanguage();
            RefreshLocalizedTexts();
        }

        public int GetCurrentLanguageIndex()
        {
            return m_supportedLanguages.FindIndex(x => x == m_currentLanguage);
        }

        #endregion

        #region Language loading
        public void LoadCurrentLanguage()
        {
            m_localizedString.Clear();

            // Read current language csv
            string l_LanguagePath = Path.Combine(c_languagesFolderPath, "" + m_currentLanguage);
            string[][] data = CSVReader.Read(l_LanguagePath, ';');
            Debug.Log("[Localization] Load " + l_LanguagePath);

            // Add csv lines (key, value) to the dictionnary
            for (int i = 0; i < data.Length; i++)
            {
                // Get line columns (key, value)     
                string[] columns = data[i];
                if (columns.Length < 1)
                    break;

                // Replace character literals in value string
                string l_value = CSVReader.ReplaceCharacterLiterals(columns[1]);

                // Add (key, value) 
                m_localizedString.Add(columns[0], l_value);
                Debug.Log("[Localization] Add |" + columns[0] + "|" + l_value + "|");
            }
        }

        public string GetLocalizedString(string key)
        {
            string value = "";
            if (m_localizedString.TryGetValue(key, out value))
                return value;
            return key;
        }

        public void RefreshLocalizedTexts()
        {
            LocalizedText[] l_Texts = UnityEngine.Object.FindObjectsOfType(typeof(LocalizedText)) as LocalizedText[];
            foreach (LocalizedText text in l_Texts)
                text.Refresh();
        }

        #endregion
    }
}
