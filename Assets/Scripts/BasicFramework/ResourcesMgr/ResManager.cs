using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResManager : BaseManager<ResManager>
{
    public T Load<T>(string path) where T:Object
    {
        T res = Resources.Load<T>(path);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    public void LoadAsyn<T>(string path,UnityAction<T> action) where T:Object
    {
        MonoManger.Instance.StartCoroutine(RellyLoadAsyn<T>(path, action));
    }

    private IEnumerator RellyLoadAsyn<T>(string path, UnityAction<T> action) where T:Object
    {
        ResourceRequest rs = Resources.LoadAsync<T>(path);
        yield return rs;

        if (rs.asset is GameObject)
        {
            action(GameObject.Instantiate(rs.asset) as T);
        }
        else
        {
            action(rs.asset as T);
        }
    }
}
