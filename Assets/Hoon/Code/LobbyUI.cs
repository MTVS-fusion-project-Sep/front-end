using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using Unity.VisualScripting;
using WebSocketSharp;
using static MainUI;

public class LobbyUI : MonoBehaviour
{
    GameObject btn_LobbyExit;
    MainUIObject mainUiObject;
    MultiPlayerMove multiPlayerMove;
    

    public Text userName;
    public GameObject img_MoveScene_Object;
    public GameObject Img_MvoveOhterRoom;
    public GameObject Input_OtherRoom_Object;
    public GameObject Text_OtherRoom_Object;
    public GameObject otherPlayer;
    public GameObject Ph_OtherRoom_Object;
    public string userId;

    // Start is called before the first frame update
    void Start()
    {
        
        btn_LobbyExit = GameObject.Find("Btn_LobbyExit");
        
        if(userName.text.Contains("User"))
        {
            userName.text = "";
             
        }

        //img_MoveScene_Object 캐싱
        img_MoveScene_Object = GameObject.Find("Img_MoveScene");
        print("img_MoveScene_Object 캐싱");

        //1초뒤에 비활성
        StartCoroutine(CloseUIDelay());
        //img_MoveScene_Object.SetActive(false);

        //다른룸으로 이동 UI를 끄자.
        Img_MvoveOhterRoom.SetActive(false);

       
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public void SaveOntriggerPlayer(GameObject player)
    {
        otherPlayer = player;


    }


    public void MoveSceneController(string objectName)
    {
        print("씬컨트롤");

        if (objectName == "MoveMyRoomObject")
        {
            PhotonView pv = otherPlayer.GetComponent<PhotonView>();
            if (pv.IsMine)
            {
                Img_MvoveOhterRoom.SetActive(true);
            }
        }

        //다른룸으로 이동 버튼
        if(objectName == "Btn_MoveOtherRoom")
        {
            print("Btn_MoveOtherRoom object");
            //
            Text moveUserName = Text_OtherRoom_Object.GetComponent<Text>();
            string userNickName = moveUserName.text;
            print("이동할방의 유저이름" + userNickName);


            //Input_OtherRoom .text 에서 닉네임으로 가져오자.
            InputField inputOtherRoom = Input_OtherRoom_Object.GetComponent<InputField>();
            //inputOtherRoom.GetComponent<PlaceHolder>
          
            if (userNickName.IsNullOrEmpty())
            {
                inputOtherRoom.text = "닉네임입력";
                moveUserName.color = Color.red;
                //inputOtherRoom.text = color.red;
            }
            //뭐라도 적었다면
            else
            {
                //플레이어 리스트에서 이름을 가져오자.
                Player[] playerList = PhotonNetwork.PlayerList;

                bool isUserStay = false;
                foreach (Player players in playerList)
                {
                    //닉네임이 일치한다면
                    if (players.NickName == userNickName)
                    {
                        print("방주인 있습니다" + userNickName);
                        //유저있음
                        isUserStay = true;

                        //모든 포톤뷰를 찾고 owner 네임이 방주인과 일치하는 component를 찾자
                        foreach (PhotonView view in FindObjectsOfType<PhotonView>())
                        {
                            //방주인이 오너라면 
                            if (view.Owner.NickName == userNickName)
                            {
                                print("오너의 이름" + view.Owner.NickName);
                                MultiPlayerMove multiPlayerMove = view.GetComponent<MultiPlayerMove>();
                                string userId = multiPlayerMove.myId;
                                print("userNickname"+ userNickName + "userId" + multiPlayerMove.myId);
                            }

                        }

                        break;
                    }
                    //유저가 없다면
                    if(!isUserStay)
                    {
                        print("유저가 없습니다");
                        inputOtherRoom.text = "유저없음";
                        moveUserName.color = Color.red;
                    }
                
                }

            }
            
            

            //닉네임이랑 일치하는 id를 가져오자.
            //
            //다른룸으로 이동하자.

        }
    }

    public void ColoseUI(string objectName)
    {
        if(objectName  == "Btn_CloseUI")
        {
            Img_MvoveOhterRoom.SetActive(false);
        }
    }


    IEnumerator CloseUIDelay()
    {
        //1초뒤에 호출해주자.
        yield return new WaitForEndOfFrame();

        if (img_MoveScene_Object)
        {
            img_MoveScene_Object.SetActive(false);
            print("img_MoveScene_Object 끄기");
        }

    }

    public void LobbyExit()
    {
        // 씬을 로드하고 씬 로드 완료 후 호출될 메서드를 이벤트에 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(0);
    }
    // 씬이 로드된 후 호출되는 콜백 메서드
    //void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        //StartCoroutine(WaitAndDisableObjects());
            
    }

    // Coroutine을 사용하여 씬 로드 후 약간의 지연을 주고 오브젝트를 비활성화
    IEnumerator WaitAndDisableObjects()
    {

        yield return new WaitForEndOfFrame(); // 또는 적절한 지연을 줄 수 있음

        print("프레임 이후에 ");
        if (MainUI.Instance != null)
        {
            print("메인있음");

            // 씬이 로드된 후, 오브젝트를 찾고 참조를 저장
            //mainUiObject.imgRegist_Object = GameObject.Find("Img_Regist");
            //mainUiObject.imgLogin_Object = GameObject.Find("Img_Login");

            // 오브젝트 비활성화
            mainUiObject.imgRegist_Object.SetActive(false);
            mainUiObject.imgLogin_Object.SetActive(false);


           
            /*if (MainUI.Instance.imgLogin_Object)
            {
                print("로그인이미지 있음");
                MainUI.Instance.imgLogin_Object.SetActive(false);
            }    
           else
            {
                print("로그인이미지 없음" + "찾기");
                MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");
                MainUI.Instance.imgLogin_Object.SetActive(false);

            }
            if(MainUI.Instance.img_Regist_Object)
            {
                print("등록이미지 있음");
                MainUI.Instance.img_Regist_Object.SetActive(false);
            }
            else
            {
                print("등록 이미지 없음" + "찾기");
                MainUI.Instance.img_Regist_Object = GameObject.Find("Img_Regist");
                MainUI.Instance.img_Regist_Object.SetActive(false);

            }*/
        }
        else
        {
            print("메인없음");            
        }
        // 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;

        
    }


}//클래스끝
