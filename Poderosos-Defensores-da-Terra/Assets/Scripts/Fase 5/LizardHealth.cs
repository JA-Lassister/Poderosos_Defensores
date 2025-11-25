using UnityEngine;

public class LizardHealth : MonoBehaviour
{
    public int MaxHealth = 50;
    public HealthBar healthBar;

    public int CurrentHealth { get; private set; }

    // >>> EVENTO para notificar a morte
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

        if (CurrentHealth == 0)
        {
            Debug.Log("[Lizard] Derrotado!");
            OnDeath?.Invoke();     // CHAMA O EVENTO
        }
    }

    private void RefreshUI()
    {
        if (healthBar)
            healthBar.SetHealthPercent((float)CurrentHealth / MaxHealth);
    }
}