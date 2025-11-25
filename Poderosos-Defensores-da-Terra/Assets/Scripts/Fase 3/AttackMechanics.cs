using UnityEngine;
using System.Collections;

public class AttackMechanics : MonoBehaviour
{
    private Animator anim;

    public GameObject attackPrefab;   // Prefab do projetil
    public Transform pontoAtaque;     // De onde sai o ataque
    public float velocidadeAtaque = 10f;
    public Transform alvo;            // Referencia ao aviao

    public float cadencia = 1f;
    float tempo;

    public AudioSource attackSound; // som do ataque

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        tempo += Time.deltaTime;

        if (tempo >= cadencia && alvo != null)
        {
            tempo = 0f;

            attackSound.time = 0.6f;
            attackSound.Play();

            anim.SetTrigger("Ataque");
            RealizaDisparo();

        }
    }

    void RealizaDisparo()
    {
        // Calcula a direção do inimigo até o avião
        Vector3 direcao = (alvo.position - pontoAtaque.position).normalized;

        // Faz o projétil apontar nessa direção
        Quaternion rotacao = Quaternion.LookRotation(direcao);

        // Cria o projétil
        GameObject tiro = Instantiate(attackPrefab, pontoAtaque.position, rotacao);

        // Move o projétil nessa direção
        Rigidbody rb = tiro.GetComponent<Rigidbody>();
        rb.linearVelocity = direcao * velocidadeAtaque;
    }
}