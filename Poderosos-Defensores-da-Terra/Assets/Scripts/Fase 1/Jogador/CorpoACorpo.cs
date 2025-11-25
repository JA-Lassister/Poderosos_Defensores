using UnityEngine;
using System.Collections;

public class CorpoACorpo : MonoBehaviour
{
    [Header("Configurações do Ataque")]
    public float alcance = 2f;               
    public float dano = 25f;                 
    public float tempoEntreAtaques = 0.8f;   
    public Transform pontoAtaque;            
    public LayerMask camadaInimigos;         

    [Header("Animação e Som")]
    public Animator anim;
    public AudioSource somAtaque;

    private bool podeAtacar = true;

    void Update()
    {
        // Agora usa o BOTÃO ESQUERDO (0) para atacar
        if (Input.GetMouseButtonDown(0) && podeAtacar)
        {
            StartCoroutine(Atacar());
        }
    }

    private IEnumerator Atacar()
    {
        podeAtacar = false;

        // 🔥 Aciona o Trigger para a animação
        if (anim != null)
            anim.SetTrigger("Bater");

        // Som opcional
        if (somAtaque != null)
            somAtaque.Play();

        // Delay sincronizado com o golpe
        float delayDano = 0.3f;
        yield return new WaitForSeconds(delayDano);
        anim.SetBool("Bater2", true);

        // 🎯 Detecta inimigos atingidos
        Collider[] atingidos = Physics.OverlapSphere(
            pontoAtaque.position,
            alcance,
            camadaInimigos
        );

        foreach (Collider inimigo in atingidos)
        {
            VidaInimigo vida = inimigo.GetComponent<VidaInimigo>();
            if (vida != null)
                vida.ReceberDano(dano);
        }

        // Tempo até poder atacar novamente
        yield return new WaitForSeconds(tempoEntreAtaques - delayDano);

        podeAtacar = true;
    }

    // Gizmo para visualizar o alcance
    void OnDrawGizmosSelected()
    {
        if (pontoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pontoAtaque.position, alcance);
    }
}