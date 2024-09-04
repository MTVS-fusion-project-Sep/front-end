using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureData_GH : MonoBehaviour
{
    public FurnitureInfo furnitureInfo;
    public List<Transform> Ground_furniturePos = new List<Transform>();
    public List<Transform> Wall_furniturePos = new List<Transform>();
    


    void LateStart()
    {
        DragManager_GH dragM = GameObject.Find("DragManager").GetComponent<DragManager_GH>();
        transform.position = new Vector3(dragM.ground_Xs[furnitureInfo.furni_Current_X].transform.position.x, 0.25f, dragM.ground_Ys[furnitureInfo.furni_Current_Y].transform.position.z);

    }

    void Update()
    {
        
    }
}
