#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Screen Space Reflections")]
public class PPScreenSpaceReflectionsTween : TweenData
{
    [Space(15)]
    [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
    public bool automaticOverride = true;
    [Space]
    public ScreenSpaceReflectionsCommand command;

    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
        var setting = volume.profile.GetSetting<ScreenSpaceReflections>();

        if (setting != null)
        {
            setting.active = automaticOverride;
            switch (command)
            {
                case ScreenSpaceReflectionsCommand.MaximumMarchDistance:
                    setting.maximumMarchDistance.overrideState = automaticOverride;
                    return DOTween.To(() => setting.maximumMarchDistance.value, x => setting.maximumMarchDistance.value = x, to, duration);
                case ScreenSpaceReflectionsCommand.DistanceFade:
                    setting.distanceFade.overrideState = automaticOverride;
                    return DOTween.To(() => setting.distanceFade.value, x => setting.distanceFade.value = x, to, duration);
                case ScreenSpaceReflectionsCommand.Vignette:
                    setting.vignette.overrideState = automaticOverride;
                    return DOTween.To(() => setting.vignette.value, x => setting.vignette.value = x, to, duration);
            }
        }
        else
        {
            Debug.Log("UniTween could not find a Screen Space Reflections to tween. Be sure to add it on your Post Process Volume component");
        }

        return null;
    }

    public enum ScreenSpaceReflectionsCommand
    {
        MaximumMarchDistance,
        DistanceFade,
        Vignette,
    }
}
#endif