using UnityEngine;
using System.Collections;

public class MaterialTet : MonoBehaviour
{
    public SpriteRenderer tetRenderer;

    [Header("Material Hit")]
    public Material hitMaterial;

    [Header("Material phase 2")]
    public Material phase2Material;

    [Header("Effect Settings Hit")]
    public float hitDuration = 0.05f;

    [Header("Original Material")]
    public Material originalMaterial;

    [Header("Flags")]
    public bool isPhase2 = false;

    public void TetHitEffect()
    {
        StartCoroutine(TetHitEffectRoutine());
    }

    public void SetPhase2Material()
    {
        tetRenderer.material = phase2Material;
        isPhase2 = true;
    }

    private IEnumerator TetHitEffectRoutine()
    {
        tetRenderer.material = hitMaterial;
        yield return new WaitForSeconds(hitDuration);
        if (isPhase2)
        {
            tetRenderer.material = phase2Material;
            yield break;
        }
        tetRenderer.material = originalMaterial;
    }
}
