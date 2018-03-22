using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Audio;

public struct UniTween {

    [OnValueChanged("NewOperation")]
    public TweenOperation operation;
    [ShowIf("IsTweenOperation")]
    [OnValueChanged("NewTarget")]
    public TweenData tweenData;
    [ShowIf("IsIntervalOperation")]
    public float interval;
    [ShowIf("IsCallbackOperation")]
    [HideReferenceObjectPicker]
    public UnityEvent unityEvent;
    [HideReferenceObjectPicker]
    [ShowIf("ShowTarget")]
    [HideLabel]
    public UniTweenTarget target;

    private string tweenDataCurrentType;

    private void NewOperation()
    {
        if (operation == TweenOperation.AppendCallback && unityEvent == null)
        {
            unityEvent = new UnityEvent();
        }
    }

    private void NewTarget()
    {
        if (tweenDataCurrentType == null)
        {
            SetCurrentType();
        }

        if (target == null || (tweenData != null && tweenData.GetType().ToString() != tweenDataCurrentType))
        {
            SetCurrentType();
            SetNewTarget();
        }
    }

    private void SetNewTarget()
    {
        if (tweenData is RectTween && !(target is UniTweenTarget<RectTransform>))
        {
            target = new UniTweenTarget<RectTransform>();
        }
        else if (tweenData is CanvasGroupTween && !(target is UniTweenTarget<CanvasGroup>))
        {
            target = new UniTweenTarget<CanvasGroup>();
        }
        else if (tweenData is TransformTween && !(target is UniTweenTarget<Transform>))
        {
            target = new UniTweenTarget<Transform>();
        }
        else if (tweenData is ImageTween && !(target is UniTweenTarget<Image>))
        {
            target = new UniTweenTarget<Image>();
        }
        else if (tweenData is TextTween && !(target is UniTweenTarget<Text>))
        {
            target = new UniTweenTarget<Text>();
        }
        else if (tweenData is SpriteRendererTween && !(target is UniTweenTarget<SpriteRenderer>))
        {
            target = new UniTweenTarget<SpriteRenderer>();
        }
        else if (tweenData is RigidbodyTween && !(target is UniTweenTarget<Rigidbody>))
        {
            target = new UniTweenTarget<Rigidbody>();
        }
        else if (tweenData is Rigidbody2DTween && !(target is UniTweenTarget<Rigidbody2D>))
        {
            target = new UniTweenTarget<Rigidbody2D>();
        }
        else if (tweenData is LightTween && !(target is UniTweenTarget<Light>))
        {
            target = new UniTweenTarget<Light>();
        }
        else if (tweenData is MaterialTween && !(target is UniTweenTarget<MaterialTween>))
        {
            target = new UniTweenTarget<MeshRenderer>();
        }
        else if (tweenData is AudioMixerTween && !(target is UniTweenTarget<AudioMixerTween>))
        {
            target = new UniTweenTarget<AudioMixer>();
        }
        else if (tweenData is AudioSourceTween && !(target is UniTweenTarget<AudioSourceTween>))
        {
            target = new UniTweenTarget<AudioSource>();
        }
        else if (tweenData is CameraTween && !(target is UniTweenTarget<CameraTween>))
        {
            target = new UniTweenTarget<Camera>();
        }
        else if (tweenData is OutlineTween && !(target is UniTweenTarget<OutlineTween>))
        {
            target = new UniTweenTarget<Outline>();
        }
        else if (tweenData is LineRendererTween && !(target is UniTweenTarget<LineRendererTween>))
        {
            target = new UniTweenTarget<LineRenderer>();
        }
    }

    private void SetCurrentType()
    {
        tweenDataCurrentType = tweenData.GetType().ToString();
    }

    private bool ShowTarget()
    {
        return !IsTweenDataNull() && IsTweenOperation();
    }

    private bool IsTweenDataNull()
    {
        return tweenData == null;
    }

    private bool IsTweenOperation()
    {
        return operation != TweenOperation.AppendCallback && operation != TweenOperation.AppendInterval;
    }

    private bool IsCallbackOperation()
    {
        return operation == TweenOperation.AppendCallback;
    }

    private bool IsIntervalOperation()
    {
        return operation == TweenOperation.AppendInterval;
    }

    public enum TweenOperation
    {
        Append,
        AppendInterval,
        AppendCallback,
        Join
    }

    #region Wrapper
    public abstract class UniTweenTarget
    {
    }

    public class UniTweenTarget<T> : UniTweenTarget
    {
        public T component;
    }
    #endregion
}
