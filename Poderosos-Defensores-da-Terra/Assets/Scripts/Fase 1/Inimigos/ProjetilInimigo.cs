using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    private Vector3 direcao;
    private float velocidade;
    private float dano;
    private string tagAlvo;

    public void Iniciar(Vector3 dir, float vel, float danoCausado, string alvo)
    {
        direcao = dir;
        velocidade = vel;
        dano = danoCausado;
        tagAlvo = alvo;
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += direcao * velocidade * Time.deltaTime;
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.CompareTag(tagAlvo))
        {
            VidaJogador vida = outro.GetComponent<VidaJogador>();
            if (vida != null)
            {
                vida.ReceberDano(dano);
            }
            Destroy(gameObject);
        }
    }
}