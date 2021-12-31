/*
 * 作者：RCY
 * 时间：2021/12/31 4:06:20
 * 
 * 
 * **/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUpPanel : BasePanel
{
    public string pyScriptName = "main";
    public string saveName = "upData";
    public int mid = 394736064;
    string basePath;
    string savePath;

    void Start()
    {

        basePath = Application.streamingAssetsPath + "/pachong/";
        savePath = Application.persistentDataPath + "/" + saveName + ".json";
        print(savePath);

    }
    protected override void OnClick(string name)
    {
        if (name == "btn_ZhuangTian")
        {
            
            
            
            RunPython.RunPythonScript(basePath + pyScriptName + ".py", GetControl<InputField>("input_MID").text, savePath);

            StartCoroutine(loading());

        }
    }

    IEnumerator loading()
    {
       
        UIManager.Instance.ShowPanel<LoadingPanel>("LoadingPanel");
        yield return new WaitForSeconds(3);
        UIManager.Instance.HidePanel("LoadUpPanel");
        UIManager.Instance.HidePanel("LoadingPanel");
        UIManager.Instance.ShowPanel<ZhuaQuPanel>("ZhuaQuPanel");
    }


}
