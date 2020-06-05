using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


/// <summary>
/// Se encarga de guardar y cargar la partida del jugador, para mantener ciertos datos
/// </summary>
public static class GuardarPartida
{
    /// <summary>
    /// Este método guarda la partida
    /// </summary>
    /// <param name="player"> jugador</param>
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
    /// Este metodo carga la partida
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