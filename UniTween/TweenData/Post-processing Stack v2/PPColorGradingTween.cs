#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Color Grading")]
public class PPColorGradingTween : TweenData
{
    [Space(15)]
    [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
    public bool automaticOverride = true;
    [Space]
    public ColorGradingCommand command;

    [HideIf("HideTo")]
    public float to;
    [ShowIf("ShowColor")]
    public Color color;
    [ShowIf("ShowVector")]
    public Vector4 vector;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
        var setting = volume.profile.GetSetting<ColorGrading>();

        if (setting != null)
        {
            setting.active = automaticOverride;
            switch (command)
            {
                case ColorGradingCommand.Temperature:
                    setting.temperature.overrideState = automaticOverride;
                    return DOTween.To(() => setting.temperature.value, x => setting.temperature.value = x, to, duration);
                case ColorGradingCommand.Tint:
                    setting.tint.overrideState = automaticOverride;
                    return DOTween.To(() => setting.tint.value, x => setting.tint.value = x, to, duration);
                case ColorGradingCommand.PostExposure:
                    setting.postExposure.overrideState = automaticOverride;
                    return DOTween.To(() => setting.postExposure.value, x => setting.postExposure.value = x, to, duration);
                case ColorGradingCommand.ColorFilter:
                    setting.colorFilter.overrideState = automaticOverride;
                    return DOTween.To(() => setting.colorFilter.value, x => setting.colorFilter.value = x, color, duration);
                case ColorGradingCommand.Saturation:
                    setting.saturation.overrideState = automaticOverride;
                    return DOTween.To(() => setting.saturation.value, x => setting.saturation.value = x, to, duration);
                case ColorGradingCommand.Contrast:
                    setting.contrast.overrideState = automaticOverride;
                    return DOTween.To(() => setting.contrast.value, x => setting.contrast.value = x, to, duration);
                case ColorGradingCommand.RedMixerRed:
                    setting.mixerRedOutRedIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerRedOutRedIn.value, x => setting.mixerRedOutRedIn.value = x, to, duration);
                case ColorGradingCommand.RedMixerGreen:
                    setting.mixerRedOutGreenIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerRedOutGreenIn.value, x => setting.mixerRedOutGreenIn.value = x, to, duration);
                case ColorGradingCommand.RedMixerBlue:
                    setting.mixerRedOutBlueIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerRedOutBlueIn.value, x => setting.mixerRedOutBlueIn.value = x, to, duration);
                case ColorGradingCommand.GreenMixerRed:
                    setting.mixerGreenOutRedIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerGreenOutRedIn.value, x => setting.mixerGreenOutRedIn.value = x, to, duration);
                case ColorGradingCommand.GreenMixerGreen:
                    setting.mixerGreenOutGreenIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerGreenOutGreenIn.value, x => setting.mixerGreenOutGreenIn.value = x, to, duration);
                case ColorGradingCommand.GreenMixerBlue:
                    setting.mixerGreenOutBlueIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerGreenOutBlueIn.value, x => setting.mixerGreenOutBlueIn.value = x, to, duration);
                case ColorGradingCommand.BlueMixerRed:
                    setting.mixerBlueOutRedIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerBlueOutRedIn.value, x => setting.mixerBlueOutRedIn.value = x, to, duration);
                case ColorGradingCommand.BlueMixerGreen:
                    setting.mixerBlueOutGreenIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerBlueOutGreenIn.value, x => setting.mixerBlueOutGreenIn.value = x, to, duration);
                case ColorGradingCommand.BlueMixerBlue:
                    setting.mixerBlueOutBlueIn.overrideState = automaticOverride;
                    return DOTween.To(() => setting.mixerBlueOutBlueIn.value, x => setting.mixerBlueOutBlueIn.value = x, to, duration);
                case ColorGradingCommand.Lift:
                    setting.lift.overrideState = automaticOverride;
                    return DOTween.To(() => setting.lift.value, x => setting.lift.value = x, vector, duration);
                case ColorGradingCommand.Gamma:
                    setting.gamma.overrideState = automaticOverride;
                    return DOTween.To(() => setting.gamma.value, x => setting.gamma.value = x, vector, duration);
                case ColorGradingCommand.Gain:
                    setting.gamma.overrideState = automaticOverride;
                    return DOTween.To(() => setting.gamma.value, x => setting.gamma.value = x, vector, duration);
            }
        }
        else
        {
            Debug.Log("UniTween could not find a Color Grading to tween. Be sure to add it on your Post Process Volume component");
        }

        return null;
    }

    private bool HideTo()
    {
        return ShowColor() || ShowVector();
    }

    private bool ShowColor()
    {
        return command == ColorGradingCommand.ColorFilter;
    }

    private bool ShowVector()
    {
        return command == ColorGradingCommand.Lift
            || command == ColorGradingCommand.Gain
            || command == ColorGradingCommand.Gamma;
    }

    public enum ColorGradingCommand
    {
        Temperature,
        Tint,
        PostExposure,
        ColorFilter,
        Saturation,
        Contrast,
        RedMixerRed = 101,
        RedMixerGreen = 102,
        RedMixerBlue = 103,
        GreenMixerRed = 201,
        GreenMixerGreen = 202,
        GreenMixerBlue = 203,
        BlueMixerRed = 301,
        BlueMixerGreen = 302,
        BlueMixerBlue = 303,
        Lift = 400,
        Gamma = 500,
        Gain = 600,
    }
}
#endif