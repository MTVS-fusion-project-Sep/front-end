using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomData_GH : MonoBehaviour
{
    public RoomInfo_GH roomInfo;
    Button enterRoom_but;

    public TMP_Text roomName;

    //룸 아이디와 룸이름을 정보에 있는거로 바꾼다.
    
    void Start()
    {
        enterRoom_but = GetComponentInChildren<Button>();
        //람다식으로 썼는데 되는지 확인하기
        enterRoom_but.onClick.AddListener(() => ChatManager.instance.EnterRoom(roomInfo.roomId));

        roomName.text = roomInfo.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
