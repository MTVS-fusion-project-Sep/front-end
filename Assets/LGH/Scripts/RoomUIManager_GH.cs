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
    public static RoomUIManager_GH instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    //http
    public string httpIP = "192.168.0.76";
    public string httpPort = "8080";


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
    public GameObject roomSettingPanel;
    public bool onRoomPanel = false;


    // 기존 세팅
    public GameObject contentFurniture;


    //http 받아오기
    //가구
    public FurnitureInfoList furnitureInfoList;
    //벽 가구
    public WallObjectInfoList wallObjectInfoList;

    //타일 및 벽
    public UserRoomInfo userRoomInfo;

    //현재의 벽과 땅 타일
    int cur_W_Index = 0;
    int cur_T_Index = 0;

    //유저 아이디 더미 데이터
    public string roomUserId = "user1";

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

        roomSettingPanel.SetActive(false);
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
        roomSettingPanel.SetActive(true);
        roomSettingBut.gameObject.SetActive(false);
        onRoomPanel = true;
    }

    public void OnSave()
    {
        // 가구 카테고리 별로 나누기
        // 가구 키값들 가구 이름으로 입력하기
        // 가구 땅가구 벽가구 따로 보내기
        // 현재 방에 배치되어있는 정보 방정보 보내는 거로 바꾸기.
        for (int i = 0; i < list_Furniture.Count; i++)
        {
            //땅 가구 데이터 보내기
            if (list_Furniture[i].layer == LayerMask.NameToLayer("Furniture"))
            {
                FurnitureData_GH fd = list_Furniture[i].GetComponent<FurnitureData_GH>();
                HttpInfo info = new HttpInfo();
                info.url = "http://" + httpIP + ":" + httpPort + "/ground-furniture";
                info.body = JsonUtility.ToJson(fd.furnitureInfo);
                info.contentType = "application/json";
                info.onComplete = (DownloadHandler downloadHandler) =>
                {
                    print(downloadHandler.text);
                };
                StartCoroutine(NetworkManager_GH.GetInstance().Post(info));
            }
            // 벽가구 데이터 보내기
            else if (list_Furniture[i].layer == LayerMask.NameToLayer("WallObject"))
            {
                WallObjectData_GH wd = list_Furniture[i].GetComponent<WallObjectData_GH>();
                HttpInfo info = new HttpInfo();
                info.url = "http://" + httpIP + ":" + httpPort + "/wall-furniture";
                info.body = JsonUtility.ToJson(wd.wallObjectInfo);
                info.contentType = "application/json";
                info.onComplete = (DownloadHandler downloadHandler) =>
                {
                    print(downloadHandler.text);
                };
                StartCoroutine(NetworkManager_GH.GetInstance().Post(info));
            }
        }

        // 방 데이터 보내기
        UserRoomInfo roomInfo = new UserRoomInfo();
        roomInfo.wallIndex = cur_W_Index;
        roomInfo.tileIndex = cur_T_Index;
        roomInfo.userId = roomUserId;

        HttpInfo roomHttpInfo = new HttpInfo();
        roomHttpInfo.url = "http://" + httpIP + ":" + httpPort + "/room-info";
        roomHttpInfo.body = JsonUtility.ToJson(roomInfo);
        roomHttpInfo.contentType = "application/json";
        roomHttpInfo.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Post(roomHttpInfo));
    }

    public void OnLoad()
    {
        FurniLoad();
        RoomLoad();
    }

    void FurniLoad()
    {
        //룸 유저 아이디를 보내준다?

        // 로그인하면 룸 유저 아이디를 기본으로 그냥 아이디로 바꾸는걸로 함수 구현

        // 또 로비에서 방 방문을 누르게 되면 그 해당 방 정보를 받아오도록 구현


        //받아올때 아이디 뒤에 붙이게

        HttpInfo info = new HttpInfo();
        info.url = "http://" + httpIP + ":" + httpPort + "/ground-furniture/user?userId=" + roomUserId;
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자. 
            furnitureInfoList = JsonUtility.FromJson<FurnitureInfoList>(jsonData);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info));

        HttpInfo info2 = new HttpInfo();
        info2.url = "http://" + httpIP + ":" + httpPort + "/wall-furniture/user?userId=" + roomUserId;
        info2.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자. 
            wallObjectInfoList = JsonUtility.FromJson<WallObjectInfoList>(jsonData);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info2));

        StartCoroutine(Setting());

    }

    IEnumerator Setting()
    {

        yield return new WaitForSeconds(3);
        Set();
    }
    public void Set()
    {
        //벽, 땅가구 데이터 세팅
        for (int i = 0; i < list_Furniture.Count; i++)
        {
            if (list_Furniture[i].layer == LayerMask.NameToLayer("Furniture"))
            {
                print("가구 돼라");
                for (int j = 0; j < furnitureInfoList.data.Count; j++)
                {
                    FurnitureData_GH fd = list_Furniture[i].GetComponent<FurnitureData_GH>();
                    if (list_Furniture[i].name == furnitureInfoList.data[j].furniName)
                    {
                        fd.furnitureInfo = furnitureInfoList.data[j];
                    }
                }
            }
            else if (list_Furniture[i].layer == LayerMask.NameToLayer("WallObject"))
            {
                print("벽 돼라");
                for (int j = 0; j < wallObjectInfoList.data.Count; j++)
                {
                    WallObjectData_GH wd = list_Furniture[i].GetComponent<WallObjectData_GH>();
                    if (list_Furniture[i].name == wallObjectInfoList.data[j].furniName)
                    {
                        wd.wallObjectInfo = wallObjectInfoList.data[j];
                    }
                }
            }
        }
    }

    void RoomLoad()
    {
        // 방정보 받아오기
        HttpInfo info = new HttpInfo();
        info.url = "http://" + httpIP + ":" + httpPort + "/room-info?userId=" + roomUserId;
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자. todo리스트 어떻게 받을지 물어보기
            userRoomInfo = JsonUtility.FromJson<UserRoomInfo>(jsonData);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info));
    }
    public void OnExit()
    {
        //OnLoad();

        roomSettingPanel.SetActive(false);
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

        RectTransform furniListRec = contentFurniture.GetComponent<RectTransform>();

        // 카테고리 변경시 스크롤 되돌리기
        furniListRec.position = new Vector3(furniListRec.position.x, 0, furniListRec.position.z);
    }

    void UIListReset()
    {
        for (int i = 0; i < contentFurniture.transform.childCount; i++)
        {
            Destroy(contentFurniture.transform.GetChild(i).gameObject);
        }
    }


    public void WallIndexSetting(int w_Index)
    {
        cur_W_Index = w_Index;
    }
    public void TileIndexSetting(int t_Index)
    {
        cur_T_Index = t_Index;
    }



}
[System.Serializable]
public struct UserRoomInfo
{
    public int wallIndex;
    public int tileIndex;
    public string userId;
}

