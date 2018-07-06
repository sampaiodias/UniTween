namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Image")]
    public class ImageTween : TweenData
    {

        [Space(15)]
        public ImageCommand command;
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
            List<Image> images = (List<Image>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in images)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in images)
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
        public Tween GetTween(Image img)
        {
            switch (command)
            {
                case ImageCommand.Color:
                    return img.DOColor(color, duration);
                case ImageCommand.Fade:
                    return img.DOFade(to, duration);
                case ImageCommand.FillAmount:
                    return img.DOFillAmount(to, duration);
                case ImageCommand.GradientColor:
                    return img.DOGradientColor(gradient, duration);
                case ImageCommand.BlendableColor:
                    return img.DOBlendableColor(color, duration);
                default:
                    return null;
            }
        }

        private bool HideColor()
        {
            return !command.ToString().Contains("Color") || command == ImageCommand.GradientColor;
        }

        private bool HideTo()
        {
            return command != ImageCommand.Fade && command != ImageCommand.FillAmount;
        }

        private bool HideGradient()
        {
            return command != ImageCommand.GradientColor;
        }

        public enum ImageCommand
        {
            Color,
            Fade,
            FillAmount,
            GradientColor,
            BlendableColor
        }
    }
}