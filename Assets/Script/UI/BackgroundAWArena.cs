using UnityEngine;
using System.Collections;

public class BackgroundAWArena : MonoBehaviour
{
    private Material material;
    private bool isDistorting = true;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (isDistorting)
        {
            isDistorting = false;
            StartCoroutine(SetDistortionRoutine(1f));
        }
    }

    private IEnumerator SetDistortionRoutine(float distortion)
    {
        float t = 0f;


        while (t < 1f)
        {
            t += Time.deltaTime / 5f;

            float eased = TemporalMath.EaseIn(t);
            material.SetFloat("_DistortionAmount", eased * distortion);
            yield return null;
        }


        yield return new WaitForSeconds(0.25f);

        t = 0f;


        while (t < 1f)
        {
            t += Time.deltaTime / 5f;

            float eased = TemporalMath.SinPulse(t, 0.02f);
            material.SetFloat("_DissolveAmount", eased * distortion);
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);

        while (t > 0f)
        {
            t -= Time.deltaTime / 5f;

            float eased = TemporalMath.SinPulse(t, 0.03f);
            material.SetFloat("_DissolveAmount", eased * distortion);
            yield return null;
        }

        yield return new WaitForSeconds(0.25f);

        t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime / 5f;

            float eased = TemporalMath.EaseInOut(t);
            material.SetFloat("_DistortionAmount", eased * distortion);
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        isDistorting = true;
    }
}
