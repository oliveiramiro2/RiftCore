using UnityEngine;
using System.Collections;

public class AWMaterialManager : MonoBehaviour
{
    private AstralWeaverController controller;
    public Material regularMaterial;
    public Material growMaterial;
    public Material hitMaterial;

    private void Awake()
    {
        controller = GameObject.FindAnyObjectByType<AstralWeaverController>();
    }

    public void SetRegularMaterial()
    {
        controller.spriteRenderer.material = regularMaterial;
    }

    public void SetGrowMaterial()
    {
        controller.spriteRenderer.material = growMaterial;
    }

    public void SetHitMaterial()
    {
        controller.spriteRenderer.material = hitMaterial;
        StartCoroutine(ResetMaterialAfterDelay(0.1f));
    }

    private IEnumerator ResetMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (controller.Phase2())
        {
            SetGrowMaterial();
            yield break;
        }
        SetRegularMaterial();
    }
}
