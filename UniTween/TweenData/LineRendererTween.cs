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

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<LineRenderer> lines = (List<LineRenderer>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in lines)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in lines)
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