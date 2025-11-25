using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Barra de vida
    public LifeBarEnemy barraVidaInimigo;
    public LifeTime barraTempo;
    public LifeBarAirplane barraVidaAviao;

    public Restart restart;

    public EnemyMover movimentacaoInimigo;

    public float vidaInicialInimigo = 100.0f;
    private float vidaInimigo;
    public float danoTiro = 2.0f;

    public float vidaInicialAviao = 100.0f;
    private float vidaAviao;
    public float danoAtaque = 20.0f;
    public ParticleSystem explosao;
    public AudioSource somExplosao;
    public AudioSource somDanoRecebido;

    public float tempoEmSegundos = 1;
    float tempoFase;

    public GameObject enemy;
    public GameObject airplane;

    private Animator anim;
    public float tempoMorteDestruicao = 1f;

    public static GameManager Instance;

    bool perdeu = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        barraVidaInimigo.slider.maxValue = vidaInicialInimigo;
        barraVidaInimigo.alteraVida(vidaInicialInimigo);
        vidaInimigo = vidaInicialInimigo;

        tempoFase = tempoEmSegundos;
        barraTempo.alteraTempo(tempoFase);

        barraVidaAviao.slider.maxValue = vidaInicialAviao;
        barraVidaAviao.alteraVida(vidaInicialAviao);
        vidaAviao = vidaInicialAviao;

        anim = enemy.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        processaTempo();
    }

    public void processaDano()
    {
        if (vidaInimigo > 0)
        {
            vidaInimigo -= danoTiro;
            barraVidaInimigo.alteraVida(vidaInimigo);
        }
        else
        {   
            barraVidaInimigo.gameObject.SetActive(false);
            barraTempo.gameObject.SetActive(false);

            anim.SetTrigger("Morte");
            movimentacaoInimigo.inimigoMorreu();           
            
            StartCoroutine(EsperaParaDestruir(tempoMorteDestruicao));
            if(!perdeu)
                StartCoroutine(SelecaoFase.CarregarFase(4,3f));
        }
    }

    IEnumerator EsperaParaDestruir(float tempoDeEspera)
    {
        yield return new WaitForSeconds(tempoDeEspera);

        if (enemy != null)
        {
            Destroy(enemy);
        }
    }

    public void processaTempo()
    {
        if (tempoFase > 0)
        {
            tempoFase -= Time.deltaTime;
            barraTempo.alteraTempo(tempoFase);

            if (movimentacaoInimigo != null)
                movimentacaoInimigo.movimentacaoNormal();
        }
        else
        {
            tempoFase -= Time.deltaTime;
            if (movimentacaoInimigo != null)
            {
                movimentacaoInimigo.inimigoFugiu();
                restart.ExibirMensagem();
            }

            if (tempoFase < -1.5 && enemy != null)
            {
                enemy = null;
                barraVidaInimigo.gameObject.SetActive(false);
                barraTempo.gameObject.SetActive(false);
            }
        }
    }

    public void processaAtaque()
    {
        vidaAviao -= danoAtaque;
        if (vidaAviao > 0)
        {
            barraVidaAviao.alteraVida(vidaAviao);
            somDanoRecebido.Play();
        }
        else
        {
            somExplosao.Play();
            explosao.Play();
            Destroy(airplane);
            barraVidaAviao.gameObject.SetActive(false);
            restart.ExibirMensagem();
            perdeu = true;
        }
    }
}
