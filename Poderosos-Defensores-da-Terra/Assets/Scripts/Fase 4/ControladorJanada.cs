using UnityEngine;

public enum EstadoJanada { PERSEGUIR, SUBMERSO, ACELERACAO }

public class ControladorJanada : Controlador
{
    public ControladorBarco jogador;
    public Animator animator;
    
    public HealthBar_Fase4 vidaInimigo;
    
    public float velocidadeRotacao = 6f;
    public float velocidadeMovimentacaoNormal = 1f;
    public float velocidadeMovimentacaoDash = 2f;
    
    public float danoInimigo = 5f;

    [Header("Tempo entre ataques")]
    public float tempoMin;
    public float tempoMax;
    public float tempoMaxDash;

    [Header("Temporização da submersao")] 
    public float tempoSubmersao;
    public float profundidadeSubmersao;
    public float tempoEmergir;

    private float _intervaloTempo;
    private float _yInicial;
    private EstadoJanada _estado;

    private void Awake()
    {
        _yInicial = transform.position.y;
        _intervaloTempo = tempoMax - tempoMin;
        _estado = EstadoJanada.PERSEGUIR;
        EscolherProximoAtaque();
    }

    private void FixedUpdate()
    {
        switch (_estado)
        {
            case EstadoJanada.PERSEGUIR:
                RotacionarAoJogador(transform.position - jogador.transform.position); 
                PerseguirJogador(transform.position - jogador.transform.position, velocidadeMovimentacaoNormal); 
                break;
            case EstadoJanada.ACELERACAO:
                RotacionarAoJogador(transform.position - jogador.transform.position); 
                PerseguirJogador(transform.position - jogador.transform.position, velocidadeMovimentacaoDash);
                break;
        }
    }

    private void RotacionarAoJogador(Vector3 dir)
    {
        Quaternion target = Quaternion.LookRotation(dir);
        transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * velocidadeRotacao);
    }

    private void PerseguirJogador(Vector3 dir, float velocidade)
    {
        Vector3 v = transform.position - velocidade * Time.deltaTime * dir;
        transform.position = ParedesInvisiveis.PosicaoAjustada(v);
    }

    private void EscolherProximoAtaque()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                Invoke(nameof(IniciarDash), tempoMin + Random.value * _intervaloTempo);
                break;
            case 1:
                Invoke(nameof(IniciarSubmersao), tempoMin + Random.value * _intervaloTempo);
                break;
        }
    }

    private void IniciarDash()
    {
        animator.SetTrigger("Dash");
        _estado = EstadoJanada.ACELERACAO;
        Invoke(nameof(EncerrarDash), tempoMaxDash);
    }

    private void IniciarSubmersao()
    {
        animator.SetTrigger("Submerge");
        _estado = EstadoJanada.SUBMERSO;
        Invoke(nameof(Emergir), tempoSubmersao);
    }

    private void Emergir()
    {
        transform.position = new Vector3(jogador.transform.position.x,
            - profundidadeSubmersao,
            jogador.transform.position.z
            );
        animator.SetTrigger("Emerge");
        Invoke(nameof(RetornarAoEstadoInicial), tempoEmergir);
    }

    private void RetornarAoEstadoInicial()
    {
        transform.position = new Vector3(transform.position.x, _yInicial, transform.position.z);
        _estado = EstadoJanada.PERSEGUIR;
        EscolherProximoAtaque();
    }

    private void EncerrarDash()
    {
        animator.SetTrigger("Calm");
        _estado = EstadoJanada.PERSEGUIR;
        EscolherProximoAtaque();
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player")) {
            if (Vector3.Dot(transform.forward, transform.position - jogador.transform.position) > 0f)
                animator.SetTrigger("Bite");

            Invoke(nameof(EfetivarDano), 1f);
        }
        else if (c.CompareTag("Bala")) {
            vidaInimigo.DecreaseHealthPercent(danoInimigo / 100);
        }
    }

    private void EfetivarDano() => jogador.EfetivarDano();

    public override void Morrer()
    {
        animator.SetTrigger("Submerge");
        StartCoroutine(SelecaoFase.CarregarFase(5, 3f));
    }
}
