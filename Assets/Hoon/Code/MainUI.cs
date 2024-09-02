using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance;

    private void Awake()
    {
        //�ν��Ͻ��� ������
        if (Instance == null)
        {
            //���� ����
            Instance = this;
            // ������Ʈ�� �ı����� �ʰ� ����. // �ʿ信 ���� �߰�
            //DontDestroyOnLoad(gameObject); 
        }
        else
        {
            //�ν��Ͻ��� ������ ����
            Destroy(gameObject);
        }
    }

    //���
    GameObject bg_Object;
    //���η�
    GameObject mainRoom_Object;
    //������
    GameObject myInfo_Object;
    //�ƹ�Ÿ
    GameObject playerImg_Object;
    //�α���ȭ��
    public GameObject imgLogin_Object;
    //�α׾ƿ�
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
    //img_resit
    public GameObject img_Regist_Object;


    //�ƾƵ� �ʵ�
    InputField id_InputField;
    //�н� �ʵ�
    InputField pass_InputField;

    Text idText;
    Text passText;
    Text phID_Text;
    Text phPass_Text;

    string phID_STR = "���̵�(�̸���)";
    string phPass_STR = "��й�ȣ";


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
        MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");
        MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Regist");

        //bg
        bg_Object = GameObject.Find("BG");
        //mainRoom
        mainRoom_Object = GameObject.Find("MainRoom");
        //myInfo
        myInfo_Object = GameObject.Find("Img_MyInfo");
        //playerImg
        playerImg_Object = GameObject.Find("PlayerImage");
        //
        if (imgLogin_Object != null)
        {
            print("imgLogin_Object true");
            imgLogin_Object = GameObject.Find("Img_Login");
        }
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
        
        //�н������ý�Ʈ
        if(loginPass_Text_Obejct == null)
        {
            loginPass_Text_Obejct = GameObject.Find("Text_Pass");
            if (loginPass_Text_Obejct != null)  passText = loginPass_Text_Obejct.GetComponent<Text>();
        }
        if(phID_Obejct == null)
        {
            //ID �̸�����
            phID_Obejct = GameObject.Find("Ph_ID");
            if (phID_Obejct != null)  phID_Text = phID_Obejct.GetComponent<Text>();
        }
        if(phPass_Object == null)
        {
            //Pass �̸�����
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

        //�н�



    }

    // Update is called once per frame
    void Update()
    {

       

    }//������Ʈ

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���ϴ� ������Ʈ ��Ȱ��ȭ
       imgLogin_Object.SetActive(false);
       img_Regist_Object.SetActive(false);

        // �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void ResetLoginText()
    {
        //���̵� �ؽ�Ʈ �ʱ�ȭ
        id_InputField.text = "";
        //��й�ȣ �ؽ�Ʈ �ʱ�ȭ
        pass_InputField.text = "";
        //���̵� Ȧ�� �ؽ�Ʈ �ʱ�ȭ
        phID_Text.text = phID_STR;
        phID_Text.color = Color.gray;
        //�н� Ȧ�� �ؽ�Ʈ �ʱ�ȭ
        phPass_Text.text = phPass_STR;
        phPass_Text.color = Color.gray;
    }

    public void MoveNewRegist()
    {
        //�α����̹����� ������.
        imgLogin_Object.SetActive(false);
        ResetLoginText();




    }

    public void MoveLogin()
    {
        //img_Regist_Object.SetActive(false);
        //�α����̹����� ������.
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
        
        //���̵�� ��й�ȣ ��ġ�ϸ� �α���
        if (enteredID == test_Id && enteredPass == test_Pass)
        {
            //�α������ֱ�
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
        print("������");
        //�����̸� �� Ȯ������.
        if(MainUI.Instance != null)
        {
            GameObject canVas = GameObject.Find("Canvas");
            if (canVas != null) print(1111);
            GameObject imgLogin = canVas.transform.Find("Img_Login").gameObject;
            imgLogin.SetActive(true);
            //MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");
            //MainUI.Instance.imgLogin_Object.SetActive(true);

        }

        //MainUI.Instance.img_Regist_Object.SetActive(true);

        /*if (imgLogin_Object != null)
        {
            print("�̹��� �α��� ������Ʈ ����");
            imgLogin_Object.SetActive(true);
        }
        else
        {
            print("�̹��� �α��� ������Ʈ ���� ");
            imgLogin_Object = GameObject.Find("Img_Login");
            if (imgLogin_Object == null) print("�̹��� �α����� ã���� ����");
            //MainUI.Instance.imgLogin_Object.SetActive(true);
        }
        if (img_Regist_Object != null)
        {
            img_Regist_Object.SetActive(true);
        } 
        else
        {
            print("�̹��� ��� ������Ʈ ���� ");
            img_Regist_Object = GameObject.Find("img_Regist");
            img_Regist_Object.SetActive(true);
        }*/

    }
    

    public void Login()
    {
        if(imgLogin_Object != null) imgLogin_Object.SetActive(false);
        //�α��ο� �Է��� �ؽ�Ʈ �ʱ�ȭ
        ResetLoginText();
        //�̹��� ȸ������ ����
        img_Regist_Object.SetActive(false);

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
