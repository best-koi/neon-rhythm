
using UnityEngine;
/// <summary>
/// IDataPersistence has SaveData and LoadData to store and retrieve data from external files.
/// This is a helper interface to connect serializable data along with using that serializable data to record changes to default values.
/// Use a data container class T as an object to store and hold serialized data easier. 
/// </summary>
public interface IDataPersistence
{
    void SaveData<T>(string path, T data);
    T LoadData<T>(string path);
    T LoadData<T>(TextAsset asset);

    void DeleteData(string path);
    void CheckGameDataDirectory();
}
