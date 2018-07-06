namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Scroll Rect")]
    public class ScrollRectTween : TweenData
    {
        [Space(15)]
        public ScrollRectCommand command;
        [ShowIf("ShowVector2")]
        public Vector2 pos;
        [HideIf("ShowVector2")]
        public float to;
        public bool snapping;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<ScrollRect> scrolls = (List<ScrollRect>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in scrolls)
                {
                    if (t != null)
                        tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in scrolls)
                {
                    if (t != null)
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
        public Tween GetTween(ScrollRect scroll)
        {
            switch (command)
            {
                case ScrollRectCommand.NormalizedPos:
                    return scroll.DONormalizedPos(pos, duration, snapping);
                case ScrollRectCommand.HorizontalNormalizedPos:
                    return scroll.DOHorizontalNormalizedPos(to, duration, snapping);
                case ScrollRectCommand.VerticalPos:
                    return scroll.DOVerticalNormalizedPos(to, duration, snapping);
                default:
                    return null;
            }
        }

        private bool ShowVector2()
        {
            return command == ScrollRectCommand.NormalizedPos;
        }

        public enum ScrollRectCommand
        {
            NormalizedPos,
            HorizontalNormalizedPos,
            VerticalPos
        }
    }
}