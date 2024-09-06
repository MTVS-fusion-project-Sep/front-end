using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FurnitureInfo
{
    public int furni_Size_X = 0;
    public int furni_Size_Z = 0;
    public bool onPlace = false;
    public int furni_Current_X;
    public int furni_Current_Z;
    public bool furni_Rotate;
}