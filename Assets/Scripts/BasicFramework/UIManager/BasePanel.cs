using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();

    protected virtual void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Text>();
        FindChildrenControl<Image>();
        FindChildrenControl<Scrollbar>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<InputField>();
        FindChildrenControl<Slider>();
    }

    /// <summary>
    /// 获取控件
    /// </summary>
    /// <typeparam name="T">控件类型</typeparam>
    /// <param name="controlName">控件名</param>
    /// <returns></returns>
    public T GetControl<T>(string controlName) where T:UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; i++)
            {
                if (controlDic[controlName][i] is T)
                {
                    return controlDic[controlName][i] as T;
                }
            }
        }
        return null;
    }


    /// <summary>
    /// 寻找子控件组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>()where T:UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>();

        for (int i = 0; i < controls.Length; i++)
        {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName))
            {
                controlDic[objName].Add(controls[i]);
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour>() { controls[i] });
            }

            //如果是按钮组件
            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    OnClick(objName);
                });
            }
            else if(controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName,value);
                });
            }
        }
    }

    /// <summary>
    /// 显示面板
    /// </summary>
    public virtual void Show()
    {

    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public virtual void Hide()
    {

    }


    /// <summary>
    /// 按钮点击
    /// </summary>
    /// <param name="name"></param>

    protected virtual void OnClick(string name)
    {

    }

    /// <summary>
    /// Toggle值变化
    /// </summary>
    /// <param name="toggleName"></param>
    /// <param name="value"></param>
    protected virtual void OnValueChanged(string toggleName,bool value)
    {

    }

}
