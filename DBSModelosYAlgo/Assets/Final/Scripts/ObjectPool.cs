using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly T _prefab;
    private readonly Queue<T> _pool = new Queue<T>();
    private readonly Transform _parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        _prefab = prefab;
        _parent = parent;
        for (int i = 0; i < initialSize; i++)
        {
            var obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T Get()
    {
        if (_pool.Count == 0)
        {
            var obj = GameObject.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }

        var instance = _pool.Dequeue();
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void Release(T instance)
    {
        instance.gameObject.SetActive(false);
        _pool.Enqueue(instance);
    }
}
