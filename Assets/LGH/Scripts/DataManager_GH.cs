using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager_GH : MonoBehaviour
{ 
    public static DataManager_GH instance;
    //유저 아이디
    public string userId = "user1";
    //룸 아이디
    public string roomId = "user1";

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UserIdUpdate(string value)
    {
        userId = value;
    }
    public void RoomIdUpdate(string value)
    {
        roomId = value;
    }
}
