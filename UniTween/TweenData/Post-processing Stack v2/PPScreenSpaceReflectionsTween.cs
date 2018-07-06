#if UNITY_POST_PROCESSING_STACK_V2
namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.Rendering.PostProcessing;

    [CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Screen Space Reflections")]
    public class PPScreenSpaceReflectionsTween : TweenData
    {
        [Space(15)]
        [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
        public bool automaticOverride = true;
        [Space]
        public ScreenSpaceReflectionsCommand command;

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
            var setting = volume.profile.GetSetting<ScreenSpaceReflections>();

            if (setting != null)
            {
                setting.active = automaticOverride;
                switch (command)
                {
                    case ScreenSpaceReflectionsCommand.MaximumMarchDistance:
                        setting.maximumMarchDistance.overrideState = automaticOverride;
                        return DOTween.To(() => setting.maximumMarchDistance.value, x => setting.maximumMarchDistance.value = x, to, duration);
                    case ScreenSpaceReflectionsCommand.DistanceFade:
                        setting.distanceFade.overrideState = automaticOverride;
                        return DOTween.To(() => setting.distanceFade.value, x => setting.distanceFade.value = x, to, duration);
                    case ScreenSpaceReflectionsCommand.Vignette:
                        setting.vignette.overrideState = automaticOverride;
                        return DOTween.To(() => setting.vignette.value, x => setting.vignette.value = x, to, duration);
                }
            }
            else
            {
                Debug.Log("UniTween could not find a Screen Space Reflections to tween. Be sure to add it on your Post Process Volume component");
            }

            return null;
        }

        public enum ScreenSpaceReflectionsCommand
        {
            MaximumMarchDistance,
            DistanceFade,
            Vignette,
        }
    }
}
#endif