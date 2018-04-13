using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[HideMonoScript]
public class UniTweenSequence : SerializedMonoBehaviour
{
    [Space(10)]
    [ListDrawerSettings(AlwaysAddDefaultValue = true)]
    [InlineProperty]
    [LabelText("Sequence")]
    [PropertyOrder(10)]
    [HideReferenceObjectPicker]
    [OnValueChanged("CreateNewUniTween")]
    public List<UniTween> uniTweens = new List<UniTween>();
    [HideInInspector]
    private Sequence sq;
    [Space]
    public bool playOnStart;
    public bool playOnEnable;
    public bool killOnDisable;
    public float timeScale = 1;
    [Tooltip("Set this to -1 for infinite loops.")]
    public int loops;
    [ShowIf("IsLoopSequence")]
    public LoopType loopType;
    [Tooltip("OPTIONAL: If you want to play this Sequence using Play(string id), set this.")]
    [LabelText("ID")]
    public string id;

    private static List<UniTweenSequence> sequences = new List<UniTweenSequence>();
    private static Lookup<string, UniTweenSequence> sequenceLookup = (Lookup<string, UniTweenSequence>)(sequences.ToLookup(obj => obj.id));

    private void Start()
    {
        if (playOnStart)
        {
            Play();
        }
    }

    private void CreateNewUniTween()
    {
        for (int i = 0; i < uniTweens.Count; i++)
        {
            if (uniTweens[i] == null)
                uniTweens[i] = new UniTween();
        }
    }

    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    public void Play()
    {
        sq = DOTween.Sequence();
        foreach (var uniTween in uniTweens)
        {
            switch (uniTween.operation)
            {
                case UniTween.TweenOperation.Append:
                    sq.Append(GetTween(uniTween));
                    break;
                case UniTween.TweenOperation.AppendInterval:
                    sq.AppendInterval(uniTween.interval);
                    break;
                case UniTween.TweenOperation.AppendCallback:
                    sq.AppendCallback(() => uniTween.unityEvent.Invoke());
                    break;
                case UniTween.TweenOperation.Join:
                    sq.Join(GetTween(uniTween));
                    break;
            }
        }
        sq.SetLoops(loops, loopType);
        sq.timeScale = timeScale;
    }

    public Tween GetTween(UniTween uniTween)
    {
        Tween t = uniTween.tweenData.GetTween(uniTween.target);
        if (uniTween.tweenData.customEase)
            t.SetEase(uniTween.tweenData.curve);
        else
            t.SetEase(uniTween.tweenData.ease);
        t.SetDelay(uniTween.tweenData.delay);
        return t;
    }

    public void PlayBackwards()
    {
        sq.PlayBackwards();
    }

    public void Rewind()
    {
        sq.Rewind();
    }

    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    public void Pause()
    {
        sq.Pause();
    }

    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    public void Kill()
    {
        sq.Kill();
    }

    private bool IsPlaying()
    {
        return Application.isPlaying;
    }

    private bool IsLoopSequence()
    {
        return loops != 0;
    }

    private void OnEnable()
    {
        if (id != "")
            sequences.Add(this);

        if (playOnEnable)
            Play();
    }

    private void OnDisable()
    {
        if (id != "")
            sequences.Remove(this);

        if (killOnDisable)
            Kill();
    }

    /// <summary>
    /// Play a UniTween Sequence based on an ID (defined on the UniTween Sequence component via inspector)
    /// </summary>
    /// <param name="id"></param>
    public static void Play(string id)
    {
        sequenceLookup = (Lookup<string, UniTweenSequence>)(sequences.ToLookup(obj => obj.id));

        foreach (var sequence in sequenceLookup[id])
        {
            sequence.Play();
        }
    }

    /// <summary>
    /// The same as Play(string id), but this one can be called via UnityEvents
    /// </summary>
    /// <param name="id"></param>
    public void PlayViaID(string id)
    {
        Play(id);
    }
}
