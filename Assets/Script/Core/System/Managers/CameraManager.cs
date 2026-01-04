using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraManager : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Offset e Suavização")]
    public Vector3 offset = new(0f, 1.5f, -10f);
    public float smoothTime = 0.2f;

    [Header("Limites (opcional)")]
    public bool useBounds = false;
    public Vector2 minPosition;
    public Vector2 maxPosition;
    private Vector3 velocity = Vector3.zero;

    void Awake()
    {
        Camera cam = GetComponent<Camera>();
        float baseAspect = 16f / 9f;
        float currentAspect = (float)Screen.width / Screen.height;
        cam.orthographicSize = 4f * (baseAspect / currentAspect);
    }

    private void LateUpdate()
    {
        if (target == null) return;

        offset.x = target.localScale.x == 1 ? 1 : -1;

        Vector3 targetPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (useBounds)
        {
            smoothedPosition.x = Mathf.Clamp(smoothedPosition.x, minPosition.x, maxPosition.x);
            smoothedPosition.y = Mathf.Clamp(smoothedPosition.y, minPosition.y, maxPosition.y);
        }

        transform.position = smoothedPosition;
    }
}