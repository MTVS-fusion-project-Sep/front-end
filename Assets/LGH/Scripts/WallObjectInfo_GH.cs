using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WallType
{
    NONE,
    LEFT_TWO,
    RIGHT_ONE,
    RIGHT_TWO
}

[System.Serializable]
public class WallObjectInfo
{
    public string furniCategory;
    public string furniName;
    public WallType furniPos = WallType.LEFT_TWO;
    public bool furniOnPlace;
    public string userId;
}
[System.Serializable]
public class WallObjectInfoList
{
    public List<WallObjectInfo> data;
}