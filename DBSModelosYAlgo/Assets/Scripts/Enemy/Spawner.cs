using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Builder

{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Enemy _enemyPrefab;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var builder = new EnemyBuilder(_enemyPrefab)
                    .SetPosition(Random.Range(-10, 10f), Random.Range(1, 5f), Random.Range(-10f, 10f))
                    .SetColor(Random.ColorHSV())
                    .SetScale(new Vector3(Random.Range(1, 2), Random.Range(2, 5), 2))
                    .SetMaxLife(Random.Range(1, 101));

                builder.Done();

                //var enemy = Instantiate(_enemyPrefab);

                

              /*  enemy.GetComponent<Renderer>().material.color = Random.ColorHSV();

                enemy.MaxLife = Random.Range(1, 101);

                enemy.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                
                enemy.transform.localScale = new Vector3(Random.Range(0.1f, 5f), Random.Range(0.1f , 5f), Random.Range(0.1f, 5f));*/


            }
        }
    }
}
