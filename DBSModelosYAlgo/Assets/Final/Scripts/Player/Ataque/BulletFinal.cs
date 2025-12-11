using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletFinal : MonoBehaviour
{
    public Rigidbody2D rb;

    private float _lifeTime;
    private float _timer;
    private float _damage;
    private BulletPoolFinal _pool;

    private void Reset()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Inicializa la bala (la llama la BulletFactory)
    /// </summary>
    public void Init(BulletPoolFinal pool, Vector2 direction, float speed, float lifeTime, float damage)
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

        Desactivar();
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
}
