namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Canvas Group")]
    public class CanvasGroupTween : TweenData
    {

        [Space(15)]
        public CanvasGroupCommand command;
        public float value;

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<CanvasGroup> groups = (List<CanvasGroup>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in groups)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        public Tween GetTween(CanvasGroup canvasGroup)
        {
            switch (command)
            {
                case CanvasGroupCommand.Fade:
                    return canvasGroup.DOFade(value, duration);
            }
            return null;
        }

        public enum CanvasGroupCommand
        {
            Fade
        }
    }
}