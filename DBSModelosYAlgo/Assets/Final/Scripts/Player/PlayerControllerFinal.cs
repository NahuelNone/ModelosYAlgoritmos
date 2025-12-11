using UnityEngine;

[RequireComponent(typeof(PlayerViewFinal))]
public class PlayerControllerFinal : MonoBehaviour
{
    [Header("MVC")]
    public PlayerViewFinal view;
    public PlayerModelFinal model = new PlayerModelFinal();

    [Header("Ground Check (simple por tag)")]
    public string groundTag = "Ground";

    [Header("Pool / Arma")]
    public BulletPoolFinal bulletPool;       // 🔹 asigná el objeto con BulletPool
    public float bulletSpeed = 15f;
    public float bulletLifeTime = 2f;
    public float bulletDamage = 10f;

    private float _horizontalInput;
    private IWeaponFinal _currentWeapon;
    private BulletFactoryFinal _bulletFactory;

    private void Awake()
    {
        if (view == null)
            view = GetComponent<PlayerViewFinal>();

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

    // W para saltar / doble salto
    private void ManejarSalto()
    {
        if (Input.GetKeyDown(KeyCode.W))
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
}
