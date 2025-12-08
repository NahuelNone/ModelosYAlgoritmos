// Estrategia
using UnityEngine;

public interface IMovementStrategy
{
    void Move(Transform t);
}

// Estrategia concreta A
public class HorizontalMovement : IMovementStrategy
{
    public float speed = 2f;

    public void Move(Transform t)
    {
        t.Translate(Vector3.right * speed * Time.deltaTime);
    }
}

// Estrategia concreta B
public class ZigZagMovement : IMovementStrategy
{
    public float speed = 2f;
    public float amplitude = 1f;

    private float time;

    public void Move(Transform t)
    {
        time += Time.deltaTime;
        float y = Mathf.Sin(time * speed) * amplitude;
        t.Translate(new Vector3(speed * Time.deltaTime, y * Time.deltaTime, 0));
    }
}

// Contexto
public class Enemy : MonoBehaviour
{
    private IMovementStrategy movementStrategy;

    public void SetMovement(IMovementStrategy strategy)
    {
        movementStrategy = strategy;
    }

    void Update()
    {
        movementStrategy?.Move(transform);
    }
}
