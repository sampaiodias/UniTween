using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Tween Data/Audio Source")]
public class AudioSourceTween : TweenData
{
    public AudioSourceCommand command;

    public float to;

    public override Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        AudioSource source = (AudioSource)GetComponent(uniTweenTarget);

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
