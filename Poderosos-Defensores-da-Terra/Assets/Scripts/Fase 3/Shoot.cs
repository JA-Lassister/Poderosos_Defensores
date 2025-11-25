using UnityEngine.InputSystem;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float tempoDeVida = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, tempoDeVida);     
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Enemy") 
        {
            GameManager.Instance.processaDano();
            // Destrói o projétil imediatamente após a colisão.
            Destroy(gameObject);
        }

        
    }
}
