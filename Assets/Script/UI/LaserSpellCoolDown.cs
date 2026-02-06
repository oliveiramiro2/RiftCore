using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LaserSpellCoolDown : MonoBehaviour
{
  private Image cooldownImage;
    private readonly float cooldownDuration = 8f;

    void Start()
    {
        cooldownImage = GetComponent<Image>();
        cooldownImage.fillAmount = 0f;
    }

    public void ActivateCooldown()
    {
        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        float elapsed = cooldownDuration;
        while (elapsed > 0)
        {
            elapsed -= Time.deltaTime;
            cooldownImage.fillAmount = elapsed / cooldownDuration;
            yield return null;
        }
        cooldownImage.fillAmount = 0f;
    }
}