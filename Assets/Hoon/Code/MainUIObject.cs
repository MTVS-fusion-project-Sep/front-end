using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MainUIObject : MonoBehaviour
{

    public GameObject bg_Object;
    public GameObject mainRoom_Object;
    public GameObject myInfo_Object;
    public GameObject playerImg_Object;
    public GameObject imgLogin_Object;
    public GameObject imgRegist_Object;
    public GameObject imgLogout_Object;
    public GameObject loginID_Text_Object;
    public GameObject loginPass_Text_Object;
    public GameObject phID_Object;
    public GameObject phPass_Object;
    public GameObject inputField_ID_Object;
    public GameObject inputField_Pass_Object;
    public GameObject btn_Lobby_Object;
    public GameObject btn_MyInfo_Object;
    public GameObject panel_MyInfo_Object;
    public GameObject panel_Exit;
    //종료오브젝트
    public GameObject btnExit_Menu_Object;
    public GameObject text_UserNameObject;
    public GameObject firstLikeObject;
    public GameObject secondLikeObject;
    public GameObject thirdLikeObject;
    //MyInfo btn
    public GameObject btn_MyInfo_Obejct;
    public GameObject img_RegistComplite_Object;
    public GameObject write_MyMemo_Object;

    // Text elements
    public Text idText;
    public Text passText;
    public Text phID_Text;
    public Text phPass_Text;
    public Text nameTextComp;
    public Text myMemo_TextComp;

    //아아디 필드
    public InputField id_InputField;
    //패스 필드
    public InputField pass_InputField;
    //나의메모
    public InputField writeMomo_Input;

    public Button move_Lobby_Btn;
    Button btn_LoginGet;
    Button btn_Login;
    Button btnViewPass;
    Button btn_NewRegist;
    Button btn_MoveLogin;
    Button btn_Regist;
    Button btn_ExitMenu;
    Button btn_Write;

    

    public bool isViewPass = false;
    public bool isViewExitMenu = false;
    

    void Start()
    {
        print("MainObjectStart");
        Initialize();
        ButtonEvent();
        LikeTextCast();
        Etc();
        //마지막에 캐싱
        OFFUI();
       

    }
    public void Initialize()
    {   
        print("캐싱시작");
        //캐싱검사완료
        bg_Object = GameObject.Find("BG");
        mainRoom_Object = GameObject.Find("MainRoom");
        myInfo_Object = GameObject.Find("Img_MyInfo");
        playerImg_Object = GameObject.Find("PlayerImage");
        imgLogin_Object = GameObject.Find("Img_Login");
        imgRegist_Object = GameObject.Find("Img_Regist");
        imgLogout_Object = GameObject.Find("imgLogout");
        loginID_Text_Object = GameObject.Find("Text_ID");
        loginPass_Text_Object = GameObject.Find("Text_Pass");
        phID_Object = GameObject.Find("Ph_ID");
        phPass_Object = GameObject.Find("Ph_Pass");
        //
        inputField_ID_Object = GameObject.Find("IF_ID");
        inputField_Pass_Object = GameObject.Find("IF_Pass");
        btn_Lobby_Object = GameObject.Find("Btn_Lobby");
        btn_MyInfo_Object = GameObject.Find("Btn_MyInfo");
        panel_MyInfo_Object = GameObject.Find("Panel_MyInfo");
        panel_Exit = GameObject.Find("Panel_Exit");
        btnExit_Menu_Object = GameObject.Find("Btn_ExitMenu");
        move_Lobby_Btn = btn_Lobby_Object.GetComponent<Button>();
        passText = loginPass_Text_Object.GetComponent<Text>();
        phID_Text = phID_Object.GetComponent<Text>();
        phPass_Text = phPass_Object.GetComponent<Text>();
        //아이디잇풋필드 가져오기
        id_InputField = inputField_ID_Object.GetComponent<InputField>();
        pass_InputField = inputField_Pass_Object.GetComponent<InputField>();
        text_UserNameObject = GameObject.Find("Text_UserName");
        nameTextComp = text_UserNameObject.GetComponent<Text>();     
        print("캐싱완료");
        img_RegistComplite_Object = GameObject.Find("Img_RegistComplite");

    }

    public void Etc()
    {
        write_MyMemo_Object = GameObject.Find("IF_MyMemo");
        writeMomo_Input = GameObject.Find("IF_MyMemo").GetComponent<InputField>();
        myMemo_TextComp = GameObject.Find("Text_MyMemo").GetComponent<Text>();
     

    }

    public void ButtonEvent()
    {
        if(MainUI.Instance.loginCount != 0)
        {
            //버튼캐싱
            print("버튼캐싱");
            RegistInfo registInfo = MainUI.Instance.GetComponent<RegistInfo>();
            //로그인 Get
            btn_LoginGet = GameObject.Find("Btn_LoginGet").GetComponent<Button>();
            btn_LoginGet.onClick.AddListener(MainUI.Instance.GetJSONUserInfo);
            //
            btn_Login = GameObject.Find("Btn_Login").GetComponent<Button>();
            btn_Login.onClick.AddListener(MainUI.Instance.TestLocalLoginJson);
            //
            btnViewPass = GameObject.Find("Btn_View_Pass").GetComponent<Button>();
            btnViewPass.onClick.AddListener(MainUI.Instance.ViewPass);
            //
            btn_NewRegist = GameObject.Find("Btn_NewRegist").GetComponent<Button>();
            btn_NewRegist.onClick.AddListener(MainUI.Instance.MoveNewRegist);
            //Btn _MoveLogin
            btn_MoveLogin = GameObject.Find("Btn_MoveLogin").GetComponent<Button>();
            btn_MoveLogin.onClick.AddListener(MainUI.Instance.MoveLogin);
            //Btn _Regist
            btn_Regist = GameObject.Find("Btn_Regist").GetComponent<Button>();
            btn_Regist.onClick.AddListener(registInfo.SaveLocalRegistJSON);
            //Btn_ExitMenu
            btn_ExitMenu = GameObject.Find("Btn_ExitMenu").GetComponent<Button>();
            btn_ExitMenu.onClick.AddListener(MainUI.Instance.ViewExitPanel);
            //Btn_Write
            btn_Write = GameObject.Find("Btn_ExitMenu").GetComponent<Button>();
            btn_Write.onClick.AddListener(MainUI.Instance.ViewExitPanel);

        }


    }

    public void LikeTextCast()
    {
        firstLikeObject = GameObject.Find("Text_First_Like");
        secondLikeObject = GameObject.Find("Text_Second_Like");
        thirdLikeObject = GameObject.Find("Text_Third_Like");

    }

    public void MyInfoCast()
    {
        //내정보버튼
        btn_MyInfo_Obejct = GameObject.Find("Btn_MyInfo");
        Button myinfo_Button = btn_MyInfo_Obejct.GetComponent<Button>();

        //내정보 패널을 가져오자.
        panel_MyInfo_Object = GameObject.Find("Panel_MyInfo");
        Button myInfoPanel = panel_MyInfo_Object.GetComponent<Button>();

    }

    public void OFFUI()
    {
        img_RegistComplite_Object.SetActive(false);
        write_MyMemo_Object.SetActive(false);
    }

}//클래스끝


/*



void Update()
{

}*/









