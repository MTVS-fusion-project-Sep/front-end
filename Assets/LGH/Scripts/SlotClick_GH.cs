using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SlotClick_GH : MonoBehaviour
{
    int myIndex = 0;
    int myCate = 0;

    public MeshRenderer[] rooms;
    DragManager_GH dragManager;

    public void IndexSet(int index, int cate)
    {
        myIndex = index;
        myCate = cate;
    }
    void Start()
    {
        rooms = new MeshRenderer[3];
        rooms[0] = GameObject.Find("Wall_L").GetComponent<MeshRenderer>();
        rooms[1] = GameObject.Find("Wall_R").GetComponent<MeshRenderer>();
        rooms[2] = GameObject.Find("Ground").GetComponent<MeshRenderer>();

       
        dragManager = GameObject.Find("DragManager").GetComponent<DragManager_GH>();
        AddOnClick(GetComponent<Button>());
    }

    void Update()
    {
        
    }
    void AddOnClick(Button button)
    {
        if (myCate == 0)
        {
            button.onClick.AddListener(FunitureSet);
        }
        else if (myCate == 1)
        {
            button.onClick.AddListener(WallSet);

        }
        else
        {
            button.onClick.AddListener(TileSet);

        }

    }

    void FunitureSet()
    {
        FurnitureData_GH furnitureData = RoomUIManager_GH.instance.list_Furniture[myIndex].GetComponent<FurnitureData_GH>();
        WallObjectData_GH wallObjectData = RoomUIManager_GH.instance.list_Furniture[myIndex].GetComponent<WallObjectData_GH>();
        int wallObjectNum = 0;
        if (wallObjectData != null)
        {
            wallObjectNum = (int)wallObjectData.wallObjectInfo.furniPos - 1;
        }

        if (RoomUIManager_GH.instance.list_Furniture[myIndex].activeSelf)
        {
            if (RoomUIManager_GH.instance.list_Furniture[myIndex].gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                furnitureData.furnitureInfo.onPlace = false;

            }
            else if (RoomUIManager_GH.instance.list_Furniture[myIndex].gameObject.layer == LayerMask.NameToLayer("WallObject"))
            {
                wallObjectData.wallObjectInfo.furniOnPlace = false;
                dragManager.onWallObjects[wallObjectNum] = false;
            }
            RoomUIManager_GH.instance.list_Furniture[myIndex].SetActive(false);
        }
        else
        {
            if (RoomUIManager_GH.instance.list_Furniture[myIndex].gameObject.layer == LayerMask.NameToLayer("Furniture"))
            {
                furnitureData.furnitureInfo.onPlace = true;
            }
            else if (RoomUIManager_GH.instance.list_Furniture[myIndex].gameObject.layer == LayerMask.NameToLayer("WallObject"))
            {
                wallObjectData.wallObjectInfo.furniOnPlace = true;
                dragManager.onWallObjects[wallObjectNum] = true;

            }
            RoomUIManager_GH.instance.list_Furniture[myIndex].SetActive(true);

        }
    }
    void WallSet()
    {
        Image slotImage = RoomUIManager_GH.instance.ui_Wall[myIndex].GetComponent<Image>();
        rooms[0].material = slotImage.material;
        rooms[1].material = slotImage.material;
        RoomUIManager_GH.instance.WallIndexSetting(myIndex);
    }
    void TileSet()
    {
        Image slotImage = RoomUIManager_GH.instance.ui_Ground[myIndex].GetComponent<Image>();
        rooms[2].material = slotImage.material;
        RoomUIManager_GH.instance.TileIndexSetting(myIndex);

    }
}
