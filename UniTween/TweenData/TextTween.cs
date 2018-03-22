using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tween Data/Canvas/Text")]
public class TextTween : TweenData {

    [Space(15)]
    public TextCommand command;

    [HideIf("HideColor")]
    public Color color;
    [HideIf("HideTo")]
    public float to;
    [HideIf("HideNewText")]
    public string newText;
    [HideIf("HideNewText")]
    public bool richText;
    [HideIf("HideNewText")]
    public ScrambleMode scrambleMode;

    private bool HideColor()
    {
        return !command.GetType().ToString().Contains("Color");
    }

    private bool HideTo()
    {
        return command != TextCommand.Fade;
    }

    private bool HideNewText()
    {
        return command != TextCommand.Text;
    }

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Text text = (Text)GetComponent(uniTweenTarget);

        switch (command)
        {
            case TextCommand.Color:
                return text.DOColor(color, duration);
            case TextCommand.Fade:
                return text.DOFade(to, duration);
            case TextCommand.Text:
                return text.DOText(newText, duration, richText, scrambleMode);
            case TextCommand.BlendableColor:
                return text.DOBlendableColor(color, duration);
            default:
                return null;
        }
    }

    public enum TextCommand
    {
        Color,
        Fade,
        Text,
        BlendableColor
    }
}
