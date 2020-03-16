using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GuardarPartida
{

    //it's static so we can call it from anywhere
    public static void Save(GameObject player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/PlayerData.json";
        FileStream stream;
        if (!File.Exists(path))
        {
            stream = new FileStream(path, FileMode.Create);
        }
        else
        {
            stream = new FileStream(path, FileMode.Open);

        }
        DatosCheckPoint datos = new DatosCheckPoint(player);
        formatter.Serialize(stream, datos);
        stream.Close();
    }

    /// <summary>
    /// Este metodo hace algo
    /// </summary>
    /// <returns></returns>
    public static DatosCheckPoint Load()
    {

        string path = Application.persistentDataPath + "/PlayerData.json";
        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DatosCheckPoint data = formatter.Deserialize(stream) as DatosCheckPoint;
            stream.Close();
            return data;

        }
        else
        {
            Debug.Log("Datos no encontrados");
            return null;

        }
    }
}
