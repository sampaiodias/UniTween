#if UNITY_EDITOR && ODIN_INSPECTOR
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

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
        //tree.MenuItems.AddIcons(EditorIcons.Play);

        return tree;
    }

    public List<UniTweenSequence> GetSequences()
    {
        var evs = FindObjectsOfType<UniTweenSequence>();

        sequences.Clear();
        for (int i = 0; i < evs.Length; i++)
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

            // Draws a toolbar with the name of the currently selected menu item.

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null)
                {
                    //GUILayout.Label(((GameEventListener)selected.ObjectInstance).name + " > " + selected.Name);
                    //GUILayout.Label(selected.Name);

                    if (SirenixEditorGUI.ToolbarButton(new GUIContent("Select GameObject")))
                    {
                        Selection.activeGameObject = (((UniTweenSequence)selected.ObjectInstance).gameObject);
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
}
#endif