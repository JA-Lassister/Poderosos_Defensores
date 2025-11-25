using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerFase1 : MonoBehaviour
{
    public static GameManagerFase1 instancia;

    [Header("Configurações da Fase")]
    public int totalInimigos = 0;   // Defina no Inspector
    private int inimigosRestantes;

    void Awake()
    {
        // Singleton simples
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        inimigosRestantes = totalInimigos;
    }

    // Chamado pelos inimigos quando morrem
    public void InimigoMorreu()
    {
        inimigosRestantes--;

        Debug.Log("Inimigos restantes: " + inimigosRestantes);

        if (inimigosRestantes <= 0)
        {
            AvancarParaProximaFase();
        }
    }

    void AvancarParaProximaFase()
    {
        SceneManager.LoadScene("Fase2");
    }
}