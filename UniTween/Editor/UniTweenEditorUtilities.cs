#if UNITY_EDITOR
namespace UniTween.Editor
{
    using UniTween.Core;
    using UnityEditor;
    using UnityEngine;

    public class UniTweenEditorUtilities : Editor
    {
        [MenuItem("GameObject/UniTween Sequence", priority = 11)]
        public static void CreateGameObjectWithSequence()
        {
            GameObject obj = new GameObject();
            obj.AddComponent<UniTweenSequence>();
            obj.name = "Sequence NewSequence";

            if (Selection.activeTransform != null)
            {
                obj.transform.SetParent(Selection.activeTransform);
                obj.transform.position = Selection.activeTransform.position;
            }
            Selection.activeTransform = obj.transform;
        }
    }
}
#endif