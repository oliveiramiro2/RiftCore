using UnityEngine;
using System.Collections;

public class ToxicSlimeMaterialManager : MonoBehaviour
{
  private ToxicSlimeController controller;
  public Material defaultMaterial, hurtMaterial, auxMaterial;

  private void Awake()
  {
    controller = GameObject.FindAnyObjectByType<ToxicSlimeController>();
  }


  public void SetHitMaterial()
  {
    controller.spriteRenderer.material = hurtMaterial;
    StartCoroutine(ResetMaterialAfterDelay(0.1f));
  }

  private IEnumerator ResetMaterialAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    if (controller.Phase2())
      SetMaterialPhase2();
    else
      SetRegularMaterial();
  }

  public void SetRegularMaterial()
  {
    controller.spriteRenderer.material = defaultMaterial;
  }

  public void SetMaterialPhase2()
  {
    controller.spriteRenderer.material = auxMaterial;
  }
}