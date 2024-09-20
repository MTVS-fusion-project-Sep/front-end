using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class MemoManager_GH : MonoBehaviour
{
    [SerializeField]
    List<GameObject> memoList = new List<GameObject>();

    //메모 더미데이터를 위한 갯수
    public int memoCount = 0;

    //메모 기본 오브젝트
    public GameObject memoFactory;

    //메모 스와이프
    public SwipeUI_GH memoSwipe;

    //메모 저장 / 붙이기 / 삭제
    public GameObject[] memoButtons;



    //메모 저장 버튼 활성화
    bool onSaveButton = true;


    //메모 데이터를 받은 값을 저장
    public MemoInfoList memoInfoList;
    void Awake()
    {
       

        memoButtons[0].SetActive(false);
    }

    public void memoWrite()
    {
        if (onSaveButton)
        {

            memoCount++;
            memoSwipe.memoPageUpdate(memoCount);
            memoList.Add(Instantiate(memoFactory, GameObject.Find("ContentMemo").transform));
            memoSwipe.currentPage = memoCount - 1;

            TMP_Text text = memoList[memoCount - 1].GetComponentInChildren<TMP_Text>();
            text.text = "";
            onSaveButton = false;
        }
        else
        {
            memoSwipe.currentPage = memoSwipe.maxPage - 1;
        }

    }

    public void memoSave()
    {
        TMP_Text text = memoList[memoCount - 1].GetComponentInChildren<TMP_Text>();
        TMP_InputField inputfield = memoList[memoCount - 1].GetComponentInChildren<TMP_InputField>();

        text.text = inputfield.text;
        inputfield.gameObject.SetActive(false);
        onSaveButton = true;

        // 새로운 메모 데이터 보내기
        MemoInfo_GH memoInfo = new MemoInfo_GH();
        memoInfo.userID = "이규현";
        memoInfo.memoText = inputfield.text;
        memoInfo.writerID = "전성표";
        memoInfo.writeDate = DateTime.Now.ToString(("yyyy-MM-dd HH:mm"));

        HttpInfo HttpInfo = new HttpInfo();
        HttpInfo.url = "";
        HttpInfo.body = JsonUtility.ToJson(memoInfo);
        HttpInfo.contentType = "";
        HttpInfo.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Post(HttpInfo));
    }
    public void memoLoad()
    {

        //메모 받아오기
        HttpInfo info = new HttpInfo();
        info.url = "";
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자. 
            memoInfoList = JsonUtility.FromJson<MemoInfoList>(jsonData);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info));

        memoCount = memoInfoList.data.Count;
        //기록되어 있는 메모에 따른 메모 생성
        for (int i = 0; i < memoCount; i++)
        {
            memoList.Add(Instantiate(memoFactory, GameObject.Find("ContentMemo").transform));
            MemoData_GH md = memoList[i].GetComponent<MemoData_GH>();
            md.memoInfo = memoInfoList.data[i];
        }
    }

    public void memoDelete()
    {
        // 메모 삭제시 메모 아이디 추가??======================================================
        //현재 순서에 있는 메모를 삭제 시킨다.
        Destroy(memoList[memoSwipe.currentPage].gameObject);
        //메모의 총 숫자를 하나 낮춘다.
        memoCount--;
        memoSwipe.memoPageUpdate(memoCount);
        memoList.RemoveAt(memoSwipe.currentPage);
        memoSwipe.currentPage = memoCount - 1;

    }



    void Update()
    {
        // 메모붙이기 메모 삭제 안보이게하기
        if (memoSwipe.currentPage + 1 == memoSwipe.maxPage && !onSaveButton)
        {
            memoButtons[0].SetActive(true);
            memoButtons[1].SetActive(false);
            memoButtons[2].SetActive(false);

        }
        else
        {
            memoButtons[0].SetActive(false);
            memoButtons[1].SetActive(true);
            memoButtons[2].SetActive(true);
        }
    }
}

