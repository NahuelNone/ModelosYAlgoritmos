using UnityEngine;

public class Level1Builder : MonoBehaviour, ILevelBuilder
{
    [Header("Prefabs")]
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private GameObject collectiblePrefab;

    [Header("Root del nivel")]
    [SerializeField] private Transform levelRoot; // Padre de todo

    private Level _level;

    public void Reset()
    {
        // Si ya había un nivel, lo limpiamos
        if (_level != null && _level.root != null)
        {
            Destroy(_level.root.gameObject);
        }

        _level = new Level();
        // Creamos un objeto padre para organizar la jerarquía
        GameObject rootGO = new GameObject("Level1Root");
        rootGO.transform.position = Vector3.zero;

        if (levelRoot != null)
        {
            rootGO.transform.SetParent(levelRoot, false);
        }

        _level.root = rootGO;
    }

    public void BuildGround()
    {
        // Ejemplo: instanciar varias piezas de suelo en línea
        for (int i = 0; i < 20; i++)
        {
            Vector3 pos = new Vector3(i, -1f, 0f);
            GameObject ground = Instantiate(groundPrefab, pos, Quaternion.identity, _level.root.transform);
            _level.grounds.Add(ground);
        }
    }

    public void BuildPlatforms()
    {
        // Plataformas flotantes en posiciones específicas
        Vector3[] platformPositions =
        {
            new Vector3(3, 1, 0),
            new Vector3(6, 2, 0),
            new Vector3(9, 3, 0)
        };

        foreach (var pos in platformPositions)
        {
            GameObject platform = Instantiate(platformPrefab, pos, Quaternion.identity, _level.root.transform);
            _level.platforms.Add(platform);
        }
    }

    public void BuildEnemies()
    {
        // Colocar enemigos en ciertas posiciones
        Vector3[] enemyPositions =
        {
            new Vector3(5, -0.5f, 0),
            new Vector3(10, -0.5f, 0)
        };

        foreach (var pos in enemyPositions)
        {
            GameObject enemy = Instantiate(enemyPrefab, pos, Quaternion.identity, _level.root.transform);
            _level.enemies.Add(enemy);
        }
    }

    //public void BuildCollectibles()
    //{
    //    // Moneditas
    //    Vector3[] collectiblePositions =
    //    {
    //        new Vector3(3, 2f, 0),
    //        new Vector3(6, 3f, 0),
    //        new Vector3(9, 4f, 0)
    //    };
    //
    //    foreach (var pos in collectiblePositions)
    //    {
    //        GameObject col = Instantiate(collectiblePrefab, pos, Quaternion.identity, _level.root.transform);
    //        _level.collectibles.Add(col);
    //    }
    //}

    public Level GetResult()
    {
        return _level;
    }
}
