using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;

public class RoomUIManager_GH : MonoBehaviour
{
    //http ip
    public string httpIP = "192.168.0.76";

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
        // 가구 카테고리 별로 나누기
        // 가구 키값들 가구 이름으로 입력하기
        // 가구 땅가구 벽가구 따로 보내기
        // 현재 방에 배치되어있는 정보 방정보 보내는 거로 바꾸기.



        print("저장되었습니다~");
        for (int i = 0; i < list_Furniture.Count; i++)
        {
            // 땅 가구 데이터 보내기
            if (list_Furniture[i].layer == LayerMask.NameToLayer("Furniture"))
            {
                FurnitureData_GH fd = list_Furniture[i].GetComponent<FurnitureData_GH>();
                HttpInfo info = new HttpInfo();
                info.url = "http://192.168.0.76:8080/ground-furniture";
                info.body = JsonUtility.ToJson(fd.furnitureInfo);
                info.contentType = "application/json";
                info.onComplete = (DownloadHandler downloadHandler) =>
                {
                    print(downloadHandler.text);
                };
                StartCoroutine(NetworkManager_GH.GetInstance().Post(info));

            }
            //// 벽가구 데이터 보내기
            //else if (list_Furniture[i].layer == LayerMask.NameToLayer("WallObject"))
            //{
            //    WallObjectData_GH wd = list_Furniture[i].GetComponent<WallObjectData_GH>();
            //    HttpInfo info = new HttpInfo();
            //    info.url = "http://192.168.0.76:8080/ground-furniture";
            //    info.body = JsonUtility.ToJson(wd.wallObjectInfo);
            //    info.contentType = "application/json";
            //    info.onComplete = (DownloadHandler downloadHandler) =>
            //    {
            //        print(downloadHandler.text);
            //    };
            //    StartCoroutine(NetworkManager_GH.GetInstance().Post(info));
            //}
        }

        // 방 데이터 보내기
        UserRoomInfo roomInfo = new UserRoomInfo();
        roomInfo.groundTileName = "격자무늬";
        roomInfo.wallTileName = "나무무늬";

        HttpInfo roomHttpInfo = new HttpInfo();
        roomHttpInfo.url = "";
        roomHttpInfo.body = JsonUtility.ToJson(roomInfo);
        roomHttpInfo.contentType = "";
        roomHttpInfo.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Post(roomHttpInfo));
    }

    public void OnLoad()
    {
        //받아올때 아이디 뒤에 붙이게
        for (int i = 0; i < list_Furniture.Count; i++)
        {
            // 땅 가구 데이터 보내기
            if (list_Furniture[i].layer == LayerMask.NameToLayer("Furniture"))
            {
                HttpInfo info = new HttpInfo();
                info.url = "http://192.168.0.76:8080/ground-furniture/user/user1";
                info.onComplete = (DownloadHandler downloadHandler) =>
                {
                    print(downloadHandler.text);
                    string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
                    print(jsonData);
                    //jsonData를 PostInfoArray 형으로 바꾸자. TodoTOdoTOdoTodoTodoTodoTodoTOdoTGodoTodoTOdoTodo
                    //allPostInfo = JsonUtility.FromJson<PostInfoArray>(jsonData);
                };
                StartCoroutine(NetworkManager_GH.GetInstance().Get(info));
            }
        }
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
        for (int i = 0; i < contentFurniture.transform.childCount; i++)
        {
            Destroy(contentFurniture.transform.GetChild(i).gameObject);
        }
    }
}
[System.Serializable]
public struct UserRoomInfo
{
    public string groundTileName;
    public string wallTileName;
}
