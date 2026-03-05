using UnityEngine;
using System.Collections;

public class SWMaterialManager : MonoBehaviour
{
  private SwordMasterController controller;
  public Material regularMaterial;
  public Material hitMaterial;

  void Start()
  {
    controller = GameObject.FindAnyObjectByType<SwordMasterController>();
  }

  public void SetHitMaterial()
  {
    controller.spriteRenderer.material = hitMaterial;
    StartCoroutine(ResetMaterialAfterDelay(0.1f));
  }

  private IEnumerator ResetMaterialAfterDelay(float delay)
  {
    yield return new WaitForSeconds(delay);
    SetRegularMaterial();
  }

  private void SetRegularMaterial()
  {
    controller.spriteRenderer.material = regularMaterial;
  }
}