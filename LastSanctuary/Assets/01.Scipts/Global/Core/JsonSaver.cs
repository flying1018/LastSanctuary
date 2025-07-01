using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSaver
{
    private static string savePath;
    public static void SaveData(string json)
    {
        File.WriteAllText(savePath, json);
    }
}
