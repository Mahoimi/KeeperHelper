using UnityEngine;

namespace KeeperHelper
{
    public class MainMenuUI : MonoBehaviour
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
    }
}