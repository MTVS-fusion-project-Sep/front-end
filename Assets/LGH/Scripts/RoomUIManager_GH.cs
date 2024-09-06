using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomUIManager_GH : MonoBehaviour
{
    // 가구 리스트를 만든다.
    public List<Image> ui_Furniture = new List<Image>();

    //가구 리스트
    public List<GameObject> list_Furniture = new List<GameObject>();


    // 벽지 리스트를 만든다.
    public List<Image> ui_Wall = new List<Image>();

    // 바닥 리스트를 만든다.
    public List<Image> ui_Ground = new List<Image>();



    // 버튼 리스트
    public List<Button> slot_furnis = new List<Button>();

    // 버튼 프리팹
    public Button slot_Prefab;



    // 0.방꾸 
    public Button roomSettingBut;

    //방꾸 패널
    public GameObject roomSettinfPanel;
    public bool onRoomPanel = false;


    // 기존 세팅
    public GameObject contentFurniture;

    void Start()
    {

        for (int i = 0; i < ui_Furniture.Count; i++)
        {
            slot_furnis.Add(Instantiate(slot_Prefab, contentFurniture.transform));
            Image slotImage = slot_furnis[i].transform.GetChild(0).GetComponent<Image>();
            SlotClick_GH slotClick = slot_furnis[i].GetComponent<SlotClick_GH>();
            slotClick.IndexSet(i, 0);
            slotImage.sprite = ui_Furniture[i].sprite;
        }

        roomSettinfPanel.SetActive(false);
        roomSettingBut.gameObject.SetActive(true);
    }

    void Update()
    {

    }
    public void OnFurniture()
    {
        ForUI(ui_Furniture, 0);
    }

    public void OnWall()
    {
        ForUI(ui_Wall, 1);
    }
    public void OnGround()
    {
        ForUI(ui_Ground, 2);
    }

    public void SettingRoom()
    {
        roomSettinfPanel.SetActive(true);
        roomSettingBut.gameObject.SetActive(false);
        onRoomPanel = true;
    }

    public void OnSave()
    {
        print("저장되었습니다~");
    }

    public void OnExit()
    {
        roomSettinfPanel.SetActive(false);
        roomSettingBut.gameObject.SetActive(true);
        onRoomPanel = false;
    }

    void ForUI(List<Image> cate, int cateindex)
    {
        slot_furnis.Clear();
        UIListReset();
        for (int i = 0; i < cate.Count; i++)
        {
            slot_furnis.Add(Instantiate(slot_Prefab, contentFurniture.transform));
            Image slotImage = slot_furnis[i].transform.GetChild(0).GetComponent<Image>();
            SlotClick_GH slotClick = slot_furnis[i].GetComponent<SlotClick_GH>();
            slotClick.IndexSet(i, cateindex);

            slotImage.sprite = cate[i].sprite;
           // slotImage.material = cate[i].material;

        }

    }

    void UIListReset()
    {
        for(int i = 0; i < contentFurniture.transform.childCount; i++)
        {
            Destroy(contentFurniture.transform.GetChild(i).gameObject);
        }
    }
}
