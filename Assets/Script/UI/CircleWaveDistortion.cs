using UnityEngine;
using System.Collections;

public class CircleWaveDistortion : MonoBehaviour
{
    public Transform bossTransform;
    public bool followBoss = false;
    private string shaderStartPointProperty = "_FocalPoint";
    private string shaderTimeProperty = "_TimeScale";
    private string shaderAlphaProperty = "_Alpha";
    private float duration = 2f;
    public Material material;
    private PlayerController playerController;
    private BossController bossController;

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        bossController = bossTransform.GetComponent<BossController>();
    }

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
        playerController.canMove = false;
        bossController.CanMove = false;
        material.SetFloat(shaderAlphaProperty, 1f);
        material.SetVector(shaderStartPointProperty, new Vector4(screenPos.x, screenPos.y, 0, 0));
        material.SetFloat(shaderTimeProperty, 1f);
        yield return new WaitForSeconds(duration);
        followBoss = false;
        material.SetFloat(shaderTimeProperty, 0f);
        material.SetFloat(shaderAlphaProperty, 0f);
        playerController.canMove = true;
        bossController.CanMove = true;
    }
}
