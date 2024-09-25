using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditorInternal.ReorderableList;

public class DragManager_GH : MonoBehaviour
{
    #region 땅 전역변수
    //초록색 큐브들 8X8
    GameObject[,] greenTiles;

    //초록색큐브
    public GameObject greenTilefPrefab;

    // 큐브 Mat
    public Material[] cubeMat = new Material[2];

    //땅 격자
    public GameObject groundTileLine;

    public GameObject noticeBoard;

    bool onNotice = false;

    // 바닥 X, Y 축의 기준점들
    public List<Transform> groundPoint = new List<Transform>();

    // 바닥 격자 갯수
    public int groundTileNum = 8;

    GameObject obj_Point;

    public GameObject[] ground_Xs;
    public GameObject[] ground_Zs;
    #endregion
    #region 가구 전역 변수
    //기본 가구의 y값
    float defaultY = 0.25f;

    //가구 움직임 변수들
    GameObject moveFurniture;
    bool onMoveFurniture;

    FurnitureData_GH moveFurniData;

    //가구 검출하기
    public float radius = 7f;
    Collider[] colliders;

    List<List<bool>> donXZ = new List<List<bool>>();
    #endregion
    #region 벽 오브젝트 전역변수
    //벽가구
    GameObject wallObject;

    public Transform[] wallPos = new Transform[3];

    WallObjectData_GH wallObjectData;

    RoomUIManager_GH roomUIMag;

    //벽에 가구가 있는지 확인
    public bool[] onWallObjects = new bool[3];

    // 처음 클릭했을 때 벽 오브젝트가 있는 위치;
    public int beforeWallObPos = 0;
    #endregion

    // 방꾸 버튼
    public GameObject roomSetBut;

    public SwipeUI_GH swipe;

    private void Awake()
    {
        //포인트 찍기
        obj_Point = new GameObject("Point");
        obj_Point.transform.position = groundPoint[0].position;

        ground_Xs = new GameObject[groundTileNum];
        ground_Zs = new GameObject[groundTileNum];


        for (int i = 0; i < groundTileNum; i++)
        {
            ground_Xs[i] = Instantiate(obj_Point);
        }

        for (int i = 0; i < groundTileNum; i++)
        {
            ground_Zs[i] = Instantiate(obj_Point);
        }

        //타일을 생성합니다
        GroundTile();

        for (int x = 0; x < groundTileNum; x++)
        {
            donXZ.Add(new List<bool>());
            for (int z = 0; z < groundTileNum; z++)
            {
                donXZ[x].Add(false);
            }
        }
    }

