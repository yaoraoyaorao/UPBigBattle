using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartPanel : BasePanel
{
    protected override void OnClick(string name)
    {
        switch (name)
        {
            case "btn_GameStart":
                UIManager.Instance.ShowPanel<LoadUpPanel>("LoadUpPanel");
                UIManager.Instance.HidePanel("GameStartPanel");
                break;
            case "btn_GameQuit":
                Application.Quit();
                break;
        }
    }
}
