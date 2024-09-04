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
        if (photonView_Comp != null) print("����� �־��");


        //����䰡 �������
        if (photonView_Comp.IsMine)
        {
            playerModel = transform.Find("Ch21");
            if (playerModel != null) print("��ã�Ҵ�");
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
        // ����, �����͸� ������ ����(PhotonView.IsMine == true)�ϴ� ���¶��...
        if (stream.IsWriting)
        {
            // iterable �����͸� ������.
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        // �׷��� �ʰ�, ���� �����͸� �����κ��� �о��� ���¶��...
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

        //���͸� �ǰ� ����.
        if (photonView_Comp.IsMine)
        {

            print("������������");
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



            //���ù������� ����
            //transform.Translate(dir * playerMoveSpeed * Time.deltaTime);
            //transform.position += dir * Time.deltaTime;

            //������
            if (h > 0)
            {
                //print("h");
                Vector3 modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = -90;
                playerModel.transform.localEulerAngles = modelRot;
            }
            //����
            if (h < 0)
            {
                //print("-h");
                Vector3 modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 90;
                playerModel.transform.localEulerAngles = modelRot;
            }
            //��
            if (y > 0)
            {
                //print("y");
                Vector3 modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 180;
                playerModel.transform.localEulerAngles = modelRot;
            }
            //�Ʒ�
            if (y < 0)
            {
                //print("-y");
                Vector3 modelRot = playerModel.transform.localEulerAngles;
                modelRot.y = 0;
                playerModel.transform.localEulerAngles = modelRot;
            }


        }


    }//���곡

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