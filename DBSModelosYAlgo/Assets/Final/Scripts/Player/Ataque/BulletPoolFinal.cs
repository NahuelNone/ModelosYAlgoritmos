using System.Collections.Generic;
using UnityEngine;

public class BulletPoolFinal : MonoBehaviour
{
    [Header("Pool de balas")]
    public BulletFinal bulletPrefab;
    public int initialSize = 20;

    private Queue<BulletFinal> _pool;

    private void Awake()
    {
        _pool = new Queue<BulletFinal>();

        for (int i = 0; i < initialSize; i++)
        {
            CrearNuevaBala();
        }
    }

    private BulletFinal CrearNuevaBala()
    {
        BulletFinal bulletFinal = Instantiate(bulletPrefab, transform);
        bulletFinal.gameObject.SetActive(false);
        _pool.Enqueue(bulletFinal);
        return bulletFinal;
    }

    public BulletFinal GetBullet()
    {
        if (_pool.Count == 0)
        {
            CrearNuevaBala();
        }

        BulletFinal BulletFinal = _pool.Dequeue();
        BulletFinal.gameObject.SetActive(true);
        return BulletFinal;
    }

    public void ReturnBullet(BulletFinal bulletFinal)
    {
        bulletFinal.gameObject.SetActive(false);
        _pool.Enqueue(bulletFinal);
    }
}
