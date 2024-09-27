using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitChat_GH : MonoBehaviour
{
    Button exit;
    public GameObject maingame;
        void Start()
    {
        exit = GetComponent<Button>();
        exit.onClick.AddListener(exita);
    }

    void exita()
    {
        Destroy(maingame);
    }
}
