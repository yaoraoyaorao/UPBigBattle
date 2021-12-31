using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 输入模块
/// </summary>
public class InputManager : BaseManager<InputManager>
{
    private bool isStart = false;

    public InputManager()
    {
        MonoManger.Instance.AddListener(MyUpdate);
    }

    /// <summary>
    /// 是否开启输入检测
    /// </summary>
    public bool IsStart
    {
        get { return isStart; }
        set { isStart = value; }
    }

    private void CheckKeyCode(KeyCode key)
    {
        
        if (Input.GetKeyDown(key))
            EventCenter.Instance.EventTrigger<KeyCode>("KeyDown", key);

        if (Input.GetKeyUp(key))
            EventCenter.Instance.EventTrigger<KeyCode>("KeyOn", key);
    }

    private void CheckMouse(int i)
    {
        if(Input.GetMouseButtonDown(i))
            EventCenter.Instance.EventTrigger<int>("MouseDown", i);
        if (Input.GetMouseButtonUp(i))
            EventCenter.Instance.EventTrigger<int>("MouseUp", i);
        if(Input.GetMouseButton(i))
            EventCenter.Instance.EventTrigger<int>("MouseButton", i);
    }



    private void MyUpdate()
    {
        if (!isStart)
            return;

        //checkkeycode(键)
        CheckKeyCode(KeyCode.W);
        CheckKeyCode(KeyCode.A);
        CheckKeyCode(KeyCode.S);
        CheckKeyCode(KeyCode.D);

        CheckMouse(0);
        CheckMouse(1);
        CheckMouse(2);

    }
}
