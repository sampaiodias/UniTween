namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Tween Data/Light")]
    public class LightTween : TweenData
    {

        [Space(15)]
        public LightCommand command;

        [HideIf("HideColor")]
        public Color color;
        [ShowIf("HideColor")]
        public float to;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<Light> lights = (List<Light>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in lights)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in lights)
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
        public Tween GetTween(Light light)
        {
            switch (command)
            {
                case LightCommand.Color:
                    return light.DOColor(color, duration);
                case LightCommand.Intensity:
                    return light.DOIntensity(to, duration);
                case LightCommand.ShadowStrength:
                    return light.DOShadowStrength(to, duration);
                case LightCommand.BlendableColor:
                    return light.DOBlendableColor(color, duration);
                default:
                    return null;
            }
        }

        private bool HideColor()
        {
            return !command.ToString().Contains("Color");
        }

        public enum LightCommand
        {
            Color,
            Intensity,
            ShadowStrength,
            BlendableColor
        }
    }
}