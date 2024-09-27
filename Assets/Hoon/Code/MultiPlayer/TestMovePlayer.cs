using UnityEngine;
//포톤서버
using Photon.Pun;
using UnityEngine.UI;
using System;
using System.Xml.Linq;
using TMPro;

public class TestMovePlayer : MonoBehaviourPun, IPunObservable
{
    CharacterController playerCharacterController;

    public float playerMoveSpeed = 3;
    public float playerRotSpeed = 500;
    public float trackingSpeed = 3;
    Vector3 dir;

    GameObject plyerPoint;
    Animator animator;
    float posY;

    float v;
    float h;
    float syncV;
    float syncH;
    Vector3 modelRot;

    PhotonView photonView_Comp;

    Vector3 myPos;
    Quaternion myRot;
    Vector3 modelLocalRot;
    Quaternion myModelRot;

    Transform playerModel;

    bool isHelloKey = false;

    Transform myName_Object;
    Text myName;
    public string myId;
    GameObject chatManager_Object;
    ChatManager chatManagerComp;

    public GameObject panel_ChatPack;
    public GameObject textMesh_ChatPack;
    string playerText;


    void Start()
    {
        chatManager_Object = GameObject.Find("ChatManager");
        chatManagerComp = chatManager_Object.GetComponent<ChatManager>();

        playerCharacterController = GetComponent<CharacterController>();
        photonView_Comp = GetComponent<PhotonView>();
        //if (photonView_Comp != null) print("포톤뷰 있어요");

        //내 위치의 모델 찾기
        playerModel = transform.Find("Ch21");
        //애니메이션 찾기
        animator = GetComponentInChildren<Animator>();

        plyerPoint = GameObject.Find("PlayerPoint");
        if (plyerPoint != null) transform.position = plyerPoint.transform.position;

        //내 자식에서 이름 찾기
        myName = GetComponentInChildren<Text>();
        print("내이름" + myName.text);
        myName.text = photonView.Owner.NickName;
        print("오너의이름" + myName.text);
        myId = MainUI.Instance.idText;
        print("내아이디" + myId);

        /*myName_Object = transform.Find("Text_MyName");
        print("이름오브젝트" + myName_Object.name);
        if (myName == null) print("myName이 없음");
        */

        if (textMesh_ChatPack != null)
        {
            //내가 한말 저장

            TextMeshProUGUI tmpComponent = textMesh_ChatPack.GetComponent<TextMeshProUGUI>();
            if (tmpComponent == null)
            {
                print("TextMeshPro 없음");

            }
            else
            {
                playerText = tmpComponent.text;
            }


        }
        else
        {
            textMesh_ChatPack.transform.Find("Text_ChatPack");
            // TextMeshPro 컴포넌트가 존재하는지 확인
            TextMeshProUGUI tmpComponent = textMesh_ChatPack.GetComponent<TextMeshProUGUI>();
            //내가 한말 저장
            playerText = tmpComponent.text;

        }

        //패널 감추자
        panel_ChatPack.SetActive(false);
        //챗 말풍선 감추기
        //textMesh_ChatPack.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        //Move();
        MoveSync();
        ExpressionFeelingHi();
        // ExpressionFeelingHiSync();

        //Rotate();
        //RotateDir();


    }


    void ExpressionFeelingHi()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //버튼눌렀을때 내건지 확인
            if (photonView_Comp.IsMine)
            {
                //내것만 되게 하자
                animator.CrossFade("Hi", 0);

                //다른 애들 애니메이션 실행
                photonView.RPC("ExpressionFeelingHiSync", RpcTarget.Others);

            }
            //내거 아닌것


        }

    }

    // RPC로 애니메이션 실행 명령을 전달받은 다른 클라이언트에서 실행되는 함수
    [PunRPC]
    void ExpressionFeelingHiSync()
    {
        // 이 함수는 로컬 클라이언트에서만 실행
        animator.CrossFade("Hi", 0);

    }




    void MoveSync()
    {
        if (photonView_Comp.IsMine)
        {
            v = Input.GetAxisRaw("Vertical");
            h = Input.GetAxisRaw("Horizontal");
            dir = new Vector3(-h, 0, -v);
            dir = transform.TransformDirection(dir);

            if (v == 0 && h == 0)
            {
                animator.SetBool("Walk", false);
            }
            else
            {
                animator.SetBool("Walk", true);
            }

            Vector3 playerPos = transform.position;

            if (playerPos.y > -0.1f)
            {
                float posY = transform.position.y;
                posY -= 1;
                dir.y = posY;
                dir.Normalize();
                playerCharacterController.Move(dir * playerMoveSpeed * Time.deltaTime);
            }
            else
            {
                dir.Normalize();
                playerCharacterController.Move(dir * playerMoveSpeed * Time.deltaTime);
                animator.SetBool("Walk", true);
            }

            // 로컬 회전 설정
            if (h > 0)
            {
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = -90;
                playerModel.transform.localEulerAngles = modelRot;
            }
            else if (h < 0)
            {
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 90;
                playerModel.transform.localEulerAngles = modelRot;
            }
            else if (v > 0)
            {
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 180;
                playerModel.transform.localEulerAngles = modelRot;
            }
            else if (v < 0)
            {
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 0;
                playerModel.transform.localEulerAngles = modelRot;
            }
        }
        else
        {
            // 서버에서 받은 위치 및 회전을 부드럽게 동기화
            transform.position = myPos;
            //transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime * trackingSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, myRot, Time.deltaTime * trackingSpeed);
            playerModel.transform.localEulerAngles = modelLocalRot;
            // 서버에서 받은 로컬 회전을 부드럽게 동기화
            //playerModel.transform.localEulerAngles = Vector3.Lerp(playerModel.transform.localEulerAngles, modelLocalRot, Time.deltaTime * trackingSpeed);

            //animator.SetBool("Walk", true);

            if (syncV == 0 && syncH == 0)
            {
                animator.SetBool("Walk", false);
            }
            else
            {
                animator.SetBool("Walk", true);
            }


        }


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 만일, 데이터를 서버에 전송(PhotonView.IsMine)하는 상태라면...
        if (stream.IsWriting)
        {
            // iterable 데이터를 보낸다.
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(playerModel.transform.localEulerAngles);
            // playerModel이 null인지 확인
            if (playerModel != null)
            {
                // print("playerModel=true");
                stream.SendNext(playerModel.transform.localEulerAngles);
            }
            else
            {
                //print("playerModel=false");
                // null일 경우 Vector3.zero 또는 기본 값을 전송
                stream.SendNext(Vector3.zero);
            }
            stream.SendNext(Input.GetAxisRaw("Vertical"));
            stream.SendNext(Input.GetAxisRaw("Horizontal"));


        }
        // 그렇지 않고, 만일 데이터를 서버로부터 읽어어는 상태라면...
        else if (stream.IsReading)
        {
            myPos = (Vector3)stream.ReceiveNext();
            myRot = (Quaternion)stream.ReceiveNext();
            modelLocalRot = (Vector3)stream.ReceiveNext();
            //myModelRot = (Quaternion)stream.ReceiveNext();
            syncV = (float)stream.ReceiveNext();
            syncH = (float)stream.ReceiveNext();



        }


    }




}//클래스끝
