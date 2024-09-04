using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//포톤서버
using Photon.Pun;


public class TestPlayerMove : MonoBehaviour, IPunObservable
{
    CharacterController playerCharacterController;


    public float playerMoveSpeed = 3;
    public float playerRotSpeed = 500;
    public float trackingSpeed = 3;
    Vector3 dir;

    GameObject plyerPoint;
    Animator animator;
    float posY;

    float y;
    float h;
    Vector3 modelRot;


    PhotonView photonView_Comp;
    
    Vector3 myPos;
    Quaternion myRot;
    Vector3 modelLocalRot;
    Quaternion myModelRot;

    Transform playerModel;

    void Start()
    {
        playerCharacterController = GetComponent<CharacterController>();
        photonView_Comp = GetComponent<PhotonView>();
        //if (photonView_Comp != null) print("포톤뷰 있어요");

        //내 위치의 모델 찾기
        playerModel = transform.Find("Ch21");
        
        
        //포톤뷰가 내꺼라면 애니메이션 동기화.
        if (photonView_Comp.IsMine)
        {
            //if (playerModel != null) print("모델찾았다");
            //애니메이션 찾기
            animator = GetComponentInChildren<Animator>();

        }

        plyerPoint = GameObject.Find("PlayerPoint");
        if (plyerPoint != null) transform.position = plyerPoint.transform.position;






    }

    // Update is called once per frame
    void Update()
    {

        //Move();
        MoveSync();
        ExpressionFeelingHi();


        //Rotate();
        //RotateDir();


    }
   


    void ExpressionFeelingHi()
    {

        //내것만 되게 하자.
        if (photonView_Comp.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //print(1111);
                animator.CrossFade("Hi", 0);
            }
        }
    }

    void MoveSync()
    {
        if (photonView_Comp.IsMine)
        {
            y = Input.GetAxisRaw("Vertical");
            h = Input.GetAxisRaw("Horizontal");
            dir = new Vector3(-h, 0, -y);
            dir = transform.TransformDirection(dir);

            if (y == 0 && h == 0)
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
            else if (y > 0)
            {
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 180;
                playerModel.transform.localEulerAngles = modelRot;
            }
            else if (y < 0)
            {
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 0;
                playerModel.transform.localEulerAngles = modelRot;
            }
        }
        else
        {
            // 서버에서 받은 위치 및 회전을 부드럽게 동기화
            transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime * trackingSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, myRot, Time.deltaTime * trackingSpeed);

            playerModel.transform.localEulerAngles = modelLocalRot;
            // 서버에서 받은 로컬 회전을 부드럽게 동기화
            //playerModel.transform.localEulerAngles = Vector3.Lerp(playerModel.transform.localEulerAngles, modelLocalRot, Time.deltaTime * trackingSpeed);
        }
    }

    void Move()
    {


        //내것만 되게 하자.
        if (photonView_Comp.IsMine)
        {
            //print("나만움직이자");
            y = Input.GetAxisRaw("Vertical");
            h = Input.GetAxisRaw("Horizontal");
            dir = new Vector3(-h, 0, -y);
            dir = transform.TransformDirection(dir);

            //print("vertical" + y);

            if (y == 0 && h == 0)
            {
                animator.SetBool("Walk", false);
            }
            else
            {
                animator.SetBool("Walk", true);
            }

            Vector3 playerPos = transform.position;

            //0보다 크면
            if (playerPos.y > -0.1f)
            {
                //print(playerPos.y);
                float posY = transform.position.y;
                posY -= 1;

                dir.y = posY;
                dir.Normalize();
                playerCharacterController.Move(dir * playerMoveSpeed * Time.deltaTime);
                //animator.SetBool("Walk", true);
            }
            else
            {
                dir.Normalize();
                playerCharacterController.Move(dir * playerMoveSpeed * Time.deltaTime);
                animator.SetBool("Walk", true);

            }


        }
        else
        {
            //서버의 나의 위치
            //transform.position = myPos;
            //서버에서 받을때 지연으로 움직임이 끊기니까 Lerp로 부드럽게 처리해보자.
            //transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime);
            //부드러운데 느리니까 보정값을 주자.
            transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime * trackingSpeed);
        }

        //로컬방향으로 변경
        //transform.Translate(dir * playerMoveSpeed * Time.deltaTime);
        //transform.position += dir * Time.deltaTime;


        //오른쪽
        if (h > 0)
        {
            //내꺼라면
            if (photonView_Comp.IsMine)
            {
                //print("h");
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = -90;
                playerModel.transform.localEulerAngles = modelRot;
            }
            //내꺼아니면
            else
            {
                /*playerModel.transform.localEulerAngles = modelLocalRot;
                modelLocalRot.y = -90;
                playerModel.transform.localEulerAngles = modelLocalRot;*/


                if (modelLocalRot != null)
                {
                    print(1111);
                    playerModel.transform.localEulerAngles = modelLocalRot;
                }
                else
                {
                    print("modelSyncRot 없음");
                }
            }




        }
       




        if (photonView_Comp.IsMine)
        {
            //왼쪽
            if (h < 0)
            {
                //print("-h");
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 90;
                playerModel.transform.localEulerAngles = modelRot;
            }
        }
        else
        {



        }

        if (photonView_Comp.IsMine)
        {
            //위
            if (y > 0)
            {
                //print("y");
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 180;
                playerModel.transform.localEulerAngles = modelRot;
            }

        }
        else
        {

        }
        if (photonView_Comp.IsMine)
        {
            //아래
            if (y < 0)
            {
                //print("-y");
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 0;
                playerModel.transform.localEulerAngles = modelRot;
            }

        }

        //내것이 아닐때 움직임으로 부드럽게 동기화
        else
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, myRot, Time.deltaTime * trackingSpeed);
            

        }




    }//무브끝


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 만일, 데이터를 서버에 전송(PhotonView.IsMine == true)하는 상태라면...
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

        }
        // 그렇지 않고, 만일 데이터를 서버로부터 읽어어는 상태라면...
        else if (stream.IsReading)
        {
            myPos = (Vector3)stream.ReceiveNext();
            myRot = (Quaternion)stream.ReceiveNext();
            modelLocalRot = (Vector3)stream.ReceiveNext();
            //myModelRot = (Quaternion)stream.ReceiveNext();
        }


    }


    /* void RotateDir()
     {
         if (dir.x < 0)
         {
             print(1111);

             Vector3 modelRot = playerModel.transform.localEulerAngles;
             modelRot.y = 90;
             playerModel.transform.localEulerAngles = modelRot;

             *//*Vector3 rot = transform.eulerAngles;
             rot.y = -45;
             transform.eulerAngles = rot;*//*

         }
         if (dir.x > 0)
         {
             print(2222);
             *//*Vector3 rot = transform.eulerAngles;
             rot.y = 135;
             transform.eulerAngles = rot;*//*
         }
         if (dir.z < 0)
         {
             *//*Vector3 rot = transform.eulerAngles;
             rot.y = -90;
             transform.eulerAngles = rot;*//*
         }
         if (dir.z > 0)
         {
             *//*Vector3 rot = transform.eulerAngles;
             rot.y = 90;
             transform.eulerAngles = rot;*//*
         }

     }*/


    /* void Rotate()
     {
         float h = Input.GetAxisRaw("Horizontal");

         Vector3 rot = new Vector3(0, h, 0);
         transform.localEulerAngles += rot * playerRotSpeed * Time.deltaTime;
     }*/





}//클래스끝