using UnityEngine;

public class RobotHealth : MonoBehaviour
{
    public Restart restart;
    public int MaxHealth = 50;
    public HealthBar healthBar;

    public int CurrentHealth { get; private set; }

    // Evento de morte
    public System.Action OnDeath;

    void Awake()
    {
        CurrentHealth = MaxHealth;
        RefreshUI();
    }

    public void ApplyDamage(int amount)
    {
        if (amount <= 0) return;

        CurrentHealth -= amount;
        if (CurrentHealth < 0) CurrentHealth = 0;

        RefreshUI();

        // morte
        if (CurrentHealth == 0)
        {
            OnDeath?.Invoke();
            restart.ExibirMensagem();
        }
    }

    private void RefreshUI()
    {
        if (healthBar)
            healthBar.SetHealthPercent((float)CurrentHealth / MaxHealth);
    }
}