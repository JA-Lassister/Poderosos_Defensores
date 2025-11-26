using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerFase1 : MonoBehaviour
{
    public static GameManagerFase1 instancia;

    public AlteraContagemInimigos qtdInimigosTxt;

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
        qtdInimigosTxt.alteraQuantidade(inimigosRestantes);
    }

    // Chamado pelos inimigos quando morrem
    public void InimigoMorreu()
    {
        inimigosRestantes--;
        qtdInimigosTxt.alteraQuantidade(inimigosRestantes);

        if (inimigosRestantes == 0)
        {
            StartCoroutine(SelecaoFase.CarregarFase(2,3f));
        }
    }


}