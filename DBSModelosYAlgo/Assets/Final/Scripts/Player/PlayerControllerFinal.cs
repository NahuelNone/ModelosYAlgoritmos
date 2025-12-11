using UnityEngine;

[RequireComponent(typeof(PlayerView))]
public class PlayerControllerFinal : MonoBehaviour
{
    [Header("MVC")]
    public PlayerViewFinal view;
    public PlayerModelFinal model = new PlayerModelFinal();

    [Header("Ground Check (opcional, simple por tag)")]
    public string groundTag = "Ground";

    private float _horizontalInput;

    private void Awake()
    {
        if (view == null)
            view = GetComponent<PlayerViewFinal>();
    }

    private void Update()
    {
        // 🔽 Actualizamos cooldown de ataque
        model.TickAttackCooldown(Time.deltaTime);

        LeerInputMovimiento();
        ManejarMovimiento();
        ManejarSalto();
        ManejarAtaque();   // 🔽 nuevo
    }

    private void LeerInputMovimiento()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal"); // A/D o flechas
    }

    private void ManejarMovimiento()
    {
        view.Move(_horizontalInput, model.moveSpeed);
    }

    // 🔴 CAMBIO: salto ahora con W
    private void ManejarSalto()
    {
        if (Input.GetKeyDown(KeyCode.W))   // antes: Input.GetButtonDown("Jump")
        {
            if (model.CanJump())
            {
                model.OnJump();
                view.Jump(model.jumpForce);
            }
        }
    }

    // 🟢 NUEVO: ataque con barra espaciadora
    private void ManejarAtaque()
    {
        if (Input.GetKeyDown(KeyCode.Space) && model.CanAttack())
        {
            model.OnAttack();

            // Vista (animación, etc.)
            view.Attack();

            // Lógica actual (más adelante armas de verdad)
            Debug.Log("ATAQUE! (acá después voy a disparar el arma actual)");
        }
    }

    // Suelo por tag, igual que antes
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
