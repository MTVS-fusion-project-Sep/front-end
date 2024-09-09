using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
//파일불러오기
using System.IO;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;

    private void Awake()
    {
        //인스턴스가 없으면
        if (Instance == null)
        {
            //나를 생성
            Instance = this;
            // 오브젝트를 파괴하지 않고 유지. // 필요에 따라 추가
            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            //인스턴스가 잇으면 삭제
            Destroy(gameObject);
        }
    }

    //배경
    GameObject bg_Object;
    //메인룸
    GameObject mainRoom_Object;
    //내정보
    GameObject myInfo_Object;
    //아바타
    GameObject playerImg_Object;
    //로그인화면
    public GameObject imgLogin_Object;
    //회원가입
    GameObject imgRegist_Object;
    //로그아웃
    GameObject imgLogout_Object;
    //ID
    GameObject loginID_Text_Obejct;
    //Pass
    GameObject loginPass_Text_Obejct;
    //ID 미리보기
    GameObject phID_Obejct;
    //pass미리보기
    GameObject phPass_Object;
    //id_Input 
    GameObject inputField_ID_Obejct;
    //pass_Input
    GameObject inputField_Pass_Obejct;
    //img_resit
    public GameObject img_Regist_Object;
    //Looby button
    GameObject btn_Lobby_Obejct;
    //MyInfo btn
    GameObject btn_MyInfo_Obejct;
    //MyInfo Panel
    GameObject panel_MyInfo_Object;
    //로비이동 버튼
    public Button move_Lobby_Btn;
    //종료패널
    GameObject panel_Exit;

    GameObject btnExit_Menu_Object;

    //아아디 필드
    InputField id_InputField;
    //패스 필드
    InputField pass_InputField;

    Text idText;
    Text passText;
    Text phID_Text;
    Text phPass_Text;

    string phID_STR = "아이디(이메일)";
    string phPass_STR = "비밀번호";


    //test 아이디
    string test_Id = "1111";
    //test 비밀번호
    string test_Pass = "2222";


    //아이디
    string current_Id = "mtvs3th";
    //비밀번호
    string current_Pass = "2024";

    //로그출력
    string log;

    string enteredID;
    string enteredPass;
    //유저정보 불러오기
    string loadUserInfo;

    bool isRoomActive = false;
    //
    bool isViewPass = false;
    bool isViewMyInfo = false;
    bool isViewExitMenu = false;

    //로비를 제외하고 버튼은 계속 유지합니다.
    // Start is called before the first frame update
    void Start()
    {
        //로그인 오브젝트
        imgLogin_Object = GameObject.Find("Img_Login");
        //회원가입 오브젝트
        imgRegist_Object = GameObject.Find("Img_Regist");
        if (imgLogin_Object != null)
        {
            print("imgLogin_Object true");
            imgLogin_Object = GameObject.Find("Img_Login");
           
        }
        //Lobby button
        btn_Lobby_Obejct = GameObject.Find("Btn_Lobby");
        move_Lobby_Btn = btn_Lobby_Obejct.GetComponent<Button>();
        //bg
        bg_Object = GameObject.Find("BG");
        //mainRoom
        mainRoom_Object = GameObject.Find("MainRoom");
        //myInfo
        myInfo_Object = GameObject.Find("Img_MyInfo");
        //playerImg
        playerImg_Object = GameObject.Find("PlayerImage");
        //
       
       /* else
        {
            print("imgLogin_Object null");
            MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");
        }*/
        //
        imgLogout_Object = GameObject.Find("imgLogout");
        //
        if (loginID_Text_Obejct == null)
        {
            loginID_Text_Obejct = GameObject.Find("Text_ID");
            if(loginID_Text_Obejct != null) idText = loginID_Text_Obejct.GetComponent<Text>();
        }
        
        //패스워드택스트
        if(loginPass_Text_Obejct == null)
        {
            loginPass_Text_Obejct = GameObject.Find("Text_Pass");
            if (loginPass_Text_Obejct != null)  passText = loginPass_Text_Obejct.GetComponent<Text>();
        }
        if(phID_Obejct == null)
        {
            //ID 미리보기
            phID_Obejct = GameObject.Find("Ph_ID");
            if (phID_Obejct != null)  phID_Text = phID_Obejct.GetComponent<Text>();
        }
        if(phPass_Object == null)
        {
            //Pass 미리보기
            phPass_Object = GameObject.Find("Ph_Pass");
            if (phPass_Object != null)  phPass_Text = phPass_Object.GetComponent<Text>();
        }
        if(inputField_ID_Obejct == null)
        {
            
            inputField_ID_Obejct = GameObject.Find("IF_ID");
            if (inputField_ID_Obejct != null)  id_InputField = inputField_ID_Obejct.GetComponent<InputField>();
        }
        if(inputField_Pass_Obejct == null)
        {
            //
            inputField_Pass_Obejct = GameObject.Find("IF_Pass");
            if (inputField_Pass_Obejct != null)  pass_InputField = inputField_Pass_Obejct.GetComponent<InputField>();
        }
        
        //
        img_Regist_Object = GameObject.Find("Img_Regist");
     

        //내정보버튼
        btn_MyInfo_Obejct = GameObject.Find("Btn_MyInfo");
        Button myinfo_Button = btn_MyInfo_Obejct.GetComponent<Button>();

        //내정보 패널을 가져오자.
        panel_MyInfo_Object = GameObject.Find("Panel_MyInfo");
        panel_MyInfo_Object.SetActive(false);


        panel_Exit = GameObject.Find("Panel_Exit");
        panel_Exit.SetActive(false);

        //버튼exit
         btnExit_Menu_Object = GameObject.Find("Btn_ExitMenu");

        OFFIMG();


    }

    // Update is called once per frame
    void Update()
    {

       

    }//업데이트

    void OFFIMG()
    {
        //로그인 오브젝트 끄기
        imgLogin_Object.SetActive(false);
        //회원가입 오브젝트 끄기
        img_Regist_Object.SetActive(false);
    }

    public void LoadTest()
    {
        //입력된 아이디 가져오기
        string idText = id_InputField.text;
        //입력된 패스워드 가져오기
        string passText = pass_InputField.text;

        //문자열로 저장할경로 + 파일이름.
        //C:\Users\Admin\AppData\LocalLow\DefaultCompany\front-end
        string path = Application.persistentDataPath + "/SaveRegist.txt";
        //파일에 저장될 모양과 값.
        string content = "ID" + ":" + idText + "," + "Password" + ":" + passText + "\n";

        // 파일이 존재하는지, 그리고 동일한 내용이 있는지 확인
        if (File.Exists(path))
        {
            //pah의 모든 택스트를 가져오자.
            loadUserInfo = File.ReadAllText(path);

            //content가 포함되어 있다면
            if (loadUserInfo.Contains(content))
            {
               /* id_Regist_InputField.text = "";
                ph_Regist_ID_Text.text = "아이디가중복됩니다";
                ph_Regist_ID_Text.color = Color.red;
                return;*/
                
            }

            //갱신
            //loadUserInfo = loadUserInfo + content;


        }

        //using System.IO; 인클루드 해줘야함. //파일을 텍스트에 저장해주자.
        //File.WriteAllText(path, loadUserInfo);
        print("LoadComplite");

    }



    public void ViewExitPanel()
    {
        if(isViewExitMenu == false)
        {
            panel_Exit.SetActive(true);
            isViewExitMenu = true;
        }
        else
        {
            panel_Exit.SetActive(false);
            isViewExitMenu = false;
        }

    }

    public void ViewMyInfo()
    {
        if(isViewMyInfo == false)
        {
            panel_MyInfo_Object.SetActive(true);
            isViewMyInfo = true;
        }
        else
        {
            panel_MyInfo_Object.SetActive(false);
            isViewMyInfo = false;
        }
      
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 원하는 오브젝트 비활성화
       imgLogin_Object.SetActive(false);
       img_Regist_Object.SetActive(false);

        // 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ResetLoginText()
    {
        //아이디 텍스트 초기화
        id_InputField.text = "";
        //비밀번호 텍스트 초기화
        pass_InputField.text = "";
        //아이디 홀드 텍스트 초기화
        phID_Text.text = phID_STR;
        phID_Text.color = Color.gray;
        //패스 홀드 텍스트 초기화
        phPass_Text.text = phPass_STR;
        phPass_Text.color = Color.gray;
    }

    public void MoveNewRegist()
    {
        //로그인이미지를 꺼주자.
        imgLogin_Object.SetActive(false);
        ResetLoginText();


    }

    public void MoveLogin()
    {
        //img_Regist_Object.SetActive(false);
        //로그인이미지를 켜주자.
        imgLogin_Object.SetActive(true);
    }
        
    public void ViewPass()
    {
        //print(1111);
        if(isViewPass == false)
        {
            pass_InputField.contentType = InputField.ContentType.Standard;
            
            isViewPass = true;
        }
        else
        {
            //print(2222);
            pass_InputField.contentType = InputField.ContentType.Password;
            isViewPass = false;
        }
        // 텍스트를 강제로 재설정하여 변경된 contentType 반영
        pass_InputField.ForceLabelUpdate();
        string currentText = pass_InputField.text;
        pass_InputField.text = "";
        pass_InputField.text = currentText;


    }
    public void TestCheckLogin()
    {
        print("11");
        //입력된 아이디 가져오기
        string idText = id_InputField.text;
        //입력된 패스워드 가져오기
        string passText = pass_InputField.text;

        //문자열로 저장할경로 + 파일이름.
        //C:\Users\Admin\AppData\LocalLow\DefaultCompany\front-end
        string path = Application.persistentDataPath + "/SaveRegist.txt";
        //파일에 저장될 모양과 값.
        string content = "ID" + ":" + idText + "," + "Password" + ":" + passText + "\n";

        // 파일이 존재하는지, 그리고 동일한 내용이 있는지 확인
        if (File.Exists(path))
        {
            //pah의 모든 택스트를 가져오자.
            loadUserInfo = File.ReadAllText(path);

            //content가 포함되어 있다면
            if (loadUserInfo.Contains(content))
            {
                //로그인해주기
                Login();
                print("LoadComplite");
                return;
                /* id_Regist_InputField.text = "";
                 ph_Regist_ID_Text.text = "아이디가중복됩니다";
                 ph_Regist_ID_Text.color = Color.red;
                 return;*/

            }
            else 
            {
                if (loadUserInfo.Contains(idText))
                {
                    print("아이디가 틀림");
                    //빈문자열로 해줍니다. //영어로만 들어 갑니다.
                    id_InputField.text = "";
                    phID_Text.text = "아이디가 틀림";
                    phID_Text.color = Color.red;
                }
                if (loadUserInfo.Contains(passText))
                {
                    print("비밀번호가 틀림");
                    pass_InputField.text = "";
                    phPass_Text.text = "비밀번호 틀림";
                    phPass_Text.color = Color.red;
                }
            }
            
                  
            //갱신
            //loadUserInfo = loadUserInfo + content;


        }

        //using System.IO; 인클루드 해줘야함. //파일을 텍스트에 저장해주자.
        //File.WriteAllText(path, loadUserInfo);
       

       /* if (id_InputField != null)
        {
            //아이디 필드의 텍스트 가져오기
            enteredID = id_InputField.text;
        }
        else
        {
            print("아이디 인풋필드 없음");
        }
        if (pass_InputField != null)
        {
            //패스 필드의 텍스트 가져오기
            enteredPass = pass_InputField.text;
        }
        else
        {
            print("패스 인풋필드 없음");
        }

        //아이디와 비밀번호 일치하면 로그인
        if (enteredID == test_Id && enteredPass == test_Pass)
        {
            //로그인해주기
            Login();
            return;

        }
        if (enteredID == test_Id)
        {
            print("아이디가 맞습니다");
           
        }
        else
        {
            print("아이디가 틀림" );
            //빈문자열로 해줍니다. //영어로만 들어 갑니다.
            id_InputField.text = "";
            phID_Text.text = "아이디가 틀림";
            phID_Text.color = Color.red;

        }
        if (enteredPass == test_Pass)
        {
            print("비밀번호가 맞습니다");

        }
        else
        {
            print("비밀번호가 틀림");
            pass_InputField.text = "";
            phPass_Text.text = "비밀번호 틀림";
            phPass_Text.color = Color.red;
        }*/
        

    }


    public void CheckLogin()
    {
        //아이디 필드의 텍스트 가져오기
         enteredID = idText.text;
        //패스 필드의 텍스트 가져오기
         enteredPass = passText.text;

        if(enteredID == current_Id)
        {
            print(111);
        }
        else
        {
            print(222);
        }


    }

    public void LogData()
    {

    }
    public void LogOut()
    {
        print("나가기");
        //변수이름 잘 확인하자.
        if(MainUI.Instance != null)
        {
            GameObject canVas = GameObject.Find("HoonCanvas");
            if (canVas != null) print(1111);
            GameObject imgLogin = canVas.transform.Find("Img_Login").gameObject;
            imgLogin.SetActive(true);
            imgRegist_Object.SetActive(true);
            //MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");
            //MainUI.Instance.imgLogin_Object.SetActive(true);

        }

        //MainUI.Instance.img_Regist_Object.SetActive(true);

        /*if (imgLogin_Object != null)
        {
            print("이미지 로그인 오브젝트 있음");
            imgLogin_Object.SetActive(true);
        }
        else
        {
            print("이미지 로그인 오브젝트 없음 ");
            imgLogin_Object = GameObject.Find("Img_Login");
            if (imgLogin_Object == null) print("이미지 로그인을 찾을수 없음");
            //MainUI.Instance.imgLogin_Object.SetActive(true);
        }
        if (img_Regist_Object != null)
        {
            img_Regist_Object.SetActive(true);
        } 
        else
        {
            print("이미지 등록 오브젝트 없음 ");
            img_Regist_Object = GameObject.Find("img_Regist");
            img_Regist_Object.SetActive(true);
        }*/

    }
       
    public void Login()
    {
        if(imgLogin_Object != null) imgLogin_Object.SetActive(false);
        //로그인에 입력한 텍스트 초기화
        ResetLoginText();
        //이미지 회원가입 끄기
        img_Regist_Object.SetActive(false);

    }

    //메인키를 누르면 모든 UI를 보여주게하자.
    public void ViewMain()
    {
        //BG를 끄자.
        bg_Object.SetActive(true);
        //mainRoom 끄자
        mainRoom_Object.SetActive(true);
        //myInfo 끄자
        myInfo_Object.SetActive(true);
        //PlayerImg 끄자
        playerImg_Object.SetActive(true);


    }

    //Room 버튼을 누르면 모든 UI를 끄고 Room으로 이동
    public void ViewRoom()
    {

        //BG를 끄자.
        bg_Object.SetActive(false);
        //mainRoom 끄자
        mainRoom_Object.SetActive(false);
        //myInfo 끄자
        myInfo_Object.SetActive(false);
        //PlayerImg 끄자
        playerImg_Object.SetActive(false);
        //room을 켜주자.
        isRoomActive = true;




    }
  
    public void MoveLobby()
    {
        SceneManager.LoadScene("HoonLobbyScene");
       
    }

    public void MoveChat()
    {

    }

    public void PrintLog(string message)
    {
        log += message + '\n';
        //text_logText.text = log;
    }


}
