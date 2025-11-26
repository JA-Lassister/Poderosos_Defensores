using UnityEngine;

public class InimigoAtaque : MonoBehaviour
{
    public Transform jogador;          // Referência ao transform do player
    public GameObject projetilPrefab;  // Prefab do projétil para ataque à distância
    public Transform pontoDisparo;     // Ponto onde o inimigo dispara (um empty à frente dele)

    [Header("Ataque Corpo a Corpo")]
    public float alcanceMelee = 2f;    // Distância máxima para ataque corpo a corpo
    public float danoMelee = 15f;      // Dano do ataque corpo a corpo
    public float tempoEntreAtaquesMelee = 1.5f;
    
    [Header("Ataque à Distância")]
    public float alcanceTiro = 15f;    // Distância máxima para atirar
    public float danoTiro = 10f;
    public float velocidadeProjetil = 20f;
    public float cadenciaTiro = 2f;

    private float proximoAtaqueMelee = 0f;
    private float proximoTiro = 0f;

    private VidaInimigo scriptVida;

    void Start()
    {
        scriptVida = GetComponent<VidaInimigo>();
    }

    void Update()
    {
        if (jogador == null) return;

        float distancia = Vector3.Distance(transform.position, jogador.position);

        // --- ATAQUE CORPO A CORPO ---
        if (distancia <= alcanceMelee && Time.time >= proximoAtaqueMelee && !scriptVida.morto)
        {
            AtaqueCorpoACorpo();
            proximoAtaqueMelee = Time.time + tempoEntreAtaquesMelee;
        }
        // --- ATAQUE À DISTÂNCIA ---
        else if (distancia <= alcanceTiro && Time.time >= proximoTiro && !scriptVida.morto)
        {
            Atirar();
            proximoTiro = Time.time + cadenciaTiro;
        }
    }

    void AtaqueCorpoACorpo()
    {
        if (jogador != null)
        {
            VidaJogador vida = jogador.GetComponent<VidaJogador>();
            if (vida != null)
            {
                vida.ReceberDano(danoMelee);
            }
        }
    }

    void Atirar()
    {
        if (projetilPrefab == null || pontoDisparo == null) return;

        // Instancia o projétil e dispara em direção ao jogador
        GameObject projetil = Instantiate(projetilPrefab, pontoDisparo.position, Quaternion.identity);
        Vector3 direcao = (jogador.position - pontoDisparo.position).normalized;

        // Movimento simples sem Rigidbody
        projetil.AddComponent<ProjetilInimigo>().Iniciar(direcao, velocidadeProjetil, danoTiro, "Player");
    }
}
