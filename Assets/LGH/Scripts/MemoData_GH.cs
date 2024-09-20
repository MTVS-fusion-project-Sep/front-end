using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemoData_GH : MonoBehaviour
{
    public MemoInfo_GH memoInfo;
    void Start()
    {
        TMP_InputField inputfield = GetComponentInChildren<TMP_InputField>();
        inputfield.gameObject.SetActive(false);

        TMP_Text memoText = GetComponentInChildren<TMP_Text>();
        memoText.text = memoInfo.memoText;

    }

    void Update()
    {
        
    }
}
