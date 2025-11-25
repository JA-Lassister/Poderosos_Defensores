using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Fase4 : MonoBehaviour
{
    public Image fill;
    public Image delayedFill;
    public Controlador controlador;
    public float delayedSpeed = 3f;

    private float targetFill = 1f;

    void Update()
    {
        fill.fillAmount = Mathf.Clamp01(targetFill);

        if (fill.fillAmount == 0)
            controlador.Morrer();
        
        if (delayedFill.fillAmount > targetFill)
        {
            delayedFill.fillAmount = Mathf.MoveTowards(
            delayedFill.fillAmount,
            targetFill,
            Time.deltaTime * delayedSpeed
            );
        }
        else
        {
            delayedFill.fillAmount = targetFill;
        }
    }

    public void DecreaseHealthPercent(float percent)
    {
        targetFill = Mathf.Clamp01(targetFill - percent);
    }
}
