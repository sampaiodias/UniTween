namespace UniTween.Core
{
    using DG.Tweening;
    using Sirenix.OdinInspector;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [HideMonoScript]
    [HelpURL("https://github.com/sampaiodias/UniTween/wiki")]
    public class UniTweenSequence : SerializedMonoBehaviour
    {
        #region Attributes
        [Space(10)]
        [ListDrawerSettings(Expanded = true, AlwaysAddDefaultValue = true)]
        [InlineProperty]
        [LabelText("Sequence")]
        [PropertyOrder(10)]
        [HideReferenceObjectPicker]
        [OnValueChanged("CreateNewUniTween")]
        public List<UniTweenObject> uniTweens = new List<UniTweenObject>();
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
        [FoldoutGroup("Settings", true)]
        [Tooltip("If true, this Sequence will ignore the current Time Scale of the application (Time.timeScale).")]
        public bool ignoreUnityTimeScale = false;
        [FoldoutGroup("Settings", true)]
        [Tooltip("DOTween's update type for THIS Sequence.\n\nNormal: updates on Update() calls.\nFixed: updates on FixedUpdate() calls.\nLate: updates on LateUpdate() calls.")]
        public UpdateType updateTime = UpdateType.Normal;
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
        #endregion

        #region Tween
        /// <summary>
        /// Plays a UniTween Sequence based on an ID (defined on the UniTween Sequence component via inspector). 
        /// </summary>
        /// <param name="id">The ID defined on the UniTween Sequence component</param>
        public static void Play(string id)
        {
            if (id != "")
            {
                sequenceLookup = (Lookup<string, UniTweenSequence>)(sequences.ToLookup(obj => obj.id));

                foreach (var sequence in sequenceLookup[id])
                {
                    sequence.Play();
                }
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
            sq.SetUpdate(updateTime, ignoreUnityTimeScale);
            foreach (var uniTween in uniTweens)
            {
                switch (uniTween.operation)
                {
                    case UniTweenObject.TweenOperation.Append:
                        sq.Append(GetTween(uniTween));
                        break;
                    case UniTweenObject.TweenOperation.AppendInterval:
                        sq.AppendInterval(uniTween.interval);
                        break;
                    case UniTweenObject.TweenOperation.AppendCallback:
                        sq.AppendCallback(() => uniTween.unityEvent.Invoke());
                        break;
                    case UniTweenObject.TweenOperation.Join:
                        sq.Join(GetTween(uniTween));
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
            sq.SetUpdate(updateTime, ignoreUnityTimeScale);
            IEnumerable<UniTweenObject> sequence = uniTweens;
            foreach (var uniTween in sequence.Reverse())
            {
                switch (uniTween.operation)
                {
                    case UniTweenObject.TweenOperation.Append:
                        sq.Append(GetTween(uniTween));
                        break;
                    case UniTweenObject.TweenOperation.AppendInterval:
                        sq.AppendInterval(uniTween.interval);
                        break;
                    case UniTweenObject.TweenOperation.AppendCallback:
                        sq.AppendCallback(() => uniTween.unityEvent.Invoke());
                        break;
                    case UniTweenObject.TweenOperation.Join:
                        sq.Join(GetTween(uniTween));
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
        #endregion

        #region TweenAll
        /// <summary>
        /// Plays all Sequences currently enabled.
        /// </summary>
        public void PlayAll()
        {
            foreach (var sq in sequences)
            {
                sq.Play();
            }
        }

        /// <summary>
        /// Initializes all Sequences backwards and plays them.
        /// </summary>
        public void PlayBackwardsAll()
        {
            foreach (var sq in sequences)
            {
                sq.PlayBackwards();
            }
        }

        /// <summary>
        /// Resumes all Sequences currently enabled.
        /// Only works for Sequences that were initialized before (using Play or PlayBackwards).
        /// </summary>
        public void ResumeAll()
        {
            foreach (var sq in sequences)
            {
                sq.Resume();
            }
        }

        /// <summary>
        /// Pauses all Sequences currently enabled.
        /// </summary>
        public void PauseAll()
        {
            foreach (var sq in sequences)
            {
                sq.Pause();
            }
        }

        /// <summary>
        /// Rewinds all Sequences currently enabled.
        /// </summary>
        public void RewindAll()
        {
            foreach (var sq in sequences)
            {
                sq.Rewind();
            }
        }

        /// <summary>
        /// Kills all Sequences currently enabled.
        /// </summary>
        public void KillAll()
        {
            foreach (var sq in sequences)
            {
                sq.Kill();
            }
        }
        #endregion

        #region TweenDelayed
        /// <summary>
        /// Plays the Sequence after the specified time (in seconds).
        /// </summary>
        /// <param name="delay">Delay (in seconds) before the Sequence will be played.</param>
        public void PlayDelayed(float delay)
        {
            StartCoroutine(PlayDelayed(this, new WaitForSeconds(delay)));
        }

        /// <summary>
        /// Plays the Sequence after the specified time.
        /// </summary>
        /// <param name="delay">Delay before the Sequence will be played.</param>
        public void PlayDelayed(WaitForSeconds delay)
        {
            StartCoroutine(PlayDelayed(this, delay));
        }

        private IEnumerator PlayDelayed(UniTweenSequence sequence, WaitForSeconds delay)
        {
            yield return delay;
            sequence.Play();
        }

        /// <summary>
        /// Plays the Sequence backwards after the specified time (in seconds).
        /// </summary>
        /// <param name="delay">Delay (in seconds) before the Sequence will be played.</param>
        public void PlayBackwardsDelayed(float delay)
        {
            StartCoroutine(PlayBackwardsDelayed(this, new WaitForSeconds(delay)));
        }

        /// <summary>
        /// Plays the Sequence backwards after the specified time.
        /// </summary>
        /// <param name="delay">Delay before the Sequence will be played.</param>
        public void PlayBackwardsDelayed(WaitForSeconds delay)
        {
            StartCoroutine(PlayDelayed(this, delay));
        }

        private IEnumerator PlayBackwardsDelayed(UniTweenSequence sequence, WaitForSeconds delay)
        {
            yield return delay;
            sequence.Play();
        }

        /// <summary>
        /// Resumes the Sequence after the specified time (in seconds).
        /// </summary>
        /// <param name="delay">Delay (in seconds) before the Sequence will be resumed.</param>
        public void ResumeDelayed(float delay)
        {
            StartCoroutine(ResumeDelayed(this, new WaitForSeconds(delay)));
        }

        /// <summary>
        /// Resumes the Sequence after the specified time.
        /// </summary>
        /// <param name="delay">Delay before the Sequence will be resumed.</param>
        public void ResumeDelayed(WaitForSeconds delay)
        {
            StartCoroutine(ResumeDelayed(this, delay));
        }

        private IEnumerator ResumeDelayed(UniTweenSequence sequence, WaitForSeconds delay)
        {
            yield return delay;
            sequence.Resume();
        }

        /// <summary>
        /// Pauses the Sequence after the specified time (in seconds).
        /// </summary>
        /// <param name="delay">Delay (in seconds) before the Sequence will be paused.</param>
        public void PauseDelayed(float delay)
        {
            StartCoroutine(PauseDelayed(this, new WaitForSeconds(delay)));
        }

        /// <summary>
        /// Pauses the Sequence after the specified time.
        /// </summary>
        /// <param name="delay">Delay before the Sequence will be paused.</param>
        public void PauseDelayed(WaitForSeconds delay)
        {
            StartCoroutine(PauseDelayed(this, delay));
        }

        private IEnumerator PauseDelayed(UniTweenSequence sequence, WaitForSeconds delay)
        {
            yield return delay;
            sequence.Pause();
        }

        /// <summary>
        /// Rewinds the Sequence after the specified time (in seconds).
        /// </summary>
        /// <param name="delay">Delay (in seconds) before the Sequence will be rewinded.</param>
        public void RewindDelayed(float delay)
        {
            StartCoroutine(RewindDelayed(this, new WaitForSeconds(delay)));
        }

        /// <summary>
        /// Rewinds the Sequence after the specified time.
        /// </summary>
        /// <param name="delay">Delay before the Sequence will be rewinded.</param>
        public void RewindDelayed(WaitForSeconds delay)
        {
            StartCoroutine(RewindDelayed(this, delay));
        }

        private IEnumerator RewindDelayed(UniTweenSequence sequence, WaitForSeconds delay)
        {
            yield return delay;
            sequence.Rewind();
        }

        /// <summary>
        /// Kills the Sequence after the specified time (in seconds).
        /// </summary>
        /// <param name="delay">Delay (in seconds) before the Sequence will be killed.</param>
        public void KillDelayed(float delay)
        {
            StartCoroutine(KillDelayed(this, new WaitForSeconds(delay)));
        }

        /// <summary>
        /// Kills the Sequence after the specified time.
        /// </summary>
        /// <param name="delay">Delay before the Sequence will be killed.</param>
        public void KillDelayed(WaitForSeconds delay)
        {
            StartCoroutine(KillDelayed(this, delay));
        }

        private IEnumerator KillDelayed(UniTweenSequence sequence, WaitForSeconds delay)
        {
            yield return delay;
            sequence.Kill();
        }
        #endregion

        #region Private Methods
        private void CreateNewUniTween()
        {
            for (int i = 0; i < uniTweens.Count; i++)
            {
                if (uniTweens[i] == null)
                    uniTweens[i] = new UniTweenObject();
            }
        }

        private Tween GetTween(UniTweenObject uniTween)
        {
            Tween t = uniTween.tweenData.GetTween(uniTween.target);
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
            sequences.Remove(this);

            if (rewindOnDisable)
                Rewind();

            if (killOnDisable)
                Kill();
        }
        #endregion
    }
}