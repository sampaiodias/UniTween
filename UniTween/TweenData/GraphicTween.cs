namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Graphic")]
    public class GraphicTween : TweenData
    {
        [Space(15)]
        public GraphicCommand command;
        [ShowIf("IsColor")]
        public Color color;
        [HideIf("IsColor")]
        public float to;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<Graphic> graphics = (List<Graphic>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in graphics)
                {
                    if (t != null)
                        tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in graphics)
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
        public Tween GetTween(Graphic graphic)
        {
            switch (command)
            {
                case GraphicCommand.Color:
                    return graphic.DOColor(color, duration);
                case GraphicCommand.Fade:
                    return graphic.DOFade(to, duration);
                case GraphicCommand.BlendableColor:
                    return graphic.DOBlendableColor(color, duration);
                default:
                    return null;
            }
        }

        private bool IsColor()
        {
            return command == GraphicCommand.Color || command == GraphicCommand.BlendableColor;
        }

        public enum GraphicCommand
        {
            Color,
            Fade,
            BlendableColor
        }
    }
}