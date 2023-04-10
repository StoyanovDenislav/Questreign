using System;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public static class SaveSystem
{
    public static void SaveNumberString(string str)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/string.dat";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        StringData allData = new StringData(str);

        binaryFormatter.Serialize(fileStream, allData);

        fileStream.Close();
    }


    public static StringData LoadNumberString()
    {
        string path = Application.persistentDataPath + "/string.dat";

        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            StringData data = binaryFormatter.Deserialize(fileStream) as StringData;

            fileStream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in: " + path);

            return null;
        }
    }
}