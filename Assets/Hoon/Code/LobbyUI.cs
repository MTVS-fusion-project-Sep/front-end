using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    GameObject btn_LobbyExit;
    MainUIObject mainUiObject;
    public Text userName;
    public GameObject img_MoveScene_Object;

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

    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
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
