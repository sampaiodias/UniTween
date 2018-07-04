namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Tween Data/Line Renderer")]
    public class LineRendererTween : TweenData
    {

        public LineRendererCommand command;

        public Color startColorA;
        public Color startColorB;
        public Color endColorA;
        public Color endColorB;

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<LineRenderer> lines = (List<LineRenderer>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in lines)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        public Tween GetTween(LineRenderer line)
        {
            switch (command)
            {
                case LineRendererCommand.Color:
                    return line.DOColor(new Color2(startColorA, startColorB), new Color2(endColorA, endColorB), duration);
                default:
                    return null;
            }
        }

        public enum LineRendererCommand
        {
            Color
        }
    }
}