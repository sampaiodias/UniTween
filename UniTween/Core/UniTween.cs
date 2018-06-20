using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

[HelpURL("https://github.com/sampaiodias/UniTween")]
public class UniTween
{
    [OnValueChanged("NewOperation")]
    [Tooltip("Choose between one of four operations:\n\nAppend: adds a tween to play when the previous Append operation ends.\n\nAppendInterval: adds an interval (in seconds) between the previous and the next Append operation.\n\nAppendCallback: invokes the methods registered on the callback/UnityEvent when the previous Append operation ends.\n\nJoin: adds a tween to play at the same time of the previous Append operation.")]
    public TweenOperation operation;
    [ShowIf("IsTweenOperation")]
    [OnValueChanged("NewTarget")]
    [Tooltip("To create a new TweenData right-click inside of any folder in your project, go to Create/TweenData and choose the kind of TweenData that modifies the component (or MonoBehaviour) you want to tween.")]
    public TweenData tweenData;
    [ShowIf("IsIntervalOperation")]
    public float interval;
    [ShowIf("IsIntervalOperation")]
    [Tooltip("A random value between these two specified values will be added to the value of \"Interval\".\n\nThe random value will NOT change between loops. It is set only when Play or Play Backwards is called.")]
    public Vector2 randomVariance;
    [ShowIf("IsCallbackOperation")]
    [HideReferenceObjectPicker]
    [Sirenix.Serialization.OdinSerialize]
    public UnityEvent unityEvent;
    [Sirenix.Serialization.OdinSerialize]
    [HideReferenceObjectPicker]
    [ShowIf("ShowTarget")]
    [HideLabel]
    [SerializeField]
    public UniTweenTarget target;

    private string tweenDataCurrentType;

    public float GetInterval()
    {
        return interval + Random.Range(randomVariance.x, randomVariance.y);
    }

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

    /// <summary>
    /// Changes the Type of the tween's target. Don't worry about performance, this method
    /// is only called inside the Editor.
    /// </summary>
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
        else if (tweenData is MaterialTween && !(target is UniTweenTarget<Material>))
        {
            target = new UniTweenTarget<MeshRenderer>();
        }
        else if (tweenData is AudioMixerTween && !(target is UniTweenTarget<AudioMixer>))
        {
            target = new UniTweenTarget<AudioMixer>();
        }
        else if (tweenData is AudioSourceTween && !(target is UniTweenTarget<AudioSource>))
        {
            target = new UniTweenTarget<AudioSource>();
        }
        else if (tweenData is CameraTween && !(target is UniTweenTarget<Camera>))
        {
            target = new UniTweenTarget<Camera>();
        }
        else if (tweenData is OutlineTween && !(target is UniTweenTarget<Outline>))
        {
            target = new UniTweenTarget<Outline>();
        }
        else if (tweenData is LineRendererTween && !(target is UniTweenTarget<LineRenderer>))
        {
            target = new UniTweenTarget<LineRenderer>();
        }
        else if (tweenData is TrailRendererTween && !(target is UniTweenTarget<TrailRenderer>))
        {
            target = new UniTweenTarget<TrailRenderer>();
        }
        else if (tweenData is GraphicTween && !(target is UniTweenTarget<Graphic>))
        {
            target = new UniTweenTarget<Graphic>();
        }
        else if (tweenData is SliderTween && !(target is UniTweenTarget<Slider>))
        {
            target = new UniTweenTarget<Slider>();
        }
        else if (tweenData is ScrollRectTween && !(target is UniTweenTarget<ScrollRect>))
        {
            target = new UniTweenTarget<ScrollRect>();
        }
        else if (tweenData is LayoutElementTween && !(target is UniTweenTarget<LayoutElement>))
        {
            target = new UniTweenTarget<LayoutElement>();
        }
#if UNITWEEN_TEXTMESH
        else if (tweenData is TextMeshProUGUITween && !(target is UniTweenTarget<TMPro.TextMeshProUGUI>))
        {
            target = new UniTweenTarget<TMPro.TextMeshProUGUI>();
        }
        else if (tweenData is TextMeshProTween && !(target is UniTweenTarget<TMPro.TextMeshPro>))
        {
            target = new UniTweenTarget<TMPro.TextMeshPro>();
        }
#endif
#if UNITY_POST_PROCESSING_STACK_V2
        else if (tweenData is PostProcessVolumeTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPVignetteTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPChromaticAberrationTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPBloomTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPGrainTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPDepthOfFieldTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPLensDistortionTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPMotionBlurTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPScreenSpaceReflectionsTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPAmbientOcclusionTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
        else if (tweenData is PPColorGradingTween && !(target is UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>))
        {
            target = new UniTweenTarget<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
        }
#endif
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
