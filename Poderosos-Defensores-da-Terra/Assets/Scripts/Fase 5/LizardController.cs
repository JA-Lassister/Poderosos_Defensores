using UnityEngine;
using System.Collections;

public class LizardController : MonoBehaviour
{
    [Header("Referências")]
    public Animator anim;
    public RobotController player;
    public RobotHealth playerHealth;
    public LizardHealth lizardHealth;

    [Header("Dano")]
    public int enemyDamage = 5;
    public int playerDamage = 5;

    [Header("Ataques")]
    public float minAttackDelay = 1.5f;
    public float maxAttackDelay = 3.0f;
    public float doubleAttackChance = 0.25f;

    private float nextAttackTimer = 0f;
    private bool isAttacking = false;
    private bool punchRoutineRunning = false;
    private bool bufferedPunch = false;
    private bool isDead = false;

    private int lastAttackSide = 0; // usado pelo evento da animação

    void OnEnable()
    {
        player.OnPunchSignal += OnPlayerPunch;
        playerHealth.OnDeath += OnPlayerDied;
    }

    void OnDisable()
    {
        player.OnPunchSignal -= OnPlayerPunch;
        playerHealth.OnDeath -= OnPlayerDied;
    }

    void Start()
    {
        ResetAttackTimer();
    }

    void Update()
    {
        if (isDead) return;

        nextAttackTimer -= Time.deltaTime;

        if (!isAttacking && nextAttackTimer <= 0f)
            StartCoroutine(PerformAttack());
    }

    // ============================================================
    //                     SOCO DO JOGADOR
    // ============================================================
    private void OnPlayerPunch()
    {
        if (isDead) return;

        if (!punchRoutineRunning)
        {
            StartCoroutine(PlayerPunchRoutine());
        }
        else
        {
            bufferedPunch = true;
        }
    }

    private IEnumerator PlayerPunchRoutine()
    {
        punchRoutineRunning = true;

        AnimatorStateInfo info = player.animSuperior.GetCurrentAnimatorStateInfo(0);
        float hitMoment = info.length * 0.5f; // frame 30

        yield return new WaitForSeconds(hitMoment);

        // dano do jogador
        lizardHealth.ApplyDamage(playerDamage);

        // HIT somente se não estiver atacando
        if (!isAttacking && !isDead)
            anim.SetTrigger("Hit");

        // espera a animação acabar
        yield return new WaitForSeconds(info.length - hitMoment);
        punchRoutineRunning = false;

        if (lizardHealth.CurrentHealth <= 0 && !isDead)
        {
            Die();
            yield break;
        }

        if (bufferedPunch)
        {
            bufferedPunch = false;
            StartCoroutine(PlayerPunchRoutine());
        }
    }

    // ============================================================
    //                        ATAQUE DO INIMIGO
    // ============================================================
    private IEnumerator PerformAttack()
    {
        isAttacking = true;

        int lean = player.GetLeanState();
        bool doDouble = (lean == 0 && Random.value < doubleAttackChance);

        if (doDouble)
        {
            int first = Random.value < 0.5f ? -1 : 1;
            int second = -first;

            yield return AttackOnce(first);
            yield return AttackOnce(second);
        }
        else
        {
            int side = Random.value < 0.5f ? -1 : 1;

            if (lean == -1) side = 1;
            if (lean == 1)  side = -1;

            yield return AttackOnce(side);
        }

        isAttacking = false;
        ResetAttackTimer();
    }

    private IEnumerator AttackOnce(int side)
    {
        lastAttackSide = side;

        if (side == -1) anim.SetTrigger("AttackLeft");
        else anim.SetTrigger("AttackRight");

        // espera duração da animação (evento cuidará do dano)
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(info.length);
    }

    // ============================================================
    //                  EVENTO DE ANIMAÇÃO (frame 45)
    // ============================================================
    public void EnemyAttackHit()
    {
        if (isDead) return;

        ApplyDamageToPlayer(lastAttackSide);
    }

    private void ApplyDamageToPlayer(int attackSide)
    {
        int lean = player.GetLeanState();
        int dmg = 0;

        if (lean == 0)
        {
            dmg = enemyDamage;
        }
        else
        {
            bool doubleDamage =
                (lean == -1 && attackSide == 1) ||
                (lean == 1 && attackSide == -1);

            dmg = doubleDamage ? enemyDamage * 2 : 0;
        }

        if (dmg > 0)
            playerHealth.ApplyDamage(dmg);
    }

    private void ResetAttackTimer()
    {
        nextAttackTimer = Random.Range(minAttackDelay, maxAttackDelay);
    }

    // ============================================================
    //                            MORTE
    // ============================================================
    private void Die()
    {
        isDead = true;
        anim.SetTrigger("Die");
        player.PlayVictory();
        StopAllCoroutines();
        StartCoroutine(SelecaoFase.CarregarFase(6, 4));
    }

    // ============================================================
    //                 MORTE DO JOGADOR
    // ============================================================
    private void OnPlayerDied()
    {
        StopAllCoroutines();
    }
}
