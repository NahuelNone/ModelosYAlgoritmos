using UnityEngine;

[RequireComponent(typeof(PlayerViewFinal))]
public class PlayerControllerFinal : MonoBehaviour, IDamagable
{
    [Header("MVC")]
    public PlayerViewFinal view;
    public PlayerModelFinal model = new PlayerModelFinal();

    [Header("Ground Check (simple por tag)")]
    public string groundTag = "Ground";

    [Header("Pool / Arma")]
    public BulletPoolFinal bulletPool;
    public float bulletSpeed = 15f;
    public float bulletLifeTime = 2f;
    public int bulletDamage = 10;

    private float _horizontalInput;
    private IWeaponFinal _currentWeapon;
    private BulletFactoryFinal _bulletFactory;

    private void Awake()
    {
        if (view == null)
            view = GetComponent<PlayerViewFinal>();

        model.OnHealthChanged += HandleHealthChanged;
        model.OnDeath += HandleDeath;

        if (bulletPool != null)
        {
            _bulletFactory = new BulletFactoryFinal(bulletPool);
            _currentWeapon = new RangedWeaponFinal(_bulletFactory, bulletSpeed, bulletLifeTime, bulletDamage);
        }
        else
        {
            Debug.LogWarning("BulletPool no asignado en PlayerController. No se podrá disparar.");
        }
    }

    private void Start()
    {
        model.Init();               // inicializa vida y dispara OnHealthChanged
    }


    private void Update()
    {
        // Cooldown de ataque
        model.TickAttackCooldown(Time.deltaTime);

        LeerInputMovimiento();
        ManejarMovimiento();
        ManejarSalto();
        ManejarAtaque();
    }

    private void LeerInputMovimiento()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal"); // A/D o flechas
    }

    private void ManejarMovimiento()
    {
        view.Move(_horizontalInput, model.moveSpeed);
    }

    private void ManejarSalto()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (model.CanJump())
            {
                model.OnJump();
                view.Jump(model.jumpForce);
            }
        }
    }


    // Space para atacar
    private void ManejarAtaque()
    {
        if (Input.GetKeyDown(KeyCode.Space) && model.CanAttack())
        {
            model.OnAttack();

            if (_currentWeapon != null)
            {
                _currentWeapon.Attack(view);
            }
            else
            {
                Debug.Log("ATAQUE (sin arma configurada todavía)");
            }
        }
    }

    // Suelo por tag
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            model.SetGrounded(true);
            view.SetGrounded(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            model.SetGrounded(false);
            view.SetGrounded(false);
        }
    }

    public void ReceiveDamage(int amount)
    {
        // 1) Actualizar modelo
        model.TakeDamage(amount);

        // 2) Actualizar vista/UI
        view.UpdateHealthUI((int)model.CurrentHealth, model.maxHealth);

        Debug.Log("Vida actual del player: " + model.CurrentHealth);

        // 3) ¿murió?
        if (model.IsDead)
        {
            HandleDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            ReceiveDamage(20); // o el daño que quieras
        }
    }

    private void HandleHealthChanged(int current, int max)
    {
        view.UpdateHealthUI(current, max);
    }

    private void HandleDeath()
    {
        view.PlayDeath();
        Debug.Log("El jugador murió (evento del Model)");

        // Podés además desactivar input, llamar GameManager, etc.
    }


}
