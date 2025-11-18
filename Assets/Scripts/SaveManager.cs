using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

[System.Serializable]
public class SavedData  //MODIFY MigrateData() and CURRENT_VERSION when modifying the structure of this class !!!
{
    public int version;
    public float[] bestTime;
    public List<float>[] ghostXList;
    public List<float>[] ghostYList;
    public List<float>[] ghostRotationList;

    //defauld initializer
    public SavedData()
    {
        version = SaveManager.CURRENT_VERSION;
        bestTime = new float[GameData.NUMBER_LEVEL];
        ghostXList = new List<float>[GameData.NUMBER_LEVEL];
        ghostYList = new List<float>[GameData.NUMBER_LEVEL];
        ghostRotationList = new List<float>[GameData.NUMBER_LEVEL];

        for (int i = 0; i < GameData.NUMBER_LEVEL; i++){ 
            bestTime[i] = float.PositiveInfinity;
            ghostXList[i] = new List<float>();
            ghostYList[i] = new List<float>();
            ghostRotationList[i] = new List<float>();
        }
    }
}

public static class SaveManager
{
    public const int CURRENT_VERSION = 3;

    private static readonly string filePath = Path.Combine(Application.persistentDataPath, GameData.SAVEDDATA_FILENAME);

    private static SavedData MigrateData(SavedData oldData)
    {
        SavedData newData = oldData;

        if (newData.version == 1)   //Migrate from v1 to v2
        {
            newData.version = 2;
            newData.bestTime = oldData.bestTime;
            newData.ghostXList = oldData.ghostXList;
            newData.ghostYList = oldData.ghostYList;
            newData.ghostRotationList = oldData.ghostRotationList;
        }
        if (newData.version == 2)   //Migrate from v2 to v3
        {
            newData.version = 3;
            newData.bestTime = oldData.bestTime;
            newData.ghostXList = oldData.ghostXList;
            newData.ghostYList = oldData.ghostYList;
            newData.ghostRotationList = oldData.ghostRotationList;
        }

        if (newData.version != CURRENT_VERSION)
        {
            Debug.Log("Error migrating Data");
        }
        else
        {
            Debug.Log("Data migration completed!");
        }

        return newData;
    }

    public static void SaveData(SavedData data)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (MemoryStream memStream = new MemoryStream())
            {
                formatter.Serialize(memStream, data);
                byte[] encryptedData = EncryptData(memStream.ToArray());
                File.WriteAllBytes(filePath, encryptedData);
            }
            Debug.Log("Data successfully saved.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error saving data: " + ex.Message);
        }
    }

    public static SavedData LoadData()
    {
        try
        {
            if (File.Exists(filePath))
            {
                //File.Delete(filePath); //!!! THIS LINE IS TO UNCOMMENT IF WE WANT TO DELETE ALL THE DATA


                byte[] encryptedData = File.ReadAllBytes(filePath);
                byte[] decryptedData = DecryptData(encryptedData);

                BinaryFormatter formatter = new BinaryFormatter();

                using (MemoryStream memStream = new MemoryStream(decryptedData))
                {
                    SavedData loadedData = (SavedData)formatter.Deserialize(memStream);

                    if (loadedData.version != CURRENT_VERSION)
                    {
                        Debug.Log("Old version detected! Migrating...");
                        loadedData = MigrateData(loadedData);
                        SaveData(loadedData);
                    }

                    return loadedData;
                }
            }
            else
            {
                //No savedData found (because first launch of the game) --> returning default
                Debug.Log("No SavedData yet");
                return new SavedData(); // Retourne SavedData object with default values (see constructor)
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error loading data: " + ex.Message);
            return null;
        }
    }



    private static readonly string encryptionKey = "M73vcLD0sGQMdZ50scPF6qm7GSm820Dl";

    public static byte[] EncryptData(byte[] data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aes.IV = new byte[16]; // IV nul pour simplifier, mais peut être aléatoire

            using (MemoryStream memStream = new MemoryStream())
            using (CryptoStream crypStream = new CryptoStream(memStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                crypStream.Write(data, 0, data.Length);
                crypStream.FlushFinalBlock();
                return memStream.ToArray();
            }
        }
    }

    public static byte[] DecryptData(byte[] data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey);
            aes.IV = new byte[16];

            using (MemoryStream memStream = new MemoryStream())
            using (CryptoStream crypStream = new CryptoStream(memStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                crypStream.Write(data, 0, data.Length);
                crypStream.FlushFinalBlock();
                return memStream.ToArray();
            }
        }
    }

}
