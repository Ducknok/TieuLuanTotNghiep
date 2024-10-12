using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

[Serializable]
public class SaveData
{
    //ITEMS
    public List<int> goToAddID = new List<int>();
    public List<string> inventoryItemsName = new List<string>();
    public List<int> inventoryItemsAmount = new List<int>();

    //Player
    public bool playerUnlockDash;
    public bool playerUnlockAxeThrowing;
}
public class GameData : MonoBehaviour
{
    [SerializeField] private static GameData instance;
    public static GameData Instance => instance;
    public SaveData saveData;
    

    private void Awake()
    {
        if(GameData.instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            GameData.instance = this;
        }
        else if(GameData.instance != null)
        {
            Destroy(GameData.instance.gameObject);
            GameData.instance = this;
        }
        if(File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            this.Load();
        }
        else
        {
            this.Save();
        }
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Create);
        SaveData data = new SaveData();
        data = this.saveData;
        formatter.Serialize(file, data);
        file.Close();
        Debug.Log("Data Saved");
    }

    public void Load()
    {
       if(File.Exists(Application.persistentDataPath + "/player.dat"))
       {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/player.dat", FileMode.Open);
            this.saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            Debug.Log("Data Loaded");
       }
    }

    public void ClearData()
    {
        if(File.Exists(Application.persistentDataPath + "/player.dat"))
        {
            File.Delete(Application.persistentDataPath + "/player.dat");
        }
    }
    public void ClearAllDataList()
    {
        this.saveData.goToAddID.Clear();
        this.saveData.inventoryItemsName.Clear();
        this.saveData.inventoryItemsAmount.Clear();
        this.saveData.playerUnlockDash = false;
        this.saveData.playerUnlockAxeThrowing = false;
        this.Save();
    }
}
