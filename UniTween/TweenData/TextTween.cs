namespace UniTween.Data
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.UI;

    [CreateAssetMenu(menuName = "Tween Data/Canvas/Text")]
    public class TextTween : TweenData
    {

        [Space(15)]
        public TextCommand command;

        [HideIf("HideColor")]
        public Color color;
        [HideIf("HideTo")]
        public float to;
        [HideIf("HideNewText")]
        public string newText;
        [HideIf("HideNewText")]
        public bool richText;
        [HideIf("HideNewText")]
        public ScrambleMode scrambleMode;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<Text> texts = (List<Text>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in texts)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in texts)
                {
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
        public Tween GetTween(Text text)
        {
            switch (command)
            {
                case TextCommand.Color:
                    return text.DOColor(color, duration);
                case TextCommand.Fade:
                    return text.DOFade(to, duration);
                case TextCommand.Text:
                    return text.DOText(newText, duration, richText, scrambleMode);
                case TextCommand.BlendableColor:
                    return text.DOBlendableColor(color, duration);
                default:
                    return null;
            }
        }

        private bool HideColor()
        {
            return !command.GetType().ToString().Contains("Color");
        }

        private bool HideTo()
        {
            return command != TextCommand.Fade;
        }

        private bool HideNewText()
        {
            return command != TextCommand.Text;
        }

        public enum TextCommand
        {
            Color,
            Fade,
            Text,
            BlendableColor
        }
    }
}