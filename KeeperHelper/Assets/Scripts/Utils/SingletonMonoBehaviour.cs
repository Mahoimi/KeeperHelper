using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Variables
        private static T m_instance;

        public static T Instance { get { return m_instance; } }
        #endregion

        #region Monobehaviour
        protected SingletonMonoBehaviour() { }

        public virtual void Awake()
        {
            if (m_instance == null)
                m_instance = GetComponent<T>();
        }
        #endregion
    }
}