using UnityEngine;
using UnityEngine.InputSystem;

public class RobotController : MonoBehaviour
{
    [Header("Referências")]
    public Animator animSuperior;
    public Animator animInferior;

    private PlayerInput input;
    private InputAction moveAction;

    private bool isTilting = false;
    private int tiltDirection = 0;
    private bool punchRightNext = true;

    private bool hasDied = false;

    public System.Action OnPunchSignal;

    public int GetLeanState()
    {
        if (isTilting) return tiltDirection;
        return 0;
    }

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions["Move"];
    }

    void Start()
    {
        // 🔥 REGISTRA A MORTE DO JOGADOR
        RobotHealth h = GetComponent<RobotHealth>();
        if (h != null)
            h.OnDeath += PlayDefeat;
    }

    void Update()
    {
        if (hasDied) return;

        Vector2 move = moveAction.ReadValue<Vector2>();
        float h = move.x;

        if (h < -0.1f)
        {
            if (!isTilting || tiltDirection != -1)
            {
                isTilting = true;
                tiltDirection = -1;
                animSuperior.SetBool("IsTilting", true);
                animSuperior.SetInteger("TiltDirection", -1);
            }
            return;
        }

        if (h > 0.1f)
        {
            if (!isTilting || tiltDirection != 1)
            {
                isTilting = true;
                tiltDirection = 1;
                animSuperior.SetBool("IsTilting", true);
                animSuperior.SetInteger("TiltDirection", 1);
            }
            return;
        }

        if (isTilting)
        {
            isTilting = false;
            animSuperior.SetBool("IsTilting", false);
            animSuperior.SetInteger("TiltDirection", 0);
        }
    }

    void OnAttack()
    {
        if (hasDied) return;

        string anim = punchRightNext ? "Punch_Right" : "Punch_Left";
        punchRightNext = !punchRightNext;

        animSuperior.SetTrigger("Punch");
        OnPunchSignal?.Invoke();
    }

    // 🔥 CHAMADO PELO RobotHealth
    public void PlayDefeat()
    {
        if (hasDied) return;
        hasDied = true;

        animSuperior.SetTrigger("Die");
        StartCoroutine(DisableInferior());
    }

    private System.Collections.IEnumerator DisableInferior()
    {
        yield return new WaitForSeconds(0.1f);
        if (animInferior != null)
            animInferior.enabled = false;
    }

    public void PlayVictory()
    {
        if (hasDied) return;

        animSuperior.SetTrigger("Victory");
        if (animInferior != null)
            animInferior.enabled = false;
    }
}
