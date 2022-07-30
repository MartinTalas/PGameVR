using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed class JsonParser
{
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------[ SINGLETON PATTERN ]--------------------------------------------------------------------

    private static readonly Lazy<JsonParser> lazy = new Lazy<JsonParser>(() => new JsonParser());

    public static JsonParser Instance { get { return lazy.Value; } }

    private JsonParser()
    {
        
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
    //---------------------------------------------------------------------[ PARSING FUNCTIONS ]--------------------------------------------------------------------

    public void toJson<T>(T obj, string filename) 
    {
        string json = JsonUtility.ToJson(obj, true);

        string path = Application.persistentDataPath + "/" +filename + ".json";
        File.WriteAllText(path, json);
    }

    public T toObject<T>(string filename) 
    {
        string json;
        string path = Application.persistentDataPath + "/" + filename + ".json";

        json = File.ReadAllText(path);

        T obj;
        obj = JsonUtility.FromJson<T>(json);
        
        return obj;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------
}
