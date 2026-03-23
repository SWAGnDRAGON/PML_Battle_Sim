using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 normalScale = Vector3.one;
    private Vector3 hoverScale = new Vector3(1.08f, 1.08f, 1f);
    private Vector3 pressedScale = new Vector3(0.95f, 0.95f, 1f);

    private Coroutine scaleCoroutine;

    public void OnPointerEnter(PointerEventData e)
    {
        ScaleTo(hoverScale, 0.1f);
    }

    public void OnPointerExit(PointerEventData e)
    {
        ScaleTo(normalScale, 0.1f);
    }

    public void OnPointerDown(PointerEventData e)
    {
        ScaleTo(pressedScale, 0.05f);
    }

    public void OnPointerUp(PointerEventData e)
    {
        ScaleTo(hoverScale, 0.1f);
    }

    private void ScaleTo(Vector3 target, float duration)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleCoroutine(target, duration));
    }

    private IEnumerator ScaleCoroutine(Vector3 target, float duration)
    {
        Vector3 start = transform.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(start, target, elapsed / duration);
            yield return null;
        }

        transform.localScale = target;
    }
}