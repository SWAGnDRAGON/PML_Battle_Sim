using TMPro;
using UnityEngine;

public class FloatingNumber : MonoBehaviour
{
    [Header("General Settings")]
    public float lifetime = 1f;
    public AnimationCurve scaleCurve;
    public float baseFontSize = 20f;
    public float sizePer100Value = 0.5f; // Number gets bigger as value increases
    public float maxFontSize = 50f;

    [Header("Damage Movement (Arc)")]
    public float damageArcHeight = 1f;
    public float damageArcWidth = 0.5f;

    [Header("Healing Movement (Float Up)")]
    public float healRiseSpeed = 1.5f;

    [Header("Fade Settings")]
    [Range(0f, 1f)] public float fadeStartPercent = 0.7f;

    private TextMeshPro text;
    private float elapsed;
    private int numberValue;

    private enum PopupType { Damage, Heal, Perfect } //can probably add things like 'weak, resist, critical, perfect' which tack on those words to the numbers.
    private PopupType popupType;

    private Vector3 startPos;
    private Vector3 controlPos;
    private Vector3 endPos;

    void Awake()
    {
        text = GetComponent<TextMeshPro>();
        startPos = transform.position;
        //SetLayerOrder();
    }

    //public void SetLayerOrder()
    //{
    //    text.sortingOrder = StaticData.damageNumberOrder;
    //    StaticData.damageNumberOrder++;
       
    //    if(StaticData.damageNumberOrder >= 32767)
    //    {
    //        StaticData.damageNumberOrder = 0;
    //    }
    //    Debug.Log($"sorting order set to {StaticData.damageNumberOrder - 1} and raised to {StaticData.damageNumberOrder}.");
    //}
    public void Initialize(int value, bool isHealing, string dmgType, bool perfect = false)
    {
        numberValue = value;
        popupType = isHealing ? PopupType.Heal : PopupType.Damage;

        text.color = StaticData.GetElementColor(dmgType);

        if (perfect)
        {
            text.text = $"{value}\nPerfect!";
        }
        else
        {
            text.text = value.ToString();
        }

        // Size scaling
        float sizeBoost = (value / 1f) * sizePer100Value;
        if (sizeBoost > maxFontSize)
        {
            sizeBoost = maxFontSize;
        }
        text.fontSize = baseFontSize + sizeBoost;

        if (!isHealing)
            SetupDamageBezier();
    }

    void SetupDamageBezier()
    {
        // End point: slight horizontal randomness + upward
        endPos = startPos + new Vector3(
            Random.Range(-damageArcWidth, damageArcWidth),
            Random.Range(damageArcHeight * 0.5f, damageArcHeight),
            0f
        );

        // Control point: above midpoint for the arc
        controlPos = (startPos + endPos) / 2f +
                     new Vector3(0f, damageArcHeight * 0.75f, 0f);
    }

    Vector3 GetBezierPoint(float t)
    {
        // Quadratic Bezier
        return (1 - t) * (1 - t) * startPos +
               2 * (1 - t) * t * controlPos +
               t * t * endPos;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        float t = elapsed / lifetime;

        if (popupType == PopupType.Damage)
        {
            transform.position = GetBezierPoint(t);
        }
        else // Healing
        {
            transform.position += Vector3.up * healRiseSpeed * Time.deltaTime;
        }

        // Fade out
        Color c = text.color;
        float fadeStart = fadeStartPercent;
        if (t < fadeStart)
        {
            c.a = 1f; // fully visible early in animation
        }
        else
        {
            float fadeT = (t - fadeStart) / (1f - fadeStart);
            c.a = 1f - fadeT; // fades only near the end
        }
        text.color = c;

        if (elapsed >= lifetime)
            Destroy(gameObject);
    }
}
