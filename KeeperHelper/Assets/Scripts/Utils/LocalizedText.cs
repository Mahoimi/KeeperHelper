using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KeeperHelper.Utils
{
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string m_key = "";

        private Text m_uiText;
        private List<string> m_Arguments = new List<string>();

        void OnEnable()
        {
            m_uiText = GetComponent<Text>();
            Refresh();
        }

        public void UpdateKey(string p_key)
        {
            m_key = p_key;
            Refresh();
        }

        public void Refresh()
        {
            if (!string.IsNullOrEmpty(m_key))
                m_uiText.text = MainProcess.Localization.GetLocalizedString(m_key);
        }
    }
}
