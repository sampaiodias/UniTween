using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName = "Tween Data/Camera")]
public class CameraTween : TweenData
{

    [Space(15)]
    public CameraCommand command;

    [HideIf("HideColor")]
    public Color color;
    [HideIf("HideFloat")]
    public float to;
    [ShowIf("ShowRect")]
    public Rect rect;
    [ShowIf("IsShake")]
    public float strength = 3;
    [ShowIf("IsShake")]
    public int vibrato = 10;
    [ShowIf("IsShake")]
    public float randomness = 90;
    [ShowIf("IsShake")]
    public bool fadeOut = true;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        Camera cam = (Camera)GetComponent(uniTweenTarget);

        switch (command)
        {
            case CameraCommand.Aspect:
                return cam.DOAspect(to, duration);
            case CameraCommand.Color:
                return cam.DOColor(color, duration);
            case CameraCommand.FarClipPlane:
                return cam.DOFarClipPlane(to, duration);
            case CameraCommand.FieldOfView:
                return cam.DOFieldOfView(to, duration);
            case CameraCommand.NearClipPlane:
                return cam.DONearClipPlane(to, duration);
            case CameraCommand.OrthoSize:
                return cam.DOOrthoSize(to, duration);
            case CameraCommand.PixerRect:
                return cam.DOPixelRect(rect, duration);
            case CameraCommand.Rect:
                return cam.DORect(rect, duration);
            case CameraCommand.ShakePosition:
                return cam.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
            case CameraCommand.ShakeRotation:
                return cam.DOShakeRotation(duration, strength, vibrato, randomness, fadeOut);
            default:
                return null;
        }
    }

    private bool HideColor()
    {
        return !command.ToString().Contains("Color");
    }

    private bool HideFloat()
    {
        return ShowRect() || !HideColor() || IsShake();
    }

    private bool ShowRect()
    {
        return command.ToString().Contains("Rect");
    }

    private bool IsShake()
    {
        return command.ToString().Contains("Shake");
    }

    public enum CameraCommand
    {
        Aspect,
        Color,
        FarClipPlane,
        FieldOfView,
        NearClipPlane,
        OrthoSize,
        PixerRect,
        Rect,
        ShakePosition,
        ShakeRotation
    }
}
