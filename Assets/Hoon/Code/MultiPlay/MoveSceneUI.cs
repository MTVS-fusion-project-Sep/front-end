using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class MoveSceneUI : MonoBehaviour
{
    public LobbyUI lobbyUI;
    public MoveSceneManager moveSceneanager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        print("충돌됬다");
        if (other.transform.name.Contains("Player"))
        {
            //print("플레이어 충돌됬다");
            //print("오브젝트 이름" + other.transform.name);

            //자신이름
            string objectName = transform.name;
            //어더의 이름
            GameObject player = other.gameObject;
            lobbyUI.SaveOntriggerPlayer(player);

            //액티브 오브젝트
            lobbyUI.MoveSceneController(objectName);
            print("otherObject" + other.gameObject + "userId" + MainUI.Instance.idText);


            /*if (lobbyUI.img_MoveScene_Object)
            {
                lobbyUI.img_MoveScene_Object.SetActive(true);
            }
            else
            {
                lobbyUI.img_MoveScene_Object = GameObject.Find("Img_MoveScene");
                lobbyUI.img_MoveScene_Object.SetActive(true);
            }*/

        }
    }

}//클래스끝
