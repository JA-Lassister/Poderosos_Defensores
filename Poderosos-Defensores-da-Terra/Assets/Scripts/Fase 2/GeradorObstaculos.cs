using UnityEngine;

public class GeradorObstaculos : MonoBehaviour
{
    public GerenciadorObstaculos gerenciador;
    public Alerta[] alertas = new Alerta[4];
    
    private void OnTriggerEnter(Collider c)
    {
        // Ignora colisões com outros objetos
        if (!c.CompareTag("Player")) return;
        
        // Ao detectar colisão, atualiza os obstáculos presentes na cena e recebe como
        // retorno uma máscara indicando quais posições da estrada estão ocupadas
        bool[] obst = gerenciador.AtualizarObstaculos();
        
        // Exibe alertas nas posições ocupadas por obstáculos
        for(int i = 0; i < 4; i++)
            if(obst[i]) alertas[i].gameObject.SetActive(true);
    }
}
