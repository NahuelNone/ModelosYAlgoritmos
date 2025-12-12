public class LevelDirector
{
    private ILevelBuilder _builder;

    public LevelDirector(ILevelBuilder builder)
    {
        _builder = builder;
    }

    public Level ConstructBasicLevel()
    {
        _builder.Reset();
        _builder.BuildGround();
        _builder.BuildPlatforms();
        _builder.BuildEnemies();
        //_builder.BuildCollectibles();
        return _builder.GetResult();
    }
}
