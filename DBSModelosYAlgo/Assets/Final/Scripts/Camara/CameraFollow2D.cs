using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow2D : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    public Vector2 offset = Vector2.zero;

    [Header("Dead Zone (world units)")]
    public Vector2 deadZoneSize = new Vector2(3f, 12.4f);

    [Header("Smoothing")]
    [Range(0.01f, 1f)]
    public float smoothTime = 0.7f;

    [Header("Bounds (optional)")]
    public bool useBounds = false;
    public Vector2 boundsMin; // esquina inferior izquierda del mundo
    public Vector2 boundsMax; // esquina superior derecha del mundo

    private Camera _cam;
    private Vector3 _vel;

    void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!target) return;

        Vector3 camPos = transform.position;
        Vector2 desired = new Vector2(camPos.x, camPos.y);

        Vector2 t = (Vector2)target.position + offset;
        float halfX = deadZoneSize.x * 0.5f;
        float halfY = deadZoneSize.y * 0.5f;

        // X: solo mover si sale del “rectángulo”
        if (t.x > desired.x + halfX) desired.x = t.x - halfX;
        else if (t.x < desired.x - halfX) desired.x = t.x + halfX;

        // Y
        if (t.y > desired.y + halfY) desired.y = t.y - halfY;
        else if (t.y < desired.y - halfY) desired.y = t.y + halfY;

        Vector3 desired3 = new Vector3(desired.x, desired.y, camPos.z);

        // Clamp a límites del nivel (si querés)
        if (useBounds && _cam.orthographic)
        {
            float vert = _cam.orthographicSize;
            float horiz = vert * _cam.aspect;

            float minX = boundsMin.x + horiz;
            float maxX = boundsMax.x - horiz;
            float minY = boundsMin.y + vert;
            float maxY = boundsMax.y - vert;

            desired3.x = Mathf.Clamp(desired3.x, minX, maxX);
            desired3.y = Mathf.Clamp(desired3.y, minY, maxY);
        }

        transform.position = Vector3.SmoothDamp(camPos, desired3, ref _vel, smoothTime);
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        // Dibujar la dead zone aproximada en escena
        Gizmos.color = Color.yellow;
        Vector3 center = transform.position;
        Gizmos.DrawWireCube(new Vector3(center.x, center.y, 0f),
            new Vector3(deadZoneSize.x, deadZoneSize.y, 0.1f));
    }
#endif
}
