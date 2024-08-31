using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class MainUI : MonoBehaviour
{
    //���
    GameObject bg_Object;
    //���η�
    GameObject mainRoom_Object;
    //������
    GameObject myInfo_Object;
    //�ƹ�Ÿ
    GameObject playerImg_Object;
    //�α���ȭ��
    GameObject imgLogin_Object;
    //�α׾ƿ�ȭ��
    GameObject imgLogout_Object;
    //ID
    GameObject loginID_Text_Obejct;
    //Pass
    GameObject loginPass_Text_Obejct;
    //ID �̸�����
    GameObject phID_Obejct;
    //pass�̸�����
    GameObject phPass_Object;
    //id_Input 
    GameObject inputField_ID_Obejct;
    //pass_Input
    GameObject inputField_Pass_Obejct;

    //�ƾƵ� �ʵ�
    InputField id_InputField;
    //�н� �ʵ�
    InputField pass_InputField;

    Text idText;
    Text passText;
    Text phID_Text;
    Text phPass_Text;


    //test ���̵�
    string test_Id = "1111";
    //test ��й�ȣ
    string test_Pass = "2222";


    //���̵�
    string current_Id = "mtvs3th";
    //��й�ȣ
    string current_Pass = "2024";



    bool isRoomActive = false;
    //
    bool isViewPass = false;

    //�κ� �����ϰ� ��ư�� ��� �����մϴ�.
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
        //�н������ý�Ʈ
        loginPass_Text_Obejct = GameObject.Find("Text_Pass");
        passText = loginPass_Text_Obejct.GetComponent<Text>();
        //ID �̸�����
        phID_Obejct = GameObject.Find("Ph_ID");
        phID_Text = phID_Obejct.GetComponent<Text>();
        //Pass �̸�����
        phPass_Object = GameObject.Find("Ph_Pass");
        phPass_Text = phPass_Object.GetComponent<Text>();
        //
        inputField_ID_Obejct = GameObject.Find("IF_ID");
        id_InputField = inputField_ID_Obejct.GetComponent<InputField>();
        //
        inputField_Pass_Obejct = GameObject.Find("IF_Pass");
        pass_InputField = inputField_Pass_Obejct.GetComponent<InputField>();


        //�н�



    }

    // Update is called once per frame
    void Update()
    {



    }//������Ʈ

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
        // �ؽ�Ʈ�� ������ �缳���Ͽ� ����� contentType �ݿ�
        pass_InputField.ForceLabelUpdate();
        string currentText = pass_InputField.text;
        pass_InputField.text = "";
        pass_InputField.text = currentText;


    }
    public void TestChickLogin()
    {
        //���̵� �ʵ��� �ؽ�Ʈ ��������
        string enteredID = id_InputField.text;
        //�н� �ʵ��� �ؽ�Ʈ ��������
        string enteredPass = pass_InputField.text;
        
        if (enteredID == test_Id && enteredPass == test_Pass)
        {
            
            Login();
            return;
        }
        if (enteredID == test_Id)
        {
            print("���̵� �½��ϴ�");
           
        }
        else
        {
            print("���̵� Ʋ��" );
            //���ڿ��� ���ݴϴ�. //����θ� ��� ���ϴ�.
            id_InputField.text = "";
            phID_Text.text = "���̵� Ʋ��";
            phID_Text.color = Color.red;

        }
        if (enteredPass == test_Pass)
        {
            print("��й�ȣ�� �½��ϴ�");

        }
        else
        {
            print("��й�ȣ�� Ʋ��");
            pass_InputField.text = "";
            phPass_Text.text = "��й�ȣ Ʋ��";
            phPass_Text.color = Color.red;
        }

        


    }



    public void CheckLogin()
    {
        //���̵� �ʵ��� �ؽ�Ʈ ��������
        string enteredID = idText.text;
        //�н� �ʵ��� �ؽ�Ʈ ��������
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
        //�����̸� �� Ȯ������.
        if (imgLogin_Object != null) imgLogin_Object.SetActive(true);
    }
    

    public void Login()
    {
        if(imgLogin_Object != null) imgLogin_Object.SetActive(false);
        
       
    }

    //����Ű�� ������ ��� UI�� �����ְ�����.
    public void ViewMain()
    {
        //BG�� ����.
        bg_Object.SetActive(true);
        //mainRoom ����
        mainRoom_Object.SetActive(true);
        //myInfo ����
        myInfo_Object.SetActive(true);
        //PlayerImg ����
        playerImg_Object.SetActive(true);


    }

    //Room ��ư�� ������ ��� UI�� ���� Room���� �̵�
    public void ViewRoom()
    {

        //BG�� ����.
        bg_Object.SetActive(false);
        //mainRoom ����
        mainRoom_Object.SetActive(false);
        //myInfo ����
        myInfo_Object.SetActive(false);
        //PlayerImg ����
        playerImg_Object.SetActive(false);
        //room�� ������.
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
