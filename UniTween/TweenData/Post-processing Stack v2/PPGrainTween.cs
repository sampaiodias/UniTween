#if UNITY_POST_PROCESSING_STACK_V2
namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.Rendering.PostProcessing;

    [CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Grain")]
    public class PPGrainTween : TweenData
    {
        [Space(15)]
        [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
        public bool automaticOverride = true;
        [Space]
        public GrainCommand command;

        public bool colored;
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
            if (customEase)
            {
                foreach (var t in volumes)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in volumes)
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
        public Tween GetTween(PostProcessVolume volume)
        {
            var setting = volume.profile.GetSetting<Grain>();

            if (setting != null)
            {
                setting.colored.value = colored;
                setting.active = automaticOverride;
                switch (command)
                {
                    case GrainCommand.Intensity:
                        setting.intensity.overrideState = automaticOverride;
                        return DOTween.To(() => setting.intensity.value, x => setting.intensity.value = x, to, duration);
                    case GrainCommand.Size:
                        setting.size.overrideState = automaticOverride;
                        return DOTween.To(() => setting.size.value, x => setting.size.value = x, to, duration);
                    case GrainCommand.LuminanceContribution:
                        setting.lumContrib.overrideState = automaticOverride;
                        return DOTween.To(() => setting.lumContrib.value, x => setting.lumContrib.value = x, to, duration);
                }
            }
            else
            {
                Debug.Log("UniTween could not find a Grain to tween. Be sure to add it on your Post Process Volume component");
            }

            return null;
        }

        public enum GrainCommand
        {
            Intensity,
            Size,
            LuminanceContribution,
        }
    }
}
#endif