using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class EnemyBulletFinal : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private LayerMask hitMask = ~0;

    [Header("Impacto")]
    [SerializeField] private bool stopOnHit = true;
    [SerializeField] private bool deactivateOnHit = true;
    [SerializeField] private float deactivateDelay = 0f;
    [SerializeField] private Animator animator;
    [SerializeField] private string impactTrigger = "EndBala";

    private Rigidbody2D _rb;
    private Collider2D _col;
    private float _timer;
    private Transform _shooterRoot;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _timer = 0f;

        if (_col != null) _col.enabled = true;

        if (_rb != null)
        {
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _rb.gravityScale = 0f;
            _rb.angularVelocity = 0f;
        }
    }
    public void Fire(Vector2 direction, Transform shooterRoot = null, float? overrideSpeed = null, float? overrideLifeTime = null, int? overrideDamage = null)
    {
        _shooterRoot = shooterRoot;

        if (overrideSpeed.HasValue) speed = overrideSpeed.Value;
        if (overrideLifeTime.HasValue) lifeTime = overrideLifeTime.Value;
        if (overrideDamage.HasValue) damage = overrideDamage.Value;

        IgnoreShooterCollisions(shooterRoot);

        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = direction.normalized * speed;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= lifeTime)
            Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;

        if (((1 << other.gameObject.layer) & hitMask.value) == 0) return;

        if (_shooterRoot != null && (other.transform == _shooterRoot || other.transform.IsChildOf(_shooterRoot)))
            return;

        if (other.CompareTag(targetTag))
        {
            var dmg = other.GetComponentInParent<IDamagable>(); 
            if (dmg != null)
                dmg.ReceiveDamage(damage);

            Impact();
            return;
        }

    }

    private void Impact()
    {
        if (animator != null && !string.IsNullOrEmpty(impactTrigger))
            animator.SetTrigger(impactTrigger);

        if (stopOnHit) StopBullet();

        if (deactivateOnHit)
        {
            if (deactivateDelay > 0f) Invoke(nameof(Deactivate), deactivateDelay);
            else Deactivate();
        }
    }

    private void StopBullet()
    {
        if (_rb != null)
        {
            _rb.velocity = Vector2.zero;
            _rb.angularVelocity = 0f;
            _rb.bodyType = RigidbodyType2D.Kinematic;
        }

        if (_col != null)
            _col.enabled = false;
    }

    private void Deactivate()
    {
        if (_rb != null) _rb.velocity = Vector2.zero;

        gameObject.SetActive(false);
    }

    private void IgnoreShooterCollisions(Transform shooterRoot)
    {
        if (shooterRoot == null || _col == null) return;

        var shooterCols = shooterRoot.GetComponentsInChildren<Collider2D>();
        foreach (var sc in shooterCols)
        {
            if (sc != null)
                Physics2D.IgnoreCollision(_col, sc, true);
        }
    }
}
