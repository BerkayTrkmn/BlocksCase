using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONOperations
{
    public static void SaveData(object data, string fileName)
    {
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.dataPath + "/"+fileName+".json", json);
    }

}
