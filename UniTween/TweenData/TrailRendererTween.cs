using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Tween Data/Trail Renderer")]
public class TrailRendererTween : TweenData
{
    [Space(15)]
    public TrailCommand command;
    [ShowIf("IsResize")]
    public float toStartWidth;
    [ShowIf("IsResize")]
    public float toEndWidth;
    [HideIf("IsResize")]
    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        TrailRenderer trail = (TrailRenderer)GetComponent(uniTweenTarget);

        switch (command)
        {
            case TrailCommand.Resize:
                return trail.DOResize(toStartWidth, toEndWidth, duration);
            case TrailCommand.Time:
                return trail.DOTime(to, duration);
            default:
                return null;
        }
    }

    private bool IsResize()
    {
        return command == TrailCommand.Resize;
    }

    public enum TrailCommand
    {
        Resize,
        Time
    }
}
