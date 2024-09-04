using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomUIManager_GH : MonoBehaviour
{
    // 가구 리스트를 만든다.
    public List<Image> ui_Furniture = new List<Image>();
    string st_Furniture = "ui_Furniture";

    // 벽지 리스트를 만든다.
    public List<Image> ui_Wall = new List<Image>();
    string st_Wall = "ui_Wall";

    // 바닥 리스트를 만든다.
    public List<Image> ui_Ground = new List<Image>();
    string st_Ground = "ui_Ground";


    // 셋액티브 true / false
    bool setAct;

    // 0.방꾸 
    public Button roomSettingBut;

    //방꾸 패널
    public GameObject roomSettinfPanel;


    // 기존 세팅


    void Start()
    {


        for (int i = 0; i < ui_Furniture.Count; i++)
        {
            ui_Furniture[i] = Instantiate(ui_Furniture[i], GameObject.Find("ContentFurniture").transform);
            ui_Furniture[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < ui_Wall.Count; i++)
        {
            ui_Wall[i] = Instantiate(ui_Wall[i], GameObject.Find("ContentFurniture").transform);
            ui_Wall[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < ui_Ground.Count; i++)
        {
            ui_Ground[i] = Instantiate(ui_Ground[i], GameObject.Find("ContentFurniture").transform);
            ui_Ground[i].gameObject.SetActive(false);
        }
        roomSettinfPanel.SetActive(false);
        roomSettingBut.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnFurniture()
    {
        ForUI(true, false, false);
    }

    public void OnWall()
    {
        ForUI(false, true, false);
    }
    public void OnGround()
    {
        ForUI(false, false, true);
    }

    public void SettingRoom()
    {
        roomSettinfPanel.SetActive(true);
        roomSettingBut.gameObject.SetActive(false);

    }

    public void OnSave()
    {

    }

    public void OnExit()
    {
        roomSettinfPanel.SetActive(false);
        roomSettingBut.gameObject.SetActive(true);

    }


    public void ForUI(bool furniture, bool wall, bool ground)
    {
        for (int i = 0; i < ui_Furniture.Count; i++)
            ui_Furniture[i].gameObject.SetActive(furniture);


        for (int i = 0; i < ui_Wall.Count; i++)
            ui_Wall[i].gameObject.SetActive(wall);


        for (int i = 0; i < ui_Ground.Count; i++)
            ui_Ground[i].gameObject.SetActive(ground);
    }
}
