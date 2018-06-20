#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using Sirenix.OdinInspector;
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

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
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
#endif