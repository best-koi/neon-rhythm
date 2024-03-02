using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// JSonDataPersistence is the IDataPersistence grounded implementation that can accept any data type T and serialize it.
/// It will SaveData and LoadData to and from a specified path within the Directory.GetCurrentDirectory() directory.
/// There is some protections in place so data is backed up if saving goes wrong, but it is only one backup for each file.
/// </summary>
public class JsonDataPersistence : IDataPersistence
{
    /// <summary>
    /// SaveData will take any data type T and convert it into a Json stored in the GameData folder within the game's directory.
    /// The path you provide will be the name of the file and its file type to store within this GameData folder.
    /// You do not need anything more than "/filename.json" for the path. It will automatically find the game's directory for you.
    /// If saving fails, it will make a temporary file of the previous settings if there were any, but there will only be 1 backup.
    /// </summary>
    /// <typeparam name="T">Whatever container class you use to store the serializable data in.</typeparam>
    /// <param name="path">"/filename.json" should be all you need.</param>
    /// <param name="data">Fields/Properties in this class should be public or tagged [Newtonsoft.Json.JsonIgnore] on unserializable properties.</param>
    public void SaveData<T>(string path, T data)
    {
        string savePath = Directory.GetCurrentDirectory() + "/GameData/" + path;

        CheckGameDataDirectory();

        //If existing data is found, we use it as our backup. Delete old backup data if it exists for some reason.
        //Doesn't matter if it is null, since on successful save it will be replaced and on failure we catch it and delete it.
        DeleteData(path + ".tmp");
        if (File.Exists(savePath)) { File.Copy(savePath, savePath + ".tmp"); }

        try
        {
            DeleteData(path);
            using FileStream stream = File.Create(savePath);
            stream.Close();
            File.WriteAllText(savePath, JsonConvert.SerializeObject(data));
            DeleteData(path + ".tmp");
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to save data: {e.Message} {e.StackTrace}");

            //If saving went wrong, we check to see if there is a backup save file to use in its place.
            //We also check whatever JSON is output in case it did save and called a different error unrelated to the files.
            T saveddata = JsonConvert.DeserializeObject<T>(File.ReadAllText(savePath));
            T backupdata = JsonConvert.DeserializeObject<T>(File.ReadAllText(savePath + ".tmp"));
            
            //File validations
            if (!File.Exists(savePath)) { return; }
            if (saveddata != null) { return; }
            DeleteData(path);
            if (!File.Exists(savePath + ".tmp")) { return; }
            if (backupdata == null) { DeleteData(path + ".tmp"); return; }

            File.Copy(savePath + ".tmp", savePath);
            DeleteData(path + ".tmp");
        }
    }



    /// <summary>
    /// LoadData will take an existing Json at the path provided and convert it into data type T for use within the game.
    /// The path you provide will be the name of the file and its file type to load from within this GameData folder.
    /// You do not need anything more than "/filename.json" for the path. It will automatically find the game's directory for you.
    /// If loading fails, it will try to look for a backup file if one exists, though it is unlikely that would happen.
    /// </summary>
    /// <typeparam name="T">Whatever container class you use to store the serializable data in.</typeparam>
    /// <param name="path">"/filename.json" should be all you need.</param>
    /// <returns></returns>
    /// <exception cref="FileNotFoundException"></exception>
    public T LoadData<T>(string path)
    {
        string savePath = Directory.GetCurrentDirectory() + "/GameData/" + path;

        CheckGameDataDirectory();

        if (!File.Exists(savePath))
        {
            Debug.LogError($"Cannot load file at {savePath}. File does not exist.");
            throw new FileNotFoundException($"There is no file at {savePath}");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(savePath));
            return data == null ? throw new NullReferenceException($"There is no data in {savePath}") : data;
        }
        catch (Exception e) 
        {
            Debug.LogError($"Unable to load data: {e.Message} {e.StackTrace}");

            if (!File.Exists(savePath + ".tmp"))
            {
                throw e;
            }

            T backupdata = JsonConvert.DeserializeObject<T>(File.ReadAllText(savePath + ".tmp"));
            if (backupdata == null)
            {
                DeleteData(path + ".tmp");
                throw e;
            }

            return backupdata;
        }
    }

    public T LoadData<T>(TextAsset asset)
    {
        try
        {
            T data = JsonConvert.DeserializeObject<T>(asset.text);
            return data == null ? throw new NullReferenceException($"There is no data in {asset}") : data;
        } 
        catch (Exception e)
        {
            Debug.LogError($"Unable to load data: {e.Message} {e.StackTrace}");
            throw e;
        }
    }

    public void DeleteData(string path) 
    {
        string savePath = Directory.GetCurrentDirectory() + "/GameData/" + path;
        if (File.Exists(savePath)) { File.Delete(savePath); }
    }

    public void CheckGameDataDirectory()
    {
        try
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "/GameData"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/GameData");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Unable to create save directory: {e.Message} {e.StackTrace}");
        }
    }
}
