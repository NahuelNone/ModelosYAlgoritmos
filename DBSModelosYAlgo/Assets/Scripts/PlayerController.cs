
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

        rb.isKinematic = true;
        col.isTrigger = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (model == null) return; 
        
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            model.Move(new Vector3 (h, 0f, v));

        if (Input.GetKeyDown(KeyCode.J)) //poner aca on collision enter y todo eso
            model.TakeDamage(damage);

        if (Input.GetKeyDown(KeyCode.K))
            model.heal(heal);

        if (Input.GetKeyDown(KeyCode.R))
            model.ResetStats();
        

    }
}
