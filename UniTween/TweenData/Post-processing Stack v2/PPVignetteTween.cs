#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Vignette")]
public class PPVignetteTween : TweenData
{
    [Space(15)]
    [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
    public bool automaticOverride = true;
    [Space]
    public VignetteCommand command;

    [HideIf("ShowColor")]
    public float to;
    [ShowIf("ShowColor")]
    public Color color;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
        var setting = volume.profile.GetSetting<Vignette>();

        if (setting != null)
        {
            setting.active = automaticOverride;
            switch (command)
            {
                case VignetteCommand.Color:
                    setting.color.overrideState = automaticOverride;
                    return DOTween.To(() => setting.color.value, x => setting.color.value = x, color, duration);
                case VignetteCommand.Intensity:
                    setting.intensity.overrideState = automaticOverride;
                    return DOTween.To(() => setting.intensity.value, x => setting.intensity.value = x, to, duration);
                case VignetteCommand.Smoothness:
                    setting.smoothness.overrideState = automaticOverride;
                    return DOTween.To(() => setting.smoothness.value, x => setting.smoothness.value = x, to, duration);
                case VignetteCommand.Roundness:
                    setting.roundness.overrideState = automaticOverride;
                    return DOTween.To(() => setting.roundness.value, x => setting.roundness.value = x, to, duration);
                case VignetteCommand.Opacity:
                    setting.opacity.overrideState = automaticOverride;
                    return DOTween.To(() => setting.opacity.value, x => setting.opacity.value = x, to, duration);
            }
        }
        else
        {
            Debug.Log("UniTween could not find a Vignette to tween. Be sure to add it on your Post Process Volume component");
        }

        return null;
    }

    private bool ShowColor()
    {
        return command == VignetteCommand.Color;
    }

    public enum VignetteCommand
    {
        Color,
        Intensity,
        Smoothness,
        Roundness,
        Opacity,
    }
}
#endif