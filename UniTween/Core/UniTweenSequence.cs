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
    [FoldoutGroup("Settings", true)]
    [Tooltip("OPTIONAL: If you want to play this Sequence using Play(string id), set this.")]
    [LabelText("ID")]
    public string id;
    [Space]
    [Tooltip("Set this to -1 for infinite loops.")]
    [FoldoutGroup("Settings", true)]
    public int loops;
    [FoldoutGroup("Settings", true)]
    [ShowIf("IsLoopSequence")]
    public LoopType loopType;
    [FoldoutGroup("Settings", true)]
    public float timeScale = 1;
    [Space]
    [FoldoutGroup("Settings", true)]
    public bool playOnStart;
    [FoldoutGroup("Settings", true)]
    public bool playOnEnable;
    [FoldoutGroup("Settings", true)]
    public bool playBackwardsOnStart;
    [FoldoutGroup("Settings", true)]
    public bool playBackwardsOnEnable;
    [FoldoutGroup("Settings", true)]
    public bool resumeOnEnable;
    [FoldoutGroup("Settings", true)]
    public bool rewindOnDisable;
    [FoldoutGroup("Settings", true)]
    public bool killOnDisable;

    private static List<UniTweenSequence> sequences = new List<UniTweenSequence>();
    private static Lookup<string, UniTweenSequence> sequenceLookup = (Lookup<string, UniTweenSequence>)(sequences.ToLookup(obj => obj.id));

    /// <summary>
    /// Plays a UniTween Sequence based on an ID (defined on the UniTween Sequence component via inspector). 
    /// </summary>
    /// <param name="id">The ID defined on the UniTween Sequence component</param>
    public static void Play(string id)
    {
        sequenceLookup = (Lookup<string, UniTweenSequence>)(sequences.ToLookup(obj => obj.id));

        foreach (var sequence in sequenceLookup[id])
        {
            sequence.Play();
        }
    }

    /// <summary>
    /// The same as Play(string id), but this one can be called via UnityEvents and other non-static environments.
    /// </summary>
    /// <param name="id">The ID defined on the UniTween Sequence component</param>
    public void PlayViaID(string id)
    {
        Play(id);
    }

    /// <summary>
    /// Initializes and plays the Sequence. If you already called this once and didn't change 
    /// the Sequence, consider using Resume() for a performance boost.
    /// </summary>
    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    [PropertyOrder(-1)]
    [Button(ButtonSizes.Medium)]
    public void Play()
    {
        sq = DOTween.Sequence();
        for (int i = 0; i < uniTweens.Count; i++)
        {
            switch (uniTweens[i].operation)
            {
                case UniTween.TweenOperation.Append:
                    sq.Append(GetTween(uniTweens[i]));
                    break;
                case UniTween.TweenOperation.AppendInterval:
                    sq.AppendInterval(uniTweens[i].interval);
                    break;
                case UniTween.TweenOperation.AppendCallback:
                    sq.AppendCallback(() => uniTweens[i].unityEvent.Invoke());
                    break;
                case UniTween.TweenOperation.Join:
                    sq.Join(GetTween(uniTweens[i]));
                    break;
            }
        }
        sq.SetLoops(loops, loopType);
        sq.timeScale = timeScale;
        sq.Play();
    }

    /// <summary>
    /// Initializes and Sequence backwards and plays the Sequence.
    /// If you already called this once and didn't change 
    /// the Sequence, consider using Resume() for a small performance boost.
    /// </summary>
    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    [PropertyOrder(-1)]
    [Button(ButtonSizes.Medium)]
    public void PlayBackwards()
    {
        sq = DOTween.Sequence();
        for (int i = uniTweens.Count - 1; i >= 0; i--)
        {
            switch (uniTweens[i].operation)
            {
                case UniTween.TweenOperation.Append:
                    sq.Append(GetTween(uniTweens[i]));
                    break;
                case UniTween.TweenOperation.AppendInterval:
                    sq.AppendInterval(uniTweens[i].interval);
                    break;
                case UniTween.TweenOperation.AppendCallback:
                    sq.AppendCallback(() => uniTweens[i].unityEvent.Invoke());
                    break;
                case UniTween.TweenOperation.Join:
                    sq.Join(GetTween(uniTweens[i]));
                    break;
            }
        }
        sq.SetLoops(loops, loopType);
        sq.timeScale = timeScale;
        sq.Play();
    }

    /// <summary>
    /// Resumes the playing Sequence, playing it where it was paused. 
    /// Only works if the Sequence was initialized before (using Play() or PlayBackwards()).
    /// </summary>
    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    [PropertyOrder(-1)]
    [Button(ButtonSizes.Medium)]
    public void Resume()
    {
        sq.Play();
    }

    /// <summary>
    /// Pauses the Sequence.
    /// </summary>
    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    [PropertyOrder(-1)]
    [Button(ButtonSizes.Medium)]
    public void Pause()
    {
        sq.Pause();
    }

    /// <summary>
    /// Rewinds and pauses the Sequence.
    /// </summary>
    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    [PropertyOrder(-1)]
    [Button(ButtonSizes.Medium)]
    public void Rewind()
    {
        sq.Rewind();
    }

    /// <summary>
    /// Kills the Sequence.
    /// </summary>
    [ShowIf("IsPlaying")]
    [ButtonGroup("Player")]
    [PropertyOrder(-1)]
    [Button(ButtonSizes.Medium)]
    public void Kill()
    {
        sq.Kill();
    }

    private void CreateNewUniTween()
    {
        for (int i = 0; i < uniTweens.Count; i++)
        {
            if (uniTweens[i] == null)
                uniTweens[i] = new UniTween();
        }
    }

    private Tween GetTween(UniTween uniTween)
    {
        Tween t = uniTween.tweenData.GetTween(uniTween.target);
        if (uniTween.tweenData.customEase)
            t.SetEase(uniTween.tweenData.curve);
        else
            t.SetEase(uniTween.tweenData.ease);
        t.SetDelay(uniTween.tweenData.delay);
        return t;
    }

    private bool IsPlaying()
    {
        return Application.isPlaying;
    }

    private bool IsLoopSequence()
    {
        return loops != 0;
    }

    private void Start()
    {
        if (playOnStart)
            Play();

        if (playBackwardsOnStart)
            PlayBackwards();
    }

    private void OnEnable()
    {
        if (id != "")
            sequences.Add(this);

        if (playOnEnable)
            Play();

        if (resumeOnEnable)
            Resume();

        if (playBackwardsOnEnable)
            PlayBackwards();
    }

    private void OnDisable()
    {
        if (id != "")
            sequences.Remove(this);

        if (rewindOnDisable)
            Rewind();

        if (killOnDisable)
            Kill();
    }
}
