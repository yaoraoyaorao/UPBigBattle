using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : BasePanel
{
    private void Start()
    {
        Time.timeScale = 0;
    }
    protected override void OnClick(string name)
    {
        switch (name)
        {
            case "btn_jixu":
                UIManager.Instance.HidePanel("MenuPanel");
                Time.timeScale = 1;
                break;
            case "btn_mainMenu":
                UIManager.Instance.HidePanel("MenuPanel");
                SceneMgr.Instance.LoadScene("GameStart",()=> { });
                break;
            case "btn_quite":
                Application.Quit();
                break;
        }
    }
}
