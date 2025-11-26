using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [System.Serializable]
    public struct ScreenEntry
    {
        public ScreenId id;
        public GameObject screenObject;
    }

    [SerializeField] private ScreenEntry[] screens;

    // Look Up Table
    private Dictionary<ScreenId, GameObject> screenTable;

    void Awake()
    {
        screenTable = new Dictionary<ScreenId, GameObject>();

        foreach (var entry in screens)
        {
            if (!screenTable.ContainsKey(entry.id))
            {
                screenTable.Add(entry.id, entry.screenObject);
            }
        }
    }

    public void Show(ScreenId id)
    {
        // Apago todas
        foreach (var kvp in screenTable)
        {
            kvp.Value.SetActive(false);
        }

        // Prendo solo la pedida
        if (screenTable.TryGetValue(id, out GameObject screen))
        {
            screen.SetActive(true);
        }
        else
        {
            Debug.LogError($"Screen {id} no encontrada en el ScreenManager");
        }
    }
}
