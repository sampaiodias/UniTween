#if UNITY_POST_PROCESSING_STACK_V2
namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.Rendering.PostProcessing;

    [CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Post-process Volume")]
    public class PostProcessVolumeTween : TweenData
    {
        [Space(15)]
        public PPVolumeCommand command;

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
            switch (command)
            {
                case PPVolumeCommand.Weigth:
                    return DOTween.To(() => volume.weight, x => volume.weight = x, to, duration);
                case PPVolumeCommand.Priority:
                    return DOTween.To(() => volume.priority, x => volume.priority = x, to, duration);
                case PPVolumeCommand.BlendDistance:
                    return DOTween.To(() => volume.blendDistance, x => volume.blendDistance = x, to, duration);
            }
            return null;
        }

        public enum PPVolumeCommand
        {
            Weigth,
            Priority,
            BlendDistance,
        }
    }
}
#endif