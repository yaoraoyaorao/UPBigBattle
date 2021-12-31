/*
 * 作者：RCY
 * 时间：2021/12/14 11:32:33
 * **/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ZhuaQuPanel : BasePanel
{
    public string saveName = "upData";
    public Transform context;
    private UpInfoData upList;

    void Start()
    {
        string jsonstr = File.ReadAllText(Application.persistentDataPath + "/" + saveName + ".json");
        upList = JsonUtility.FromJson<UpInfoData>(jsonstr);
        //UpInfo upinf = JsonUtility.FromJson<UpInfo>(jsonstr);
        ShowUpList();
    }
    
    private void ShowUpList()
    {
        for (int i = 0; i < upList.list.Count; i++)
        {
            GameObject obj = ResManager.Instance.Load<GameObject>("UI/UP");
            UpPanel up = obj.GetComponent<UpPanel>();
            up.Init(upList.list[i].Name, upList.list[i].ID, upList.list[i].follower);
            obj.transform.SetParent(context,false);
        }
    }

    protected override void OnClick(string name)
    {
        if (name == "btn_GameStart")
        {
            UIManager.Instance.HidePanel("ZhuaQuPanel");
            SceneMgr.Instance.LoadScene("GameScenes", () =>
            {
               
            });
        }
    }


}
