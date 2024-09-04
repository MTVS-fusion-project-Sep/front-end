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

        //인풋필드 아이디 오브젝트
        if_ID_Obejct = GameObject.Find("IF_Regist_ID");
        //인풋필드 컴포넌트
        if(id_Regist_InputField != null) id_Regist_InputField = if_ID_Obejct.GetComponent<InputField>();
        //플레이스홀드 오브젝트
        ph_Regist_ID_Object = GameObject.Find("Ph_Regist_ID");
        //플레이스홀드 아이드 텍스트 
        if (ph_Regist_ID_Text != null)  ph_Regist_ID_Text = ph_Regist_ID_Object.GetComponent<Text>();

        //인풋필드 패스 오브젝트
        if_Pass_Obejct = GameObject.Find("IF_Regist_Pass");
        //인풋필드 컴포넌트
        if (pass_Regist_InputField != null) pass_Regist_InputField = if_Pass_Obejct.GetComponent<InputField>();
        //플레이스홀드
        ph_Rigist_Pass_Obejct = GameObject.Find("Ph_Regist_Pass");
        //플레이스홀드 패스 텍스트
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

        //아이디 입력한게 없고 null이면
        if (string.IsNullOrEmpty(idText))
        {
            ph_Regist_ID_Text.text = "아이디를입력하세요";
            ph_Regist_ID_Text.color = Color.red;
        }
        //비밀번호 입력한게 없고 null이면
        if (string.IsNullOrEmpty(passText))
        {
            ph_Regist_Pass_Text.text = "비밀번호를입력하세요";
            ph_Regist_Pass_Text.color = Color.red;
        }
        //입력한게 있다면
        else
        {
           
            //문자열로 저장할경로 + 파일이름.
            //C:\Users\Admin\AppData\LocalLow\DefaultCompany\front-end
            string path = Application.persistentDataPath + "/SaveRegist.txt";
            //파일에 저장될 모양과 값.
            string content = "ID" + ":" + idText + "," + "Password" + ":" + passText +"\n";
            // 
            // 파일이 존재하는지, 그리고 동일한 내용이 있는지 확인
            if (File.Exists(path))
            {
                //pah의 모든 택스트를 가져오자.
                existingContent = File.ReadAllText(path);

                //content가 포함되어 있다면
                if (existingContent.Contains(content))
                {
                    id_Regist_InputField.text = "";
                    ph_Regist_ID_Text.text = "아이디가중복됩니다";
                    ph_Regist_ID_Text.color = Color.red;
                    return;

                }
                
                print("SaveComplite");
                existingContent = existingContent + content;

                
            }

            //using System.IO; 인클루드 해줘야함. //파일을 텍스트에 저장해주자.
            File.WriteAllText(path, existingContent);

        }







    }
    public void SaveReistInfo()
    {

    }




}//클래스끝






