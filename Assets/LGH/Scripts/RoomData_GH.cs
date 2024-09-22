using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    //채팅룸===========================================================================
public class RoomData_GH : MonoBehaviour
{
    public RoomInfo_GH roomInfo;
    Button enterRoom_but;

    public TMP_Text roomName;
    public TMP_Text roomCate;
    public TMP_Text roomCount;
    public TMP_Text roomState;

    //룸 아이디와 룸이름을 정보에 있는거로 바꾼다.
    
    void Start()
    {
        enterRoom_but = GetComponentInChildren<Button>();
        //람다식으로 썼는데 되는지 확인하기
        enterRoom_but.onClick.AddListener(() => ChatManager.instance.EnterRoom(roomInfo.roomId));

        roomName.text = roomInfo.name;
        roomCate.text = roomInfo.category;
    }

    void Update()
    {
        roomCount.text = roomInfo.headCnt + "/" + roomInfo.maxCnt;

        if (roomInfo.headCnt >= roomInfo.maxCnt)
        {
            roomState.text = "입장불가능";
            enterRoom_but.interactable = false;
        }
        else
        {
            roomState.text = "입장가능";
            enterRoom_but.interactable = true;
        }
    }
}
