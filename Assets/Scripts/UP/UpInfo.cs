using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpInfo
{
    public int ID;
    public int follower;
    public string Name;
    public string face;
    public string sign;
}

public class UpInfoData
{
    public List<UpInfo> list;
}
