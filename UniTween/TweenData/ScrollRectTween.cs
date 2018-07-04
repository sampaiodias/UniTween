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

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<ScrollRect> scrolls = (List<ScrollRect>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in scrolls)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

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