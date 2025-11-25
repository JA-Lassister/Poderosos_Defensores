using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorCarro : MonoBehaviour
{
    public ParticleSystem explosao;
    public ContadorFase contador;
    public ControladorEstrada estrada;
    public ControladorInimigo inimigo;
    public float velocidadeRotacao;
    public float tempoImunidade;
    public int vidas;
    
    private bool _invulneravel;
    private bool _parado;
    private Animator _animator;
    private InputAction _move;

    private void Start()
    {
        _move = InputSystem.actions.FindAction("MoveHorizontal");
        _animator = transform.Find("Veiculo").GetComponent<Animator>();
    }

    // Escuta as teclas "A" e "D" para aplicar rotação ao veículo
    private void FixedUpdate()
    {
        if (_parado) return;
        
        var v3 = new Vector3(- _move.ReadValue<float>(), 0.0f, 1.0f); 
        var qTo = Quaternion.LookRotation(v3);
        
        transform.rotation = Quaternion.Slerp(transform.rotation, qTo, velocidadeRotacao * Time.deltaTime);
    }

    // Aplicar dano ao jogador
    // Retorna verdadeiro se o dano for aplicado e falso caso contrário (jogador invulnerável)
    public bool Danificar() 
    {
        if (_invulneravel) return false;
        
        // Inimigo se aproxima a cada dano recebido
        inimigo.Aproximar();

        // Decrementa a vida e verifica se chegou a 0
        if (--vidas > 0) {
            
            // Se o jogador ainda tem vidas, torna-o invulnerável
            _invulneravel = true;
            
            // Ativa a animação de recebimento de dano
            _animator.SetTrigger("Crash");
            
            // Restitui a vulnerabilidade ao jogador no tempo estipulado
            Invoke(nameof(LiberarDano), tempoImunidade); 
        }
        // Se o jogador não tem mais vidas, mate-o
        else Morrer();

        return true;
    }
    
    public void LiberarDano() {
        // Torna o jogador sensível a danos novamente
        _invulneravel = false;
        
        // Encerra a animação de dano
        _animator.SetTrigger("Recover");
    }

    private void Morrer() {
        
        // Faz o jogador parar de se mover
        Parar();
        
        // Inicia "animação" de explosão
        explosao.Play();
        
        // Faz o veículo desaparecer em 1 segundo
        Invoke(nameof(Desaparecer), 1f);
        
        // Inimigo continua a correr
        inimigo.Correr();
        
        // Encerra a fase por derrota
        contador.EncerrarDerrota();
    }
    
    // Faz a parte visível do veículo desaparecer
    private void Desaparecer() => transform.Find("Veiculo").gameObject.SetActive(false);

    // Atira no inimigo
    public void Atirar()
    {
        // Para antes de atirar
        Parar();

        // Dispara o tiro na direção do inimigo, levemente inclinado para cima
        transform.Find("Disparo")
            .GetComponent<Tiro>()
            .Disparar(inimigo.transform.position - transform.position + 2 * Vector3.up);
    }
    
    private void Parar()
    {
        _parado = true;
        
        // O efeito de parada do personagem depende da esteira da estrada parar de mover
        estrada.enabled = false;
    }
}
