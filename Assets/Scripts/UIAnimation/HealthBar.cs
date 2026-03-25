using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [Header("HealthBar Values")]
    [Tooltip("The PlayerHander you want to display the health of")]
    public PlayerHandler playerHandler;

    [Tooltip("How quickly the health bar animates to its target value. Higher = snappier")]
    public float healthBarSpeed = 2f;

    [Tooltip("Minimum constant speed so the bar never crawls to a stop")]
    public float minBarSpeed = 0.1f;

    private Slider _slider;

    void Awake()
    {
        // Runs on object load
        _slider = GetComponent<Slider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Runs on first frame
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHandler == null) return;
        float targetPercent = (float)playerHandler.currentHealth / (float)playerHandler.maxHealth;

        float lerpSpeed = Mathf.Lerp(_slider.value, targetPercent, Time.deltaTime * healthBarSpeed);
        float moveSpeed = Mathf.MoveTowards(_slider.value, targetPercent, Time.deltaTime * minBarSpeed);

        // Take whichever moves further each frame
        _slider.value = Mathf.Abs(lerpSpeed - targetPercent) < Mathf.Abs(moveSpeed - targetPercent)
            ? lerpSpeed
            : moveSpeed;

    }
}
