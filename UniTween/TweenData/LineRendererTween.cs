using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Tween Data/Line Renderer")]
public class LineRendererTween : TweenData
{

    public LineRendererCommand command;

    public Color startColorA;
    public Color startColorB;
    public Color endColorA;
    public Color endColorB;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        LineRenderer line = (LineRenderer)GetComponent(uniTweenTarget);

        switch (command)
        {
            case LineRendererCommand.Color:
                return line.DOColor(new Color2(startColorA, startColorB), new Color2(endColorA, endColorB), duration);
            default:
                return null;
        }
    }

    public enum LineRendererCommand
    {
        Color
    }
}
