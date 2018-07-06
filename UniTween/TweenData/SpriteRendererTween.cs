namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Tween Data/Sprite Renderer")]
    public class SpriteRendererTween : TweenData
    {

        [Space(15)]
        public SpriteRendererCommand command;

        [HideIf("HideColor")]
        public Color color;
        [HideIf("HideTo")]
        public float to;
        [HideIf("HideGradient")]
        public Gradient gradient;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<SpriteRenderer> spriteRenderers = (List<SpriteRenderer>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in spriteRenderers)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in spriteRenderers)
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
        public Tween GetTween(SpriteRenderer sr)
        {
            switch (command)
            {
                case SpriteRendererCommand.Color:
                    return sr.DOColor(color, duration);
                case SpriteRendererCommand.Fade:
                    return sr.DOFade(to, duration);
                case SpriteRendererCommand.GradientColor:
                    return sr.DOGradientColor(gradient, duration);
                case SpriteRendererCommand.BlendableColor:
                    return sr.DOBlendableColor(color, duration);
                default:
                    return null;
            }
        }

        private bool HideColor()
        {
            return !command.ToString().Contains("Color") || command == SpriteRendererCommand.GradientColor;
        }

        private bool HideTo()
        {
            return command != SpriteRendererCommand.Fade;
        }

        private bool HideGradient()
        {
            return command != SpriteRendererCommand.GradientColor;
        }

        public enum SpriteRendererCommand
        {
            Color,
            Fade,
            GradientColor,
            BlendableColor
        }
    }
}