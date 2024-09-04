using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
//���漭��
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
        //if (photonView_Comp != null) print("����� �־��");

        //�� ��ġ�� �� ã��
        playerModel = transform.Find("Ch21");
        
        
        //����䰡 ������� �ִϸ��̼� ����ȭ.
        if (photonView_Comp.IsMine)
        {
            //if (playerModel != null) print("��ã�Ҵ�");
            //�ִϸ��̼� ã��
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

        //���͸� �ǰ� ����.
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

            // ���� ȸ�� ����
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
            // �������� ���� ��ġ �� ȸ���� �ε巴�� ����ȭ
            transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime * trackingSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, myRot, Time.deltaTime * trackingSpeed);

            playerModel.transform.localEulerAngles = modelLocalRot;
            // �������� ���� ���� ȸ���� �ε巴�� ����ȭ
            //playerModel.transform.localEulerAngles = Vector3.Lerp(playerModel.transform.localEulerAngles, modelLocalRot, Time.deltaTime * trackingSpeed);
        }
    }

    void Move()
    {


        //���͸� �ǰ� ����.
        if (photonView_Comp.IsMine)
        {
            //print("������������");
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

            //0���� ũ��
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
            //������ ���� ��ġ
            //transform.position = myPos;
            //�������� ������ �������� �������� ����ϱ� Lerp�� �ε巴�� ó���غ���.
            //transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime);
            //�ε巯� �����ϱ� �������� ����.
            transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime * trackingSpeed);
        }

        //���ù������� ����
        //transform.Translate(dir * playerMoveSpeed * Time.deltaTime);
        //transform.position += dir * Time.deltaTime;


        //������
        if (h > 0)
        {
            //�������
            if (photonView_Comp.IsMine)
            {
                //print("h");
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = -90;
                playerModel.transform.localEulerAngles = modelRot;
            }
            //�����ƴϸ�
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
                    print("modelSyncRot ����");
                }
            }




        }
       




        if (photonView_Comp.IsMine)
        {
            //����
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
            //��
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
            //�Ʒ�
            if (y < 0)
            {
                //print("-y");
                modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 0;
                playerModel.transform.localEulerAngles = modelRot;
            }

        }

        //������ �ƴҶ� ���������� �ε巴�� ����ȭ
        else
        {

            transform.rotation = Quaternion.Lerp(transform.rotation, myRot, Time.deltaTime * trackingSpeed);
            

        }




    }//���곡


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ����, �����͸� ������ ����(PhotonView.IsMine == true)�ϴ� ���¶��...
        if (stream.IsWriting)
        {
            // iterable �����͸� ������.
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(playerModel.transform.localEulerAngles);
            // playerModel�� null���� Ȯ��
            if (playerModel != null)
            {
               // print("playerModel=true");
                stream.SendNext(playerModel.transform.localEulerAngles);
            }
            else
            {
                //print("playerModel=false");
                // null�� ��� Vector3.zero �Ǵ� �⺻ ���� ����
                stream.SendNext(Vector3.zero);
            }

        }
        // �׷��� �ʰ�, ���� �����͸� �����κ��� �о��� ���¶��...
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





}//Ŭ������