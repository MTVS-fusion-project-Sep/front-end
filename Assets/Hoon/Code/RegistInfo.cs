using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;
using System.IO;

public class RegistInfo : MonoBehaviour
{
    MainUI mainUI;

    GameObject if_ID_Obejct;
    GameObject if_Pass_Obejct;
    GameObject if_Name_Obejct;
    GameObject if_Age_Obejct;

    InputField id_Regist_InputField;
    InputField pass_Regist_InputField;
    InputField name_Regist_InputField;
    InputField age_Regist_InputField;


    GameObject ph_Regist_ID_Object;
    GameObject ph_Rigist_Pass_Obejct;
    GameObject ph_Rigist_Name_Obejct;
    GameObject ph_Rigist_Age_Obejct;



    Text ph_Regist_ID_Text;
    Text ph_Regist_Pass_Text;
    Text ph_Regist_Name_Text;
    Text ph_Regist_Age_Text;

    string existingContent;

    // Start is called before the first frame update
    void Start()
    {

        mainUI = gameObject.GetComponent<MainUI>();

        //��ǲ�ʵ� ���̵� ������Ʈ
        if_ID_Obejct = GameObject.Find("IF_Regist_ID");
        //��ǲ�ʵ� ������Ʈ
        if(id_Regist_InputField != null) id_Regist_InputField = if_ID_Obejct.GetComponent<InputField>();
        //�÷��̽�Ȧ�� ������Ʈ
        ph_Regist_ID_Object = GameObject.Find("Ph_Regist_ID");
        //�÷��̽�Ȧ�� ���̵� �ؽ�Ʈ 
        if (ph_Regist_ID_Text != null)  ph_Regist_ID_Text = ph_Regist_ID_Object.GetComponent<Text>();

        //��ǲ�ʵ� �н� ������Ʈ
        if_Pass_Obejct = GameObject.Find("IF_Regist_Pass");
        //��ǲ�ʵ� ������Ʈ
        if (pass_Regist_InputField != null) pass_Regist_InputField = if_Pass_Obejct.GetComponent<InputField>();
        //�÷��̽�Ȧ��
        ph_Rigist_Pass_Obejct = GameObject.Find("Ph_Regist_Pass");
        //�÷��̽�Ȧ�� �н� �ؽ�Ʈ
        if(ph_Regist_Pass_Text != null) ph_Regist_Pass_Text = ph_Rigist_Pass_Obejct.GetComponent <Text>();

        if_Name_Obejct = GameObject.Find("IF_Regist_Name");
        if(name_Regist_InputField != null) name_Regist_InputField = if_ID_Obejct.GetComponent<InputField>();
        ph_Rigist_Name_Obejct = GameObject.Find("Ph_Regist_Name");

        //mainUI.imgLogin_Object.SetActive(true);





    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveTest()
    {
        string idText = id_Regist_InputField.text;
        string passText = pass_Regist_InputField.text;

        //���̵� �Է��Ѱ� ���� null�̸�
        if (string.IsNullOrEmpty(idText))
        {
            ph_Regist_ID_Text.text = "���̵��Է��ϼ���";
            ph_Regist_ID_Text.color = Color.red;
        }
        //��й�ȣ �Է��Ѱ� ���� null�̸�
        if (string.IsNullOrEmpty(passText))
        {
            ph_Regist_Pass_Text.text = "��й�ȣ���Է��ϼ���";
            ph_Regist_Pass_Text.color = Color.red;
        }
        //�Է��Ѱ� �ִٸ�
        else
        {
           
            //���ڿ��� �����Ұ�� + �����̸�.
            //C:\Users\Admin\AppData\LocalLow\DefaultCompany\front-end
            string path = Application.persistentDataPath + "/SaveRegist.txt";
            //���Ͽ� ����� ���� ��.
            string content = "ID" + ":" + idText + "," + "Password" + ":" + passText +"\n";
            // 
            // ������ �����ϴ���, �׸��� ������ ������ �ִ��� Ȯ��
            if (File.Exists(path))
            {
                //pah�� ��� �ý�Ʈ�� ��������.
                existingContent = File.ReadAllText(path);

                //content�� ���ԵǾ� �ִٸ�
                if (existingContent.Contains(content))
                {
                    id_Regist_InputField.text = "";
                    ph_Regist_ID_Text.text = "���̵��ߺ��˴ϴ�";
                    ph_Regist_ID_Text.color = Color.red;
                    return;

                }
                
                print("SaveComplite");
                existingContent = existingContent + content;

                
            }

            //using System.IO; ��Ŭ��� �������. //������ �ؽ�Ʈ�� ����������.
            File.WriteAllText(path, existingContent);

        }







    }
    public void SaveReistInfo()
    {

    }




}//Ŭ������






