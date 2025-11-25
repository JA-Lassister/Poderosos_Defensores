using UnityEngine;

public class TrocaChao : MonoBehaviour
{
    public Transform chaoComplementar;
    public ControladorEstrada estrada;
    
    private void OnTriggerEnter(Collider c)
    {
        // Associado a um chão
        // Ao detectar colisão com o jogador, instrui o controlador da estrada a
        // colocar o chão complementar atrás de seu chão associado
        if (!c.CompareTag("Player")) return;
        estrada.MoverChao(chaoComplementar);
    }
}
