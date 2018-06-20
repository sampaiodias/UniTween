#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
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

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
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
#endif