using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    GameObject btn_LobbyExit;

    // Start is called before the first frame update
    void Start()
    {
        btn_LobbyExit = GameObject.Find("Btn_LobbyExit");

    
    
    }

    // Update is called once per frame
    void Update()
    {
        



    }
    public void LobbyExit()
    {
        SceneManager.LoadScene("HoonMainScene");

    }

}
