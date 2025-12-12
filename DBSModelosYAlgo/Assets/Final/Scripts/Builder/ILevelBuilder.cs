public interface ILevelBuilder
{
    void Reset();              // Limpiar nivel anterior
    void BuildGround();        // Suelo/base
    void BuildPlatforms();     // Plataformas
    void BuildEnemies();       // Enemigos
    //void BuildCollectibles();  // Ítems/monedas
    Level GetResult();         // Devolver el Level construido
}
