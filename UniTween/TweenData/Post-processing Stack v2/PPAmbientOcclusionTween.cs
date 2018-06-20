#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Ambient Occlusion")]
public class PPAmbientOcclusionTween : TweenData
{
    [Space(15)]
    [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
    public bool automaticOverride = true;
    [Space]
    public AmbientOcclusionCommand command;

    [HideIf("ShowColor")]
    public float to;
    [ShowIf("ShowColor")]
    public Color color;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
        var setting = volume.profile.GetSetting<AmbientOcclusion>();

        if (setting != null)
        {
            setting.active = automaticOverride;
            switch (command)
            {
                case AmbientOcclusionCommand.Intensity:
                    setting.intensity.overrideState = automaticOverride;
                    return DOTween.To(() => setting.intensity.value, x => setting.intensity.value = x, to, duration);
                case AmbientOcclusionCommand.Radius:
                    setting.radius.overrideState = automaticOverride;
                    return DOTween.To(() => setting.radius.value, x => setting.radius.value = x, to, duration);
                case AmbientOcclusionCommand.Color:
                    setting.color.overrideState = automaticOverride;
                    return DOTween.To(() => setting.color.value, x => setting.color.value = x, color, duration);
            }
        }
        else
        {
            Debug.Log("UniTween could not find an Ambient Occlusion to tween. Be sure to add it on your Post Process Volume component");
        }

        return null;
    }

    private bool ShowColor()
    {
        return command == AmbientOcclusionCommand.Color;
    }

    public enum AmbientOcclusionCommand
    {
        Intensity,
        Radius,
        Color,
    }
}
#endif