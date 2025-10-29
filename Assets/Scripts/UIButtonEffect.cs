using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;
    public float pressedScale = 0.9f;
    public float hoverScale = 1.05f;
    public float animationSpeed = 8f;

    private bool isHovered = false;
    private bool isPressed = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        Vector3 targetScale = originalScale;

        if (isPressed)
            targetScale = originalScale * pressedScale;
        else if (isHovered)
            targetScale = originalScale * hoverScale;

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * animationSpeed);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
