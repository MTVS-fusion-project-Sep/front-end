using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureData_GH : MonoBehaviour
{
    public FurnitureInfo furnitureInfo;
    
    void Start()
    {
        DragManager_GH dragM = GameObject.Find("DragManager").GetComponent<DragManager_GH>();
        transform.position = new Vector3(dragM.ground_Xs[furnitureInfo.furni_Current_X].transform.position.x, 0.25f, dragM.ground_Zs[furnitureInfo.furni_Current_Z].transform.position.z);
        if (furnitureInfo.onPlace)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
    private void Update()
    {
        
    }
}
