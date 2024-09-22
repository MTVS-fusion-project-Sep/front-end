using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerUserInfoTest : MonoBehaviourPun
{
    LobbyUI lobbyUI;
    PhotonView photonView_Comp;
    void Start()
    {
        lobbyUI = GameObject.Find("LobbyUI").GetComponent<LobbyUI>();
        print("로비UI");
        photonView_Comp = GetComponent<PhotonView>();
        print("포톤뷰");
        string userList = lobbyUI.userName.text;
        //유저수가 1이면
        if(PhotonNetwork.PlayerList.Length == 1)
        {
            //오너의 닉네임을 붙여넣기
            userList += photonView_Comp.Owner.NickName;
            lobbyUI.userName.text = userList;
        }
        else
        {
            userList += "\n" + photonView_Comp.Owner.NickName;
            lobbyUI.userName.text = userList;
        }
                
        print("유저리스트" + lobbyUI.userName.text);
        print("현재유저수" + PhotonNetwork.PlayerList.Length);

    }


    void Update()
    {
        //마우스가 캐릭터 위에 있고 UI가 보이게 하자.
      /*  if(Input.GetMouseButtonDown(1))
        {

        }*/
    
    }


}
