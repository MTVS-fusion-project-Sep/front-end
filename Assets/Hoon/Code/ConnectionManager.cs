using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;
using System;
using UnityEngine.Rendering.LookDev;
using System.Linq;
//using Photon.Pun.Demo.Asteroids;
//using UnityEditor.Experimental.GraphView;


//�θ� MonoBehaviourPunCallbacks �ٲߴϴ�. 
public class ConnectionManager : MonoBehaviourPunCallbacks
{
    //RoomInfo ����Ʈ �ʱ�ȭ 
    List<RoomInfo> cachedRoomList = new List<RoomInfo>();
    string roomName = "LobbyTest";


    // Start is called before the first frame update
    void Start()
    {
        //���忡�� ������ ũ�⸦ ��������.
        Screen.SetResolution(640, 480, FullScreenMode.Windowed);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLobby()
    {
        //���� ����� �̸��� �ִٸ�, �̸��� ���̰� 0�� �ƴϾ���Ѥ�
        print("�α����ϴ���...");
        //gameVersion 
        PhotonNetwork.GameVersion = "1.0.0";
        //�г���
        PhotonNetwork.NickName = "CyworldAvata";
        //ȭ�鵿��ȭ
        PhotonNetwork.AutomaticallySyncScene = true;
        // ������ ������ ��û�ϱ�
        PhotonNetwork.ConnectUsingSettings();
        //��ư�� ��Ȱ��ȭ ������.
        //LobbyUIController.lobbyUI.btn_login.interactable = false;
        MainUI.Instance.move_Lobby_Btn.interactable = false;

    }
    //���������ݹ�
    public override void OnConnected()
    {
        base.OnConnected();

        // ���� ������ ������ �Ϸ�Ǿ����� �˷��ش�.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");

    }
    //���������ݹ�
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        // ���� ������ ����Ѵ�.

        print(MethodInfo.GetCurrentMethod().Name + " is call");
        MainUI.Instance.move_Lobby_Btn.interactable = true;

    }
    //�����Ϳ��� �ݹ�
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // ������ ������ ������ �Ϸ�Ǿ����� �˷��ش�.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");

        // ������ �κ�� ����.
        PhotonNetwork.JoinLobby();

    }
    //�κ񿬰� �ݹ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        // ���� �κ� ������ �˷��ش�.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");
        //LobbyUIController.lobbyUI.ShowRoomPanel();
      
        
    }

    public void CreateRoom()
    {
        string roomName = "LoobyTest";
        int playerCount = 10;

        //�� ���� ���̰� 0���� ��� �÷��� ī��Ʈ�� 1���� ũ�ٸ�
        if (roomName.Length > 0 && playerCount > 1)
        {
            // ���� �� �ɼ� �����.
            RoomOptions roomOpt = new RoomOptions();
            //�ִ��ο�
            roomOpt.MaxPlayers = playerCount;
            //�뿡 ����� ������ ����.
            roomOpt.IsOpen = true;
            //���� �˻��� �� �ְ� ������. 
            roomOpt.IsVisible = true;

          
            //������������.
            PhotonNetwork.CreateRoom(roomName, roomOpt, TypedLobby.Default);
            

        }

        //������� �Ϸ�Ǹ�
        //JoinRoom();
    }
    public void JoinRoom()
    {
        string roomName = "LoobyTest";
        
        //���̸� ���̰� 0���� ũ��
        if (roomName.Length > 0)
        {
            PhotonNetwork.JoinRoom(roomName);
        }

    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();

        // ���������� ���� �����Ǿ����� �˷��ش�.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");
        print("�� �������!");
        //LobbyUIController.lobbyUI.PrintLog("�� �������!");

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        // ���������� �濡 ����Ǿ����� �˷��ش�.
        print(MethodInfo.GetCurrentMethod().Name + " is Call!");
        print("�濡 ���� ����");
        //LobbyUIController.lobbyUI.PrintLog("�濡 ���� ����!");

        // �濡 ������ ģ������ ��� 2�� ������ �̵�����! //���弼�ÿ� �߰��ؾ߸� �̵����� idx Ȯ�� �ʼ�
        PhotonNetwork.LoadLevel(2);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        // �뿡 ������ ������ ������ ����Ѵ�.
        Debug.LogError(message);
        print("���� ����..." + message);
        //LobbyUIController.lobbyUI.PrintLog("���� ����..." + message);
        MainUI.Instance.move_Lobby_Btn.interactable = true;

    }

    // �뿡 �ٸ� �÷��̾ �������� ���� �ݹ� �Լ�
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        string playerMsg = $"{newPlayer.NickName}���� �����ϼ̽��ϴ�.";
        print(playerMsg);
        //LobbyUIController.lobbyUI.PrintLog(playerMsg);
    }
    // �뿡 �ִ� �ٸ� �÷��̾ �������� ���� �ݹ� �Լ�
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        string playerMsg = $"{otherPlayer.NickName}���� �����ϼ̽��ϴ�.";
        print(playerMsg);
        //LobbyUIController.lobbyUI.PrintLog(playerMsg);
    }

    //�븮��Ʈ �ݹ��� �κ� ���������� �ڵ����� ȣ��
    // //���� �κ񿡼� �� ����Ʈ�� �޾ƺ���. �κ񿡼��� ȣ�Ⱑ��
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);
        print("�븮��Ʈ ������Ʈ");



        //print("���� ������ �����ؾ���");
        // �κ� ������ ���� ����
        //JoinRoom();


        if (roomList.Count == 0)
        {
            
            print("�氳��" + roomList.Count + "���̾����� ��������...");
            CreateRoom();
        }
        else
        {
            print("�氳��" + roomList.Count);
            foreach (RoomInfo roomInfo in roomList)
            {
                if (roomInfo.Name.Contains("LoobyTest"))
                {
                    print("���������ϱ� �����ؾ���~");
                    JoinRoom();
                    return;
                }

            }
        }
       
        


    }   






    }//Ŭ������
