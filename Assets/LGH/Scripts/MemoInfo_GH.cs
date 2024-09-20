using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
[System.Serializable]
public class MemoInfo_GH
{
    public string userID;
    public string memoText;
    public string writerID;
    public string writeDate;
}
[System.Serializable]
public struct MemoInfoList
{
    public List<MemoInfo_GH> data;
}
