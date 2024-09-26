using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MultiNpcManager : MonoBehaviour
{
    public GameObject img_NpcKey;
    public AIChatManager aiChat;
    public GameObject imgAIChat;

    bool isImgNpcKey = false;
    bool isNpcActive = false;
    // Start is called before the first frame update
    void Start()
    {
        img_NpcKey.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isNpcActive)
        {
            print("aichat 하자");
            aiChat.OpenAIChat();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("NPC충돌");
        //print("플레이어 충돌");
        img_NpcKey.SetActive(true);
    }



    //여기서 키입력 하면 frame 문제가 있음.
    private void OnTriggerStay(Collider other)
    {
        isNpcActive = true;

    }
    private void OnTriggerExit(Collider other)
    {
        img_NpcKey.SetActive(false);
        imgAIChat.SetActive(false);
        isNpcActive = false;


    }




}
