using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SlotClick_GH : MonoBehaviour
{
    RoomUIManager_GH roomUIMa;
    int myIndex = 0;
    int myCate = 0;

    public MeshRenderer[] Rooms = new MeshRenderer[3];

    public void IndexSet(int index, int cate)
    {
        myIndex = index;
        myCate = cate;
    }
    void Start()
    {
        Rooms[0] = GameObject.Find("Wall_L").GetComponent<MeshRenderer>();
        Rooms[1] = GameObject.Find("Wall_R").GetComponent<MeshRenderer>();
        Rooms[2] = GameObject.Find("Ground").GetComponent<MeshRenderer>();

        roomUIMa = GameObject.Find("RoomUIManager").GetComponent<RoomUIManager_GH>();
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
        else if(myCate == 1)
        {
            button.onClick.AddListener(WallSet);

        }
        else
        {
            button.onClick.AddListener(CarpetSet);

        }

    }

    void FunitureSet()
    {
        if (roomUIMa.list_Furniture[myIndex].activeSelf)
        {
            roomUIMa.list_Furniture[myIndex].SetActive(false);
        }
        else
        {
            roomUIMa.list_Furniture[myIndex].SetActive(true);

        }
    }
    void WallSet()
    {
        Image slotImage = roomUIMa.list_Furniture[myIndex].transform.GetChild(0).GetComponent<Image>();
        Rooms[0].material = slotImage.material;
        Rooms[1].material = slotImage.material;

    }
    void CarpetSet()
    {
        Image slotImage = roomUIMa.list_Furniture[myIndex].transform.GetChild(0).GetComponent<Image>();
        Rooms[2].material = slotImage.material;
    }
}
