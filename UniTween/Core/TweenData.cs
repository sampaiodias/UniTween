namespace UniTween.Core
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [HelpURL("https://github.com/sampaiodias/UniTween/wiki/TweenData-Documentation")]
    public class TweenData : ScriptableObject
    {
        public float duration = 1;
        public float delay;
        public bool customEase;
        [HideIf("customEase")]
        public Ease ease;
        [ShowIf("customEase")]
        public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        public virtual Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            return null;
        }

        public object GetComponent(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            return uniTweenTarget.GetType().GetField("components").GetValue(uniTweenTarget);
        }
    }
}
