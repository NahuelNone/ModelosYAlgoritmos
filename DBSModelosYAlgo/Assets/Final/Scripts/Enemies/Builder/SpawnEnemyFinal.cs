using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyFinal : MonoBehaviour
{
    public enum SpawnMode { RoundRobin, RandomNoRepeat, Random }

    [System.Serializable]
    public class SpawnRoute
    {
        public Transform spawnPoint;
        public PatrolPathFinal patrolPath;
    }

    [Header("Prefab")]
    public EnemyFinal enemyPref;

    [Header("Refs escena")]
    public Transform player;

    [Header("Rutas disponibles (cada una: spawn + path)")]
    [SerializeField] private SpawnRoute[] routes;

    [Header("Cantidad a spawnear")]
    [SerializeField] private int spawnCount = 1;

    [Header("Modo")]
    [SerializeField] private SpawnMode spawnMode = SpawnMode.RandomNoRepeat;

    private bool _triggered;
    private int _rrIndex = 0;

    private readonly List<int> _bag = new();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_triggered) return;
        if (!collision.CompareTag("Player")) return;

        _triggered = true;

        for (int i = 0; i < spawnCount; i++)
        {
            var route = PickRoute();
            if (route == null || route.spawnPoint == null)
            {
                Debug.LogWarning("[SpawnEnemyFinal] Ruta o spawnPoint nulo.");
                continue;
            }

            EnemyFinal enemy = Instantiate(enemyPref, route.spawnPoint.position, route.spawnPoint.rotation);

            Transform[] points = route.patrolPath != null ? route.patrolPath.Points : null;
            enemy.Init(player, points); // <-- acá cambia el patrolPath por instancia
        }

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }

    private SpawnRoute PickRoute()
    {
        if (routes == null || routes.Length == 0)
        {
            Debug.LogWarning("[SpawnEnemyFinal] No hay routes asignadas.");
            return null;
        }

        switch (spawnMode)
        {
            case SpawnMode.RoundRobin:
                {
                    var r = routes[_rrIndex];
                    _rrIndex = (_rrIndex + 1) % routes.Length;
                    return r;
                }

            case SpawnMode.Random:
                return routes[Random.Range(0, routes.Length)];

            case SpawnMode.RandomNoRepeat:
            default:
                {
                    if (_bag.Count == 0) RefillAndShuffleBag();

                    int idx = _bag[_bag.Count - 1];
                    _bag.RemoveAt(_bag.Count - 1);

                    return routes[idx];
                }
        }
    }

    private void RefillAndShuffleBag()
    {
        _bag.Clear();
        for (int i = 0; i < routes.Length; i++)
            _bag.Add(i);

        // Fisher–Yates shuffle
        for (int i = 0; i < _bag.Count; i++)
        {
            int j = Random.Range(i, _bag.Count);
            (_bag[i], _bag[j]) = (_bag[j], _bag[i]);
        }
    }
}
