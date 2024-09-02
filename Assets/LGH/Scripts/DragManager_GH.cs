using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragManager_GH : MonoBehaviour
{
    public GameObject noticeBoard;

    bool onNotice = false;


    // 바닥 X, Y 축의 기준점들
    public List<Transform> groundPoint = new List<Transform>();

    // 바닥 격자 갯수
    public int groundTileNum = 8;

    GameObject obj_Point;

    [SerializeField]
    GameObject[] ground_Ys;
    [SerializeField]
    GameObject[] ground_Xs;

    //기본 가구의 y값
    float defaultY = 0.25f;

    //가구 움직임 변수들
    GameObject moveFurniture;
    bool onMoveFurniture;

    FurnitureData_GH moveFurniData;

    void Start()
    {
        noticeBoard.SetActive(false);


        obj_Point = new GameObject("Point");
        obj_Point.transform.position = groundPoint[0].position;

        ground_Xs = new GameObject[groundTileNum];
        ground_Ys = new GameObject[groundTileNum];


        for (int i = 0; i < groundTileNum; i++)
        {
            ground_Xs[i] = Instantiate(obj_Point);
        }

        for (int i = 0; i < groundTileNum; i++)
        {
            ground_Ys[i] = Instantiate(obj_Point);
        }
    }
    void Update()
    {
        //타일을 생성합니다
        GroundTile();

        if (Input.GetMouseButtonDown(0) && !onNotice)
        {
            MouseDown();
        }
        if (Input.GetMouseButton(0) && !onNotice)
        {
            OnMouse();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MouseUp();
        }
    }

    private void MouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Furniture") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("NoticeBoard")))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                if (!onMoveFurniture)
                {
                    moveFurniture = hit.transform.gameObject;
                    moveFurniData = moveFurniture.GetComponent<FurnitureData_GH>();
                    onMoveFurniture = true;
                }
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("NoticeBoard"))
            {
                noticeBoard.SetActive(true);
                onNotice = true;
            }
        }
    }


    private void OnMouse()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Wall")) && moveFurniData != null)
        {
            int myX = 99;
            int myZ = 99;

            float minX = 9999;
            float minZ = 9999;

            float xDistance = 99f;
            float zDistance = 99f;

            // x값의 최댓 값 구하기
            for (int i = 0; i <= groundTileNum - moveFurniData.furnitureInfo.furni_Size_X; i++)
            {
                xDistance = Vector3.Distance(hit.point, ground_Xs[i].transform.position);
                //x값을 가지고 ground_Xs[i] 이랑 비교해서 가장 가까운 ground_Xs를 구하고
                if(minX > xDistance)
                {
                    myX = i;
                    minX = xDistance;
                }
            }

            // z값의 최댓 값 구하기
            for (int i = 0; i <= groundTileNum - moveFurniData.furnitureInfo.furni_Size_Y; i++)
            {
                zDistance = Vector3.Distance(hit.point, ground_Ys[i].transform.position);
                //z값을 가지고 ground_Ys[i] 이랑 비교해서 가장 가까운 ground_Ys 구해서
                if (minZ > zDistance)
                {
                    myZ = i;
                    minZ = zDistance;
                }
            }

            print(moveFurniture.name);
            print(myX);
            print(myZ);

            //moveFurniture.transform.position = new Vector3(ground_Xs[myX].transform.position.x, defaultY, ground_Ys[myZ].transform.position.z);
            moveFurniture.transform.position = new Vector3(ground_Xs[myX].transform.position.x, defaultY, ground_Ys[myZ].transform.position.z);
            moveFurniData.furnitureInfo.furni_Current_X = myX;
            moveFurniData.furnitureInfo.furni_Current_Y = myZ;



        }
    }

    private void MouseUp()
    {
        onMoveFurniture = false;
        moveFurniture = new GameObject();
    }

    public void GroundTile()
    {
        for (int i = 0; i < groundTileNum; i++)
        {
            ground_Ys[i].transform.position = Vector3.Lerp(groundPoint[0].position, groundPoint[1].position, ((float)(i) / (float)groundTileNum));
            ground_Xs[i].transform.position = Vector3.Lerp(groundPoint[0].position, groundPoint[2].position, ((float)(i) / (float)groundTileNum));
        }
    }

    public void ButtonExit()
    {
        noticeBoard.SetActive(false);
        onNotice = false;
    }
}
