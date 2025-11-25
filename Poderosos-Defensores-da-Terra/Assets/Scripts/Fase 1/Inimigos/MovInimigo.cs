using UnityEngine;

public class MovInimigo : MonoBehaviour
{
    [Header("Referências")]
    public Transform player;
    public Animator anim;

    [Header("Movimentação")]
    public float speed = 3f;
    public float stopDistance = 2f;

    [Header("Campo de Visão Esférico")]
    public float distanciaVisao = 10f;  // Raio de detecção

    private bool jogadorVisivel = false;

    void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        // Agora a detecção é apenas pela distância (esfera)
        jogadorVisivel = VerificarCampoDeVisaoEsferico();

        if (!jogadorVisivel)
        {
            anim.SetBool("Andando", false);
            return;
        }

        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia > stopDistance)
        {
            MoverAtePlayer();
        }
        else
        {
            anim.SetBool("Andando", false);
        }
    }

    bool VerificarCampoDeVisaoEsferico()
    {
        float distancia = Vector3.Distance(transform.position, player.position);

        // O player está dentro do raio de visão?
        return distancia <= distanciaVisao;
    }

    void MoverAtePlayer()
    {
        // Olha para o player, mas mantendo a mesma altura
        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookPos);

        // Anda para frente
        transform.position += transform.forward * speed * Time.deltaTime;

        anim.SetBool("Andando", true);
    }
}
