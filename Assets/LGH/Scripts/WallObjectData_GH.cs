using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObjectData_GH : MonoBehaviour
{
    public WallObjectInfo wallObjectInfo;
    DragManager_GH dragM;


    void Start()
    {
        dragM = GameObject.Find("DragManager").GetComponent<DragManager_GH>();
        SetWallPos((int)wallObjectInfo.furniPos - 1 , (int)wallObjectInfo.furniPos -1  );
        if (wallObjectInfo.furniOnPlace)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            dragM.onWallObjects[(int)wallObjectInfo.furniPos - 1] = false;
        }


    }
    private void Update()
    {
     

    }

    public void SetWallPos(int wallPos, int beforeWallPos)
    {
        //옮길 위치에 오브젝트가 없을 떄
        if (dragM.onWallObjects[wallPos] == false)
        {
            //오브젝트를 옮기고
            transform.position = dragM.wallPos[wallPos].transform.position;
            transform.forward = dragM.wallPos[wallPos].transform.forward;
            //전에 있던 오브젝트 위치에 오브젝트가 없다라고 바꾼다.
            dragM.onWallObjects[beforeWallPos] = false;
            // 현재 위치에는 오브젝트가 있다라고 바꾼다.
            dragM.onWallObjects[wallPos] = true;
            // 계속해서 옮기는 것을 대비하여 전에 있는 위치를 현재위치값으로 바꿔준다.
            dragM.beforeWallObPos = wallPos;
        }
        else
        {
            //print("이미 오브젝트가 있습니다!");
        }
    }

}
