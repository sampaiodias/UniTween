#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Lens Distortion")]
public class PPLensDistortionTween : TweenData
{
    [Space(15)]
    [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
    public bool automaticOverride = true;
    [Space]
    public LensDistortionCommand command;

    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
        var setting = volume.profile.GetSetting<LensDistortion>();

        if (setting != null)
        {
            setting.active = automaticOverride;
            switch (command)
            {
                case LensDistortionCommand.Intensity:
                    setting.intensity.overrideState = automaticOverride;
                    return DOTween.To(() => setting.intensity.value, x => setting.intensity.value = x, to, duration);
                case LensDistortionCommand.YMultiplier:
                    setting.intensityY.overrideState = automaticOverride;
                    return DOTween.To(() => setting.intensityY.value, x => setting.intensityY.value = x, to, duration);
                case LensDistortionCommand.XMultiplier:
                    setting.intensityX.overrideState = automaticOverride;
                    return DOTween.To(() => setting.intensityX.value, x => setting.intensityX.value = x, to, duration);
                case LensDistortionCommand.CenterX:
                    setting.centerX.overrideState = automaticOverride;
                    return DOTween.To(() => setting.centerX.value, x => setting.centerX.value = x, to, duration);
                case LensDistortionCommand.CenterY:
                    setting.centerY.overrideState = automaticOverride;
                    return DOTween.To(() => setting.centerY.value, x => setting.centerY.value = x, to, duration);
                case LensDistortionCommand.Scale:
                    setting.scale.overrideState = automaticOverride;
                    return DOTween.To(() => setting.scale.value, x => setting.scale.value = x, to, duration);
            }
        }
        else
        {
            Debug.Log("UniTween could not find a Lens Distortion to tween. Be sure to add it on your Post Process Volume component");
        }

        return null;
    }

    public enum LensDistortionCommand
    {
        Intensity,
        YMultiplier,
        XMultiplier,
        CenterX,
        CenterY,
        Scale,
    }
}
#endif