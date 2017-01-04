using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KeeperHelper
{
    public class MainMenuUI : MonoBehaviour
    {
        // Use this for initialization
        public void ManualAwake()
        {
            ShowMenu();
        }

        private void ShowMenu()
        {
            gameObject.SetActive(true);
        }

        private void HideMenu()
        {
            gameObject.SetActive(false);
        }
    }
}