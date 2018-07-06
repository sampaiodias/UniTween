namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;

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

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<Camera> cameras = (List<Camera>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in cameras)
                {
                    if (t != null)
                        tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in cameras)
                {
                    if (t != null)
                        tweens.Join(GetTween(t).SetEase(ease));
                }
            }
            return tweens;
        }

        /// <summary>
        /// Creates and returns a Tween for the informed component.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public Tween GetTween(Camera cam)
        {
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
}