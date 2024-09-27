using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ExitGames.Client.Photon;
using UnityEngine.EventSystems;
using System;
using Photon.Realtime;
using UnityEditor;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;
using UnityEngine.SceneManagement;


// 이벤트를 추가해주는 인터페이스 추가
public class ChatManager_GH : MonoBehaviour
{
    public GameObject roomListPanel;
    public GameObject chatPanel;
    public GameObject roomCreatePanel;


    public static ChatManager_GH instance;

    public ChatConnector chatConnector;
    public ScrollRect scrollChatWindow;
    public Button exitRoom_but;
    public RectTransform rectTransform;

    //룸 아이디
    string chatRoomId = "";
    string userId = "user1";


    // 방 리스트
    public List<GameObject> roomList = new List<GameObject>();
    // 방 리스트 URL
    public string RoomListURL = "";

    public RoomInfoList allRoomInfo;

    public GameObject roomContent;

    public GameObject roomListPrefab;

    //방 새로고침 버튼
    public Button roomReloadBut;
    //방 생성 버튼
    public Button roomCreateBut;

    public Button roomCreateOnBut;
    public Button roomCancelOnBut;


    //방 생성
    public TMP_InputField createRoomName;
    public TMP_Dropdown createRoomCate;
    public Slider createRoomMaxCnt;

    // 채팅방
    public GameObject chatprefab;

    //채팅방 리스트
    public List<GameObject> chatList;

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
        RoomLoad();
    }

    void Start()
    {


        //enterRoom_but.onClick.AddListener(EnterRoom);

        // 좌측 하단으로 콘텐트 오브젝트의 피봇을 변경한다.
        //scrollChatWindow.content.pivot = Vector2.zero;

        // 룸 새로고침 버튼에 함수 할당
        roomReloadBut.onClick.AddListener(RoomLoad);

        // 방만들기 버튼에 함수 할당
        roomCreateBut.onClick.AddListener(RoomCreate);

        // 룸생성 버튼에 함수 할당
        roomCreateOnBut.onClick.AddListener(RoomCreateOn);

        // 룸생성 취소 버튼에 할당
        roomCancelOnBut.onClick.AddListener(RoomCancel);

        //룸을 받아온다.
        RoomLoad();

        //방생성 패널 키고
        roomCreatePanel.SetActive(false);

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
            // print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자.
            allRoomInfo = JsonUtility.FromJson<RoomInfoList>(jsonData);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info));

        // 룸 숫자만큼 룸을 만든다.
        for (int i = 0; i < allRoomInfo.data.Count; i++)
        {
            roomList.Add(Instantiate(roomListPrefab, roomContent.transform));
            RoomData_GH roomData = roomList[i].GetComponent<RoomData_GH>();
            roomData.roomInfo = allRoomInfo.data[i];
        }
    }
    void RoomCreateOn()
    {
        //방생성 패널 키고
        roomCreatePanel.SetActive(true);
        //채팅 패널 끄기
        //chatPanel.SetActive(false);
    }

    void RoomCancel()
    {
        roomCreatePanel.SetActive(false);
    }

    void RoomCreate()
    {
        TestInfo ti = new TestInfo();
        ti.name = createRoomName.text;
        ti.category = createRoomCate.options[createRoomCate.value].text;
        ti.maxCnt = (int)createRoomMaxCnt.value;

        HttpInfo info = new HttpInfo();
        info.url = RoomListURL;
        info.body = JsonUtility.ToJson(ti);
        info.contentType = "application/json";
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Post(info));

    }

    GameObject go_chatroom;
    ChatData_GH chatroom;

    public void EnterRoom(string roomID, string roomName)
    {
        // 방 생성 prefab 생성
        go_chatroom = Instantiate(chatprefab, GameObject.Find("Canvas").transform);
        chatroom = go_chatroom.GetComponent<ChatData_GH>();
        chatList.Add(go_chatroom);
        // 생성된거에 이름의 룸네임으로 정의하기
        chatroom.chatRoomName.text = roomName;
        chatroom.chatInfo.roomID = roomID;
        print("엔터룸" + roomID);

        string messageType = "ENTER";
        //chatRoomId = chatroom.chatInfo.roomID;
        chatRoomId = roomID;
        // 유저 아이디 코드화하기 todo
        string sender = DataManager_GH.instance.userId;
        string message = "";

        chatConnector.SendMessageToServer(messageType, chatRoomId, sender, message);

        //채팅 데이터에 있는 리스트에 해당 채팅 대화 목록 넣기

        LoadChatLog(roomID, chatroom.chatInfoList, chatroom.pageNum);
    }

    public void TooLoadChatLog(string roomID, ChatInfoList chatInfoList, int pageNum)
    {
        ChatInfoList chatInfoList2;
        HttpInfo info = new HttpInfo();
        info.url = RoomListURL + "/log?roomId=" + roomID + "&userId=" + DataManager_GH.instance.userId + "&page=" + pageNum;
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            //print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자.
            chatInfoList2 = JsonUtility.FromJson<ChatInfoList>(jsonData);
            print(chatInfoList2.data.Count);
            for (int i = 0; i < chatInfoList2.data.Count; i++)
            {

                chatroom.chatInfoList.data.Insert(i, chatInfoList.data[i]);
            }

        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info));
    }

    //방 대화내용 불러오기
    void LoadChatLog(string roomID, ChatInfoList chatInfoList, int pageNum)
    {


        HttpInfo info = new HttpInfo();
        info.url = RoomListURL + "/log?roomId=" + roomID + "&userId=" + DataManager_GH.instance.userId + "&page=" + pageNum;
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            //print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자.
            chatInfoList = JsonUtility.FromJson<ChatInfoList>(jsonData);
            chatroom.chatInfoList = chatInfoList;

        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info));
    }




    public void MainSceneLode()
    {
        SceneManager.LoadScene(0);
    }





}

[System.Serializable]
public class RoomInfoList
{
    public List<RoomInfo_GH> data;
}

[System.Serializable]
public class TestInfo
{
    public string roomId;
    public string name = "방이름";
    public string category = "요리";
    public int maxCnt = 35;
}
