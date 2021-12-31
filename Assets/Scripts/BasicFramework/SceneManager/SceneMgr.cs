using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景切换模块
/// </summary>
public class SceneMgr : BaseManager<SceneMgr>
{
    /// <summary>
    /// 场景同步加载
    /// </summary>
    /// <param name="name">场景名</param>
    public void LoadScene(string name,UnityAction action)
    {
        SceneManager.LoadScene(name);
        action();
    }

    public void LoadSceneAsyn(string name,UnityAction action)
    {
        MonoManger.Instance.StartCoroutine(RellyLoadSceneAsyn(name, action));
    }
    private IEnumerator RellyLoadSceneAsyn(string name,UnityAction action)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(name);
        while (!load.isDone)
        {
            EventCenter.Instance.EventTrigger<float>("Loading", load.progress);
            yield return load.progress;
        }
        action();
    }
}
