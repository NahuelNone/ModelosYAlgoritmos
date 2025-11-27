using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Velocidad de movimiento hacia adelante/atrás
    public float moveSpeed = 5f;

    // Velocidad de rotación
    public float rotationSpeed = 100f;

    // Referencia al Rigidbody
    private Rigidbody rb;

    private void Start()
    {
        // Tomamos el Rigidbody del objeto (debe estar en el mismo GameObject)
        rb = GetComponent<Rigidbody>();

        // Aseguramos que no se caiga por gravedad (ya que es top-down)
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void FixedUpdate()
    {
        // Tomamos los inputs
        float moveInput = Input.GetAxis("Vertical");   // W/S o flechas ↑ ↓
        float rotationInput = Input.GetAxis("Horizontal"); // A/D o flechas ← →

        // Calculamos el movimiento (hacia adelante según la dirección del tanque)
        Vector3 movement = transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime;

        // Movemos al tanque usando el Rigidbody
        rb.MovePosition(rb.position + movement);

        // Calculamos la rotación
        Quaternion turn = Quaternion.Euler(0f, rotationInput * rotationSpeed * Time.fixedDeltaTime, 0f);

        // Rotamos al tanque
        rb.MoveRotation(rb.rotation * turn);
    }
}