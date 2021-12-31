using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.ShowPanel<GameStartPanel>("GameStartPanel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
