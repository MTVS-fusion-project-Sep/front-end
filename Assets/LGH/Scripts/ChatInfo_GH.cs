using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChatInfo_GH
{
    public int messageId;
    public string roomID;
    public string messageType;
    public string userId;
    public string message;
    public string sentTime;
}
[System.Serializable]
public class ChatInfoList
{
    public List<ChatInfo_GH> data;
}
