#if UNITY_POST_PROCESSING_STACK_V2
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[CreateAssetMenu(menuName = "Tween Data/Post-processing Stack v2/Chromatic Aberration")]
public class PPChromaticAberrationTween : TweenData
{
    [Space(15)]
    [Tooltip("If true, the post-processing effect you want to tween will be automatically activated.")]
    public bool automaticOverride = true;
    [Space]
    public ChromaticAberrationCommand command;

    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        PostProcessVolume volume = (PostProcessVolume)GetComponent(uniTweenTarget);
        var setting = volume.profile.GetSetting<ChromaticAberration>();

        if (setting != null)
        {
            setting.active = automaticOverride;
            switch (command)
            {
                case ChromaticAberrationCommand.Intensity:
                    setting.intensity.overrideState = automaticOverride;
                    return DOTween.To(() => setting.intensity.value, x => setting.intensity.value = x, to, duration);
            }
        }
        else
        {
            Debug.Log("UniTween could not find a Chromatic Aberration to tween. Be sure to add it on your Post Process Volume component");
        }

        return null;
    }

    public enum ChromaticAberrationCommand
    {
        Intensity,
    }
}
#endif