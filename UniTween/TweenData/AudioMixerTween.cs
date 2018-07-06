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

        /// <summary>
        /// Creates and returns a Tween for all components contained inside the UniTweenTarget.
        /// The Tween is configured based on the attribute values of this TweenData file.
        /// </summary>
        /// <param name="uniTweenTarget">Wrapper that contains a List of the component that this TweenData can tween.</param>
        /// <returns></returns>
        public override Tween GetTween(UniTweenObject.UniTweenTarget uniTweenTarget)
        {
            List<AudioMixer> mixers = (List<AudioMixer>)GetComponent(uniTweenTarget);
            Sequence tweens = DOTween.Sequence();
            if (customEase)
            {
                foreach (var t in mixers)
                {
                    tweens.Join(GetTween(t).SetEase(curve));
                }
            }
            else
            {
                foreach (var t in mixers)
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