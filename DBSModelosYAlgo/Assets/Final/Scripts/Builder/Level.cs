using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public GameObject root; // GameObject padre de todo el nivel

    public List<GameObject> grounds = new List<GameObject>();
    public List<GameObject> platforms = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> collectibles = new List<GameObject>();
}
