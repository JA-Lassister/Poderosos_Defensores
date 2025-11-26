using UnityEngine;
using TMPro;

public class LifeTime : MonoBehaviour
{
    public TMP_Text textoTempo;

    RectTransform rectTransform;

    void Start()
    {
        // Pega o RectTransform do proprio texto
        rectTransform = textoTempo.GetComponent<RectTransform>();

        // Centraliza no topo
        rectTransform.anchorMin = new Vector2(0.5f, 1);
        rectTransform.anchorMax = new Vector2(0.5f, 1);
        rectTransform.pivot = new Vector2(0.5f, 1);

        // Mantem centralizado, apenas desce um pouco da borda superior
        rectTransform.anchoredPosition = new Vector2(0f, -40f);
    }

    public void alteraTempo(float tempo)
    {
        textoTempo.text = tempo > 0 ? ((int)tempo).ToString() : "0";
    }
}
