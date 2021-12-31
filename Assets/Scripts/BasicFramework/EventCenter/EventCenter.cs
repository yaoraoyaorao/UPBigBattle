using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IEventInfo { }

public class EventInfo<T>:IEventInfo
{
    public UnityAction<T> action;
    public EventInfo(UnityAction<T> action)
    {
        this.action += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction action;
    public EventInfo(UnityAction action)
    {
        this.action += action;
    }
}

public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, IEventInfo> eventCenter = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加事件监听
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">函数</param>
    public void AddListener<T>(string name,UnityAction<T> action)
    {
        if (eventCenter.ContainsKey(name))
        {
            (eventCenter[name] as EventInfo<T>).action += action;
        }
        else
        {
            eventCenter.Add(name, new EventInfo<T>(action));
        }
    }

    public void AddListener(string name, UnityAction action)
    {
        if (eventCenter.ContainsKey(name))
        {
            (eventCenter[name] as EventInfo).action += action;
        }
        else
        {
            eventCenter.Add(name, new EventInfo(action));
        }
    }


    /// <summary>
    /// 移除事件监听
    /// 注意：当物体被销毁时，要移除事件，否则会造成内存泄漏,可以放在OnDestroy中执行
    /// </summary>
    /// <param name="name">事件名</param>
    /// <param name="action">函数</param>
    public void RemoveListener<T>(string name,UnityAction<T> action)
    {
        if (eventCenter.ContainsKey(name))
        {
            (eventCenter[name] as EventInfo<T>).action -= action;
        }
    }
    public void RemoveListener(string name, UnityAction action)
    {
        if (eventCenter.ContainsKey(name))
        {
            (eventCenter[name] as EventInfo).action -= action;
        }
    }
    /// <summary>
    /// 事件触发
    /// </summary>
    /// <param name="name">事件名</param>
    public void EventTrigger<T>(string name,T info)
    {
        if (eventCenter.ContainsKey(name))
        {
            (eventCenter[name] as EventInfo<T>).action.Invoke(info);
        }
    }
    public void EventTrigger(string name)
    {
        if (eventCenter.ContainsKey(name))
        {
            (eventCenter[name] as EventInfo).action?.Invoke();
        }
    }
    /// <summary>
    /// 切换场景时使用
    /// </summary>
    public void Clear()
    {
        eventCenter.Clear();
    }
}
