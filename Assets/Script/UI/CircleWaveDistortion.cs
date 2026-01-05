using UnityEngine;
using System.Collections;

public class CircleWaveDistortion : MonoBehaviour
{
    public Transform bossTransform;
    public bool followBoss = false;
    private string shaderStartPointProperty = "_FocalPoint";
    private string shaderTimeProperty = "_TimeScale";
    private float duration = 2f;
    public Material material;

    void Update()
    {
        if (followBoss)
        {
            SetBossPositionUV();
        }
    }

    public void StartDistortionAtBoss()
    {
        followBoss = true;
    }

    private void SetBossPositionUV()
    {
        Vector3 bossPos = bossTransform.position;
        Vector3 screenPos = Camera.main.WorldToViewportPoint(bossPos);

        StartCoroutine(DistortionRoutine(screenPos));
    }

    private IEnumerator DistortionRoutine(Vector3 screenPos)
    {
        material.SetVector(shaderStartPointProperty, new Vector4(screenPos.x, screenPos.y, 0, 0));
        material.SetFloat(shaderTimeProperty, 1f);
        yield return new WaitForSeconds(duration);
        followBoss = false;
        material.SetFloat(shaderTimeProperty, 0f);
    }
}
