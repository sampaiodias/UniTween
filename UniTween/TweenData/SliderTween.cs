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

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<Slider> sliders = (List<Slider>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in sliders)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

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