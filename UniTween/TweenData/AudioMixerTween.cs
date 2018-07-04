namespace UniTween.Data
{
    using DG.Tweening;
    using System.Collections.Generic;
    using UniTween.Core;
    using UnityEngine;
    using UnityEngine.Audio;

    [CreateAssetMenu(menuName = "Tween Data/Audio Mixer")]
    public class AudioMixerTween : TweenData
    {

        public AudioMixerCommand command;

        public string floatName;
        public float to;

        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<AudioMixer> mixers = (List<AudioMixer>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            foreach (var t in mixers)
            {
                tweens.Join(GetTween(t));
            }
            return tweens;
        }

        public Tween GetTween(AudioMixer mixer)
        {
            switch (command)
            {
                case AudioMixerCommand.SetFloat:
                    return mixer.DOSetFloat(floatName, to, duration);
                default:
                    return null;
            }
        }

        public enum AudioMixerCommand
        {
            SetFloat
        }
    }
}