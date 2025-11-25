using UnityEngine;

public class ControladorMureta : MonoBehaviour
{
    // Informa pelo editor se a mureta está na direita ou na esquerda da estrada
    public enum Direcao { Direita = -1, Esquerda = 1 }
    
    public ControladorEstrada estrada;
    public Direcao direcao;

    // Instrui o controlador da estrada a bloquear os movimentos na direção da mureta após
    // detectar colisão
    private void OnTriggerEnter(Collider c) => estrada.BloquearMovimentos((int) direcao);

    // Instrui o controlador da estrada a liberar os movimentos após término da colisão
    private void OnTriggerExit(Collider c) => estrada.LiberarMovimentos();
}
