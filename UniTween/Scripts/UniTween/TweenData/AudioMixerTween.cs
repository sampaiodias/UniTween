using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Tween Data/Audio Mixer")]
public class AudioMixerTween : TweenData {

    public AudioMixerCommand command;

    public string floatName;
    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        AudioMixer mixer = (AudioMixer)GetComponent(uniTweenTarget);

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
