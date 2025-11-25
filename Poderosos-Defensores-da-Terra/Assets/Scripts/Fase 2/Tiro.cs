using System;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float forca;

    // O projétil inicia invisível
    private void Start() => gameObject.SetActive(false);
    
    private void OnTriggerEnter(Collider c)
    {
        if (!c.CompareTag("Inimigo")) return;

        // Ao atingir o inimigo, mate-o e destrua o projétil
        c.GetComponent<ControladorInimigo>().Morrer();
        Destroy(gameObject);
    }

    public void Disparar(Vector3 direcao)
    {
        // Exibe o projétil.
        gameObject.SetActive(true);
        
        // Dispara o projétil na direção fornecida pelo parâmetro aplicando força ao seu Rigidbody
        GetComponent<Rigidbody>().AddForce(forca * direcao, ForceMode.Impulse);
    }
}
