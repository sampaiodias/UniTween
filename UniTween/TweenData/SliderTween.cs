namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Slider")]
    public class SliderTween : TweenData
    {
        [Space(15)]
        public SliderCommand command;
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
            List<Slider> sliders = (List<Slider>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in sliders)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in sliders)
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
        public Tween GetTween(Slider slider)
        {
            switch (command)
            {
                case SliderCommand.Value:
                    return slider.DOValue(to, duration, snapping);
                default:
                    return null;
            }
        }

        public enum SliderCommand
        {
            Value
        }
    }
}