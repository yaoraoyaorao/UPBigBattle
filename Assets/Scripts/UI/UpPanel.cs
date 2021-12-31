/*
 * 作者：RCY
 * 时间：2021/12/14 11:32:33
 * **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpPanel : BasePanel
{
    public void Init(string name,int id,int fansNum)
    {
        GetControl<Text>("text_Name").text = "昵称："+name;
        GetControl<Text>("text_MID").text = "MID：" + id;
        GetControl<Text>("text_FensNum").text = "粉丝数：" + fansNum;
    }
}
