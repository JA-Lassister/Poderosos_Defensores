using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
    [Header("Configurações de Vida")]
    public float vidaMax = 100f;
    public float vidaAtual;

    [Header("UI")]
    public Slider barraDeVida;         // O Slider da UI
    public Image fillImage;            // A parte "Fill" do slider

    [Header("Cores da Barra")]
    public Color corVerde = new Color(0f, 1f, 0f);
    public Color corAmarelo = new Color(1f, 0.92f, 0.016f);
    public Color corLaranja = new Color(1f, 0.6f, 0f);
    public Color corVermelho = new Color(1f, 0f, 0f);

    void Start()
    {
        vidaAtual = vidaMax;

        barraDeVida.maxValue = vidaMax;
        barraDeVida.value = vidaAtual;

        AtualizarCor();
    }

    public void ReceberDano(float dano)
    {
        vidaAtual -= dano;
        if (vidaAtual < 0) vidaAtual = 0;

        barraDeVida.value = vidaAtual;
        AtualizarCor();

        if (vidaAtual <= 0)
            Morrer();
    }

    public void Curar(float valor)
    {
        vidaAtual += valor;
        if (vidaAtual > vidaMax) vidaAtual = vidaMax;

        barraDeVida.value = vidaAtual;
        AtualizarCor();
    }

    void AtualizarCor()
    {
        float porcentagem = vidaAtual / vidaMax;

        if (porcentagem >= 0.75f)
        {
            fillImage.color = corVerde;      // 75% — 100%
        }
        else if (porcentagem >= 0.50f)
        {
            fillImage.color = corAmarelo;    // 50% — 74%
        }
        else if (porcentagem >= 0.25f)
        {
            fillImage.color = corLaranja;    // 25% — 49%
        }
        else
        {
            fillImage.color = corVermelho;   // 0% — 24%
        }
    }

    void Morrer()
    {
        Debug.Log("Player morreu!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Coloque respawn, animação, etc...
    }
}