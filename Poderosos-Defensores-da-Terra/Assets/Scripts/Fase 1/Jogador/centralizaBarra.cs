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

        // Fixa no centro superior
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.pivot = new Vector2(0.5f, 1);
    }

    void Update()
    {
        rectTransform.anchoredPosition = new Vector2(0, -10f);
    }

}