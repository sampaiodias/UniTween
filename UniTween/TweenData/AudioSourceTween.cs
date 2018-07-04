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