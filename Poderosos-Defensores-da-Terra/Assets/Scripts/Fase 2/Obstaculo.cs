using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public ParticleSystem explosao;
    public ControladorCarro veiculo;
    public int largura;
    
    private float y;
    private GameObject _filho;

    private void Awake() 
    {
        y = transform.localPosition.y;
        _filho = transform.Find("Obstaculo").gameObject;
    }

    // Posiciona o obstáculo usando a coordenada x passada como parâmetro
    // e a coordenada y específica do obstáculo. (Coordenadas locais)
    public void Posicionar(float x) => transform.localPosition = new Vector3(x, y, 0.0f);
    
    private void OnTriggerEnter(Collider c)
    {
        // Se a colisão for com o veículo, danifique-o
        if (c.CompareTag("Player"))
            // Tenta danificar o veículo. Caso não seja possível, retorne imediatamente
            if (!veiculo.Danificar())
                return;

        // Ativa "animação" de explosão
        explosao.Play();
        
        // Faz a parte visível do obstáculo desaparecer em 1 segundo
        Invoke(nameof(Desaparecer), 1f);
    }

    // Faz a parte física do obstáculo desaparecer (efeito de fumaça e/ou explosão continuam)
    private void Desaparecer() => _filho.SetActive(false);

    // Faz o obstáculo aparecer completamente (incluindo fumaça e/ou explosão)
    public void Habilitar()
    {
        _filho.SetActive(true);
        gameObject.SetActive(true);
    }

    // Faz o obstáculo desaparecer completamente (incluindo fumaça e/ou explosão)
    public void Desabilitar() => gameObject.SetActive(false);
}