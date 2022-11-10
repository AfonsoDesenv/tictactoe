using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string save_file_name = "/settings.ttt";

    public static void SaveSettings (SettingsUI settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + save_file_name;
        FileStream stream = new FileStream(path, FileMode.Create);
        Settings data = new Settings(settings);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Settings LoadSettings () 
    {
        string path = Application.persistentDataPath + save_file_name;
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Settings data = formatter.Deserialize(stream) as Settings;

            stream.Close();

            return data;
        }
        return null;
    }
}
