using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 序列化和反序列化Json时  使用的是哪种方案
/// </summary>
public enum JsonType
{
    JsonUtlity,
    LitJson,
}

/// <summary>
/// Json数据管理类 主要用于进行 Json的序列化存储到硬盘 和 反序列化从硬盘中读取到内存中
/// </summary>
public class JsonMgr
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance => instance;

    public string SAVE_DIR = "saveData";


    private JsonMgr() { }

    //存储Json数据 序列化
    public void SaveData(object data,string dirName,string fileName, JsonType type = JsonType.LitJson)
    {



        //设置存档总目录
        string save_Dir = Application.persistentDataPath + "/" + SAVE_DIR;
        if (!File.Exists(save_Dir))
        {
            Directory.CreateDirectory(save_Dir);
        }

        //设置单个存档目录
        string save_dirName = save_Dir + "/" + dirName;
        if (!File.Exists(save_dirName))
        {
            Directory.CreateDirectory(save_dirName);
        }

        string systemData = save_dirName + "/" + "Binary";
        if (!File.Exists(systemData))
        {
            Directory.CreateDirectory(systemData);
            string[] sv = Directory.GetFiles(BinaryDataManager.DATA_BINARY_PATH);
            foreach (string s in sv)
            {

                FileInfo fileInfo = new FileInfo(s);
                Copy(BinaryDataManager.DATA_BINARY_PATH + fileInfo.Name, systemData + "/" + fileInfo.Name);
            }


        }

        //设置存档数据
        string path = save_dirName + "/" + fileName + ".json";

        //序列化 得到Json字符串
        string jsonStr = "";
        switch (type)
        {
            case JsonType.JsonUtlity:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }
        Debug.Log(path);
        Debug.Log(jsonStr);
        //把序列化的Json字符串 存储到指定路径的文件中
        File.WriteAllText(path, jsonStr);
    }

    //读取指定文件中的 Json数据 反序列化
    public T LoadData<T>(string dirName,string fileName, JsonType type = JsonType.LitJson) where T : new()
    {

        ////设置存档总目录
        string save_Dir = Application.persistentDataPath + "/" + SAVE_DIR;
        if (!File.Exists(save_Dir))
        {
            Directory.CreateDirectory(save_Dir);
        }

        ////设置单个存档目录
        string save_dirName = save_Dir + "/" + dirName;
        if (!File.Exists(save_dirName))
        {
            Directory.CreateDirectory(save_dirName);
        }

        string systemData = save_dirName + "/" + "Binary";
        if (!File.Exists(systemData))
        {
            Directory.CreateDirectory(systemData);
            string[] sv = Directory.GetFiles(BinaryDataManager.DATA_BINARY_PATH);
            foreach (string s in sv)
            {
                FileInfo fileInfo = new FileInfo(s);
                Copy(BinaryDataManager.DATA_BINARY_PATH + fileInfo.Name, systemData + "/" + fileInfo.Name);
            }

        }

        //确定从哪个路径读取
        //首先先判断 默认数据文件夹中是否有我们想要的数据 如果有 就从中获取
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        //先判断 是否存在这个文件
        //如果不存在默认文件 就从 读写文件夹中去寻找
        if(!File.Exists(path))
        {
            path = save_dirName + "/" + fileName + ".json";
        }
        //如果读写文件夹中都还没有 那就返回一个默认对象
        if (!File.Exists(path))
        {
            return new T();
        }

        //进行反序列化
        string jsonStr = File.ReadAllText(path);
        //数据对象
        T data = default(T);
        switch (type)
        {
            case JsonType.JsonUtlity:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }

        //把对象返回出去
        return data;
    }



    public void Copy(string pStrFilePath, string pPerFilePath)
    {

        MonoManger.Instance.StartCoroutine(Load(pStrFilePath, pPerFilePath));
    }

    IEnumerator Load(string pStrFilePath, string pPerFilePath)
    {
        WWW www = new WWW(pStrFilePath);
        Debug.Log(www);
        yield return www;
        if (www.isDone)
        {
            string path = pPerFilePath;
            File.WriteAllBytes(path, www.bytes);
        }
    }
}
