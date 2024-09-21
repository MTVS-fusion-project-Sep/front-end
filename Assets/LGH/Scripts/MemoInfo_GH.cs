using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
[System.Serializable]
public class MemoInfo_GH
{
    public string content;
    public string toUserId;
    public string fromUserId;
    public string registDate;
    public long guestBookid;
}
[System.Serializable]
public struct MemoInfoList
{
    public List<MemoInfo_GH> data;
}
