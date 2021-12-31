using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public class MonoManger : BaseManager<MonoManger>
{
    private MonoController controller;
    public MonoManger()
    {
        GameObject obj = new GameObject("MonoManger");
        controller = obj.AddComponent<MonoController>();
    }

    public void AddListener(UnityAction action)
    {
        controller.AddListener(action);
    }


    public void RemoveListener(UnityAction action)
    {
        controller.RemoveListener(action);
        
    }


    #region 协程相关
    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
    {
        return controller.StartCoroutine(methodName, value);
    }

    public void StopAllCoroutines()
    {
        controller.StopAllCoroutines();
    }

    public void StopCoroutine(IEnumerator routine)
    {
        controller.StopCoroutine(routine);
    }

    public void StopCoroutine(Coroutine routine)
    {
        controller.StopCoroutine(routine);
    }

    public void StopCoroutine(string methodName)
    {
        controller.StopCoroutine(methodName);
    }
    #endregion

}
