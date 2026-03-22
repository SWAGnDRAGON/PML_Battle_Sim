using System.Collections;
using UnityEngine;

public class SpecialBtnAnimation : MonoBehaviour
{
    [Header("Positions")]
    public float hiddenX = 6000f;
    public float visibleX = 350f;

    [Header("Animation")]
    public float slideSpeed = 10000f;

    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        // move them off screen on load, they are only on screen by default for dev work
        Vector2 pos = _rectTransform.anchoredPosition;
        pos.x = hiddenX;
        _rectTransform.anchoredPosition = pos;
    }

    public void SlideIn()
    {
        // Stop any slide already in progress before starting a new one
        StopAllCoroutines();
        StartCoroutine(SlideToX(visibleX));
    }
    public void SlideOut()
    {
        StopAllCoroutines();
        StartCoroutine(SlideToX(hiddenX));
    }

    private IEnumerator SlideToX(float targetX)
    {
        //Debug.Log("SlideToX Reached on Special!");
        while (Mathf.Abs(_rectTransform.anchoredPosition.x - targetX) > 0.5f)
        {
            Vector2 pos = _rectTransform.anchoredPosition;
            pos.x = Mathf.MoveTowards(pos.x, targetX, slideSpeed * Time.deltaTime);
            _rectTransform.anchoredPosition = pos;
            yield return null; // wait one frame then continue the loop
        }

        Vector2 finalPos = _rectTransform.anchoredPosition;
        finalPos.x = targetX;
        _rectTransform.anchoredPosition = finalPos;
    }
}