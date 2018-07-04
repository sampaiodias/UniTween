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

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<TrailRenderer> trails = (List<TrailRenderer>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in trails)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

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