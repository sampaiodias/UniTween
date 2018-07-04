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