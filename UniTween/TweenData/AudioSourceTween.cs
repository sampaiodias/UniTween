namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Tween Data/Audio Source")]
    public class AudioSourceTween : TweenData
    {
        public AudioSourceCommand command;

        public float to;

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<AudioSource> sources = (List<AudioSource>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in sources)
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
        public Tween GetTween(AudioSource source)
        {
            switch (command)
            {
                case AudioSourceCommand.Fade:
                    return source.DOFade(to, duration);
                case AudioSourceCommand.Pitch:
                    return source.DOPitch(to, duration);
                default:
                    return null;
            }
        }

        public enum AudioSourceCommand
        {
            Fade,
            Pitch
        }
    }
}