using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
//contains file name and directory path
public class FileDataHandler 
{
    private string dirPath = "";
    private string fileName = "";

    public FileDataHandler(string _dataDirectoryPath, string _fileName)
    {
        this.dirPath = _dataDirectoryPath;
        this.fileName = _fileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dirPath, fileName);
        GameData loadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                //Load serialized data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception ex)
            {
                Debug.LogError("Error occure during load file: " + fullPath + "\n" + ex);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dirPath, fileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            //write the serialised data to file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception ex)
        {

            Debug.LogError("Error to save data in file: " + fullPath + "\n" + ex);
        }
    }
}
