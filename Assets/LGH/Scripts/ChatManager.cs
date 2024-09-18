using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ExitGames.Client.Photon;
using UnityEngine.EventSystems;
using System;
using Photon.Realtime;
using UnityEditor.VersionControl;
using UnityEditor;


// 이벤트를 추가해주는 인터페이스 추가
public class ChatManager : MonoBehaviour
{
    public static ChatManager instance;

    public ChatConnector chatConnector;
    public ScrollRect scrollChatWindow;
    public TMP_Text text_chatContent;
    public TMP_InputField input_chat;
    public Button sendmessage_but;
    public Button enterRoom_but;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //초기 값을 비워준다.
        input_chat.text = "";
        text_chatContent.text = "";

        //인풋 필드의 제출 이벤트에 SendMyMessage 함수를 바인딩한다.
        sendmessage_but.onClick.AddListener(SendMyMessage);
        enterRoom_but.onClick.AddListener(EnterRoom);

        // 좌측 하단으로 콘텐트 오브젝트의 피봇을 변경한다.
        scrollChatWindow.content.pivot = Vector2.zero;

    }
    void Update()
    {
        //탭키를 누르면 인풋필드를 활성화 한다.
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            EventSystem.current.SetSelectedGameObject(input_chat.gameObject);
            input_chat.OnPointerClick(new PointerEventData(EventSystem.current));
            Cursor.lockState = CursorLockMode.Confined;

        }
    }
    void EnterRoom()
    {
        string messageType = "Enter";
        string roomId = "2f474810-f68c-4edf-9295-18d774f6fa17";
        string sender = "규현";
        string message = "";

        chatConnector.SendMessageToServer(messageType, roomId, sender, message);

    }
    void SendMyMessage()
    {
        string messageType = "Text";
        string roomId = "2f474810-f68c-4edf-9295-18d774f6fa17";
        string sender = "규현";
        string message = input_chat.text;

        if (input_chat.text.Length > 0)
        {
            chatConnector.SendMessageToServer(messageType, roomId, sender, message);

        }
    }

    public void ReceivedMessage(string rm)
    {
        text_chatContent.text += rm + "/n";
    }

    // 같은 룸에 다른 사용자로부터 이벤트가 왔을 때 실행되는 함수
    public void OnEvent(EventData photonEvent)
    {
      
            // 받은 내용을 "닉네임 : 채팅 내용" 형식으로 스크롤뷰의 텍스트에 전달한다.
            object[] receiveObjects = (object[])photonEvent.CustomData;
            string recieveMessage = $"\n[{receiveObjects[2].ToString()}]{receiveObjects[0].ToString()} : {receiveObjects[1].ToString()}";

            text_chatContent.text += recieveMessage;
            input_chat.text = "";
    }

}
