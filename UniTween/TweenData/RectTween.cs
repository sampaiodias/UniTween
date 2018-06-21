using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tween Data/Canvas/Rect Transform")]
public class RectTween : TweenData {

    [Space(15)]
    public RectCommand command;
    public Vector2 value;
    [ShowIf("IsJumpAnchorPos")]
    public float jumpPower;
    [ShowIf("IsJumpAnchorPos")]
    public int numJumps;
    [HideIf("HideSnapping")]
    public bool snapping = false;

    public enum RectCommand
    {
        AnchorMax,
        AnchorMin,        
        AnchorPos,
        AnchorPosY,
        AnchorPosX,        
        JumpAnchorPos,
        Pivot,
        PivotX,
        PivotY,
        PunchAnchorPos,
        ShakeAnchorPos,
        SizeDelta
    }

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        RectTransform rect = (RectTransform)GetComponent(uniTweenTarget);
        switch (command)
        {
            case RectCommand.AnchorPosY:
                return rect.DOAnchorPosY(value.y, duration, snapping);
            case RectCommand.AnchorPosX:
                return rect.DOAnchorPosX(value.x, duration, snapping);
            case RectCommand.AnchorPos:
                return rect.DOAnchorPos(value, duration, snapping);
            case RectCommand.AnchorMin:
                return rect.DOAnchorMin(value, duration, snapping);
            case RectCommand.AnchorMax:
                return rect.DOAnchorMax(value, duration, snapping);
            case RectCommand.JumpAnchorPos:
                return rect.DOJumpAnchorPos(value, jumpPower, numJumps, duration, snapping);
            case RectCommand.Pivot:
                return rect.DOPivot(value, duration);
            case RectCommand.PivotX:
                return rect.DOPivotX(value.x, duration);
            case RectCommand.PivotY:
                return rect.DOPivotY(value.y, duration);
            case RectCommand.PunchAnchorPos:
                return rect.DOPunchAnchorPos(value, duration, snapping: snapping);
            case RectCommand.ShakeAnchorPos:
                return rect.DOShakeAnchorPos(duration, snapping: snapping);
            case RectCommand.SizeDelta:
                return rect.DOSizeDelta(value, duration, snapping);
        }

        return null;
    }

    private bool IsJumpAnchorPos()
    {
        return command == RectCommand.JumpAnchorPos;
    }

    private bool HideSnapping()
    {
        return command.ToString().Contains("Pivot");
    }
}
