using UnityEngine;

[RequireComponent(typeof(PlayerViewFinal))]
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
        LeerInputMovimiento();
        ManejarMovimiento();
        ManejarSalto();
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
        if (Input.GetButtonDown("Jump")) // por defecto, tecla Space
        {
            if (model.CanJump())
            {
                model.OnJump();
                view.Jump(model.jumpForce);
            }
        }
    }

    // Manejo MUY simple de suelo usando tag "Ground"
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
