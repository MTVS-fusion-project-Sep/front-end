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

    //로비를 제외하고 버튼은 계속 유지합니다.
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

    //메인키를 누르면 모든 UI를 보여주게하자.
    public void ViewMain()
    {
        //BG를 끄자.
        bg.SetActive(true);
        //mainRoom 끄자
        mainRoom.SetActive(true);
        //myInfo 끄자
        myInfo.SetActive(true);
        //PlayerImg 끄자
        playerImg.SetActive(true);


    }

    //Room 버튼을 누르면 모든 UI를 끄고 Room으로 이동
    public void ViewRoom()
    {
        
        //BG를 끄자.
        bg.SetActive(false);
        //mainRoom 끄자
        mainRoom.SetActive(false);
        //myInfo 끄자
        myInfo.SetActive(false);
        //PlayerImg 끄자
        playerImg.SetActive(false);
        //room을 켜주자.
        isRoomActive = true;




    }

    public void MoveLobby()
    {

    }

    public void MoveChar()
    {

    }



}
