using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tween Data/Transform")]
public class TransformTween : TweenData {

    [Space(15)]
    public TransformCommand command;

    [HideIf("HideValue")]
    public Vector3 value;
    [HideIf("HideQuaternion")]
    public Quaternion quaternion;
    [HideIf("HideJumpPower")]
    public float jumpPower;
    [HideIf("HideNumJumps")]
    public int numJumps;
    [HideIf("HideStrength")]
    public float strength;
    [HideIf("HideVibrato")]
    public int vibrato;
    [HideIf("HideRandomness")]
    public int randomness;
    [HideIf("HideWaypoints")]
    public Vector3[] waypoints;
    [HideIf("HidePathType")]
    public PathType pathType;
    [HideIf("HidePathMode")]
    public PathMode pathMode;
    [HideIf("HideConstraint")]
    public AxisConstraint constraint = AxisConstraint.None;
    [HideIf("HideRotateMode")]
    public RotateMode rotateMode;
    [ShowIf("ShowSnapping")]
    public bool snapping = false;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Transform transform = (Transform)GetComponent(uniTweenTarget);

        switch (command)
        {
            case TransformCommand.Move:
                return transform.DOMove(value, duration, snapping);
            case TransformCommand.MoveX:
                return transform.DOMoveX(value.x, duration, snapping);
            case TransformCommand.MoveY:
                return transform.DOMoveY(value.y, duration, snapping);
            case TransformCommand.MoveZ:
                return transform.DOMoveZ(value.z, duration, snapping);
            case TransformCommand.LocalMove:
                return transform.DOLocalMove(value, duration, snapping);
            case TransformCommand.LocalMoveX:
                return transform.DOLocalMoveX(value.x, duration, snapping);
            case TransformCommand.LocalMoveY:
                return transform.DOLocalMoveY(value.y, duration, snapping);
            case TransformCommand.LocalMoveZ:
                return transform.DOLocalMoveZ(value.z, duration, snapping);
            case TransformCommand.Jump:
                return transform.DOJump(value, jumpPower, numJumps, duration, snapping);
            case TransformCommand.LocalJump:
                return transform.DOLocalJump(value, jumpPower, numJumps, duration, snapping);
            case TransformCommand.Rotate:
                return transform.DORotate(value, duration, rotateMode);
            case TransformCommand.RotateQuaternion:
                return transform.DORotateQuaternion(quaternion, duration);
            case TransformCommand.LocalRotate:
                return transform.DOLocalRotate(value, duration, rotateMode);
            case TransformCommand.LocalRotateQuaternion:
                return transform.DOLocalRotateQuaternion(quaternion, duration);
            case TransformCommand.LookAt:
                return transform.DOLookAt(value, duration);
            case TransformCommand.Scale:
                return transform.DOScale(value, duration);
            case TransformCommand.ScaleX:
                return transform.DOScaleX(value.x, duration);
            case TransformCommand.ScaleY:
                return transform.DOScaleY(value.y, duration);
            case TransformCommand.ScaleZ:
                return transform.DOScaleZ(value.z, duration);
            case TransformCommand.PunchPosition:
                return transform.DOPunchPosition(value, duration, vibrato, snapping: snapping);
            case TransformCommand.PunchRotation:
                return transform.DOPunchRotation(value, duration, vibrato);
            case TransformCommand.PunchScale:
                return transform.DOPunchScale(value, duration, vibrato);
            case TransformCommand.ShakePosition:
                return transform.DOShakePosition(duration, strength, vibrato, randomness, snapping: snapping);
            case TransformCommand.ShakeRotation:
                return transform.DOShakeRotation(duration, strength, vibrato, randomness);
            case TransformCommand.ShakeScale:
                return transform.DOShakeScale(duration, strength, vibrato, randomness);
            case TransformCommand.Path:
                return transform.DOPath(waypoints, duration, pathType, pathMode);
            case TransformCommand.LocalPath:
                return transform.DOLocalPath(waypoints, duration, pathType, pathMode);
            case TransformCommand.BlendableMoveBy:
                return transform.DOBlendableMoveBy(value, duration, snapping);
            case TransformCommand.BlendableLocalMoveBy:
                return transform.DOBlendableLocalMoveBy(value, duration, snapping);
            case TransformCommand.BlendableRotateBy:
                return transform.DOBlendableRotateBy(value, duration, rotateMode);
            case TransformCommand.BlendableLocalRotateBy:
                return transform.DOBlendableLocalRotateBy(value, duration, rotateMode);
            case TransformCommand.BlendableScaleBy:
                return transform.DOBlendableScaleBy(value, duration);
            default:
                return null;
        }
    }

    private bool HideValue()
    {
        return command.ToString().Contains("Quaternion") || command.ToString().Contains("Shake") || command.ToString().Contains("Path");
    }

    private bool HideQuaternion()
    {
        return !command.ToString().Contains("Quaternion");
    }

    private bool HideJumpPower()
    {
        return command != TransformCommand.Jump && command != TransformCommand.LocalJump;
    }

    private bool HideNumJumps()
    {
        return HideJumpPower();
    }

    private bool HideStrength()
    {
        return !command.ToString().Contains("Shake");
    }

    private bool HideVibrato()
    {
        return HideStrength() && !command.ToString().Contains("Punch");
    }

    private bool HideRandomness()
    {
        return HideVibrato();
    }

    private bool HideWaypoints()
    {
        return command != TransformCommand.Path && command != TransformCommand.LocalPath;
    }

    private bool HidePathType()
    {
        return HideWaypoints();
    }

    private bool HidePathMode()
    {
        return HideWaypoints();
    }

    private bool HideRotateMode()
    {
        return command != TransformCommand.BlendableRotateBy && 
            command != TransformCommand.BlendableLocalRotateBy &&
            command != TransformCommand.Rotate &&
            command != TransformCommand.LocalRotate;
    }

    private bool ShowSnapping()
    {
        return command.ToString().Contains("Move") 
            || command.ToString().Contains("Jump")
            || command.ToString().Contains("Position");
    }

    private bool HideConstraint()
    {
        return command != TransformCommand.LookAt;
    }

    public enum TransformCommand
    {
        Move,
        MoveX,
        MoveY,
        MoveZ,
        LocalMove,
        LocalMoveX,
        LocalMoveY,
        LocalMoveZ,
        Jump,
        LocalJump,
        Rotate,
        RotateQuaternion,
        LocalRotate,
        LocalRotateQuaternion,
        LookAt,
        Scale,
        ScaleX,
        ScaleY,
        ScaleZ,
        PunchPosition,
        PunchRotation,
        PunchScale,
        ShakePosition,
        ShakeRotation,
        ShakeScale,
        Path,
        LocalPath,
        BlendableMoveBy,
        BlendableLocalMoveBy,
        BlendableRotateBy,
        BlendableLocalRotateBy,
        BlendableScaleBy
    }
}
