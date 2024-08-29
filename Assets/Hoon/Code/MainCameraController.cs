using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    
    GameObject cameraPoint;

    // Start is called before the first frame update
    void Start()
    {
        cameraPoint = GameObject.Find("CameraPoint");


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = cameraPoint.transform.position - transform.position;

        //카메라의 앞방향을 항상 focuspoint 방향으로
        Camera.main.transform.forward = dir;

    }
}

