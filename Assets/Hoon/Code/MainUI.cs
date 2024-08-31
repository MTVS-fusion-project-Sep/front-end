using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class MainUI : MonoBehaviour
{
    //배경
    GameObject bg_Object;
    //메인룸
    GameObject mainRoom_Object;
    //내정보
    GameObject myInfo_Object;
    //아바타
    GameObject playerImg_Object;
    //로그인화면
    GameObject imgLogin_Object;
    //로그아웃화면
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

    //아아디 필드
    InputField id_InputField;
    //패스 필드
    InputField pass_InputField;

    Text idText;
    Text passText;
    Text phID_Text;
    Text phPass_Text;


    //test 아이디
    string test_Id = "1111";
    //test 비밀번호
    string test_Pass = "2222";


    //아이디
    string current_Id = "mtvs3th";
    //비밀번호
    string current_Pass = "2024";



    bool isRoomActive = false;
    //
    bool isViewPass = false;

    //로비를 제외하고 버튼은 계속 유지합니다.
    // Start is called before the first frame update
    void Start()
    {
        //bg
        bg_Object = GameObject.Find("BG");
        //mainRoom
        mainRoom_Object = GameObject.Find("MainRoom");
        //myInfo
        myInfo_Object = GameObject.Find("Img_MyInfo");
        //playerImg
        playerImg_Object = GameObject.Find("PlayerImage");
        //
        imgLogin_Object = GameObject.Find("Img_Login");
        //
        imgLogout_Object = GameObject.Find("imgLogout");
        //
        loginID_Text_Obejct = GameObject.Find("Text_ID");
        idText = loginID_Text_Obejct.GetComponent<Text>();
        //패스워드택스트
        loginPass_Text_Obejct = GameObject.Find("Text_Pass");
        passText = loginPass_Text_Obejct.GetComponent<Text>();
        //ID 미리보기
        phID_Obejct = GameObject.Find("Ph_ID");
        phID_Text = phID_Obejct.GetComponent<Text>();
        //Pass 미리보기
        phPass_Object = GameObject.Find("Ph_Pass");
        phPass_Text = phPass_Object.GetComponent<Text>();
        //
        inputField_ID_Obejct = GameObject.Find("IF_ID");
        id_InputField = inputField_ID_Obejct.GetComponent<InputField>();
        //
        inputField_Pass_Obejct = GameObject.Find("IF_Pass");
        pass_InputField = inputField_Pass_Obejct.GetComponent<InputField>();


        //패스



    }

    // Update is called once per frame
    void Update()
    {



    }//업데이트

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
    public void TestChickLogin()
    {
        //아이디 필드의 텍스트 가져오기
        string enteredID = id_InputField.text;
        //패스 필드의 텍스트 가져오기
        string enteredPass = pass_InputField.text;
        
        if (enteredID == test_Id && enteredPass == test_Pass)
        {
            
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
        }

        


    }



    public void CheckLogin()
    {
        //아이디 필드의 텍스트 가져오기
        string enteredID = idText.text;
        //패스 필드의 텍스트 가져오기
        string enteredPass = passText.text;

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
        //변수이름 잘 확인하자.
        if (imgLogin_Object != null) imgLogin_Object.SetActive(true);
    }
    

    public void Login()
    {
        if(imgLogin_Object != null) imgLogin_Object.SetActive(false);
        
       
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



}
