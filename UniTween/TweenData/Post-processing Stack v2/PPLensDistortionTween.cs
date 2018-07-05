#if UNITY_POST_PROCESSING_STACK_V2
namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.Rendering.PostProcessing;

    [CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Lens Distortion")]
    public class PPLensDistortionTween : TweenData
    {
        [Space(15)]
        [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
        public bool automaticOverride = true;
        [Space]
        public LensDistortionCommand command;

        public float to;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<PostProcessVolume> volumes = (List<PostProcessVolume>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in volumes)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        /// <summary>
        /// Creates and returns a Tween for the informed component.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public Tween GetTween(PostProcessVolume volume)
        {
            var setting = volume.profile.GetSetting<LensDistortion>();

            if (setting != null)
            {
                setting.active = automaticOverride;
                switch (command)
                {
                    case LensDistortionCommand.Intensity:
                        setting.intensity.overrideState = automaticOverride;
                        return DOTween.To(() => setting.intensity.value, x => setting.intensity.value = x, to, duration);
                    case LensDistortionCommand.YMultiplier:
                        setting.intensityY.overrideState = automaticOverride;
                        return DOTween.To(() => setting.intensityY.value, x => setting.intensityY.value = x, to, duration);
                    case LensDistortionCommand.XMultiplier:
                        setting.intensityX.overrideState = automaticOverride;
                        return DOTween.To(() => setting.intensityX.value, x => setting.intensityX.value = x, to, duration);
                    case LensDistortionCommand.CenterX:
                        setting.centerX.overrideState = automaticOverride;
                        return DOTween.To(() => setting.centerX.value, x => setting.centerX.value = x, to, duration);
                    case LensDistortionCommand.CenterY:
                        setting.centerY.overrideState = automaticOverride;
                        return DOTween.To(() => setting.centerY.value, x => setting.centerY.value = x, to, duration);
                    case LensDistortionCommand.Scale:
                        setting.scale.overrideState = automaticOverride;
                        return DOTween.To(() => setting.scale.value, x => setting.scale.value = x, to, duration);
                }
            }
            else
            {
                Debug.Log("UniTween could not find a Lens Distortion to tween. Be sure to add it on your Post Process Volume component");
            }

            return null;
        }

        public enum LensDistortionCommand
        {
            Intensity,
            YMultiplier,
            XMultiplier,
            CenterX,
            CenterY,
            Scale,
        }
    }
}
#endif