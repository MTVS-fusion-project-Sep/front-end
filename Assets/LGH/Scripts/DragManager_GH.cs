using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragManager_GH : MonoBehaviour
{
    public GameObject noticeBoard;

    bool onNotice = false;


    // 바닥 X, Y 축의 기준점들
    public List<Transform> groundPoint = new List<Transform>();

    // 바닥 격자 갯수
    public int groundTileNum = 8;

    GameObject obj_Point;

    GameObject[] ground_Xs;
    GameObject[] ground_Ys;



    Vector3 dist;
    void Start()
    {
        noticeBoard.SetActive(false);


        obj_Point = new GameObject("Point");
        obj_Point.transform.position = groundPoint[0].position;
        dist = Vector3.zero;

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
        


        if (Input.GetMouseButton(0) && !onNotice)
        {
            OnMouseDown();
        }
        GroundTile();
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, float.MaxValue, 1 << LayerMask.NameToLayer("Furniture") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("NoticeBoard")))
        {
            float testX = 0;
            float testY = 0;

            Debug.Log(hit.transform.gameObject.name);
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                for (int i = 0; i < groundTileNum; i++)
                {
                    // 만약 마우스의 포인트 값이 각 포인트 위치의 사이라면 hit하고있는 물건의 트랜스폼은 ㄱ드 포인트의 값 단, 최대는 그 포인트 값 -가구의 x값
                    if (ground_Xs[i + 1] != null)
                    {
                        if (ground_Xs[i].transform.position.x < hit.transform.position.x && hit.transform.position.x <= ground_Xs[i].transform.position.x)
                        {
                            testX = ground_Xs[i + 1].transform.position.x;
                        }
                    }
                    if (ground_Ys[i + 1] != null)
                    {
                        if (ground_Ys[i].transform.position.x < hit.transform.position.x && hit.transform.position.x <= ground_Xs[i].transform.position.x)
                        {
                            testY = ground_Ys[i + 1].transform.position.z;
                        }

                    }

                }
                //hit의 포지션의 x값이 ground_Xs[i]의 값만 되게
                //hit의 포지션의 y값이 ground_Ys[i]의 값만 되게
                hit.transform.position = new Vector3(testX, 0.25f, testY);
            }

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("NoticeBoard"))
            {
                noticeBoard.SetActive(true);
                onNotice = true;
            }
        }
    }

    public void GroundTile()
    {
        for (int i = 0; i < groundTileNum; i++)
        {
            ground_Xs[i].transform.position = Vector3.Lerp(groundPoint[0].position, groundPoint[1].position, ((float)(i + 1) / (float)groundTileNum));
            ground_Ys[i].transform.position = Vector3.Lerp(groundPoint[0].position, groundPoint[2].position, ((float)(i + 1) / (float)groundTileNum));
        }
    }

    public void ButtonExit()
    {
        noticeBoard.SetActive(false);
        onNotice = false;
    }
}
