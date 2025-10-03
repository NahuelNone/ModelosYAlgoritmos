using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour, IProduct
{
    [Header("Config")]
    [SerializeField] private float defaultSpeed = 30f;
    [SerializeField] private float lifeSeconds = 6f;
    [SerializeField] private bool useGravity = false;
    [SerializeField] private int damage = 10;
    [SerializeField] private bool debug = true;

    [Header("Hit Rules")]
    [SerializeField] private LayerMask dinosaurio;
    [SerializeField] private bool includeTriggers = true;
    [SerializeField] private bool destroyRoot = true;

    private Rigidbody rb;
    private Collider myCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        rb.useGravity = useGravity;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    public void Initialize(SpawnData spawn)
    {
        transform.SetPositionAndRotation(spawn.position, spawn.rotation);
        Vector3 dir = (spawn.direction.sqrMagnitude > 0f ? spawn.direction : transform.forward).normalized;
        float speed = (spawn.speed > 0f ? spawn.speed : defaultSpeed);

        if (spawn.owner && myCollider)
        {
            foreach (var c in spawn.owner.GetComponentsInChildren<Collider>())
                if (c && c.enabled) Physics.IgnoreCollision(myCollider, c, true);
        }

        rb.velocity = dir * speed;

        if (debug)
        {
            Debug.DrawRay(transform.position, dir * 1.5f, Color.cyan, 0.5f);
            Debug.Log($"[Bullet] Spawn @ {transform.position}, dir {dir}, speed {speed}");
        }

        if (lifeSeconds > 0f) Destroy(gameObject, lifeSeconds);
    }

    private void OnCollisionEnter(Collision other)
    {
        HandleHit(other.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (includeTriggers) HandleHit(other);
    }

    private void HandleHit(Collider other)
    {

        GameObject go = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;

        if (IsInMask(go.layer, dinosaurio))
        {

            GameObject target = destroyRoot ? go.transform.root.gameObject : go;
            if (debug) Debug.Log($"[Bullet] Dino hit → destroy {target.name}");

            Destroy(target);

        }

        Destroy(gameObject);

    }

    private bool IsInMask(int layer, LayerMask mask)
    {

        return (mask.value & (1 << layer)) != 0;

    }
}
