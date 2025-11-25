using UnityEngine.InputSystem;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float tempoDeVida = 10f;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Airplane")
        {
            // Função para processar dano
            GameManager.Instance.processaAtaque();
            Destroy(gameObject);
        } else if (other.gameObject.name == "Enemy")
        {
            // Nada
        } 

        
  
    }
}
