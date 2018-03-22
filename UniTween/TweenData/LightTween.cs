using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Tween Data/Light")]
public class LightTween : TweenData {

    [Space(15)]
    public LightCommand command;

    [HideIf("HideColor")]
    public Color color;
    [ShowIf("HideColor")]
    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Light light = (Light)GetComponent(uniTweenTarget);

        switch (command)
        {
            case LightCommand.Color:
                return light.DOColor(color, duration);
            case LightCommand.Intensity:
                return light.DOIntensity(to, duration);
            case LightCommand.ShadowStrength:
                return light.DOShadowStrength(to, duration);
            case LightCommand.BlendableColor:
                return light.DOBlendableColor(color, duration);
            default:
                return null;
        }
    }

    private bool HideColor()
    {
        return !command.ToString().Contains("Color");
    }

	public enum LightCommand
    {
        Color,
        Intensity,
        ShadowStrength,
        BlendableColor
    }
}
