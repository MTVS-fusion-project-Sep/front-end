﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.WebRequestMethods;


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
    public bool onSaveButton = true;


    //메모 데이터를 받은 값을 저장
    public MemoInfoList memoInfoList;

    //메모지 저장 콘텐츠
    public GameObject memoContents;
    void Awake()
    {

        memoButtons[0].SetActive(false);
        //서버가 응답하지 않을때 error 임시주석처리
        //MemoLoad();
    }

    private void Start()
    {

    }
    public void MemoWrite()
    {
        if (onSaveButton)
        {
            memoCount++;
            memoSwipe.memoPageUpdate(memoCount);
            memoList.Add(Instantiate(memoFactory, memoContents.transform));
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

    public void MemoSave()
    {
        MemoData_GH md = memoList[memoCount - 1].GetComponent<MemoData_GH>();
        TMP_InputField inputfield = md.GetComponentInChildren<TMP_InputField>();

        inputfield.gameObject.SetActive(false);
        onSaveButton = true;

        // 새로운 메모 데이터 보내기
        MemoInfo_GH memoInfo = new MemoInfo_GH();
        memoInfo.toUserId = DataManager_GH.instance.roomId;
        memoInfo.content = inputfield.text;
        memoInfo.fromUserId = DataManager_GH.instance.userId;
        memoInfo.registDate = DateTime.Now.ToString(("yyyy-MM-dd HH:mm"));

        md.SetMemoContents(memoInfo.content, memoInfo.fromUserId, memoInfo.registDate);

        HttpInfo HttpInfo = new HttpInfo();
        HttpInfo.url = "http://" + RoomUIManager_GH.instance.httpIP + ":" + RoomUIManager_GH.instance.httpPort + "/guest-book";
        HttpInfo.body = JsonUtility.ToJson(memoInfo);
        HttpInfo.contentType = "application/json";
        HttpInfo.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Post(HttpInfo));
    }
    public void MemoLoad()
    {
        //print(RoomUIManager_GH.instance.roomUserId);
        //메모 받아오기
        HttpInfo info = new HttpInfo();
        info.url = "http://" + RoomUIManager_GH.instance.httpIP + ":" + RoomUIManager_GH.instance.httpPort + "/guest-book/reader?readerId=" + DataManager_GH.instance.roomId;
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            //print(downloadHandler.text);
            string jsonData = "{ \"data\" : " + downloadHandler.text + "}";
            print(jsonData);
            //jsonData를 PostInfoArray 형으로 바꾸자. 
            memoInfoList = JsonUtility.FromJson<MemoInfoList>(jsonData);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Get(info));
        StartCoroutine(SetMemo());
        memoSwipe.SetScrollBarValue(0);

    }
    IEnumerator SetMemo()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < memoList.Count; i++)
        {
            Destroy(memoList[i]);
        }
        memoList.Clear();
        //print("메모 세팅");
        memoCount = memoInfoList.data.Count;
        //기록되어 있는 메모에 따른 메모 생성
        for (int i = 0; i < memoCount; i++)
        {
            memoList.Add(Instantiate(memoFactory, memoContents.transform));
            TMP_InputField inputfield = memoList[i].GetComponentInChildren<TMP_InputField>();
            inputfield.gameObject.SetActive(false);
            MemoData_GH md = memoList[i].GetComponent<MemoData_GH>();
            md.memoInfo = memoInfoList.data[i];
        }
    }
    public void MemoDelete()
    {
        // 새로운 메모 데이터 보내기
        HttpInfo HttpInfo = new HttpInfo();
        HttpInfo.url = "http://" + RoomUIManager_GH.instance.httpIP + ":" + RoomUIManager_GH.instance.httpPort + "/guest-book?guestBookId=" + memoList[memoSwipe.currentPage].GetComponent<MemoData_GH>().memoInfo.id;
        HttpInfo.contentType = "application/json";
        HttpInfo.onComplete = (DownloadHandler downloadHandler) =>
        {
            // print(downloadHandler.text);
        };
        StartCoroutine(NetworkManager_GH.GetInstance().Delete(HttpInfo));

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
            if (RoomUIManager_GH.instance.selfRoom)
            {
                memoButtons[1].SetActive(false);
                if (memoCount > 0)
                {
                    memoButtons[2].SetActive(true);
                }
                else
                {
                    memoButtons[2].SetActive(false);

                }
            }
            else
            {
                memoButtons[1].SetActive(true);
                memoButtons[2].SetActive(false);
            }
        }
    }


}

