using UnityEngine;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

public static class SaveDataFile
{
    public static void SaveSaveDataData(SaveData saveData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + @"/gamesaves.meme";

        FileStream fs = new FileStream(path, FileMode.Create);

        SaveDataData data = new SaveDataData(saveData);

        bf.Serialize(fs, data);
        fs.Close();
    }

    public static SaveDataData LoadSaveData()
    {
        string path = Application.persistentDataPath + @"/gamesaves.meme";
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            SaveDataData data = bf.Deserialize(fs) as SaveDataData;

            fs.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
