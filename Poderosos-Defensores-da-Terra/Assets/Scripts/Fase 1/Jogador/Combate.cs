using UnityEngine;

public class Combate : MonoBehaviour
{
    [Header("Referências")]
    public CorpoACorpo corpoACorpo;
    public Disparar disparar;
    public GameObject laminaLaser;

    private Animator animator;
    private bool estaEmModoDisparo = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // pega o animator no player

        if (corpoACorpo == null)
            corpoACorpo = GetComponent<CorpoACorpo>();
        if (disparar == null)
            disparar = GetComponent<Disparar>();

        AlternarModo(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            AlternarModo(true);
        }

        if (Input.GetMouseButtonUp(1))
        {
            AlternarModo(false);
        }
    }

    void AlternarModo(bool modoDisparo)
    {
        estaEmModoDisparo = modoDisparo;

        // habilita/desabilita scripts
        corpoACorpo.enabled = !modoDisparo;
        disparar.enabled = modoDisparo;

        disparar.AtualizarZoomInstantaneo(modoDisparo);

        // ativa/desativa lâmina
        laminaLaser.SetActive(!modoDisparo);

        // envia para o animator
        if (animator != null)
            animator.SetBool("Atirar2", modoDisparo);
            animator.SetTrigger("Atirar");

        Debug.Log(modoDisparo ? "Modo Disparo Ativado" : "Modo Corpo a Corpo Ativado");
    }
}