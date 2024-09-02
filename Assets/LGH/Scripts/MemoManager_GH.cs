using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// 게임오브젝트 리스트로 메모를 담을 공간을 만든다.
// 시작과 동시에 리스트에 있는 메모들을 캔버스 안에 생성한다.
// 메모가 추가나 삭제 기능을 넣는다.


// 메모 추가 기능
// 메모 붙이기를 누르면 메모 리스트에 하나가 추가가 되고 현재 페이지도 맥스 페이지로 이동을 한다.
// 메모를 붙인 것과 안 붙인것은 메모 붙이기와 붙이기로 바뀐다. (bool)
// 메모를 작성한 후 붙이기를 누르게 되면 메모가 저장이 된다.

public class MemoManager_GH : MonoBehaviour
{
    [SerializeField]
    List<GameObject> memoList = new List<GameObject>();

    //메모 더미데이터를 위한 갯수
    public int memoCount = 2;

    //메모 기본 오브젝트
    public GameObject memoFactory;

    //메모 스와이프
    public SwipeUI_GH memoSwipe;

    //메모 저장 버튼
    public GameObject saveButton;

    //메모 저장 버튼 활성화
    bool onSaveButton = true;

    void Awake()
    {
        //기록되어 있는 메모에 따른 메모 생성
        for (int i = 0; i < memoCount; i++)
        {
            memoList.Add(Instantiate(memoFactory, GameObject.Find("ContentMemo").transform));
            TMP_InputField inputfield = memoList[i].GetComponentInChildren<TMP_InputField>();
            inputfield.gameObject.SetActive(false);
        }

        saveButton.SetActive(false);
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
            memoSwipe.currentPage = memoSwipe.maxPage -1;
        }

    }

    public void memoSave()
    {
        TMP_Text text = memoList[memoCount - 1].GetComponentInChildren<TMP_Text>();
        TMP_InputField inputfield = memoList[memoCount - 1].GetComponentInChildren<TMP_InputField>();

        text.text = inputfield.text;
        inputfield.gameObject.SetActive(false);
        onSaveButton = true;

    }



    void Update()
    {

        if (memoSwipe.currentPage + 1 == memoSwipe.maxPage && !onSaveButton)
        {
            saveButton.SetActive(true);
        }
        else
        {
            saveButton.SetActive(false);
        }
    }
}
