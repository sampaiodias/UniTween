using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenData : ScriptableObject {

    public float duration;
    public float delay;
    public bool customEase;
    [HideIf("customEase")]
    public Ease ease;
    [ShowIf("customEase")]
    public AnimationCurve curve;    

    public virtual Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        return null;
    }

    public object GetComponent(UniTween.UniTweenTarget uniTweenTarget)
    {
        return uniTweenTarget.GetType().GetField("component").GetValue(uniTweenTarget);
    }
}
