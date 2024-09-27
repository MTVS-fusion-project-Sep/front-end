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

    private void Update()
    {
        string text = UnityMainThred_GH.Instance().GetStgring();
        if(text != "")
        {
            ChatMessage receivedMessage = JsonUtility.FromJson<ChatMessage>(text);

            for (int i = 0; i < ChatManager_GH.instance.chatList.Count; i++)
            {
                ChatData_GH chatData = ChatManager_GH.instance.chatList[i].GetComponent<ChatData_GH>();
                //각 채팅방에 맞는 정보 넘기기
                if (receivedMessage.roomId == chatData.chatInfo.roomID)
                {
                    //웹소켓에서 넘어온 룸 아이디에 맞는 룸 아이디 정보들을 비교하고 같은 룸 아이디의 채팅 방을 찾아서 그곳에 있는 룸 데이터의 인스탠티에이트 함수를 호출하여 생성한다?
                    chatData.ReceiveMessage(receivedMessage);
                }
            }
        }
    }



}
