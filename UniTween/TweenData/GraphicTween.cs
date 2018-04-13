using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tween Data/Canvas/Graphic")]
public class GraphicTween : TweenData
{
    [Space(15)]
    public GraphicCommand command;
    [ShowIf("IsColor")]
    public Color color;
    [HideIf("IsColor")]
    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Graphic graphic = (Graphic)GetComponent(uniTweenTarget);

        switch (command)
        {
            case GraphicCommand.Color:
                return graphic.DOColor(color, duration);
            case GraphicCommand.Fade:
                return graphic.DOFade(to, duration);
            case GraphicCommand.BlendableColor:
                return graphic.DOBlendableColor(color, duration);
            default:
                return null;
        }
    }

    private bool IsColor()
    {
        return command == GraphicCommand.Color || command == GraphicCommand.BlendableColor;
    }

    public enum GraphicCommand
    {
        Color,
        Fade,
        BlendableColor
    }
}
