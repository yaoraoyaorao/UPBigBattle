using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_UILayer
{
    Bot,
    Mid,
    Top,
    System
}

public class UIManager : BaseManager<UIManager>
{
    //面板集合
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    //层级位置
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;

    //记录canvas
    public RectTransform canvas;

    public UIManager()
    {
        //加载Canvas
        GameObject obj = ResManager.Instance.Load<GameObject>("UI/Canvas");
        
        //加载EventSystem
        GameObject eventObj = ResManager.Instance.Load<GameObject>("UI/EventSystem");
        
        //设置位置
        canvas = obj.transform as RectTransform;

        //场景加载不删除
        GameObject.DontDestroyOnLoad(obj);
        GameObject.DontDestroyOnLoad(eventObj);

        //设置各层
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");
       
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板类型</typeparam>
    /// <param name="panelName">面板名</param>
    /// <param name="layer">面板层级</param>
    /// <param name="callBack">回调函数</param>
    public void ShowPanel<T>(string panelName,E_UILayer layer = E_UILayer.Mid,UnityAction<T> callBack = null) where T:BasePanel
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].Show();
            callBack?.Invoke(panelDic[panelName] as T);
            
            return;
        }
        ResManager.Instance.LoadAsyn<GameObject>("UI/" + panelName, (obj) =>
        {
            Transform father = bot;
            switch (layer)
            {
                case E_UILayer.Mid:
                    father = mid;
                    break;
                case E_UILayer.Top:
                    father = top;
                    break;
                case E_UILayer.System:
                    father = system;
                    break;
            }

            //设置父对象
            obj.transform.SetParent(father);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            //得到预制体身上的面板脚本
            T panel = obj.GetComponent<T>();

            //执行函数
            callBack?.Invoke(panel);

            panel.Show();

            //存面板
            panelDic.Add(panelName, panel);
        });
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="panelName"></param>
    public void HidePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        { 
            panelDic[panelName].Hide();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
    }

    /// <summary>
    /// 获得面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T GetPanel<T>(string name)where T:BasePanel
    {
        if (panelDic.ContainsKey(name))
        {
            return panelDic[name] as T;
        }
        return null;
    }

    /// <summary>
    /// 获取层父类的位置
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>

    public Transform GetLayerFather(E_UILayer layer)
    {
        switch (layer)
        {
            case E_UILayer.Bot:
                return this.bot;
            case E_UILayer.Mid:
                return this.mid;
            case E_UILayer.Top:
                return this.top;
            case E_UILayer.System:
                return this.system;
        }
        return null;
    }

    /// <summary>
    /// 添加自定义事件监听
    /// </summary>
    /// <param name="control">控件对象</param>
    /// <param name="type">事件类型</param>
    /// <param name="callBack">事件的相应函数</param>
    public static void AddEventListener(UIBehaviour control,EventTriggerType type,UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = control.gameObject.AddComponent<EventTrigger>();
        }
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);
        trigger.triggers.Add(entry);
    }
}
