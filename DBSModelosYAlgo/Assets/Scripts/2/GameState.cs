using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;

public abstract class GameState : MonoBehaviour
{

    protected GameStateMemento gameStateMemento;

    protected abstract void BeRewind(GameStateStorage wrappers);

    public abstract IEnumerator StartToRec();

    protected virtual void Awake()
    {

        gameStateMemento = new GameStateMemento();

        StartCoroutine(StartToRec());

    }

    public void Action()
    {

        if (gameStateMemento.MemoriesQuantity() <= 0) return;

        BeRewind(gameStateMemento.Remember());

    }

}
