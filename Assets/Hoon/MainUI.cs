using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    GameObject bg;
    GameObject mainRoom;
    GameObject myInfo;
    GameObject playerImg;

    bool isRoomActive = false;

    //�κ� �����ϰ� ��ư�� ��� �����մϴ�.
    // Start is called before the first frame update
    void Start()
    {
        //bg
        bg = GameObject.Find("BG");
        //mainRoom
        mainRoom = GameObject.Find("MainRoom");
        //myInfo
        myInfo = GameObject.Find("Img_MyInfo");
        //playerImg
        playerImg = GameObject.Find("PlayerImage");
        //
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //����Ű�� ������ ��� UI�� �����ְ�����.
    public void ViewMain()
    {
        //BG�� ����.
        bg.SetActive(true);
        //mainRoom ����
        mainRoom.SetActive(true);
        //myInfo ����
        myInfo.SetActive(true);
        //PlayerImg ����
        playerImg.SetActive(true);


    }

    //Room ��ư�� ������ ��� UI�� ���� Room���� �̵�
    public void ViewRoom()
    {
        
        //BG�� ����.
        bg.SetActive(false);
        //mainRoom ����
        mainRoom.SetActive(false);
        //myInfo ����
        myInfo.SetActive(false);
        //PlayerImg ����
        playerImg.SetActive(false);
        //room�� ������.
        isRoomActive = true;




    }

    public void MoveLobby()
    {

    }

    public void MoveChar()
    {

    }



}
