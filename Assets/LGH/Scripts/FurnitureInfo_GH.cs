using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FurnitureType
{
    Ground,
    Wall,
    Notice
}

[System.Serializable]
public class FurnitureInfo
{
    public int furni_Size_X = 0;
    public int furni_Size_Y = 0;
    public bool onPlace = false;
    public int furni_Current_X;
    public int furni_Current_Y;
    public bool furni_Rotate;
}