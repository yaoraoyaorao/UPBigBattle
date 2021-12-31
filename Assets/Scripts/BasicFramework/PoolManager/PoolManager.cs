using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolData
{
    public List<GameObject> poolList;
    public GameObject fatherObj;

    public PoolData(GameObject obj,GameObject poolObj)
    {
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;
        poolList = new List<GameObject>() { };

        Push(obj);
    }

    //压入对象
    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        poolList.Add(obj);
        obj.transform.parent = fatherObj.transform;
    }

    //取对象
    public GameObject Get()
    {
        GameObject obj = null;
        obj = poolList[0];
        poolList.RemoveAt(0);
        obj.SetActive(true);
        obj.transform.parent = null;
        return obj;
    }
}


/// <summary>
/// 缓存池模块
/// </summary>
public class PoolManager : BaseManager<PoolManager>
{
    //缓存池容器
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    private GameObject poolObj;

    /// <summary>
    /// 获取游戏对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject Get(string name,UnityAction<GameObject> callBack)
    {
        GameObject obj = null;

        //判断缓存池中是否存在对象
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            
            callBack(poolDic[name].Get());
        }
        else
        {
            ResManager.Instance.LoadAsyn<GameObject>(name, (o) =>
            {
                o.name = name;
                callBack(o);
            });
        }
     
        return obj;
    }

    /// <summary>
    /// 压入
    /// </summary>
    /// <param name="name"></param>
    /// <param name="obj"></param>
    public void Push(string name,GameObject obj)
    {
        if (poolObj == null)
        {
            poolObj = new GameObject("Pool");
        }
     
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].Push(obj);
        }
        else
        {
            poolDic.Add(name, new PoolData(obj,poolObj));
        }
    }

    /// <summary>
    /// 清空缓存池 用于场景转换
    /// </summary>
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }

}
