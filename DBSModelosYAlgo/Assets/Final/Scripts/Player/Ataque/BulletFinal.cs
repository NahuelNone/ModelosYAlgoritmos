using Builder;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletFinal : MonoBehaviour
{
    public Rigidbody2D rb;

    bool _stopped = false;

    private float _lifeTime;
    private float _timer;
    private int _damage;
    private BulletPoolFinal _pool;
    //private EnemyFinal enemy;

    private float playerController;

    public Animator animator;

    private void Start()
    {

        playerController = FindObjectOfType<PlayerControllerFinal>().bulletSpeed;

    }

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Inicializa la bala (la llama la BulletFactory)
    /// </summary>
    public void Init(BulletPoolFinal pool, Vector2 direction, float speed, float lifeTime, int damage)
    {
        _pool = pool;
        _lifeTime = lifeTime;
        _damage = damage;
        _timer = 0f;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        rb.velocity = direction.normalized * speed;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _lifeTime)
        {
            Desactivar();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: acá más adelante podés chequear si es enemigo y aplicarle daño
        // if (other.CompareTag("Enemigo")) { ... }

        EnemyFinal enemy = other.GetComponent<EnemyFinal>();

        int damage = _damage;

        if (other.CompareTag("Enemy"))
        {

            enemy.TakeDamage(damage);

            animator.SetTrigger("EndBala");

            StopBullet();

        }

        //Desactivar();
    }

    private void Desactivar()
    {
        if (rb != null)
            rb.velocity = Vector2.zero;

        if (_pool != null)
            _pool.ReturnBullet(this);
        else
            gameObject.SetActive(false);
    }

    private void StopBullet()
    {
        _stopped = true;

        if (rb != null)
        {
            rb.velocity = Vector2.zero;             // velocidad 0
            rb.angularVelocity = 0f;                // por las dudas
            rb.bodyType = RigidbodyType2D.Kinematic; // que ya no reciba fuerzas
        }

        // que no vuelva a chocar
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

}
