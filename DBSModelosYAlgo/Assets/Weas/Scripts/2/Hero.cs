using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : GameState
{

    public float life;
    public float gold;

    // Update is called once per frame
    void Update()
    {

        transform.position += transform.forward * 5f * Input.GetAxisRaw("Vertical") * Time.deltaTime;

        transform.forward += transform.right * 3 * Input.GetAxisRaw("Horizontal") * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.F))
        {

            life -= 10;
            gold *= Random.Range(1.5f, 3);

            Debug.Log(life + " <=====> " + gold);

        }

    }

    public override IEnumerator StartToRec()
    {

        while (true)
        {

            gameStateMemento.Rec(new object[] { transform.position, transform.rotation, life, gold });

            yield return new WaitForSeconds(0.1f);

        }

    }

    protected override void BeRewind(GameStateStorage wrappers)
    {

        life = (float)wrappers.parameters[2];
        gold = (float)wrappers.parameters[3];
        transform.position = (Vector3)wrappers.parameters[0];
        transform.rotation = (Quaternion)wrappers.parameters[1];

    }

}
