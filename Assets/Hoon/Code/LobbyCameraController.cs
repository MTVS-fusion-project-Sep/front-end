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
        //if(mainCamera_Object != null ) print("카메라 있음");
        //cam = GetComponentInChildren<Camera>();
        //if (Camera.main.transform != null) print("내캠 있다");
        
       
    }

    // Update is called once per frame
    void Update()
    {
        //포톤뷰 컴포넌트의 IsMine true라면
        if (playerPhotonViewComp.IsMine)
        {
            if (Camera.main.transform != null)
            {
                //print("카메라를 위치를 설정하자");
                Camera.main.transform.transform.localPosition = transform.position + new Vector3(0, 2, 5);
                Camera.main.transform.transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, 180, 0);
              

            }
        }
    }
}
