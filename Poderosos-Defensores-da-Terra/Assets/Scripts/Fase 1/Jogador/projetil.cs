using UnityEngine;

public class ProjetilVisual : MonoBehaviour
{
    private Vector3 direcao;
    private float velocidade;
    private float tempoDeVida = 2f;

    public void Iniciar(Vector3 dir, float vel)
    {
        direcao = dir;
        velocidade = vel;
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.position += direcao * velocidade * Time.deltaTime;
    }
}

   