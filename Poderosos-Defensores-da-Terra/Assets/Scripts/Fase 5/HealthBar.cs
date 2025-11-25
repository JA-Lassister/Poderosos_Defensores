using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;
    public Image delayedFill;
    public float delayedSpeed = 3f;

    private float targetFill = 1f;

    void Update()
    {
        if (fill != null)
            fill.fillAmount = Mathf.Clamp01(targetFill);

        if (delayedFill != null)
        {
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
    }

    public void SetHealthPercent(float percent)
    {
        targetFill = Mathf.Clamp01(percent);
    }
}
