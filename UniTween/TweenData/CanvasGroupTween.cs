using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tween Data/Canvas/Canvas Group")]
public class CanvasGroupTween : TweenData {

    [Space(15)]
    public CanvasGroupCommand command;
    public float value;

    public enum CanvasGroupCommand
    {
        Fade
    }

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        CanvasGroup canvasGroup = (CanvasGroup)GetComponent(uniTweenTarget);
        switch (command)
        {
            case CanvasGroupCommand.Fade:
                return canvasGroup.DOFade(value, duration);
        }
        return null;
    }
}
