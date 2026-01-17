using UnityEngine;
using System.Collections;

public class LaserManager : MonoBehaviour
{
    public float easeValue = 0.6f;
    [SerializeField] private GameObject[] lasers;

    public void LasersStart()
    {
        StartCoroutine(LasersAttackRoutine());
        StartCoroutine(HideLasersRoutine());
    }

    private IEnumerator LasersAttackRoutine()
    {
        foreach (var laser in lasers)
        {
            laser.SetActive(true);
            StartCoroutine(GrowLaser(laser, easeValue));
            yield return new WaitForSeconds(0.3f);
        }
    }
    IEnumerator GrowLaser(GameObject laser, float duration)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            float eased = TemporalMath.EaseIn(t);
            laser.transform.localScale = new Vector3(eased, 1, 1);

            yield return null;
        }

        laser.transform.localScale = new Vector3(30f, 1, 1);
    }

    private IEnumerator HideLasersRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        foreach (var laser in lasers)
        {
            StartCoroutine(ShrinkLaser(laser, 0.8f));
            yield return new WaitForSeconds(0.35f);
        }
    }

    IEnumerator ShrinkLaser(GameObject laser, float duration)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            float eased = TemporalMath.EaseOut(1f - t);
            laser.transform.localScale = new Vector3(eased, 1, 1);

            yield return null;
        }

        laser.SetActive(false);
    }

}
