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
    Vector3 dir;
   
    GameObject plyerPoint;
    Animator animator;
    float posY;

    float y;
    float h;

    PhotonView photonView_Comp;
    Vector3 myPos;
    Quaternion myRot;

    Transform playerModel;

    void Start()
    {
        playerCharacterController = GetComponent<CharacterController>();
        photonView_Comp = GetComponent<PhotonView>();
        if (photonView_Comp != null) print("포톤뷰 있어요");


        //포톤뷰가 내꺼라면
        if (photonView_Comp.IsMine)
        {
            playerModel = transform.Find("Ch21");
            if (playerModel != null) print("모델찾았다");
        }
       
        plyerPoint = GameObject.Find("PlayerPoint");
        if (plyerPoint != null) transform.position = plyerPoint.transform.position;
        animator = GetComponentInChildren<Animator>();

        
       

    }

    // Update is called once per frame
    void Update()
    {

        Move();
        ExpressionFeelingHi();


        //Rotate();
        //RotateDir();


    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 만일, 데이터를 서버에 전송(PhotonView.IsMine == true)하는 상태라면...
        if (stream.IsWriting)
        {
            // iterable 데이터를 보낸다.
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        // 그렇지 않고, 만일 데이터를 서버로부터 읽어어는 상태라면...
        else if (stream.IsReading)
        {
            myPos = (Vector3)stream.ReceiveNext();
            myRot = (Quaternion)stream.ReceiveNext();
        }


    }



    void ExpressionFeelingHi()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //print(1111);
            animator.CrossFade("Hi", 0);
        }
    }



    void Move()
    {

        //내것만 되게 하자.
        if (photonView_Comp.IsMine)
        {

            print("나만움직이자");
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



            //로컬방향으로 변경
            //transform.Translate(dir * playerMoveSpeed * Time.deltaTime);
            //transform.position += dir * Time.deltaTime;

            //오른쪽
            if (h > 0)
            {
                //print("h");
                Vector3 modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = -90;
                playerModel.transform.localEulerAngles = modelRot;
            }
            //왼쪽
            if (h < 0)
            {
                //print("-h");
                Vector3 modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 90;
                playerModel.transform.localEulerAngles = modelRot;
            }
            //위
            if (y > 0)
            {
                //print("y");
                Vector3 modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 180;
                playerModel.transform.localEulerAngles = modelRot;
            }
            //아래
            if (y < 0)
            {
                //print("-y");
                Vector3 modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 0;
                playerModel.transform.localEulerAngles = modelRot;
            }


        }


    }//무브끝

    void RotateDir()
    {
        if (dir.x < 0)
        {
            print(1111);

            Vector3 modelRot = playerModel.transform.localEulerAngles;
            modelRot.y = 90;
            playerModel.transform.localEulerAngles = modelRot;

            /*Vector3 rot = transform.eulerAngles;
            rot.y = -45;
            transform.eulerAngles = rot;*/

        }
        if (dir.x > 0)
        {
            print(2222);
            /*Vector3 rot = transform.eulerAngles;
            rot.y = 135;
            transform.eulerAngles = rot;*/
        }
        if (dir.z < 0)
        {
            /*Vector3 rot = transform.eulerAngles;
            rot.y = -90;
            transform.eulerAngles = rot;*/
        }
        if (dir.z > 0)
        {
            /*Vector3 rot = transform.eulerAngles;
            rot.y = 90;
            transform.eulerAngles = rot;*/
        }

    }


    void Rotate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        Vector3 rot = new Vector3(0, h, 0);
        transform.localEulerAngles += rot * playerRotSpeed * Time.deltaTime;
    }





}