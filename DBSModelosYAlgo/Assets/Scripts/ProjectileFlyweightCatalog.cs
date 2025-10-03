using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ProjectileFW
{
    public string id = "arrow";
    public float speed = 40f;
    public int damage = 10;
    public float lifeTime = 5f;
    public float radius = 0.1f;
    public LayerMask hitMask;            // ← p.ej. “Dinosaurio”
    public GameObject visualPrefab;      // opcional: un VFX o malla simple
}

/// Catálogo único de flyweights (editable en el Inspector)
public class ProjectileFlyweightCatalog : MonoBehaviour
{
    [SerializeField] private List<ProjectileFW> entries = new();
    private static Dictionary<string, ProjectileFW> _dict;

    private void Awake()
    {
        _dict = new Dictionary<string, ProjectileFW>();
        foreach (var e in entries)
        {
            if (!string.IsNullOrEmpty(e.id)) _dict[e.id] = e;
        }
    }

    public static ProjectileFW Get(string id)
    {
        if (_dict == null) { Debug.LogError("Catálogo no inicializado."); return null; }
        _dict.TryGetValue(id, out var fw);
        return fw;
    }
}

public class ProjectileSpawner : MonoBehaviour
{
    [Header("Disparo")]
    public string projectileId = "arrow";
    public Transform firePoint;
    public float spawnOffset = 0.15f;
    public KeyCode fireKey = KeyCode.Mouse1;   // click derecho

    private void Update()
    {
        if (Input.GetKeyDown(fireKey)) Fire();
    }

    public void Fire()
    {
        var fw = ProjectileFlyweightCatalog.Get(projectileId);
        if (fw == null || !firePoint)
        {
            Debug.LogWarning("[ProjectileSpawner] Falta flyweight o firePoint.");
            return;
        }

        // Crear GO runtime minimal
        var go = new GameObject($"Projectile_{projectileId}");
        go.transform.SetPositionAndRotation(
            firePoint.position + firePoint.forward * spawnOffset,
            firePoint.rotation
        );

        var pr = go.AddComponent<ProjectileRuntime>();
        pr.Init(fw, firePoint.forward, owner: gameObject);
    }
}

public class ProjectileRuntime : MonoBehaviour
{
    private ProjectileFW fw;     // intrínseco compartido
    private Vector3 dir;         // extrínseco
    private float age;
    private GameObject owner;

    public void Init(ProjectileFW fw, Vector3 direction, GameObject owner)
    {
        this.fw = fw;
        this.dir = direction.normalized;
        this.owner = owner;

        if (fw.visualPrefab) Instantiate(fw.visualPrefab, transform);

        var col = gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = fw.radius;
    }

    private void Update()
    {
        transform.position += dir * fw.speed * Time.deltaTime;
        age += Time.deltaTime;
        if (age >= fw.lifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return; // evitar autogolpe
        if ((fw.hitMask.value & (1 << other.gameObject.layer)) == 0) return;

        // Si tenés tu interfaz IDamageable, esto la usa directo:
        var dmg = other.GetComponent<IDamageable>();
        if (dmg != null) dmg.TakeDamage(fw.damage);

        Destroy(gameObject);
    }
}
