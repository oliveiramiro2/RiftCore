using UnityEngine;
using System.Collections;

public class SWMaterialManager : MonoBehaviour
{
  private SwordMasterController controller;
  public Material regularMaterial;
  public Material hitMaterial;
  public Material teleportMaterial;

  private readonly float dissolveTime = 0.5f;

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

  IEnumerator TeleportOut()
  {
    float t = 0;


    while (t < dissolveTime)
    {
      t += Time.deltaTime;
      float value = t / dissolveTime;
      teleportMaterial.SetFloat("_DissolveAmount", value);
      yield return null;
      teleportMaterial.SetFloat("_DissolveAmount", 1f);
    }
    yield return new WaitForSeconds(0.1f);
    controller.spriteRenderer.material = regularMaterial;

  }


  private IEnumerator TeleportIn()
  {
    float t = 0;
    controller.spriteRenderer.material = teleportMaterial;

    while (t < dissolveTime)
    {
      t += Time.deltaTime;
      float value = t / dissolveTime;
      teleportMaterial.SetFloat("_DissolveAmount", -value);
      yield return null;
      teleportMaterial.SetFloat("_DissolveAmount", -1f);
    }
    yield return new WaitForSeconds(0.1f);
  }

  private IEnumerator TeleportSequence()
  {
    yield return StartCoroutine(TeleportIn());
    yield return StartCoroutine(TeleportOut());
  }

  public void StartTeleportEffect()
  {
    StartCoroutine(TeleportSequence());
  }
}