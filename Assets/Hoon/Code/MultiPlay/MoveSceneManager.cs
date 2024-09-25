using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MoveSceneManager : MonoBehaviour
{
    public LobbyUI lobbyUI;
    //GameObject img_MoveScene_Object;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
   /* void Update()
    {
        
    }*/
    private void OnTriggerEnter(Collider other)
    {
        print("충돌됬다");
        if (other.transform.name.Contains("Player"))
        {
            print("플레이어 충돌됬다");

            if(lobbyUI.img_MoveScene_Object)
            {
                lobbyUI.img_MoveScene_Object.SetActive(true);
            }
            else
            {
                lobbyUI.img_MoveScene_Object = GameObject.Find("Img_MoveScene");
                lobbyUI.img_MoveScene_Object.SetActive(true);
            }
            
        }
    }

    public void MoveScene()
    {
        print("이동버튼 눌렀다");
    }

    public void CloseUI()
    {
        print("나가기 눌렀다");
        lobbyUI.img_MoveScene_Object.SetActive(false);
    }
    
    

}
