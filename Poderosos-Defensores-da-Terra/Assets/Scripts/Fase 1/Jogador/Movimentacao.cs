/*using UnityEngine;
using UnityEngine.Diagnostics;

public class Movimentacao : MonoBehaviour
{
    private Transform player;
    public float velocidade = 1f;
    private CharacterController controller;
    private Animator animator;
    public float jumpForce = 10f;
    public float gravidade = -9f;
    private bool noChao;
    [SerializeField]private Transform pe;
    [SerializeField]private LayerMask layer;
    private float forcaY;
    
    void Start()
    {
        player = GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool w = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool s = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool a = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool d = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            controller.Move(player.transform.forward * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            
            controller.Move(-player.transform.forward * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
           
           controller.Move(-player.transform.right * velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            controller.Move(player.transform.right * velocidade * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            forcaY = jumpForce;
        }
        if (forcaY > gravidade)
        {
            forcaY +=  gravidade *  Time.deltaTime; 
        }
        controller.Move(new Vector3(0, forcaY, 0)*Time.deltaTime);
        
        
        animator.SetBool("Frente", w && !s);
        animator.SetBool("Tras", s && !w);
        animator.SetBool("Esq", a && !d);
        animator.SetBool("Dir", d && !a);
        noChao = Physics.CheckSphere(pe.position,0.3f,layer);
    }
}
*/
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class Movimentacao8DirecoesCorrida : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidadeAndar = 3f;
    public float velocidadeCorrer = 6f;
    public float jumpForce = 8f;
    public float gravidade = -9.81f;
    public Transform pe;
    public LayerMask layerChao;

    private CharacterController controller;
    private Animator animator;
    private AudioSource audioSource;
    private Vector3 direcao;
    private float forcaY;
    private bool noChao;

    [Header("Som")]
    public AudioClip somPasso;
    public float passoIntervalo = 0.4f;
    private float tempoProximoPasso = 0f;

    [Header("Corrida")]
    public float tempoDuploToque = 0.3f; // tempo máximo entre dois toques em W
    private float ultimoToqueW = -1f;
    private bool correndo = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // --- Entrada de movimento ---
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Detecta duplo toque em W
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Time.time - ultimoToqueW < tempoDuploToque)
            {
                correndo = true; // Segundo toque rápido = correr
                animator.SetBool("correndo", true);
            }
            ultimoToqueW = Time.time;
            animator.SetBool("correndo", false);
        }

        // Se soltar W, para de correr
        if (Input.GetKeyUp(KeyCode.W))
        {
            correndo = false;
        }

        // Direção e normalização
        Vector3 movimento = transform.forward * v + transform.right * h;
        movimento.Normalize();

        // --- Pulo ---
        noChao = Physics.CheckSphere(pe.position, 0.3f, layerChao);
        if (noChao && forcaY < 0)
            forcaY = -2f;

        if (Input.GetKeyDown(KeyCode.Space) && noChao)
            forcaY = jumpForce;

        forcaY += gravidade * Time.deltaTime;

        // --- Define velocidade ---
        float velocidadeAtual = (correndo && v > 0) ? velocidadeCorrer : velocidadeAndar;
        Vector3 movimentoFinal = movimento * velocidadeAtual + Vector3.up * forcaY;

        // --- Move o personagem ---
        controller.Move(movimentoFinal * Time.deltaTime);

        // --- Atualiza animações ---
        AtualizarAnimacoes(h, v);

        // --- Som de passos ---
        if (noChao && movimento.magnitude > 0.1f)
        {
            if (Time.time >= tempoProximoPasso)
            {
                if (somPasso != null)
                    audioSource.PlayOneShot(somPasso);
                tempoProximoPasso = Time.time + (correndo ? passoIntervalo / 1.5f : passoIntervalo);
            }
        }
    }

    void AtualizarAnimacoes(float h, float v)
    {
        // Reseta os bools
        animator.SetBool("Andando", false);
        animator.SetBool("Correndo", false);
        animator.SetBool("Frente", false);
        animator.SetBool("Tras", false);
        animator.SetBool("Esq", false);
        animator.SetBool("Dir", false);
        animator.SetBool("NE", false);
        animator.SetBool("SE", false);
        animator.SetBool("SO", false);
        animator.SetBool("NO", false);

        // Direções
        if (v > 0 && h == 0) animator.SetBool("Frente", true);
        else if (v < 0 && h == 0) animator.SetBool("Tras", true);
        else if (h > 0 && v == 0) animator.SetBool("Dir", true);
        else if (h < 0 && v == 0) animator.SetBool("Esq", true);
        else if (v > 0 && h > 0) animator.SetBool("NE", true);
        else if (v < 0 && h > 0) animator.SetBool("SE", true);
        else if (v < 0 && h < 0) animator.SetBool("SO", true);
        else if (v > 0 && h < 0) animator.SetBool("NO", true);

        // Estado de movimento
        if (v != 0 || h != 0)
        {
            if (correndo && v > 0)
                animator.SetBool("Correndo", true);
            else
                animator.SetBool("Andando", true);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (pe != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(pe.position, 0.3f);
        }
    }
}

