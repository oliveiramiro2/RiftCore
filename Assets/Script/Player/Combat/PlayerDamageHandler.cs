using UnityEngine;
using System;
using System.Collections;

public class PlayerDamageHandler : DamageHandlerBase<PlayerController>
{

    [Header("Player Events")]
    public GameEvent onPlayerDamaged;

    public static event Action<int, int> OnHealthChanged;


    protected override void Awake()
    {
        base.Awake();
    }

    public override void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce)
    {
        if (base.invincibilityTimer > 0f) return;
        base.TakeDamage(damage, hitDirection, knockbackForce);
        RumbleManager.Instance.Play(RumbleType.Danger);

        StartCoroutine(HeartFeedbackCoroutine());
        if (isDead) return;

        controller.DisableMovement(controller.knockbackDuration);
        onPlayerDamaged.Raise();
    }

    private IEnumerator HeartFeedbackCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

    }

    protected override void Die()
    {
        base.Die();
        isDead = true;
        controller.canMove = false;
        RumbleManager.Instance.Play(RumbleType.Danger);
        controller.AnimatorBridge.TriggerDeath();
        controller.events.OnDeath.Raise();
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        Time.timeScale = 0.8f;
        GameFlowManager.Instance.PlayerDied();
    }
}
