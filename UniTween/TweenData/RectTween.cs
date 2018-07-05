namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Rect Transform")]
    public class RectTween : TweenData
    {

        [Space(15)]
        public RectCommand command;
        public Vector2 value;
        [ShowIf("IsJumpAnchorPos")]
        public float jumpPower;
        [ShowIf("IsJumpAnchorPos")]
        public int numJumps;
        [HideIf("HideSnapping")]
        public bool snapping = false;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<RectTransform> rects = (List<RectTransform>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in rects)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        /// <summary>
        /// Creates and returns a Tween for the informed component.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public Tween GetTween(RectTransform rect)
        {
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
    }
}