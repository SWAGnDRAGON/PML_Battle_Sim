using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

[System.Serializable]
public struct ResistantElement
{
    public RectTransform element;
    [Range(0f, 1f)] public float resistance;
}



public class BattleUIDriftScript : MonoBehaviour
{
    [Header("Effect Settings")]
    [Tooltip("How far the buttons drift from center (in pixels)")]
    public float driftStrength = 18f;

    [Tooltip("How smoothly they follow. Lower = slower/lazier")]
    public float smoothSpeed = 4f;

    [Header("ResistantElements")]
    [Tooltip("0 = drifts full, 1 = completely locked in position")]
    public List<ResistantElement> resistantElements;

    private RectTransform _rectTransform;
    private Vector2 _originalAnchoredPos;
    private List<Vector2> _elementOriginalPos = new List<Vector2>();

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalAnchoredPos = _rectTransform.anchoredPosition;

        foreach (var entry in resistantElements)
        {
            if (entry.element != null)
                _elementOriginalPos.Add(entry.element.anchoredPosition);
        }
    }

    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        Vector2 mouseOffset = mousePos - screenCenter;
        Vector2 normalizedOffset = new Vector2(
            mouseOffset.x / screenCenter.x,
            mouseOffset.y / screenCenter.y
        );

        Vector2 targetPos = _originalAnchoredPos + normalizedOffset * driftStrength;

        _rectTransform.anchoredPosition = Vector2.Lerp(
            _rectTransform.anchoredPosition,
            targetPos,
            Time.deltaTime * smoothSpeed
        );

        // Calculate how far the main container has drifted
        Vector2 currentDrift = _rectTransform.anchoredPosition - _originalAnchoredPos;

        // Move each resistant element in the opposite vector
        for (int i = 0; i < resistantElements.Count; i++ )
        {
            if(resistantElements[i].element == null)
            {
                continue;
            }

            Vector2 counterTarget = _elementOriginalPos[i] - currentDrift * resistantElements[i].resistance;

            resistantElements[i].element.anchoredPosition = Vector2.Lerp(
                resistantElements[i].element.anchoredPosition,
                counterTarget,
                Time.deltaTime * smoothSpeed
            );
        }
    }
}