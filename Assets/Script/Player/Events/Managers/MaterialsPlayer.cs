using UnityEngine;
using System.Collections;

public class MaterialsPlayer : MonoBehaviour
{
    [Header("Material Dash")]
    public Material dashMaterial;
    public SpriteRenderer playerRenderer;

    [Header("Material Hit")]
    public Material hitMaterial;

    [Header("Effect Settings Dash")]
    public float duration = 0.18f;
    public AnimationCurve intensityCurve;


    [Header("Effect Settings Hit")]
    public float hitDuration = 0.5f;

    [Header("Original Material")]
    public Material originalMaterial;

    public void DashEffect()
    {
        StartCoroutine(DashEffectRoutine());
    }

    public void HitEffect()
    {
        StartCoroutine(HitEffectRoutine());
    }

    private IEnumerator HitEffectRoutine()
    {
        playerRenderer.material = hitMaterial;
        yield return new WaitForSeconds(hitDuration);
        playerRenderer.material = originalMaterial;
    }

    private IEnumerator DashEffectRoutine()
    {
        playerRenderer.material = dashMaterial;
        float timer = 0f;

        float intensity = 1f;

        while (timer < duration)
        {
            intensity = TemporalMath.LerpWithPulse(
                intensity,
                0f,      // alvo
                6f,      // velocidade
                0.25f,   // amplitude do pulso
                20f      // frequência
            );

            dashMaterial.SetFloat("_Intensity", intensity);

            timer += Time.deltaTime;
            yield return null;
        }



        dashMaterial.SetFloat("_Intensity", 0f);

        playerRenderer.material = originalMaterial;

    }
}
