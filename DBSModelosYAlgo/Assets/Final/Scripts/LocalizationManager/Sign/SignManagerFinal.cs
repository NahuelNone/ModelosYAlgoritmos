using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignManagerFinal : MonoBehaviour
{

    public GameObject sign;

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sign.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            sign.SetActive(true);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            sign.SetActive(false);

        }

    }

}
