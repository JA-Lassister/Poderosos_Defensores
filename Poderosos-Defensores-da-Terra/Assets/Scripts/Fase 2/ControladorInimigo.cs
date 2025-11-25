using UnityEngine;

public class ControladorInimigo : MonoBehaviour
{
    public ControladorCarro veiculo;
    public float velocidadeRotacao;
    public float velocidadeLateral;
    public float aproximacao;

    private Vector3 _posOriginal;
    private Vector3 _vetorAproximacao;
    private bool _correr;
    private bool _morto;

    private void Start()
    {
        // Salva a posição inicial do inimigo
        _posOriginal = transform.position;
        
        // Determina o quanto o inimigo deve se aproximar a cada chamada do método Aproximar()
        _vetorAproximacao = aproximacao * Vector3.forward;
    } 

    private void FixedUpdate()
    {
        // Ignora quaisquer movimentos caso o inimigo esteja morto
        if (_morto) return;
        
        // Calcula o quanto o jogador de distanciou do inimigo para decidir
        // o quanto este deve se mover em direção a ele
        var deltaV = veiculo.transform.position - transform.position;
        var movX = deltaV.x * velocidadeLateral * Time.deltaTime;
        
        // Caso o inimigo esteja no estado "Corrida", desloca-o para a frente 
        if (_correr) _posOriginal.z += 1f;
        // Caso contrário, atualiza sua rotação
        else Rotacionar(Quaternion.LookRotation(deltaV));
        
        // Atualiza a posição do inimigo
        transform.position = new Vector3(transform.position.x + movX, _posOriginal.y, _posOriginal.z);
    }

    // Rotaciona o inimigo na direção do jogador
    private void Rotacionar(Quaternion qTo) =>
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, velocidadeRotacao * Time.deltaTime);

    // Inimigo se aproxima do jogador a uma quantia determinada
    public void Aproximar() => _posOriginal += _vetorAproximacao;

    // Coloca o inimigo no estado "Corrida"
    public void Correr() => _correr = true;

    public void Morrer()
    {
        // Coloca o inimigo no estado "Morto"
        _morto = true;
        
        // Inicia a animação de morte
        GetComponent<Animator>().SetTrigger("Die");
    }
}
