using UnityEngine;
using TMPro;

public class AlteraContagemInimigos : MonoBehaviour
{
    public TMP_Text textoTempo;

    RectTransform rectTransform;

    void Start()
    {
        // Pega o RectTransform do proprio texto
        rectTransform = textoTempo.GetComponent<RectTransform>();

        // Centraliza no topo
        rectTransform.anchorMin = new Vector2(1, 1);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.pivot = new Vector2(1, 1);

        rectTransform.anchoredPosition = new Vector2(-20f, -20f);
    }

    public void alteraQuantidade(float qtdInimigos)
    {
        textoTempo.text = qtdInimigos > 0 ? "inimigos:" + ((int)qtdInimigos).ToString() : "Vitoria";
    }
}
