using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Reflection;

public class ConnectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLobby()
    {
        //
        PhotonNetwork.GameVersion = "1.0.0";
        //
        PhotonNetwork.NickName = "oneMan";
        //화면동기화
        PhotonNetwork.AutomaticallySyncScene = true;

    }


}//클래스끝
