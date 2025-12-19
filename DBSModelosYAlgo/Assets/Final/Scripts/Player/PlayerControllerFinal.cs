using UnityEngine;
using System.Collections;

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
        model.OnEnergyChanged += HandleEnergyChanged;
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
        model.Init(); 
    }


    private void Update()
    {

        ManageInput();
        ManageMovement();
        ManageJump();
        ManageAttack();
        ManageAttackUi();

    }

    private void ManageInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void ManageMovement()
    {
        view.Move(_horizontalInput, model.moveSpeed);
    }

    private void ManageJump()
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

    private void ManageAttack()
    {

        model.TickAttackCooldown(Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && model.CanAttack())
        {

            model.OnAttack();

            if (_currentWeapon != null)
            {
                _currentWeapon.Attack(view);
            }
            else
            {
                Debug.Log("No hay arma disponible");
            }
        }
    }
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

        model.TakeDamage(amount);

        view.UpdateHealthUI((int)model.CurrentHealth, model.maxHealth);

        Debug.Log("Vida actual del player: " + model.CurrentHealth);

        if (model.IsDead)
        {
            HandleDeath();
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            ReceiveDamage(20);
        }
    }

    private void HandleHealthChanged(int current, int max)
    {
        view.UpdateHealthUI(current, max);
    }

    private void HandleEnergyChanged(float current, float max)
    {
        view.UpdateEnergyUI(current);
    }

    private void HandleDeath()
    {

        view.PlayDeath();

        var config = FindObjectOfType<ConfigSMFinal>();
        if (config != null)
            config.ShowGameOver();

        Debug.Log("El jugador murió");

    }

    public void ManageAttackUi()
    {

        view.UpdateEnergyUI(model.AttackCooldownTimer);

    }
    public void ApplyJumpBoost(float multiplier, float duration)
    {
        StartCoroutine(JumpBoostRoutine(multiplier, duration));
    }

    private IEnumerator JumpBoostRoutine(float multiplier, float duration)
    {

        float originalJumpForce = model.jumpForce;
        model.jumpForce *= multiplier;

        yield return new WaitForSeconds(duration);

        model.jumpForce = originalJumpForce;

    }

    private IEnumerator ShieldRoutine(float duration)
    {

        model.hasShield = true;

        yield return new WaitForSeconds(duration);
        model.hasShield = false;
    }

    public void ApplySpeedBoost(float multiplier, float duration)
    {
        StartCoroutine(SpeedBoostRoutine(multiplier, duration));
    }

    private IEnumerator SpeedBoostRoutine(float multiplier, float duration)
    {

        float originalSpeed = model.moveSpeed;

        model.moveSpeed *= multiplier;

        yield return new WaitForSeconds(duration);

        model.moveSpeed = originalSpeed;

    }

    public void ApplyAttackCooldownBoost(float multiplier, float duration)
    {
        StartCoroutine(AttackCooldownBoostRoutine(multiplier, duration));
    }

    private IEnumerator AttackCooldownBoostRoutine(float multiplier, float duration)
    {
        float originalCooldown = model.attackCooldown;

        model.attackCooldown /= multiplier;

        yield return new WaitForSeconds(duration);

        model.attackCooldown = originalCooldown;
    }

}
