using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Tween Data/Canvas/Image")]
public class ImageTween : TweenData {

    [Space(15)]
    public ImageCommand command;
    [HideIf("HideColor")]
    public Color color;
    [HideIf("HideTo")]
    public float to;
    [HideIf("HideGradient")]
    public Gradient gradient;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Image img = (Image)GetComponent(uniTweenTarget);

        switch (command)
        {
            case ImageCommand.Color:
                return img.DOColor(color, duration);
            case ImageCommand.Fade:
                return img.DOFade(to, duration);
            case ImageCommand.FillAmount:
                return img.DOFillAmount(to, duration);
            case ImageCommand.GradientColor:
                return img.DOGradientColor(gradient, duration);
            case ImageCommand.BlendableColor:
                return img.DOBlendableColor(color, duration);
            default:
                return null;
        }
    }

    private bool HideColor()
    {
        return !command.ToString().Contains("Color") || command == ImageCommand.GradientColor;
    }

    private bool HideTo()
    {
        return command != ImageCommand.Fade && command != ImageCommand.FillAmount;
    }

    private bool HideGradient()
    {
        return command != ImageCommand.GradientColor;
    }

    public enum ImageCommand
    {
        Color,
        Fade,
        FillAmount,
        GradientColor,
        BlendableColor
    }
}
