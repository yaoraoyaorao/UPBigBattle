using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 2进制数据管理器
/// </summary>
public class BinaryDataManager
{
    private byte[] bytes;

    //二进制数据存放位置
    public static string DATA_BINARY_PATH = Application.streamingAssetsPath + "/Binary/";

    /// <summary>
    /// 用于存储Excel中你的数据
    /// </summary>
    private Dictionary<string, object> tableDic = new Dictionary<string, object>();

    /// <summary>
    /// 数据存储的位置
    /// </summary>
    private static string SAVE_PATH = Application.persistentDataPath + "/Data/";

    private static BinaryDataManager instance = new BinaryDataManager();
    public static BinaryDataManager Instance => instance;

    private BinaryDataManager()
    {
        //InitDatas();
    }

    public void InitDatas()
    {
        //LoadTable<StaffInfoContainer, StaffInfo>();
        //LoadTable<OccupationContainer, Occupation>();
        //LoadTable<GamePlatformContainer, GamePlatform>();
        //LoadTable<GameTypeContainer, GameType>();
        //LoadTable<AudienceContainer, Audience>();
        //LoadTable<MailInfoContainer, MailInfo>();
        //LoadTable<EventCollectionContainer, EventCollection>();
    }


    /// <summary>
    /// 存储类对象数据
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="fileName"></param>
    public void Save(object obj, string fileName)
    {
        //先判断路径文件夹有没有
        if (!Directory.Exists(SAVE_PATH))
            Directory.CreateDirectory(SAVE_PATH);

        using (FileStream fs = new FileStream(SAVE_PATH + fileName + ".GameData", FileMode.OpenOrCreate, FileAccess.Write))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, obj);
            fs.Close();
        }
    }



    /*public void LoadTable<T,K>()
    {
        MonoManger.Instance.StartCoroutine(LoadAssetFromLocal<T,K>());
    }*/

   /* public void LoadTableForPer<T, K>()
    {
        //读取excel对应的二进制文件
        string path = Application.persistentDataPath + "/" + 
                      JsonMgr.Instance.SAVE_DIR + "/" + 
                      SaveController.Instance.playerData.studioName + "/" +
                      "Binary" + "/" +
                      typeof(K).Name + ".GameData";

        using (FileStream fs = File.Open(path, FileMode.Open, FileAccess.Read))
        {

            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            int index = 0;

            int count = BitConverter.ToInt32(bytes, index);
            index += 4;
            int keyNameLength = BitConverter.ToInt32(bytes, index);
            index += 4;
            string keyName = Encoding.UTF8.GetString(bytes, index, keyNameLength);
            index += keyNameLength;

            Type contaninerType = typeof(T);
            object contaninerobj = Activator.CreateInstance(contaninerType);

            Type classType = typeof(K);
            FieldInfo[] infos = classType.GetFields();

            for (int i = 0; i < count; i++)
            {
                object dataobj = Activator.CreateInstance(classType);
                foreach (FieldInfo info in infos)
                {
                    if (info.FieldType == typeof(int))
                    {
                        info.SetValue(dataobj, BitConverter.ToInt32(bytes, index));
                        index += 4;
                    }
                    else if (info.FieldType == typeof(float))
                    {
                        info.SetValue(dataobj, BitConverter.ToSingle(bytes, index));
                        index += 4;
                    }
                    else if (info.FieldType == typeof(bool))
                    {
                        info.SetValue(dataobj, BitConverter.ToBoolean(bytes, index));
                        index += 1;
                    }
                    else if (info.FieldType == typeof(string))
                    {
                        int length = BitConverter.ToInt32(bytes, index);
                        index += 4;
                        info.SetValue(dataobj, Encoding.UTF8.GetString(bytes, index, length));
                        index += length;
                    }
                }
                object dicObject = contaninerType.GetField("dataDic").GetValue(contaninerobj);
                MethodInfo mInfo = dicObject.GetType().GetMethod("Add");
                object keyValue = classType.GetField(keyName).GetValue(dataobj);
                mInfo.Invoke(dicObject, new object[] { keyValue, dataobj });
            }
            if (!tableDic.ContainsKey(typeof(T).Name))
            {
                tableDic.Add(typeof(T).Name, contaninerobj);
            }
            
            fs.Close();
        }
    }

    IEnumerator LoadAssetFromLocal<T,K>()
    {
        //string path = Application.persistentDataPath + "/" + JsonMgr.Instance.SAVE_DIR + "/" + SaveController.Instance.playerData.studioName;
        //WWW d = new WWW(DATA_BINARY_PATH + "/" + typeof(K).Name + ".GameData");
        WWW d = new WWW(DATA_BINARY_PATH + typeof(K).Name + ".GameData");
        //DownloadHandler d = UnityWebRequest.Get(path).downloadHandler;
        yield return d;
        
        if (d.isDone)
        {
            Debug.Log("获取资源");
            bytes = d.bytes;
            int index = 0;

            int count = BitConverter.ToInt32(bytes, index);
            index += 4;
            int keyNameLength = BitConverter.ToInt32(bytes, index);
            index += 4;
            string keyName = Encoding.UTF8.GetString(bytes, index, keyNameLength);
            index += keyNameLength;

            Type contaninerType = typeof(T);
            object contaninerobj = Activator.CreateInstance(contaninerType);

            Type classType = typeof(K);
            FieldInfo[] infos = classType.GetFields();

            for (int i = 0; i < count; i++)
            {
                object dataobj = Activator.CreateInstance(classType);
                foreach (FieldInfo info in infos)
                {
                    if (info.FieldType == typeof(int))
                    {
                        info.SetValue(dataobj, BitConverter.ToInt32(bytes, index));
                        index += 4;
                    }
                    else if (info.FieldType == typeof(float))
                    {
                        info.SetValue(dataobj, BitConverter.ToSingle(bytes, index));
                        index += 4;
                    }
                    else if (info.FieldType == typeof(bool))
                    {
                        info.SetValue(dataobj, BitConverter.ToBoolean(bytes, index));
                        index += 1;
                    }
                    else if (info.FieldType == typeof(string))
                    {
                        int length = BitConverter.ToInt32(bytes, index);
                        index += 4;
                        info.SetValue(dataobj, Encoding.UTF8.GetString(bytes, index, length));
                        index += length;
                    }
                }
                object dicObject = contaninerType.GetField("dataDic").GetValue(contaninerobj);
                MethodInfo mInfo = dicObject.GetType().GetMethod("Add");
                object keyValue = classType.GetField(keyName).GetValue(dataobj);
                mInfo.Invoke(dicObject, new object[] { keyValue, dataobj });
            }
            tableDic.Add(typeof(T).Name, contaninerobj);
        }
    }*/

    /// <summary>
    /// 得到一张表的信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetTable<T>() where T:class
    {
        string tableName = typeof(T).Name;
        if (tableDic.ContainsKey(tableName))
        {
            return tableDic[tableName] as T;
        }
        return null;
    }


    /// <summary>
    /// 读取2进制数据转换成对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    
    public T Load<T>(string fileName) where T:class
    {
        //如果不存在这个文件 就直接返回泛型对象的默认值
        if( !File.Exists(SAVE_PATH + fileName + ".GameData") )
            return default(T);

        T obj;
        using (FileStream fs = File.Open(SAVE_PATH + fileName + ".GameData", FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter bf = new BinaryFormatter();
            obj = bf.Deserialize(fs) as T;
            fs.Close();
        }

        return obj;
    }
}
