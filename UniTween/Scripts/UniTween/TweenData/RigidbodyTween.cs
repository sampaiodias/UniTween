using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tween Data/Rigidbody")]
public class RigidbodyTween : TweenData {

    [Space(15)]
    public RigidbodyCommand command;

    [HideIf("HideVector3")]
    public Vector3 vector3;
    [HideIf("HideTo")]
    public float to;
    [HideIf("HideJumpVars")]
    public float jumpPower;
    [HideIf("HideJumpVars")]
    public int numJumps;
    [HideIf("HideConstraint")]
    public AxisConstraint constraint = AxisConstraint.None;
    [ShowIf("ShowRotateMode")]
    public RotateMode rotateMode = RotateMode.Fast;
    [ShowIf("ShowSnapping")]
    public bool snapping = false;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Rigidbody rb = (Rigidbody)GetComponent(uniTweenTarget);

        switch (command)
        {
            case RigidbodyCommand.Move:
                return rb.DOMove(vector3, duration, snapping);
            case RigidbodyCommand.MoveX:
                return rb.DOMoveX(to, duration, snapping);
            case RigidbodyCommand.MoveY:
                return rb.DOMoveY(to, duration, snapping);
            case RigidbodyCommand.MoveZ:
                return rb.DOMoveZ(to, duration, snapping);
            case RigidbodyCommand.Jump:
                return rb.DOJump(vector3, jumpPower, numJumps, duration, snapping);
            case RigidbodyCommand.Rotate:
                return rb.DORotate(vector3, duration, mode: rotateMode);
            case RigidbodyCommand.LookAt:
                return rb.DOLookAt(vector3, duration, axisConstraint: constraint);
            default:
                return null;
        }
    }

    private bool HideVector3()
    {
        return command != RigidbodyCommand.Move && command != RigidbodyCommand.Jump && command != RigidbodyCommand.Rotate;
    }

    private bool HideTo()
    {
        return command != RigidbodyCommand.MoveX && command != RigidbodyCommand.MoveY;
    }

    private bool HideJumpVars()
    {
        return command != RigidbodyCommand.Jump;
    }

    private bool HideConstraint()
    {
        return command != RigidbodyCommand.LookAt;
    }

    private bool ShowSnapping()
    {
        return command.ToString().Contains("Move") || command == RigidbodyCommand.Jump;
    }

    private bool ShowRotateMode()
    {
        return command == RigidbodyCommand.Rotate;
    }

    public enum RigidbodyCommand
    {
        Move,
        MoveX,
        MoveY,
        MoveZ,
        Jump,
        Rotate,
        LookAt
    }
}
