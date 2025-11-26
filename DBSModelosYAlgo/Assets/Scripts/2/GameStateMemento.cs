using UnityEngine;

[System.Serializable]
public class GameStateMemento
{
    public int level;
    public int coins;
    public int lives;

    public GameStateMemento(int level, int coins, int lives)
    {
        this.level = level;
        this.coins = coins;
        this.lives = lives;
    }
}

public class GameState : MonoBehaviour
{
    public int level;
    public int coins;
    public int lives;

    public GameStateMemento CreateMemento()
    {
        return new GameStateMemento(level, coins, lives);
    }

    public void Restore(GameStateMemento memento)
    {
        level = memento.level;
        coins = memento.coins;
        lives = memento.lives;
    }
}

public static class GameStateStorage
{
    private const string Key = "GAME_STATE";

    public static void Save(GameStateMemento memento)
    {
        string json = JsonUtility.ToJson(memento);
        PlayerPrefs.SetString(Key, json);
        PlayerPrefs.Save();
    }

    public static GameStateMemento Load()
    {
        if (!PlayerPrefs.HasKey(Key)) return null;
        string json = PlayerPrefs.GetString(Key);
        return JsonUtility.FromJson<GameStateMemento>(json);
    }
}
