using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public Transform[] pos;
    public string saveName = "upData";
    private UpInfoData upList;

    void Start()
    {
        string jsonstr = File.ReadAllText(Application.persistentDataPath + "/" + saveName + ".json");
        upList = JsonUtility.FromJson<UpInfoData>(jsonstr);
        //UpInfo upinf = JsonUtility.FromJson<UpInfo>(jsonstr);
        CreateHero();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ShowPanel<MenuPanel>("MenuPanel");
        }
    }

    public void CreateHero()
    {
        //随机获取up主
        for (int i = 0; i < 4; i++)
        {
            UpInfo info = upList.list[i];
            GameObject obj = ResManager.Instance.Load<GameObject>("Prefabs/UpHandle");
            obj.transform.SetParent(pos[i].transform,false);
            UpHandle up = obj.GetComponent<UpHandle>();
            up.Init(info.Name, info.follower, ResManager.Instance.Load<RuntimeAnimatorController>("Animator/Up/up" + (i + 1) + "/up"+ (i + 1)));
        }

    }
}
