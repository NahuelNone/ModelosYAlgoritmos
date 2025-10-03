    
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    public PlayerModel model;

    public int damage = 10;
    public int heal = 5;
    

    private Rigidbody rb;
    private CapsuleCollider col;

    private void Awake()
    {

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    
        //rb.isKinematic = true;
        //col.isTrigger = false;

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("Pasan Cosas");

        if (model == null) return;
        
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        model.Move(new Vector3(h, 0f, v));

        if (Input.GetKeyDown(KeyCode.Q))
        {

            float j = Input.GetAxisRaw("Jump");
            model.Jump(new Vector3(0f, j, 0f));

        }
       /* if (Input.GetKeyDown(KeyCode.Space)) // click izquierdo dispara
        {
            model.Shoot();
        }*/

        //poner aca on collision enter y todo es
        if (Input.GetKeyDown(KeyCode.J))
        {

            model.TakeDamage(damage);
            Debug.Log("2");

        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            model.heal(heal);
            Debug.Log("3");

        }

        if (Input.GetKeyDown(KeyCode.R))
        {

            model.ResetStats();
            Debug.Log("4");

        }


    }
}
