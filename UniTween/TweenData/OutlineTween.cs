using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tween Data/Canvas/Outline")]
public class OutlineTween : TweenData
{

    public OutlineCommand command;

    [HideIf("HideColor")]
    public Color color;
    [ShowIf("HideColor")]
    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Outline outline = (Outline)GetComponent(uniTweenTarget);

        switch (command)
        {
            case OutlineCommand.Color:
                return outline.DOColor(color, duration);
            case OutlineCommand.Fade:
                return outline.DOFade(to, duration);
            default:
                return null;
        }
    }

    private bool HideColor()
    {
        return command != OutlineCommand.Color;
    }

    public enum OutlineCommand
    {
        Color,
        Fade
    }
}
