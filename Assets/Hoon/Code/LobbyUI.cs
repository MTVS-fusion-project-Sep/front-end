using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    GameObject btn_LobbyExit;
    

    // Start is called before the first frame update
    void Start()
    {
        btn_LobbyExit = GameObject.Find("Btn_LobbyExit");
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LobbyExit()
    {
        // 씬을 로드하고 씬 로드 완료 후 호출될 메서드를 이벤트에 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("HoonMainScene");

    }
    // 씬이 로드된 후 호출되는 콜백 메서드
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (MainUI.Instance != null)
        {
            print("메인있음");

            // 씬이 로드된 후, 오브젝트를 찾고 참조를 저장
            MainUI.Instance.img_Regist_Object = GameObject.Find("Img_Regist");
            MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");

            // 오브젝트 비활성화
            MainUI.Instance.img_Regist_Object.SetActive(false);
            MainUI.Instance.imgLogin_Object.SetActive(false);


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
}
