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
        // ���� �ε��ϰ� �� �ε� �Ϸ� �� ȣ��� �޼��带 �̺�Ʈ�� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("HoonMainScene");

    }
    // ���� �ε�� �� ȣ��Ǵ� �ݹ� �޼���
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (MainUI.Instance != null)
        {
            print("��������");

            // ���� �ε�� ��, ������Ʈ�� ã�� ������ ����
            MainUI.Instance.img_Regist_Object = GameObject.Find("Img_Regist");
            MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");

            // ������Ʈ ��Ȱ��ȭ
            MainUI.Instance.img_Regist_Object.SetActive(false);
            MainUI.Instance.imgLogin_Object.SetActive(false);


            /*if (MainUI.Instance.imgLogin_Object)
            {
                print("�α����̹��� ����");
                MainUI.Instance.imgLogin_Object.SetActive(false);
            }    
           else
            {
                print("�α����̹��� ����" + "ã��");
                MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");
                MainUI.Instance.imgLogin_Object.SetActive(false);

            }
            if(MainUI.Instance.img_Regist_Object)
            {
                print("����̹��� ����");
                MainUI.Instance.img_Regist_Object.SetActive(false);
            }
            else
            {
                print("��� �̹��� ����" + "ã��");
                MainUI.Instance.img_Regist_Object = GameObject.Find("Img_Regist");
                MainUI.Instance.img_Regist_Object.SetActive(false);

            }*/
        }
        else
        {
            print("���ξ���");
        }
        // �̺�Ʈ ����
        SceneManager.sceneLoaded -= OnSceneLoaded;



    }
}
