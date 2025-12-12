using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class SpawnEnemyFinal : MonoBehaviour
{

    public EnemyFinal enemyPref;

    public EnemyFinal patrolMeleePrototype = null;

    public GameObject spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            var enemyFinal = Instantiate(enemyPref).SetColor(Color.red).SetPosition(spawnPoint).SetScale(3, 1, 4);

            this.GetComponent<BoxCollider2D>().enabled = false;

        }

    }

}
