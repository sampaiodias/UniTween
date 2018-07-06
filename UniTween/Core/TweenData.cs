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

        /// <summary>
        /// This method should be overriden to return the Tween the TweenData 
        /// is configured to perform.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public virtual Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            return null;
        }

        /// <summary>
        /// Gets the List of components contained inside the UniTweenTarget
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public object GetComponent(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            return uniTweenTarget.GetType().GetField("components").GetValue(uniTweenTarget);
        }
    }
}
