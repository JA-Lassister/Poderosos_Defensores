using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class LifeBarAirplane : MonoBehaviour
{
    public Slider slider;

    float ScreenHeight;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        // fixa no canto inferior direito
        rectTransform.anchorMin = new Vector2(0.5f, 0f);
        rectTransform.anchorMax = new Vector2(0.5f, 0f);
        rectTransform.pivot = new Vector2(0.5f, 0f);
    }


    void Update()
    {
        rectTransform.anchoredPosition = new Vector2(0f, 30f);
    }

    public void alteraVida(float vida)
    {
        if (vida > 0)
        {
            slider.value = vida;
        }
    }
}
