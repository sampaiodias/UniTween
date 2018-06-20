using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

[HelpURL("http://dotween.demigiant.com/documentation.php#creatingTweener")]
public class TweenData : ScriptableObject
{

    public float duration = 1;
    public float delay;
    public bool customEase;
    [HideIf("customEase")]
    public Ease ease;
    [ShowIf("customEase")]
    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public virtual Tween GetTween(UniTween.UniTweenTarget uniTweenTarget)
    {
        return null;
    }

    public object GetComponent(UniTween.UniTweenTarget uniTweenTarget)
    {
        return uniTweenTarget.GetType().GetField("component").GetValue(uniTweenTarget);
    }
}
