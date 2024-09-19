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
using UnityEngine.Networking;


// 이벤트를 추가해주는 인터페이스 추가
public class ChatManager : MonoBehaviour
{
    public GameObject roomListPanel;
    public GameObject chatPanel;

    public static ChatManager instance;

    public ChatConnector chatConnector;
    public ScrollRect scrollChatWindow;
    public TMP_Text text_chatContent;
    public TMP_InputField input_chat;
    public Button sendmessage_but;
    public Button exitRoom_but;

    //룸 아이디
    public string roomId = "";
    public string userId = "";


    // 방 리스트
    public List<GameObject> roomList = new List<GameObject>();
    // 방 리스트 URL
    public string RoomListURL = "";

    public RoomInfoList allRoomInfo;

    public GameObject roomContent;

    public GameObject roomLisrPrefab;

    //방 새로고침 버튼
    public Button roomReloadBut;

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
        //enterRoom_but.onClick.AddListener(EnterRoom);

        // 좌측 하단으로 콘텐트 오브젝트의 피봇을 변경한다.
        scrollChatWindow.content.pivot = Vector2.zero;


        // 룸 새로고침 버튼에 함수 할당
        roomReloadBut.onClick.AddListener(RoomLoad);

        //룸을 받아온다.
        RoomLoad();

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
    void RoomLoad()
    {
        //룸을 초기화 한다.
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();


        HttpInfo info = new HttpInfo();
        info.url = RoomListURL;
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자.
            allRoomInfo = JsonUtility.FromJson<RoomInfoList>(jsonData);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info));

        // 룸 숫자만큼 룸을 만든다.
        for(int i = 0; i < allRoomInfo.data.Count; i++)
        {
            roomList.Add(Instantiate(roomLisrPrefab, roomContent.transform));
            RoomData_GH roomData = roomList[i].GetComponent<RoomData_GH>();
            roomData.roomInfo.roomId = allRoomInfo.data[i].roomId;
            roomData.roomInfo.name = allRoomInfo.data[i].name;
        }
    }

    public void EnterRoom(string roomID)
    {
        string messageType = "ENTER";
        roomId = roomID;
        // 유저 아이디 코드화하기 todo
        string sender = "규현";
        string message = "";

        chatConnector.SendMessageToServer(messageType, roomId, sender, message);

    }
    void SendMyMessage()
    {
        string messageType = "TALK";
        string message = input_chat.text;
        // 유저 아이디 코드화하기 todo
        string sender = "규현";
        if (input_chat.text.Length > 0)
        {
            chatConnector.SendMessageToServer(messageType, roomId, sender, message);
            input_chat.text = "";
        }
    }

    public void ReceivedMessage(string rm)
    {
        text_chatContent.text += rm + "\n";
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

[System.Serializable]
public class RoomInfoList
{
    public List<RoomInfo_GH> data;
}