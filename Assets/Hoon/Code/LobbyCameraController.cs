using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class LobbyCameraController : MonoBehaviour
{
    //GameObject player;
    PhotonView playerPhotonViewComp;
    GameObject mainCamera_Object;
    //Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("Player");
        playerPhotonViewComp = GetComponent<PhotonView>();
        //mainCamera_Object = GameObject.Fin("MainCamera");
        //if(mainCamera_Object != null ) print("ī�޶� ����");
        //cam = GetComponentInChildren<Camera>();
        //if (Camera.main.transform != null) print("��ķ �ִ�");
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //����� ������Ʈ�� IsMine true���
        if (playerPhotonViewComp.IsMine)
        {
            if (Camera.main.transform != null)
            {
                //print("ī�޶� ��ġ�� ��������");
                Camera.main.transform.transform.localPosition = transform.position + new Vector3(0, 2, 5);
                Camera.main.transform.transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 180, 0);
              

            }
        }
    }
}
