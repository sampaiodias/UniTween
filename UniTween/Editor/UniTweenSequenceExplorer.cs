#if UNITY_EDITOR && ODIN_INSPECTOR
namespace UniTween.Editor
{
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using System.Collections.Generic;
    using System.Linq;
    using UniTween.Core;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class UniTweenSequenceExplorer : OdinMenuEditorWindow
    {
        public static List<UniTweenSequence> sequences = new List<UniTweenSequence>();

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 28.00f;
            tree.Config.DrawSearchToolbar = true;

            var evs = GetSequences();

            Dictionary<string, int> sqAmount = new Dictionary<string, int>();

            for (int i = 0; i < evs.Count; i++)
            {
                string sqName = evs[i].id != "" ? evs[i].name + ": " + evs[i].id : evs[i].name;

                if (sqAmount.ContainsKey(sqName))
                {
                    sqAmount[sqName] += 1;
                }
                else
                {
                    sqAmount.Add(sqName, 0);
                }

                if (sqAmount[sqName] != 0)
                    tree.Add(sqName + " (" + sqAmount[sqName] + ")", evs[i]);
                else
                    tree.Add(sqName, evs[i]);
            }

            tree.SortMenuItemsByName();

            return tree;
        }

        public List<UniTweenSequence> GetSequences()
        {
            var evs = CustomFindObjectsOfTypeAll<UniTweenSequence>();

            sequences.Clear();
            for (int i = 0; i < evs.Count; i++)
            {
                sequences.Add(evs[i]);
            }

            return sequences;
        }

        protected override void OnBeginDrawEditors()
        {
            OdinMenuItem selected = null;
            if (MenuTree != null)
            {
                if (MenuTree.MenuItems.Count > 0)
                {
                    selected = this.MenuTree.Selection.FirstOrDefault();
                }
                var toolbarHeight = this.MenuTree.Config.SearchToolbarHeight;

                SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
                {
                    if (selected != null)
                    {
                        if (SirenixEditorGUI.ToolbarButton(new GUIContent("Select GameObject")))
                        {
                            Selection.activeGameObject = (((UniTweenSequence)selected.Value).gameObject);
                            EditorGUIUtility.PingObject(Selection.activeGameObject);
                        }
                    }

                    if (SirenixEditorGUI.ToolbarButton(new GUIContent("Reload Tree")))
                    {
                        ForceMenuTreeRebuild();
                    }
                }
                SirenixEditorGUI.EndHorizontalToolbar();
            }
            else
            {
                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Reload Tree")))
                {
                    ForceMenuTreeRebuild();
                }
            }
        }

        [MenuItem("Tools/UniTween/Sequence Explorer")]
        private static void OpenWindow()
        {
            var window = GetWindow<UniTweenSequenceExplorer>();
            window.titleContent.text = "Sequences";
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override void DrawEditor(int index)
        {
            UniTweenSequence obj = (UniTweenSequence)CurrentDrawingTargets[index];
            if (obj != null)
            {
                SirenixEditorGUI.BeginBox(obj.gameObject.name + ": " + obj.id);
                {
                    base.DrawEditor(index);
                }
                SirenixEditorGUI.EndBox();
            }
        }

        public static List<T> CustomFindObjectsOfTypeAll<T>()
        {
            List<T> results = new List<T>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var s = SceneManager.GetSceneAt(i);
                if (s.isLoaded)
                {
                    var allGameObjects = s.GetRootGameObjects();
                    for (int j = 0; j < allGameObjects.Length; j++)
                    {
                        var go = allGameObjects[j];
                        results.AddRange(go.GetComponentsInChildren<T>(true));
                    }
                }
            }
            return results;
        }
    }
}
#endif