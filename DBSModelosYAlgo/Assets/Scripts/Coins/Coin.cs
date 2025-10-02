
using UnityEngine;
//[RequireComponent (typeof(Collider))]
public class Coin : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    [SerializeField] private float spinSpeed = 90f;

    private void Reset()
    {
        var c = GetComponent<Collider>();
        c.isTrigger = true;
    }

    private void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
         PlayerModel model = other.GetComponent<PlayerModel>();

        if (model != null)
            model = other.GetComponent<PlayerModel>();
        
        if (model != null )
        {
            model.AddCoin(amount);
            Destroy(gameObject);
        }
    }
}
