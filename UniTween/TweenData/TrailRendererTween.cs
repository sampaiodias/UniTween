namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Tween Data/Trail Renderer")]
    public class TrailRendererTween : TweenData
    {
        [Space(15)]
        public TrailCommand command;
        [ShowIf("IsResize")]
        public float toStartWidth;
        [ShowIf("IsResize")]
        public float toEndWidth;
        [HideIf("IsResize")]
        public float to;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<TrailRenderer> trails = (List<TrailRenderer>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in trails)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in trails)
                {
                    tweens.Join(GetTween(t).SetEase(ease));
                }
            }
            return tweens;
        }

        /// <summary>
        /// Creates and returns a Tween for the informed component.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public Tween GetTween(TrailRenderer trail)
        {
            switch (command)
            {
                case TrailCommand.Resize:
                    return trail.DOResize(toStartWidth, toEndWidth, duration);
                case TrailCommand.Time:
                    return trail.DOTime(to, duration);
                default:
                    return null;
            }
        }

        private bool IsResize()
        {
            return command == TrailCommand.Resize;
        }

        public enum TrailCommand
        {
            Resize,
            Time
        }
    }
}