using UnityEngine;

public class InimigoCorpoACorpo : MonoBehaviour
{
    [Header("Configurações de ataque")]
    public float dano = 10f;
    public float intervaloAtaque = 1f;
    public float alcanceAtaque = 2f;
    private float proximoAtaque = 0f;

    [Header("Configurações do alvo")]
    public Transform alvo;

    [Header("Animações")]
    public Animator anim;
    public string parametroAtacar = "Atacando";
    public string parametroAndar = "Andando";

    private bool estaAtacando = false;


    private VidaInimigo scriptVida;

    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();

        scriptVida = GetComponent<VidaInimigo>();
    }

    void Update()
    {
        if (alvo == null) return;

        float distancia = Vector3.Distance(transform.position, alvo.position);

        // Controle de animação de andar (opcional)
        if (distancia > alcanceAtaque)
        {
            anim.SetBool(parametroAndar, true);
            estaAtacando = false;
        }
        else
        {
            anim.SetBool(parametroAndar, false);
        }

        // Controle de ataque
        if (distancia <= alcanceAtaque && Time.time >= proximoAtaque && !scriptVida.morto)
        {
            Atacar();
            proximoAtaque = Time.time + intervaloAtaque;
        }
    }

    void Atacar()
    {
        if (!estaAtacando)
        {
            // Aciona animação de ataque
            anim.SetBool(parametroAtacar, true);
            estaAtacando = true;

            // Reseta a animação depois do tempo de ataque
            Invoke(nameof(PararAnimacaoAtaque), 0.5f); // Ajuste o delay para o tempo real da animação
        }

        // Aplica dano ao jogador
        VidaJogador vida = alvo.GetComponent<VidaJogador>();
        if (vida != null)
        {
            vida.ReceberDano(dano);
        }
    }

    void PararAnimacaoAtaque()
    {
        anim.SetBool(parametroAtacar, false);
        estaAtacando = false;
    }
}
