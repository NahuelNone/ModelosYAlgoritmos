using UnityEngine;

public struct SpawnData
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 direction;
    public float speed;
    public Transform owner;

    public SpawnData(Vector3 position, Quaternion rotation, Vector3 direction, float speed, Transform owner = null)
    {
        this.position = position;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.owner = owner;
    }
}
