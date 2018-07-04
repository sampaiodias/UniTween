#if UNITY_POST_PROCESSING_STACK_V2
namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.Rendering.PostProcessing;

    [CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Depth Of Field")]
    public class PPDepthOfFieldTween : TweenData
    {
        [Space(15)]
        [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
        public bool automaticOverride = true;
        [Space]
        public DepthOfFieldCommand command;

        public float to;

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

        public Tween GetTween(PostProcessVolume volume)
        {
            var setting = volume.profile.GetSetting<DepthOfField>();

            if (setting != null)
            {
                setting.active = automaticOverride;
                switch (command)
                {
                    case DepthOfFieldCommand.FocusDistance:
                        setting.focusDistance.overrideState = automaticOverride;
                        return DOTween.To(() => setting.focusDistance.value, x => setting.focusDistance.value = x, to, duration);
                    case DepthOfFieldCommand.Aperture:
                        setting.aperture.overrideState = automaticOverride;
                        return DOTween.To(() => setting.aperture.value, x => setting.aperture.value = x, to, duration);
                    case DepthOfFieldCommand.FocalLength:
                        setting.focalLength.overrideState = automaticOverride;
                        return DOTween.To(() => setting.focalLength.value, x => setting.focalLength.value = x, to, duration);
                }
            }
            else
            {
                Debug.Log("UniTween could not find a Depth Of Field to tween. Be sure to add it on your Post Process Volume component");
            }

            return null;
        }

        public enum DepthOfFieldCommand
        {
            FocusDistance,
            Aperture,
            FocalLength,
        }
    }
}
#endif