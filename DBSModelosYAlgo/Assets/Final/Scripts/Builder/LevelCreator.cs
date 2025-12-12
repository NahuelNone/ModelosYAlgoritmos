using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private MonoBehaviour builderBehaviour; // arrastrás Level1Builder, Level2Builder, etc.
    private ILevelBuilder _builder;
    private LevelDirector _director;
    private Level _currentLevel;

    private void Awake()
    {
        _builder = builderBehaviour as ILevelBuilder;

        if (_builder == null)
        {
            Debug.LogError("El builder asignado no implementa ILevelBuilder");
            return;
        }

        _director = new LevelDirector(_builder);
    }

    private void Start()
    {
        // Podés llamar esto al entrar a la escena, o desde un GameManager
        _currentLevel = _director.ConstructBasicLevel();
    }

    // Ejemplo: limpiar nivel si querés cambiarlo luego
    public void RebuildLevel()
    {
        _currentLevel = _director.ConstructBasicLevel();
    }
}
