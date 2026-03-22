using UnityEngine;

public class TitleBob : MonoBehaviour
{
    [Header("Bob Settings")]
    public float amplitude = 10f;   // how far it moves up/down in pixels
    public float frequency = 0.8f;  // how fast it bobs

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = new Vector3(startPos.x, startPos.y + offset, startPos.z);
    }
}