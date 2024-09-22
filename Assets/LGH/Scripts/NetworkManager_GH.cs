using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class HttpInfo
{
    public string url = "";

    //Body 데이터
    public string body = "";

    // contentType 
    public string contentType = "";

    //통신 성공 후 호출되는 함수 담을 변수
    public Action<DownloadHandler> onComplete;
}
public class NetworkManager_GH : MonoBehaviour
{
    static NetworkManager_GH instance;

    public static NetworkManager_GH GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject();
            go.name = "NetworkManager";
            go.AddComponent<NetworkManager_GH>();
        }

        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //겟
    public IEnumerator Get(HttpInfo info)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(info.url))
        {
            //서버 요청 보내기(응답이 올 떄 까지 기다린다. ex)인스타그램에서 작동은 되는데 사진을 부른다.)
            yield return webRequest.SendWebRequest();

            //서버에게 응답이 왔다.
            DoneRequest(webRequest, info);
        }

    }

    public IEnumerator Delete(HttpInfo info)
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Delete(info.url))
        {
            //서버 요청 보내기(응답이 올 떄 까지 기다린다. ex)인스타그램에서 작동은 되는데 사진을 부른다.)
            yield return webRequest.SendWebRequest();

            //서버에게 응답이 왔다.
            DoneRequest(webRequest, info);
        }

    }

    // 서버에게 내가 보내는 데이터를 생성해줘
    public IEnumerator Post(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, info.body, info.contentType))
        {
            //서버 요청 보내기(응답이 올 떄 까지 기다린다. ex)인스타그램에서 작동은 되는데 사진을 부른다.)
            yield return webRequest.SendWebRequest();
            //서버에게 응답이 왔다.
            DoneRequest(webRequest, info);
        }
    }

    void DoneRequest(UnityWebRequest webRequest, HttpInfo info)
    {
        //만약에 결과가 정상이라면
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            //응답 온 데이터를 요청한 클래스로 보내자.
            //print(webRequest.downloadHandler.text);
            if (info.onComplete != null)
            {
                info.onComplete(webRequest.downloadHandler);
            }

            //File.WriteAllBytes(Application.dataPath + "/image.jpg", webRequest.downloadHandler.data);
        }
        //그렇지 않다면 (Error라면)
        else
        {
            //Error의 이유를 출격
            Debug.LogError("Net Error : " + webRequest.error);
        }
    }
}
