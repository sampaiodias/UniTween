namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Outline")]
    public class OutlineTween : TweenData
    {

        public OutlineCommand command;

        [HideIf("HideColor")]
        public Color color;
        [ShowIf("HideColor")]
        public float to;

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<Outline> outlines = (List<Outline>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in outlines)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        public Tween GetTween(Outline outline)
        {
            switch (command)
            {
                case OutlineCommand.Color:
                    return outline.DOColor(color, duration);
                case OutlineCommand.Fade:
                    return outline.DOFade(to, duration);
                default:
                    return null;
            }
        }

        private bool HideColor()
        {
            return command != OutlineCommand.Color;
        }

        public enum OutlineCommand
        {
            Color,
            Fade
        }
    }
}