using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMove : MonoBehaviour
{
   CharacterController playerCharacterController;
    
    public float playerMoveSpeed = 3;
    public float playerRotSpeed = 500;
    Vector3 dir;
    GameObject playerModel;
    GameObject plyerPoint;
    void Start()
    {
        playerCharacterController = GetComponent<CharacterController>();
        playerModel = GameObject.Find("Ch21");
        plyerPoint = GameObject.Find("PlayerPoint");
        transform.position = plyerPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Rotate();
        //RotateDir();

    }

    void Move()
    {

        float y = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");
        dir = new Vector3(-h, 0, -y);
        dir = transform.TransformDirection(dir);
        dir.Normalize();
        playerCharacterController.Move(dir * playerMoveSpeed * Time.deltaTime);
        //���ù������� ����
        //transform.Translate(dir * playerMoveSpeed * Time.deltaTime);
        //transform.position += dir * Time.deltaTime;

        //������
        if(h > 0)
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
        if(y > 0)
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
    
    void RotateDir()
    {
        if(dir.x < 0)
        {
            print(1111);

            Vector3 modelRot = playerModel.transform.localEulerAngles;
            modelRot.y = 90;
            playerModel.transform.localEulerAngles = modelRot;

            /*Vector3 rot = transform.eulerAngles;
            rot.y = -45;
            transform.eulerAngles = rot;*/

        }
        if(dir.x > 0)
        {
            print(2222);
            /*Vector3 rot = transform.eulerAngles;
            rot.y = 135;
            transform.eulerAngles = rot;*/
        }
        if(dir.z < 0)
        {
            /*Vector3 rot = transform.eulerAngles;
            rot.y = -90;
            transform.eulerAngles = rot;*/
        }
        if(dir.z > 0)
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


