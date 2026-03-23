using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class BattleUITransitionAnim : MonoBehaviour
{
    [Header("Main Button RectTransforms")]
    public RectTransform attackButton;
    public RectTransform forfeitButton;

    [Header("Special Options Panel")]
    public SpecialBtnAnimation specialOptions;

    [Header("Positions")]
    public float attackHomeX = -75f;
    public float forfeitHomeX = -75f;
    public float hiddenX = -6000; // Far enough to ambiguously be off screen even for widescreen gaming

    [Header("Animation")]
    public float slideSpeed = 10000; // this could probably be faster

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioMixerGroup sfxMixerGroup;
    public AudioClip[] specialSounds;
    public AudioClip[] backSounds;


    private bool _specialOpen = false;

    void Awake()
    {
        if (sfxMixerGroup != null)
            audioSource.outputAudioMixerGroup = sfxMixerGroup;
    }
    void Update()
    {
        if (_specialOpen && InputSystem.actions["UI/Cancel"].WasPressedThisFrame())
        {
            OnBack();
        }
    }
    /// <summary>
    /// Call this from the Special button's OnClick event in the Inspector
    /// </summary>
    public void OnSpecialClicked()
    {
        if (_specialOpen)
        {
            OnBack();
        }
        else
        {
            _specialOpen = true;
            StopAllCoroutines();
            StartCoroutine(SlideToX(attackButton, hiddenX));
            StartCoroutine(SlideToX(forfeitButton, hiddenX));
            specialOptions.SlideIn();
            PlayRandom(specialSounds);
        }
    }

    /// <summary>
    /// Call this from a Back button's OnClick event to return to default state
    /// </summary>
    public void OnBack()
    {
        _specialOpen = false;
        StopAllCoroutines();
        StartCoroutine(SlideToX(attackButton, attackHomeX));
        StartCoroutine(SlideToX(forfeitButton, forfeitHomeX));
        specialOptions.SlideOut();
        PlayRandom(backSounds);
    }

    /// <summary>
    /// Smoothly moves a RectTransform's anchoredPosition.x to a target value
    /// </summary>
    private IEnumerator SlideToX(RectTransform rect, float targetX)
    {
        // Keep running until the buttons are close enough to the target
        while (Mathf.Abs(rect.anchoredPosition.x - targetX) > 0.5f)
        {
            Vector2 pos = rect.anchoredPosition;
            pos.x = Mathf.MoveTowards(pos.x, targetX, slideSpeed * Time.deltaTime);
            rect.anchoredPosition = pos;
            yield return null;
        }

        // Jumps to target pos once its close to prevent weird slow movement as the diff approaches 0
        Vector2 finalPos = rect.anchoredPosition;
        finalPos.x = targetX;
        rect.anchoredPosition = finalPos;
    }

    
    /// <summary>
    /// Plays a random audio file in a given list
    /// </summary>
    private void PlayRandom(AudioClip[] clips)
    {
        if (audioSource == null || clips.Length == 0) return;
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}