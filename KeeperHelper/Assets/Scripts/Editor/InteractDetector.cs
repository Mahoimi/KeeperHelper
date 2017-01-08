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
        private static Dictionary<string, bool> s_toggles = new Dictionary<string, bool>();
        private static StringBuilder s_stringBuilder = new StringBuilder();
        
        private static GUIStyle s_ToogleStyle = new GUIStyle();
        private static GUIStyle s_LabelStyle = new GUIStyle();
        private static GUIStyle s_SelectedLabelStyle = new GUIStyle();
        private static Texture2D s_SelectedItemBackground;

        #region MenuItem
        [MenuItem(c_MenuPath)]
        public static void CreateWindow()
        {
            InteractDetector window = GetWindow<InteractDetector>("Interact Detector");
            window.Show();
        }
        #endregion

        public void OnEnable()
        {
            // TODO : Adapt GUIStyle colors according to free / pro unity skin EditorGUIUtility.isProSkin

            // Toggle LabelStyles
            s_LabelStyle.normal.textColor = EditorGUIUtility.isProSkin ? new Color(0.6f, 0.6f, 0.6f) : Color.black;
            s_SelectedLabelStyle.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.white;

            // ToggleStyle
            Color l_LightGrey = EditorGUIUtility.isProSkin? new Color(0.6f, 0.6f, 0.6f) : new Color(0.6f, 0.6f, 0.6f);
            s_SelectedItemBackground = new Texture2D(1, 1);
            s_SelectedItemBackground.hideFlags = HideFlags.DontSave;
            s_SelectedItemBackground.SetPixel(0, 0, l_LightGrey);
            s_SelectedItemBackground.Apply();

            s_ToogleStyle.onNormal.background =
                s_ToogleStyle.onActive.background = s_SelectedItemBackground;

            s_ToogleStyle.padding = new RectOffset(0, 0, 2, 2);
            s_ToogleStyle.margin = new RectOffset(0, 0, 0, 0);
        }
        
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

        void OnDisable()
        {
            DestroyImmediate(s_SelectedItemBackground);
        }

        private void DrawPlayMode()
        {
            // Clear previous results
            s_interactableButtonHierarchies.Clear();
            s_toggles.Clear();

           
            GetAllInteractableButtons();

            // Display Title
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Detected selectables :", EditorStyles.boldLabel);

            // Display all Interactables for each Scene    
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
                        // Get current Interactable "selection toggle" value
                        bool toggleValue = false;
                        if (!s_toggles.TryGetValue(list[j], out toggleValue))
                            Debug.Log("[InteractDetector] Can't find toggle value for " + list[j]);

                        // Display the Interactable name (within a toggle)
                        bool currentToggleValue = GUILayout.Toggle(toggleValue, "", s_ToogleStyle, GUILayout.MinWidth(position.width));
                        EditorGUI.LabelField(GUILayoutUtility.GetLastRect(), list[j], currentToggleValue ? s_SelectedLabelStyle : s_LabelStyle);

                        // Select / unselect the Interactable in Hierarchy if its toggle value has changed
                        if (currentToggleValue != toggleValue)
                        {
                            Transform interactable = GameObject.Find(list[j]).transform;

                            // Select
                            if (currentToggleValue)
                                Selection.activeTransform = interactable;

                            // Unselect
                            else
                                UnselectInHierarchy(interactable);
                        }

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

                    string selectablePath = GetGameObjectPath(selectables[i].gameObject.transform);

                    s_interactableButtonHierarchies[sceneName].Add(selectablePath);

                    s_toggles.Add(selectablePath, IsSelectedInHierarchy(selectables[i].gameObject.transform));
                }
            }
        }

        private bool IsSelectedInHierarchy(Transform transform)
        {
            if (Selection.activeTransform == transform)
                return true;

            foreach (Transform t in Selection.transforms)
            {
                if (t == transform)
                    return true;
            }

            return false;
        }

        private void UnselectInHierarchy(Transform transform)
        {
            Transform[] selectedTransforms = Selection.transforms;
            List<Transform> tempSelectedTransforms = new List<Transform>();
            foreach (Transform t in selectedTransforms)
            {
                if (t != transform)
                {
                    Debug.Log("ADD " + t);
                    tempSelectedTransforms.Add(t);
                }
            }
            Selection.objects = tempSelectedTransforms.ToArray();
            EditorApplication.RepaintHierarchyWindow();

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