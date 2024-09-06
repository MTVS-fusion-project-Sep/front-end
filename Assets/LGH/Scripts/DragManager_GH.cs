using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditorInternal.ReorderableList;

public class DragManager_GH : MonoBehaviour
{
    //초록색 큐브들 8X8
    GameObject[,] greenTiles;

    //초록색큐브
    public GameObject greenTilefPrefab;

    //땅 격자
    public GameObject groundTileLine;


    public GameObject noticeBoard;

    bool onNotice = false;


    // 바닥 X, Y 축의 기준점들
    public List<Transform> groundPoint = new List<Transform>();

    // 바닥 격자 갯수
    public int groundTileNum = 8;

    GameObject obj_Point;

    public GameObject[] ground_Ys;
    public GameObject[] ground_Xs;

    //기본 가구의 y값
    float defaultY = 0.25f;

    //가구 움직임 변수들
    GameObject moveFurniture;
    bool onMoveFurniture;

    FurnitureData_GH moveFurniData;


    //가구 검출하기
    public float radius = 7f;
    Collider[] colliders;
    List<int> dontX = new List<int>();
    List<int> dontY = new List<int>();



    private void Awake()
    {
        //포인트 찍기
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

        //타일을 생성합니다
        GroundTile();
    }

    void Start()
    {
        groundTileLine.SetActive(false);

        noticeBoard.SetActive(false);

        greenTiles = new GameObject[groundTileNum, groundTileNum];

        for (int i = 0; i < groundTileNum; i++)
        {
            for (int j = 0; j < groundTileNum; j++)
            {
                greenTiles[i, j] = Instantiate(greenTilefPrefab, GameObject.Find("GroundTiles").transform);
                greenTiles[i, j].transform.position = new Vector3(ground_Xs[i].transform.position.x, defaultY, ground_Ys[j].transform.position.z);
                greenTiles[i, j].gameObject.SetActive(false);
            }
        }

    }
    void Update()
    {
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
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Furniture") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("NoticeBoard")))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                if (!onMoveFurniture)
                {
                    groundTileLine.SetActive(true);
                    moveFurniture = hit.transform.gameObject;
                    moveFurniData = moveFurniture.GetComponent<FurnitureData_GH>();
                    onMoveFurniture = true;
                }

                //주변의 가구 중 furnitur의 위치 정보들을 가져온다.
                colliders = Physics.OverlapSphere(transform.position, radius, 1 << LayerMask.NameToLayer("Furniture"));



                //콜라이더들의 현재 x위치와 사이즈사이를 모두 담는다.
                for (int i = 0; i < colliders.Length; i++)
                {
                    FurnitureData_GH collFD = colliders[i].GetComponent<FurnitureData_GH>();
                    for (int j = collFD.furnitureInfo.furni_Current_X; j < collFD.furnitureInfo.furni_Current_X + collFD.furnitureInfo.furni_Size_X; j++)
                    {
                        dontX.Add(j);
                    }
                    for (int j = collFD.furnitureInfo.furni_Current_Y; j < collFD.furnitureInfo.furni_Current_Y + collFD.furnitureInfo.furni_Size_Y; j++)
                    {
                        dontY.Add(j);
                    }
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
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Ground")) && moveFurniData != null)
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
                for (int j = 0; j < dontX.Count; j++)
                {
                    //x값을 가지고 ground_Xs[i] 이랑 비교해서 가장 가까운 ground_Xs를 구하고
                    if (minX > xDistance)
                    {
                        for (int z = 0; z < dontX.Count; z++)
                        {
                            if (myX != dontX[z])
                            {
                                myX = i;
                                minX = xDistance;
                            }

                        }
                    }

                }
            }

            // z값의 최댓 값 구하기
            for (int i = 0; i <= groundTileNum - moveFurniData.furnitureInfo.furni_Size_Y; i++)
            {
                zDistance = Vector3.Distance(hit.point, ground_Ys[i].transform.position);
                for (int j = 0; j < dontY.Count; j++)
                {
                    //z값을 가지고 ground_Ys[i] 이랑 비교해서 가장 가까운 ground_Ys 구해서
                    if (minZ > zDistance)
                    {
                        for (int z = 0; z < dontY.Count; z++)
                        {
                            if (myZ != dontY[z])
                            {
                                myZ = i;
                                minZ = zDistance;
                            }

                        }
                    }

                }

            }

            moveFurniture.transform.position = new Vector3(ground_Xs[myX].transform.position.x, defaultY, ground_Ys[myZ].transform.position.z);
            moveFurniData.furnitureInfo.furni_Current_X = myX;
            moveFurniData.furnitureInfo.furni_Current_Y = myZ;

            defaultY = 1.5f;
            //타일 그라운드 키고 끄기
            FurnitureData_GH fd = moveFurniture.GetComponent<FurnitureData_GH>();
            if (fd != null)
            {
                for (int i = 0; i < groundTileNum; i++)
                {
                    for (int j = 0; j < groundTileNum; j++)
                    {
                        greenTiles[i, j].SetActive(false);
                    }
                }

                for (int i = myX; i < fd.furnitureInfo.furni_Size_X + myX; i++)
                {
                    for (int j = myZ; j < fd.furnitureInfo.furni_Size_Y + myZ; j++)
                    {
                        greenTiles[i, j].SetActive(true);
                    }
                }

            }
        }
    }

    private void MouseUp()
    {
        if (moveFurniture != null)
        {

            //타일 격자 끄기
            groundTileLine.SetActive(false);


            defaultY = 0.25f;
            moveFurniture.transform.position = new Vector3(moveFurniture.transform.position.x, defaultY, moveFurniture.transform.position.z);
            onMoveFurniture = false;
            moveFurniture = null;


            //타일 그라운드 끄기
            for (int i = 0; i < groundTileNum; i++)
            {
                for (int j = 0; j < groundTileNum; j++)
                {
                    greenTiles[i, j].SetActive(false);
                }
            }
        }
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
