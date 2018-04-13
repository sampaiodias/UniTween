using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tween Data/Canvas/Layout Element")]
public class LayoutElementTween : TweenData
{
    [Space(15)]
    public LayoutElementCommand command;
    public Vector2 to;
    public bool snapping;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        LayoutElement element = (LayoutElement)GetComponent(uniTweenTarget);

        switch (command)
        {
            case LayoutElementCommand.FlexibleSize:
                return element.DOFlexibleSize(to, duration, snapping);
            case LayoutElementCommand.MinSize:
                return element.DOMinSize(to, duration, snapping);
            case LayoutElementCommand.PreferredSize:
                return element.DOPreferredSize(to, duration, snapping);
            default:
                return null;
        }
    }

    public enum LayoutElementCommand
    {
        FlexibleSize,
        MinSize,
        PreferredSize
    }
}
