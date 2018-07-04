namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Layout Element")]
    public class LayoutElementTween : TweenData
    {
        [Space(15)]
        public LayoutElementCommand command;
        public Vector2 to;
        public bool snapping;

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<LayoutElement> elements = (List<LayoutElement>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in elements)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        public Tween GetTween(LayoutElement element)
        {
            switch (command)
            {
                case LayoutElementCommand.FlexibleSize:
                    return element.DOFlexibleSize(to, duration, snapping);
                case LayoutElementCommand.MinSize:
                    return element.DOMinSize(to, duration, snapping);
                case LayoutElementCommand.PreferredSize:
                    return element.DOPreferredSize(to, duration, snapping);
                default:
                    return null;
            }
        }

        public enum LayoutElementCommand
        {
            FlexibleSize,
            MinSize,
            PreferredSize
        }
    }
}