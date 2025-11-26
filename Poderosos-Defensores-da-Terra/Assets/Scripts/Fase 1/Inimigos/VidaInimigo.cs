using UnityEngine;

public class VidaInimigo : MonoBehaviour
{
    [Header("Configuração de Vida")]
    public float vida = 100f;
    public Animator animator;

    [Header("Campo de Visão")]
    public Transform player;            // Referência ao player
    public float distanciaVisao = 10f;  // Raio de visão
    public float anguloVisao = 90f;     // Ângulo de visão (45° para cada lado)
    public float velocidadeGiro = 5f;   // Velocidade que o inimigo vira para o player

    public bool morto = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        // Caso o player não tenha sido arrastado no inspector, tenta achar automaticamente
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (PlayerDentroDoCampoDeVisao() && !morto)
        {
            OlharParaPlayer();
        } 
    }

    bool PlayerDentroDoCampoDeVisao()
    {
        if (player == null) return false;

        Vector3 direcao = player.position - transform.position;
        float distancia = direcao.magnitude;

        // Verifica distância
        if (distancia > distanciaVisao)
            return false;

        // Verifica ângulo
        float angulo = Vector3.Angle(transform.forward, direcao);
        if (angulo > anguloVisao * 0.5f)
            return false;

        return true;
    }

    void OlharParaPlayer()
    {
        Vector3 direcao = player.position - transform.position;
        direcao.y = 0f; // Mantém o inimigo reto

        Quaternion rotacaoDesejada = Quaternion.LookRotation(direcao);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotacaoDesejada, Time.deltaTime * velocidadeGiro);
    }

    public void ReceberDano(float dano)
    {
        vida -= dano;

        if (vida <= 0f && !morto)
        {
            morto = true;

            animator.SetTrigger("Morte");

            // ⚠️ Notifica o GameManager
            GameManagerFase1.instancia.InimigoMorreu();

            Destroy(gameObject, 0.5f);
        }
    }

}
