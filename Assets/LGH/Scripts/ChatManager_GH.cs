using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ExitGames.Client.Photon;
using UnityEngine.EventSystems;
using System;
using Photon.Realtime;
//using UnityEditor.VersionControl;
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
    public TMP_Text text_chatContent;
    public TMP_InputField input_chat;
    public Button sendmessage_but;
    public Button exitRoom_but;
    public RectTransform rectTransform;

    //룸 아이디
    string roomId = "";
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


    //방 생성
    public TMP_InputField createRoomName;
    public TMP_Dropdown createRoomCate;
    public Slider createRoomMaxCnt;


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
    public void roomUserIdSet(string userID)
    {
        userId = userID;
    }
    void Start()
    {
        //초기 값을 비워준다.
        input_chat.text = "";
        text_chatContent.text = "독종규현 : " + "규현님 환영합니다!" + "\n";
        //인풋 필드의 제출 이벤트에 SendMyMessage 함수를 바인딩한다.
        sendmessage_but.onClick.AddListener(SendMyMessage);
        //enterRoom_but.onClick.AddListener(EnterRoom);

        // 좌측 하단으로 콘텐트 오브젝트의 피봇을 변경한다.
        //scrollChatWindow.content.pivot = Vector2.zero;


        // 룸 새로고침 버튼에 함수 할당
        roomReloadBut.onClick.AddListener(RoomLoad);

        // 방만들기 버튼에 함수 할당
        roomCreateBut.onClick.AddListener(RoomCreate);

        // 룸생성 버튼에 함수 할당
        roomCreateOnBut.onClick.AddListener(RoomCreateOn);

        //룸을 받아온다.
        RoomLoad();

        //방생성 패널 키고
        roomCreatePanel.SetActive(true);
        //채팅 패널 끄기
        chatPanel.SetActive(false);

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
        chatPanel.SetActive(false);
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

        ////없애자===========================================================================================
        //GameObject tt = Instantiate(roomListPrefab, roomContent.transform);
        //RoomData_GH aa = tt.GetComponent<RoomData_GH>();
        //aa.roomInfo.name = ti.name;
        //aa.roomInfo.category = ti.category;
        //aa.roomInfo.maxCnt = ti.maxCnt;
        //aa.roomInfo.headCnt = 1;

    }

    public void EnterRoom(string roomID)
    {
        //방생성 패널 끄기
        roomCreatePanel.SetActive(false);
        //채팅 패널 켜기
        chatPanel.SetActive(true);

        string messageType = "ENTER";
        roomId = roomID;
        // 유저 아이디 코드화하기 todo
        string sender = userId;
        string message = "";

        chatConnector.SendMessageToServer(messageType, roomId, sender, message);

    }
    void SendMyMessage()
    {

        //통신 후 없애기
        text_chatContent.text += "최강성표 : " + input_chat.text + "\n";
        string messageType = "TALK";
        string message = input_chat.text;
        // 유저 아이디 코드화하기 todo
        string sender = userId;
        if (input_chat.text.Length > 0)
        {
            chatConnector.SendMessageToServer(messageType, roomId, sender, message);
            input_chat.text = "";
        }


    }

    public TMP_Text aaa;
    public string bbb;
    public void ReceivedMessage(string rm)
    {
        //print(bbb);
        //bbb += rm + "\n";
        //aaa.text += rm + "\n";
        text_chatContent.text += rm + "\n";
        //LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        //Canvas.ForceUpdateCanvases();
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
    public string name = "방이름";
    public string category = "요리";
    public int maxCnt = 35;
}
