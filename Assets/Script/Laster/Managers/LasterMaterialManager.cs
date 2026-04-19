using UnityEngine;
using System.Collections;

public class LasterMaterialManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer controller;
    [SerializeField] private SpriteRenderer eyes;
    public Material eyesMaterial;
    public Material hitMaterial;
    public Material regularMaterial;


    public void SetHitMaterial()
    {
        controller.material = hitMaterial;
        StartCoroutine(ResetMaterialAfterDelay(0.1f));
    }

    private IEnumerator ResetMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetRegularMaterial();
    }

    private void SetRegularMaterial()
    {
        controller.material = regularMaterial;
    }

    public void OnPhase2ChangeEyes()
    {
        StartCoroutine(ChangeEyesMaterialAfterDelay(2f));
    }

    private IEnumerator ChangeEyesMaterialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        eyes.material = eyesMaterial;
    }
}
