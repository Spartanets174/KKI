using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class saveSystem {
    public static void savePlayer(string Name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/player.fun";
        FileStream stream = new FileStream(path,  FileMode.Create);
        string nameToSave = Name;
        formatter.Serialize(stream, nameToSave);
        stream.Close();
    }

    public static string LoadPlayer()
    {
        string path = Application.dataPath + "/player.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string nameToLoad = (string)formatter.Deserialize(stream);
            stream.Close();
            return nameToLoad;
        }
        else
        {        
            return "";
        }

    }
}
