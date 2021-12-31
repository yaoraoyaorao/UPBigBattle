using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoController : MonoBehaviour
{
    public UnityAction action;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        action?.Invoke();
    }

    public void AddListener(UnityAction action)
    {
        this.action += action;
    }


    public void RemoveListener(UnityAction action)
    {
        this.action -= action;
    }
}
