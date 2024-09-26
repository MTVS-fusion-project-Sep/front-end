using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoData_GH : MonoBehaviour
{
    public MemoInfo_GH memoInfo;
    public TMP_Text memoText;
    public TMP_Text memoFrom;
    public TMP_Text memoDate;

    void Start()
    {
        SetMemoContents(memoInfo.content, memoInfo.fromUserId, memoInfo.registDate);
        
    }

    void Update()
    {
        
    }
    public void SetMemoContents(string content, string fromUserId, string date) 
    {
        memoText.text = content;
        memoFrom.text = "From. " + fromUserId;
        memoDate.text = date;
    }
}