    void Start()
    {
        // 타일 생성
        groundTileLine.SetActive(false);

        noticeBoard.SetActive(false);

        greenTiles = new GameObject[groundTileNum, groundTileNum];

        for (int i = 0; i < groundTileNum; i++)
        {
            for (int j = 0; j < groundTileNum; j++)
            {
                greenTiles[i, j] = Instantiate(greenTilefPrefab, GameObject.Find("GroundTiles").transform);
                greenTiles[i, j].transform.position = new Vector3(ground_Xs[i].transform.position.x, defaultY, ground_Zs[j].transform.position.z);
                greenTiles[i, j].gameObject.SetActive(false);
            }
        }
        roomUIMag = GameObject.Find("RoomUIManager").GetComponent<RoomUIManager_GH>();
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
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Furniture") | 1 << LayerMask.NameToLayer("NoticeBoard") | 1 << LayerMask.NameToLayer("WallObject")))
        {
            //룸패널이 켜져있을 때만 발동

            if (roomUIMag.onRoomPanel)
            {
                // 오브젝트 옮기기
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
                    FurnitureData_GH collFD;
                    //콜라이더들의 현재 x위치와 사이즈사이를 모두 담는다.
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliders[i].gameObject != moveFurniture)
                        {
                            collFD = colliders[i].GetComponent<FurnitureData_GH>();
                            for (int j = collFD.furnitureInfo.furniCurrentX; j < collFD.furnitureInfo.furniCurrentX + collFD.furnitureInfo.furniSizeX; j++)
                            {
                                for (int k = collFD.furnitureInfo.furniCurrentZ; k < collFD.furnitureInfo.furniCurrentZ + collFD.furnitureInfo.furniSizeZ; k++)
                                {
                                    donXZ[j][k] = true;
                                }
                            }
                        }
                    }
                }
                // 벽 오브젝트 옮기기 
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("WallObject"))
                {
                    wallObject = hit.transform.gameObject;
                    wallObjectData = wallObject.GetComponent<WallObjectData_GH>();
                    beforeWallObPos = (int)wallObjectData.wallObjectInfo.furniPos - 1;

                }
            }
            else
            {
                // 방명록들어가기
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("NoticeBoard"))
                {
                    noticeBoard.SetActive(true);
                    onNotice = true;
                    roomSetBut.SetActive(false);
                }

            }
        }

    }


    private void OnMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("WallPosColl")))
        {
            //땅 가구 옮기기
            if (moveFurniture != null)
            {
                int myX = 99;
                int myZ = 99;

                float minX = 9999;
                float minZ = 9999;

                float xDistance = 99f;
                float zDistance = 99f;
                // x값의 최댓 값 구하기
                for (int i = 0; i <= groundTileNum - moveFurniData.furnitureInfo.furniSizeX; i++)
                {
                    xDistance = Vector3.Distance(hit.point, ground_Xs[i].transform.position);
                    //x값을 가지고 ground_Xs[i] 이랑 비교해서 가장 가까운 ground_Xs를 구하고
                    if (minX > xDistance)
                    {
                        myX = i;
                        minX = xDistance;
                    }
                }
                // z값의 최댓 값 구하기
                for (int i = 0; i <= groundTileNum - moveFurniData.furnitureInfo.furniSizeZ; i++)
                {
                    zDistance = Vector3.Distance(hit.point, ground_Zs[i].transform.position);
                    //z값을 가지고 ground_Zs[i] 이랑 비교해서 가장 가까운 ground_Zs 구해서
                    if (minZ > zDistance)
                    {
                        myZ = i;
                        minZ = zDistance;
                    }
                }
                bool donMove = false;
                for (int j = myX; j < myX + moveFurniData.furnitureInfo.furniSizeX; j++)
                {
                    if (j > groundTileNum)
                    {
                        break;
                    }

                    for (int k = myZ; k < myZ + moveFurniData.furnitureInfo.furniSizeZ; k++)
                    {
                        if (k > groundTileNum)
                        {
                            break;
                        }

                        if (donXZ[j][k] == true)
                        {
                            donMove = true;
                        }
                    }
                }


                if (donMove == false)
                {
                    //moveFurniture의 위치를 옮긴다
                    moveFurniture.transform.position = new Vector3(ground_Xs[myX].transform.position.x, defaultY, ground_Zs[myZ].transform.position.z);
                    moveFurniData.furnitureInfo.furniCurrentX = myX;
                    moveFurniData.furnitureInfo.furniCurrentZ = myZ;
                    defaultY = 1.5f;

                    //타일 색 바꾸기
                    TileColorSwitch(cubeMat[0]);
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

                        for (int i = myX; i < fd.furnitureInfo.furniSizeX + myX; i++)
                        {
                            for (int j = myZ; j < fd.furnitureInfo.furniSizeZ + myZ; j++)
                            {
                                greenTiles[i, j].SetActive(true);
                            }
                        }
                    }
                }
                else
                {
                    //타일 색을 빨간색으로 바꾼다.
                    TileColorSwitch(cubeMat[1]);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    //오브젝트 회전하기
                    RotateXZ();
                }
            }
            if (wallObject != null)
            {
                for (int i = 0; i < wallPos.Length; i++)
                {
                    if (hit.transform.gameObject == wallPos[i].gameObject)
                    {
                        wallObjectData.wallObjectInfo.furniPos = (WallType)(i + 1);
                        wallObjectData.SetWallPos(i, beforeWallObPos);
                    }
                }
            }
        }
    }

    private void MouseUp()
    {
        //타일 색 원래대로 돌리기
        TileColorSwitch(cubeMat[0]);

        if (moveFurniture != null)
        {
            //타일 격자 끄기
            groundTileLine.SetActive(false);

            defaultY = 0.25f;
            moveFurniture.transform.position = new Vector3(moveFurniture.transform.position.x, defaultY, moveFurniture.transform.position.z);
            onMoveFurniture = false;
            moveFurniture = null;

            for (int x = 0; x < groundTileNum; x++)
            {
                for (int z = 0; z < groundTileNum; z++)
                {
                    donXZ[x][z] = false;
                }
            }
            //타일 그라운드 끄기
            for (int i = 0; i < groundTileNum; i++)
            {
                for (int j = 0; j < groundTileNum; j++)
                {
                    greenTiles[i, j].SetActive(false);
                }
            }
        }

        if (wallObject != null)
        {
            wallObjectData = null;
            wallObject = null;
        }
    }

    public void GroundTile()
    {
        for (int i = 0; i < groundTileNum; i++)
        {
            ground_Zs[i].transform.position = Vector3.Lerp(groundPoint[0].position, groundPoint[1].position, ((float)(i) / (float)groundTileNum));
            ground_Xs[i].transform.position = Vector3.Lerp(groundPoint[0].position, groundPoint[2].position, ((float)(i) / (float)groundTileNum));
        }
    }
    public void TileColorSwitch(Material tileMat)
    {
        for (int i = 0; i < groundTileNum; i++)
        {
            for (int j = 0; j < groundTileNum; j++)
            {
                greenTiles[i, j].GetComponentInChildren<MeshRenderer>().material = tileMat;
            }
        }
    }

    public void ButtonExit()
    {
        noticeBoard.SetActive(false);
        onNotice = false;
        if (roomUIMag.selfRoom)
        {
            roomSetBut.SetActive(true);

        }
    }

    public void RotateXZ()
    {
        //moveFurniData.furnitureInfo.furni_Size_X, moveFurniData.furnitureInfo.furni_Size_Y, rotFurnicoll.center.x, rotFurnicoll.center.z, rotFurnicoll.size.x, rotFurnicoll.size.z
        int intEmpty = 0;
        Vector3 VectorEmpty = Vector3.zero;

        BoxCollider rotFurnicoll = moveFurniture.GetComponent<BoxCollider>();

        // 사이즈 값 바꾸기
        intEmpty = moveFurniData.furnitureInfo.furniSizeX;
        moveFurniData.furnitureInfo.furniSizeX = moveFurniData.furnitureInfo.furniSizeZ;
        moveFurniData.furnitureInfo.furniSizeZ = intEmpty;

        // 콜라이더 센터값 바꾸기
        VectorEmpty = rotFurnicoll.center;
        rotFurnicoll.center = new Vector3(VectorEmpty.z, VectorEmpty.y, VectorEmpty.x);

        // 콜라이더 사이즈 바꾸기
        VectorEmpty = rotFurnicoll.size;
        rotFurnicoll.size = new Vector3(VectorEmpty.z, VectorEmpty.y, VectorEmpty.x);

        if (!moveFurniData.furnitureInfo.furniRotate)
        {
            //바뀐 오브젝트 키기
            moveFurniData.transform.GetChild(0).gameObject.SetActive(false);
            moveFurniData.transform.GetChild(1).gameObject.SetActive(true);

            moveFurniData.furnitureInfo.furniRotate = true;
        }
        else
        {
            //바뀐 오브젝트 키기
            moveFurniData.transform.GetChild(0).gameObject.SetActive(true);
            moveFurniData.transform.GetChild(1).gameObject.SetActive(false);

            moveFurniData.furnitureInfo.furniRotate = false;
        }
    }
}
