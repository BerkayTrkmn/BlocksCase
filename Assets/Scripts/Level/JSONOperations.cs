using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONOperations
{
    public static void SaveData(object data, string dataPath)
    {
        File.WriteAllText(dataPath, JsonUtility.ToJson(data));
    }
    public static void LoadData()
    {

    }


}
