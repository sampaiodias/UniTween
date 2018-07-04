#if UNITY_POST_PROCESSING_STACK_V2
namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.Rendering.PostProcessing;

    [CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Chromatic Aberration")]
    public class PPChromaticAberrationTween : TweenData
    {
        [Space(15)]
        [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
        public bool automaticOverride = true;
        [Space]
        public ChromaticAberrationCommand command;

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
            var setting = volume.profile.GetSetting<ChromaticAberration>();

            if (setting != null)
            {
                setting.active = automaticOverride;
                switch (command)
                {
                    case ChromaticAberrationCommand.Intensity:
                        setting.intensity.overrideState = automaticOverride;
                        return DOTween.To(() => setting.intensity.value, x => setting.intensity.value = x, to, duration);
                }
            }
            else
            {
                Debug.Log("UniTween could not find a Chromatic Aberration to tween. Be sure to add it on your Post Process Volume component");
            }

            return null;
        }

        public enum ChromaticAberrationCommand
        {
            Intensity,
        }
    }
}
#endif