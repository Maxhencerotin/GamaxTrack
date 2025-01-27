using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SavedData
{
    public float[] bestTime;
    public List<float>[] ghostXList;
    public List<float>[] ghostYList;
    public List<float>[] ghostRotationList;

    //defauld initializer
    public SavedData()
    {
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

    private static readonly string filePath = Path.Combine(Application.persistentDataPath, GameData.SAVEDDATA_FILENAME);

    public static void SaveData(SavedData data)
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, data);
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
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    return (SavedData)formatter.Deserialize(stream);
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
}
