using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    private int lockpickCount = 0;
    private List<string> treasures = new List<string>();
    private List<GameObject> weapons = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddLockpicks(int amount)
    {
        lockpickCount += amount;
        Debug.Log($"Lockpicks: {lockpickCount}");
        NotifyUIManager();
    }

    public void UseLockpick()
    {
        if (lockpickCount > 0)
        {
            lockpickCount--;
            Debug.Log($"Used a lockpick. Remaining: {lockpickCount}");
            NotifyUIManager();
        }
        else
        {
            Debug.Log("No lockpicks to use!");
        }
    }

    public void AddTreasure(string treasureName)
    {
        treasures.Add(treasureName);
        Debug.Log($"Treasure collected: {treasureName}");
    }

    public void AddWeapon(GameObject weapon)
    {
        weapons.Add(weapon);
        Debug.Log($"Weapon added: {weapon.name}");
    }

    public int GetLockpickCount()
    {
        return lockpickCount;
    }

    private void NotifyUIManager()
    {
        if (FindObjectOfType<LockpickingUIManager>() != null)
        {
            FindObjectOfType<LockpickingUIManager>().UpdateLockpickCountUI();
        }
    }
}