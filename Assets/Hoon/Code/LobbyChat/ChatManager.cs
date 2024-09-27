using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//포톤사용을 위해 추가
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Unity.Services.Analytics.Internal;

//커스텀 프롬퍼티 사용
using ExitGames.Client.Photon;
//DateTime을 쓰기위한 추가, C#
using System;
//이벤트 시스템 컨트롤 하자.
using UnityEngine.EventSystems;
using TMPro;



//부모를 변경 MonoBehaviourPun, IOnEventCallback 인터페이스 추가 및 구현 (raiseEvent 콜백해주는 용도)
public class ChatManager : MonoBehaviourPun, IOnEventCallback
{
    //현지 윈도우
    public ScrollRect ScrollChatWindow;
    //보여질 텍스트
    public Text text_ChatContent;
    //실제 입력될 챗
    public InputField input_Chat;
    //
    public GameObject text_Chat_Object;
    //
    public GameObject scroll_Chat_Object;

    //Image img_chatBackground;
    bool isChatLog = false;
    


    //상수처리 바이트 자료형으로 보낼 채팅이벤트 구분자. 번호는 자기자신이 커스텀
    const byte chattingEvent = 1;

    public string saveMassage;

    public MultiPlayerMove multiPlayerMove;

    //raiseEvent 를 사용하기 위해서 사전 등록, 이걸 등록하지 않으면 raiseEvent 를 보낼수 없음.
    private void OnEnable()
    {
        //새로추가된 기능, 함수방식
        //PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
        //기존기능, 딜리게이트방식
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    void Start()
    {
        //text_ChatPack
        

        //채팅윈도우끄기
        scroll_Chat_Object.SetActive(false);
        //인풋챗에 값이 있다면 비우자.
        input_Chat.text = "";
        //누적된 챗 컨텐트가 있다면 비우자.
        text_ChatContent.text = "";

        //onSubmit은 enter를 입력받는 콜백. input_Chat 에 text 를 입력하고 enterKey 를 누르면 SendMyMessage 함수를 실행

        // 인풋 필드의 제출 이벤트에 SendMyMessage 함수를 바인딩한다. 딜리게이트
        //AddListen
        input_Chat.onSubmit.AddListener(SendMyMessage);


        // 좌측 하단으로 콘텐트 오브젝트의 피벗을 변경한다.
        //scrollChatWindow.content.pivot = Vector2.zero;
        //img_chatBackground = scrollChatWindow.transform.GetComponent<Image>();
        //img_chatBackground.color = new Color32(255, 255, 255, 10);
    }

    void Update()
    {
        
        // 탭 키를 누르면 인풋 필드를 선택하게 한다.
        /* if (Input.GetKeyDown(KeyCode.Tab))
         {
             //게임오브젝트를 선택했다고 해주기.
             EventSystem.current.SetSelectedGameObject(input_Chat.gameObject);
             //
             input_Chat.OnPointerClick(new PointerEventData(EventSystem.current));
             Cursor.lockState = CursorLockMode.Confined;
             Cursor.visible = true;
         }*/
    }

    public void ActiveChatLog()
    {
        if (isChatLog == false)
        {
            scroll_Chat_Object.SetActive(true);
            isChatLog = true;
        }
        else
        {
            scroll_Chat_Object.SetActive(false);
            isChatLog = false;
        }
    }



    //여기서 이벤트 매시지를 보냅니다.
    void SendMyMessage(string msg)
    {

        if (input_Chat.text.Length > 0)
        {
            
            //이벤트에 보낼 내용 중 시간값
            //현재시간을 시,분,초 로 나누어서 문자열로 보내자.
            string currentTime = DateTime.Now.ToString("hh:mm:ss");

            //오브젝트는 모든 자료형의 부모임으로 오브젝트로 보냄.
            //오브젝트 배열에 닉네임, 메시지, 시간을 담아서 보냄.
            object[] sendContent = new object[] { PhotonNetwork.NickName, msg, currentTime };

            // 송신 옵션
            RaiseEventOptions eventOptions = new RaiseEventOptions();
            // Receivers = rpcTarget, 누가 받을건지 쓰자.
            eventOptions.Receivers = ReceiverGroup.All;
            //버퍼는 이쪽으로 보냄
            //eventOptions.CachingOption = EventCaching.DoNotCache;


            // 이벤트 송신 시작
            //매개변수가 4개필요, 1번째 코드(구분자), 2번째 보낼내용, 3번째 송신옵션(받는사람), 통신방식(reliable = tcp, unreliable = udp)
            //결과 이벤트코드(1), 보낼내용(이름,내용,시간), 받는사람(모든사람), 통신방식(udp)
            PhotonNetwork.RaiseEvent(1, sendContent, eventOptions, SendOptions.SendUnreliable);


            //보냈는지 확인
            print("Send!");
            //EventSystem.current.SetSelectedGameObject(null);
        }
    }

    //인터페이스 콜백 구현
    //같은 룸 사용자로부터 이벤트가 왔을때 실행되는 함수

    public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {
        //throw new System.NotImplementedException();

        //누군가 채팅을 입력하면 여기에 콜백이 호출이 될거임.
        //메시지 받았다면
        //print("Recieve!");

        //텍스트를 메시지와 함께 업데이트하자
        //만일, 받은 이벤트가 채팅 이벤트라면
        if (photonEvent.Code == chattingEvent)
        {
            //받은 내용을 "닉네임 : 채팅 내용" 형식으로 스크롤뷰에 텍스트에 전달한다.
            //CustomData 를 object[] 형태로 컨버트.
            object[] receiveObejcts = (object[])photonEvent.CustomData;
            //받은메시지는 리시브2([현재시간]), 리시브0(닉네임), 리시브1(채팅메시지)
            string receiveMessage = $"[{receiveObejcts[2].ToString()}]:{receiveObejcts[0].ToString()}:{receiveObejcts[1].ToString()}";

            //현재텍스트에 메시지 넣기
            text_ChatContent.text += receiveMessage + "\n";

            //메시지를 임시 저장
            saveMassage = receiveObejcts[1].ToString();
            print("메시지 저장" + receiveObejcts[1].ToString());

            //채팅 메시지 초기화
            input_Chat.text = "";

        }

        //보낸메시지 확인
        print("text_ChatContent.text" + text_ChatContent.text);

        //이벤트 보낸 사람의 객체를 얻자
        print("보낸사람 찾기시작");
        // 이벤트를 발생시킨 플레이어의 ActorNumber
        int senderActorNumber = photonEvent.Sender;
        // ActorNumber를 통해 Player 객체 얻기
        Player senderPlayer = PhotonNetwork.CurrentRoom.GetPlayer(senderActorNumber);
        if (senderPlayer != null)
        {
            // 해당 플레이어가 소유한 오브젝트 찾기
            GameObject playerObject = FindPlayerObject(senderPlayer);

            if (playerObject != null)
            {
                //Debug.Log($"Found player object for {senderPlayer.NickName}: {playerObject.name}");
                Debug.Log($"Found player object for {senderPlayer.NickName}: {playerObject.name}");
                // 오브젝트에 원하는 동작을 추가할 수 있습니다.

                //멀티플레이어 컴포넌트 캐싱
                multiPlayerMove  = playerObject.GetComponent<MultiPlayerMove>();
                if(multiPlayerMove != null) print("보낸 플레이어의 MultiPlayerMove");
                TextMeshProUGUI myMassageText = multiPlayerMove.textMesh_ChatPack.GetComponent<TextMeshProUGUI>();

                //내가한말 말풍선으로 갱신
                myMassageText.text = saveMassage;
                //말풍선 보이게하자.
                multiPlayerMove.panel_ChatPack.SetActive(true);
                //5초 뒤에는 말풍선을 다시 끄자.
                StartCoroutine(ClosChatPack(playerObject));
                print("말풍선 끄기 시작");

            }
            else
            {
                Debug.Log("Player object not found.");
            }

        }
        

    }

  
    IEnumerator ClosChatPack(GameObject player)
    {
        yield return new WaitForSeconds(5);
        print("받은 플레이어" + player);

        
        print("말풍선끄기");
        //multiPlayerMove = player.GetComponent<MultiPlayerMove>();
        multiPlayerMove.panel_ChatPack.SetActive(false);
        if (multiPlayerMove != null)
        {
            print("멀티플레이어 없음");
        }
        else
        {
            //말풍선끄기
            multiPlayerMove.panel_ChatPack.SetActive(false);

        }
          


    }




    //이벤트가 끝아면 삭제
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }


    /*public void OnEvent(ExitGames.Client.Photon.EventData photonEvent)
    {
        //throw new System.NotImplementedException();

        //누군가 채팅을 입력하면 여기에 콜백이 호출이 될거임.

        //만일, 받은 이벤트가 채팅 이벤트라면
        if()

    }*/

    // PhotonView를 통해 특정 플레이어가 소유한 오브젝트 찾기
    public GameObject FindPlayerObject(Player player)
    {
        foreach (PhotonView photonView in PhotonNetwork.PhotonViews)
        {
            // PhotonView의 소유자와 플레이어가 일치하는지 확인
            if (photonView.Owner == player)
            {
                return photonView.gameObject; // 해당 플레이어의 오브젝트 반환
            }
        }
        return null;
    }







}
