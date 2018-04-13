using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tween Data/Canvas/Slider")]
public class SliderTween : TweenData
{
    [Space(15)]
    public SliderCommand command;
    public float to;
    public bool snapping;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Slider slider = (Slider)GetComponent(uniTweenTarget);

        switch (command)
        {
            case SliderCommand.Value:
                return slider.DOValue(to, duration, snapping);
            default:
                return null;
        }
    }

    public enum SliderCommand
    {
        Value
    }
}
