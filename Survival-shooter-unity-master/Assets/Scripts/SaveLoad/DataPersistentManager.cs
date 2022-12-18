using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistentManager : MonoBehaviour
{
    [Header("File storage config")]
    [SerializeField] private string fileName;

    public GameData gamedata;
    public static DataPersistentManager instance { get; private set; }

    private List<IDataPersistent> dataPersistentObjects;

    private FileDataHandler dataHandler;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("found more than once instance of DataPeresistanceManager");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistentObjects = FindAllDataPersistentObjeccts();

        Debug.Log("Application path : " + Application.persistentDataPath);

        Invoke("LoadGame", 2.0f);
    }

    private List<IDataPersistent> FindAllDataPersistentObjeccts()
    {
        IEnumerable<IDataPersistent> dataObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistent>();

        return new List<IDataPersistent>(dataObjects);
    }

    public void NewGame()
    {
        this.gamedata = new GameData();
    }

    public void LoadGame()
    {
        //todo -- load a gameData using file handler
        this.gamedata = dataHandler.Load();

        // if there is no data saved then instantiate new game data

        if(this.gamedata == null)
        {
            NewGame();
        }

        //ToDo - Pushed the loaded data to all other objects that needed

        foreach (IDataPersistent dataPersistentObject in dataPersistentObjects)
        {
            dataPersistentObject.LoadData(gamedata);
        }
    }

    public void SaveGame()
    {
        //TODO - pass the data to other scripts so they can update it
        foreach (IDataPersistent dataPersistentObject in dataPersistentObjects)
        {
            dataPersistentObject.SaveData(ref gamedata);
        }

        //ToDo - Save that data in to file using data handler
        dataHandler.Save(gamedata);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
