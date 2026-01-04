using UnityEngine;

public class LifeFlameVFX : MonoBehaviour
{
    [Header("Systems")]
    public ParticleSystem baseFlame;
    public ParticleSystem core;
    public ParticleSystem sparkles;
    public ParticleSystem combatBoost;

    public void OnDamage()
    {
        var baseMain = baseFlame.main;
        baseMain.startSizeMultiplier = 1.25f;

        baseFlame.Emit(8); // pequena explosão
    }

    public void OnCombatStart()
    {
        combatBoost.Play();
    }

    public void OnCombatEnd()
    {
        combatBoost.Stop();
    }

    public void UpdateLife(float normalizedLife)
    {
        var baseMain = baseFlame.main;
        baseMain.startSizeMultiplier = Mathf.Lerp(0.6f, 1.6f, normalizedLife);

        var emission = sparkles.emission;
        emission.rateOverTime = Mathf.Lerp(1, 10, normalizedLife);
    }
}