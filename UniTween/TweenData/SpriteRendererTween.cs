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

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<SpriteRenderer> spriteRenderers = (List<SpriteRenderer>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in spriteRenderers)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

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