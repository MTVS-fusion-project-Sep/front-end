using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveSceneManager : MonoBehaviour
{
    public LobbyUI lobbyUI;
    public GameObject img_MoveOtherRoom;
    //GameObject img_MoveScene_Object;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    /* void Update()
     {

     }*/

    public void MoveScene()
    {
        print("이동버튼 눌렀다");
        SceneManager.LoadScene(2);
    }

    public void CloseUI()
    {
        print("나가기 눌렀다");
        //
        lobbyUI.img_MoveScene_Object.SetActive(false);
    }
    
    

}
