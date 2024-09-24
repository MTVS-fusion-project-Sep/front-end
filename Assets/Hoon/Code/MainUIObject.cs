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
    public GameObject btnExit_Menu_Object;
    public GameObject text_UserNameObject;

    // Text elements
    public Text idText;
    public Text passText;
    public Text phID_Text;
    public Text phPass_Text;
    public Text nameTextComp;
   

    public Button move_Lobby_Btn;
    //아아디 필드
    public InputField id_InputField;
    //패스 필드
    public InputField pass_InputField;

    Button btn_LoginGet;
    Button btn_Login;


    void Start()
    {
        print("MainObjectStart");
        Initialize();
        
    }
    public void Initialize()
    {   //버튼캐싱
        //로그인 Get
        btn_LoginGet = GameObject.Find("Btn_LoginGet").GetComponent<Button>();
        btn_LoginGet.onClick.AddListener(MainUI.Instance.GetJSONUserInfo);
        //
        btn_Login = GameObject.Find("Btn_Login").GetComponent<Button>();
        btn_Login.onClick.AddListener(MainUI.Instance.TestLocalLoginJson);


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
        //
        id_InputField = inputField_ID_Object.GetComponent<InputField>();
        string inputText = id_InputField.text;
        //print("inputText" + inputText);
        //if (id_InputField == null) print("아이디 인풋필드 없음");
        pass_InputField = inputField_Pass_Object.GetComponent<InputField>();

        text_UserNameObject = GameObject.Find("Text_UserName");
         
        //if (text_UserNameObject == null) print("Text_UserName 오브젝트 없음");
        nameTextComp = text_UserNameObject.GetComponent<Text>();
        //if (nameTextComp == null) print("nameText 없음");
        //print("nameText" + nameTextComp.text);

       
    }




}//클래스끝


/*



void Update()
{

}*/









