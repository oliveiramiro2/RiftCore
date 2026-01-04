using UnityEngine;
using System.Collections;

public class TETDamageHandler : DamageHandlerBase<BossController>
{

    [Header("Boss Events")]
    public GameEvent onTetDamaged;
    public GameEvent onTetDeath;
    public Material mat;
    public float dissolveSpeed = 1f;

    private bool phase1 = true;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void TakeDamage(int damage, Vector2 hitDirection, float knockbackForce)
    {
        base.TakeDamage(damage, hitDirection, knockbackForce);

        if (isDead) return;
        onTetDamaged.Raise();

        if (controller.Phase2() && phase1)
        {
            controller.tetEvents.OnPhase2.Raise();
            phase1 = false;
        }
    }

    protected override void Die()
    {
        base.Die();
        isDead = true;
        StartCoroutine(DeathSequence());
        //onPlayerDeath.Raise();
    }

    IEnumerator DeathSequence()
    {
        mat.SetFloat("_Dissolve", 0f);
        controller.spriteRenderer.material = mat;
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(dissolveSpeed);
        Time.timeScale = 1f;

        PlayDeath();
        // ativa shader dissolve

        // vitória / fim
        //OnBossDefeated();
    }

    public void PlayDeath()
    {
        StartCoroutine(DissolveRoutine());
    }

    private IEnumerator DissolveRoutine()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * dissolveSpeed;
            mat.SetFloat("_Dissolve", t);
            yield return null;
        }

        Destroy(gameObject);
    }
}
