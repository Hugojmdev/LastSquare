using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public abstract class DataMgr {

    static string path = Application.persistentDataPath + "/the-last-square.save";
    static BinaryFormatter formatter = new BinaryFormatter();

    
    public void Save(Data data){
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public Data GetData(){
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