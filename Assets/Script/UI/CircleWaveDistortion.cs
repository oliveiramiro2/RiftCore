using UnityEngine;
using System.Collections;

public class CircleWaveDistortion : MonoBehaviour
{
    public bool followBoss = false;
    private string shaderStartPointProperty = "_FocalPoint";
    private string shaderTimeProperty = "_TimeScale";
    private string shaderAlphaProperty = "_Alpha";
    private float duration = 2f;



    public Material material;
    private PlayerController playerController;
    [SerializeField] private GameObject bossController;

    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
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
        Vector3 bossPos = bossController.transform.position;
        Vector3 screenPos = Camera.main.WorldToViewportPoint(bossPos);

        StartCoroutine(DistortionRoutine(screenPos));
    }

    private IEnumerator DistortionRoutine(Vector3 screenPos)
    {
        bossController.GetComponent<BaseEntity>().canMove = false;
        material.SetFloat(shaderAlphaProperty, 0.8f);
        material.SetVector(shaderStartPointProperty, new Vector4(screenPos.x, screenPos.y, 0, 0));
        material.SetFloat(shaderTimeProperty, 1f);
        yield return new WaitForSeconds(duration);
        followBoss = false;
        material.SetFloat(shaderTimeProperty, 0f);
        material.SetFloat(shaderAlphaProperty, 0f);
        if (bossController == null) yield break;
    }
}