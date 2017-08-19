﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class JsonReader : MonoBehaviour
{
    private string jsonString;
    private static JsonData itemData;

    //Use this for initialization
    void Start()
    {
        test("sheepy.json");
    }

    public static JsonData getJsonFile(string path)
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/JSON files/" + path);
        return itemData = JsonMapper.ToObject(jsonString);
    }

    //path is original json,array is top of json file, needed neededURL is what u want from original json
    //this function gives back a needed json file
    public static JsonData getJsonByName(string path, string array, string neededURL)
    {
        JsonData itemData = getJsonFile(path);

        for (int i = 0; i < itemData[array].Count; i++)
        {
            if (itemData[array][i]["url"].ToString() == neededURL)
                return itemData[array][i]["url"];
        }
        return null;
    }

    public static String getDataFromJson(JsonData json,string array, string attribute)
    {
          return itemData[array][0][attribute].ToString();
    }

    public static JsonData getDataByIndex(string array, int index)
    {
        if (itemData[array][index] != null) return itemData[array][index];
        return null;
    }

    public void test(string url)
    {
        string json = getJsonByName("sheepsDataBase.json", "sheeps", url).ToString();
        json = getJsonFile(json).ToJson();

        //string temp = getDataFromJson(json,"sheepy","timeOfSell");
        Sheep mySheep = new Sheep();
        mySheep = JsonUtility.FromJson<Sheep>(json);
        mySheep.timeOfSell = DateTime.Now.ToString();
        JsonData temp = JsonMapper.ToJson(mySheep);
        File.WriteAllText(Application.dataPath + "/JSON files/" +"sheepy.json", temp.ToString());

        json = getJsonByName("sheepsDataBase.json", "sheeps", url).ToString();
        json = getJsonFile(json).ToJson();
        print(json);
    }
}

[Serializable]
public class Sheep
{
    public string name;
    public int time;
    public string timeOfSell;
}