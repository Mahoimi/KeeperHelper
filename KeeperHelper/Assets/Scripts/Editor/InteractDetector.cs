using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using System.Text;

namespace KeeperHelper.Utils
{
	public class InteractDetector : EditorWindow
    {
        private const string c_MenuPath = "Tools/Interact Detector";

        private static Dictionary<string, List<string>> s_interactableButtonHierarchies = new Dictionary<string, List<string>>();
        private static List<bool> s_foldouts = new List<bool>();
        private static StringBuilder s_stringBuilder = new StringBuilder();

        #region MenuItem
        [MenuItem(c_MenuPath)]
        public static void CreateWindow()
        {
            InteractDetector window = GetWindow<InteractDetector>("Interact Detector");
            window.Show();
        }
        #endregion

        public void OnGUI()
        {
            if (EditorApplication.isPlaying)
            {
                DrawPlayMode();
            }
            else
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Only available in Play mode :)", EditorStyles.centeredGreyMiniLabel);
            }
        }

        public void Update()
        {
            Repaint();
        }

        private void DrawPlayMode()
        {
            // Clear previous results
            s_interactableButtonHierarchies.Clear();

            GetAllInteractableButtons();

            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Detected selectables :", EditorStyles.boldLabel);

            int i = 0;
            var enumerator = s_interactableButtonHierarchies.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                var list = current.Value;

                s_foldouts[i] = EditorGUILayout.Foldout(s_foldouts[i], current.Key);
                if (s_foldouts[i])
                {
                    EditorGUI.indentLevel++;
                    for (int j = 0; j < list.Count; j++)
                    {
                        EditorGUILayout.LabelField(list[j]);
                    }
                    EditorGUI.indentLevel--;
                }
                i++;
            }

            EditorGUILayout.EndVertical();
        }

        private void GetAllInteractableButtons()
        {
            var selectables = Selectable.allSelectables;
            for (int i = 0; i < selectables.Count; i++)
            {
                Selectable selectable = selectables[i];

                if (CanBeInteracted(selectable))
                {
                    string sceneName = selectable.gameObject.scene.name;
                    if (!s_interactableButtonHierarchies.ContainsKey(sceneName))
                    {
                        s_interactableButtonHierarchies.Add(sceneName, new List<string>());
                        if (s_foldouts.Count < s_interactableButtonHierarchies.Keys.Count)
                            s_foldouts.Add(true);
                    }

                    s_interactableButtonHierarchies[sceneName].Add(GetGameObjectPath(selectables[i].gameObject.transform));
                }
            }
        }

        private bool CanBeInteracted(Selectable selectable)
        {
            Transform transform = selectable.transform.parent;
            CanvasGroup canvasGroup = null;

            while (transform != null)
            {
                canvasGroup = transform.GetComponent<CanvasGroup>();
                if(canvasGroup && !canvasGroup.blocksRaycasts)
                {
                    return false;
                }

                transform = transform.parent;
            }

            return true;
        }

        private string GetGameObjectPath(Transform transform)
        {
            // Clear
            s_stringBuilder.Remove(0, s_stringBuilder.Length);
            char delimiter = '/';

            // Get path
            s_stringBuilder.Append(transform.name);
            while(transform.parent != null)
            {
                transform = transform.parent;
                s_stringBuilder.Insert(0, delimiter);
                s_stringBuilder.Insert(0, transform.name);
            }

            return s_stringBuilder.ToString();
        }
    }
}