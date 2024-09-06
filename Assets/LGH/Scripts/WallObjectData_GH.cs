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
       
    }
    private void Update()
    {
        transform.position = dragM.wallPos[(int)wallObjectInfo.wallPos - 1].transform.position;
        transform.forward = dragM.wallPos[(int)wallObjectInfo.wallPos - 1].transform.forward;
    }

}
