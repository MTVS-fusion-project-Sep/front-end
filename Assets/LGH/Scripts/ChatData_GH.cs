using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static MainUI;

public class ChatData_GH : MonoBehaviour
{
    public ChatInfo_GH chatInfo;
    public ChatInfoList chatInfoList;

    public TMP_Text chatRoomName;
    public Transform TextInChat;
    public TMP_InputField input_chat;
    public Button sendmessage_but;
    public ChatConnector chatConnector;


    // 받는 메시지
    public GameObject recMesPre;
    // 보내는 메시지
    public GameObject senderMesPre;
    
    //새로 고침 할 때마다 보내기
    public int pageNum = 0;
    RectTransform con_rec;
    void Start()
    {
        input_chat.text = "";
        chatConnector = GameObject.Find("ChatConnector").GetComponent<ChatConnector>();

        //인풋 필드의 제출 이벤트에 SendMyMessage 함수를 바인딩한다.
        sendmessage_but.onClick.AddListener(SendMyMessage);

        StartCoroutine(SetChat());
        con_rec = TextInChat.GetComponent<RectTransform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMyMessage();
        }
    }
    void SendMyMessage()
    {
        //통신 후 없애기
        string messageType = "TALK";
        string message = input_chat.text;
        // 유저 아이디 코드화하기 todo
        string sender = DataManager_GH.instance.userId;
        if (input_chat.text.Length > 0)
        {
            chatConnector.SendMessageToServer(messageType, chatInfo.roomID, sender, message);
            input_chat.text = "";
        }
    }

    public void ReceiveMessage(ChatMessage receivedMessage)
    {
        print(":생성해 생성");
        //만약 받은 데이터의 아이디와 유저 정보의 아이디가 일치한다면
        if (receivedMessage.userId == DataManager_GH.instance.userId)
        {
            //샌드메시지 프리팹을 생성후
            InChatData_GH selfChat = Instantiate(senderMesPre, TextInChat).GetComponent<InChatData_GH>();
            //그 값들을 넣어준다.
            //selfChat.userName.text = receivedMessage.sender;
            selfChat.userTime.text = DateTime.Now.ToString(("HH:mm"));
            selfChat.userContents.text = receivedMessage.message;
        }
        else
        {
            //만약 유저아이디가 일치하지 않는 다면
            //샌드메시지 프리팹을 생성후
            InChatData_GH selfChat = Instantiate(recMesPre, TextInChat).GetComponent<InChatData_GH>();
            //그 값들을 넣어준다.
            selfChat.userName.text = receivedMessage.userId;
            selfChat.userTime.text = DateTime.Now.ToString(("HH:mm"));
            selfChat.userContents.text = receivedMessage.message;
        }
    }
    IEnumerator SetChat()
    {

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < chatInfoList.data.Count; i++)
        {
            //만약 받은 데이터의 아이디와 유저 정보의 아이디가 일치한다면
            if (chatInfoList.data[i].userId == DataManager_GH.instance.userId)
            {
                //샌드메시지 프리팹을 생성후
                InChatData_GH selfChat = Instantiate(senderMesPre, TextInChat).GetComponent<InChatData_GH>();
                //그 값들을 넣어준다.
                selfChat.userTime.text = chatInfoList.data[i].sentTime.Substring(11, 5); ;
                selfChat.userContents.text = chatInfoList.data[i].message;
            }
            else
            {
                //만약 유저아이디가 일치하지 않는 다면
                //리시브 채팅을 넣고
                //그값들을 넣어준다.
                //샌드메시지 프리팹을 생성후
                InChatData_GH selfChat = Instantiate(recMesPre, TextInChat).GetComponent<InChatData_GH>();
                //그 값들을 넣어준다.
                selfChat.userName.text = chatInfoList.data[i].userId;
                selfChat.userTime.text = chatInfoList.data[i].sentTime.Substring(11, 5); ;
                selfChat.userContents.text = chatInfoList.data[i].message;
            }
        }

        yield return new WaitForSeconds(0.2f);

        GameObject.Find("Con_Message").GetComponent<VerticalLayoutGroup>().childControlHeight = true;
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("Con_Message").GetComponent<VerticalLayoutGroup>().childControlWidth = true;

        yield return new WaitForSeconds(0.2f);

        GameObject.Find("Con_Message").GetComponent<VerticalLayoutGroup>().childControlHeight = false;
        GameObject.Find("Con_Message").GetComponent<VerticalLayoutGroup>().childControlWidth = false;

        con_rec.anchoredPosition = new Vector3(0, con_rec.sizeDelta.y - 710, 0);
        pageNum++;
    }
}
