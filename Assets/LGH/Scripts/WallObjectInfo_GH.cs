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
    public WallType wallPos = WallType.NONE;
    public bool onPlace = true;
}
