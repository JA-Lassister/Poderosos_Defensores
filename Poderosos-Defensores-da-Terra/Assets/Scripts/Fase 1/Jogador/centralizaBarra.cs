using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class centralizaBarra : MonoBehaviour
{

    public Slider slider;

    float ScreenHeight;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);

        rectTransform.anchoredPosition = new Vector2(35f, -20f);
    }
}