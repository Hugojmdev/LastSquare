using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager {

    static string path = Application.persistentDataPath + "/the-last-square.save";
    static BinaryFormatter formatter = new BinaryFormatter();


    public static void Save(Data data){
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, data);
        stream.Close();
        
    }

    public static Data Load(){
        if(File.Exists(path)){
            FileStream stream = new FileStream(path, FileMode.Open);
            Data data = (Data)formatter.Deserialize(stream);
            stream.Close();
            return data;
        } else {
            Debug.Log("Save file not found in: " + path);
            return new Data(); // Return empty data
        }
    }

}