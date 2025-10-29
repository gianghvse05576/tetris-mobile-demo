using UnityEngine;

public class LogoLightEffect : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float pulseSpeed = 2f;
    public float pulseAmount = 0.1f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        float scaleFactor = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = originalScale * scaleFactor;
    }
}
