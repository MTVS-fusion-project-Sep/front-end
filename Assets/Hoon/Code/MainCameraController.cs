using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MainCameraController : MonoBehaviour
{
    
    GameObject cameraPoint;
    GameObject player;
    PhotonView playerPhotonVeiw;

    // Start is called before the first frame update
    void Start()
    {
        cameraPoint = GameObject.Find("CameraPoint");
        player = GameObject.Find("Player");
        playerPhotonVeiw  = player.GetComponent<PhotonView>();

    }

    // Update is called once per frame
    void Update()
    {
        if(playerPhotonVeiw.IsMine)
        {
            Vector3 dir = cameraPoint.transform.position - transform.position;

            //ī�޶��� �չ����� �׻� focuspoint ��������
            Camera.main.transform.forward = dir;
        }
       

    }
}

