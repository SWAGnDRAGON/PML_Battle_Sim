using UnityEngine;
using UnityEngine.InputSystem;

public class BattleUIDriftScript : MonoBehaviour
{
    [Header("Effect Settings")]
    [Tooltip("How far the buttons drift from center (in pixels)")]
    public float driftStrength = 18f;

    [Tooltip("How smoothly they follow. Lower = slower/lazier")]
    public float smoothSpeed = 4f;

    private RectTransform _rectTransform;
    private Vector2 _originalAnchoredPos;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _originalAnchoredPos = _rectTransform.anchoredPosition;
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
    }
}