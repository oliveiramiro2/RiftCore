using UnityEngine;
using System.Collections;

public class MawMaterialManagers : MonoBehaviour
{
    [SerializeField] private SpriteRenderer controller;
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
}
