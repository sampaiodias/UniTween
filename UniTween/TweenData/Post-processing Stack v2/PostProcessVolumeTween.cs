#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Post-process Volume")]
public class PostProcessVolumeTween : TweenData
{
    [Space(15)]
    public PPVolumeCommand command;

    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);

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
#endif