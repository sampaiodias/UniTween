#if UNITY_EDITOR && ODIN_INSPECTOR
namespace UniTween.Editor
{
    using Sirenix.OdinInspector;
    using Sirenix.OdinInspector.Editor;
    using Sirenix.Utilities;
    using Sirenix.Utilities.Editor;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEditor;

    public class UniTweenHelper : OdinEditorWindow
    {
        [ShowInInspector]
        [HideInPlayMode]
        [HideLabel]
        [ReadOnly]
        private static string HelpText { get { return "This window is unavailable outside Play Mode."; } }

        private static UniTweenHelper window;

        [HideInEditorMode]
        [Title("Runtime Helper", "Inform the ID of the Sequence you want to interact with.")]
        [LabelText(" ID: ")]
        [LabelWidth(40)]
        public string id;

        [PropertySpace]
        [ButtonGroup("ActionButtons"), HideInEditorMode]
        public void Play()
        {
            UniTweenSequence.Play(id);
        }

        [ButtonGroup("ActionButtons"), HideInEditorMode]
        public void PlayBackwards()
        {
            UniTweenSequence.PlayBackwards(id);
        }

        [ButtonGroup("ActionButtons"), HideInEditorMode]
        public void Resume()
        {
            UniTweenSequence.Resume(id);
        }

        [ButtonGroup("ActionButtons"), HideInEditorMode]
        public void Pause()
        {
            UniTweenSequence.Pause(id);
        }

        [ButtonGroup("ActionButtons"), HideInEditorMode]
        public void Rewind()
        {
            UniTweenSequence.Rewind(id);
        }

        [ButtonGroup("ActionButtons"), HideInEditorMode]
        public void Kill()
        {
            UniTweenSequence.Kill(id);
        }

        [ShowInInspector]
        [PropertyOrder(100)]
        [HideInEditorMode]
        [TableList(AlwaysExpanded = true, HideToolbar = true, IsReadOnly = true)]
        [EnableGUI]
        [PropertySpace]
        public static List<SequenceItem> Sequences;

        [MenuItem("Tools/UniTween/Runtime Helper")]
        private static void OpenWindow()
        {
            window = GetWindow<UniTweenHelper>();
            window.titleContent.text = "UniTween Helper";
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            SetSequences();
        }

        [PropertySpace(46)]
        [Button("Filter"), HideInEditorMode]
        [HorizontalGroup("Filter")]
        [PropertyOrder(40)]
        private static void SetSequences()
        {
            var items = new List<SequenceItem>();
            var sequences = UniTweenSequence.GetActiveSequences();
            for (int i = 0; i < sequences.Count; i++)
            {
                if (filter.IsNullOrWhitespace() || sequences[i].id.ToLower().Contains(filter.ToLower()))
                    items.Add(new SequenceItem(sequences[i]));
            }
            Sequences = items;
        }

        [PropertySpace(46)]
        [Button("Clear"), HideInEditorMode]
        [HorizontalGroup("Filter")]
        [PropertyOrder(40)]
        private void ClearFilter()
        {
            filter = "";
            SetSequences();
        }

        [HorizontalGroup("Filter")]
        [HideInEditorMode]
        [HideLabel]
        [PropertyOrder(40)]
        [Title("", "Sequence List")]
        [ShowInInspector]
        public static string filter;

        private void OnInspectorUpdate()
        {
            if (Sequences == null && EditorApplication.isPlaying)
            {
                SetSequences();
            }
        }
    }

    public class SequenceItem
    {
        [ReadOnly]
        public UniTweenSequence sequence;
        [ShowInInspector]
        public string SequenceID { get { return sequence.id; } }

        [TableColumnWidth(120)]
        [ResponsiveButtonGroup("Actions")] public void Play() { sequence.Play(); }
        [ResponsiveButtonGroup("Actions")] public void PlayBackwards() { sequence.PlayBackwards(); }
        [ResponsiveButtonGroup("Actions")] public void Resume() { sequence.Resume(); }
        [ResponsiveButtonGroup("Actions")] public void Pause() { sequence.Pause(); }
        [ResponsiveButtonGroup("Actions")] public void Rewind() { sequence.Rewind(); }
        [ResponsiveButtonGroup("Actions")] public void Kill() { sequence.Kill(); }

        public SequenceItem(UniTweenSequence sequence)
        {
            this.sequence = sequence;
        }
    }
}
#endif