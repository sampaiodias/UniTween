#if UNITY_POST_PROCESSING_STACK_V2
namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.Rendering.PostProcessing;

    [CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Motion Blur")]
    public class PPMotionBlurTween : TweenData
    {
        [Space(15)]
        [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
        public bool automaticOverride = true;
        [Space]
        public MotionBlurCommand command;

        [HideIf("ShowCount")]
        public float to;
        [ShowIf("ShowCount")]
        public int count;

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
            var setting = volume.profile.GetSetting<MotionBlur>();

            if (setting != null)
            {
                setting.active = automaticOverride;
                switch (command)
                {
                    case MotionBlurCommand.ShutterAngle:
                        setting.shutterAngle.overrideState = automaticOverride;
                        return DOTween.To(() => setting.shutterAngle.value, x => setting.shutterAngle.value = x, to, duration);
                    case MotionBlurCommand.SampleCount:
                        setting.sampleCount.overrideState = automaticOverride;
                        return DOTween.To(() => setting.sampleCount.value, x => setting.sampleCount.value = x, count, duration);
                }
            }
            else
            {
                Debug.Log("UniTween could not find a Motion Blur to tween. Be sure to add it on your Post Process Volume component");
            }

            return null;
        }

        private bool ShowCount()
        {
            return command == MotionBlurCommand.SampleCount;
        }

        public enum MotionBlurCommand
        {
            ShutterAngle,
            SampleCount,
        }
    }
}
#endif