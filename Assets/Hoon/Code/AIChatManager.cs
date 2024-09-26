using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEditor;
using Photon.Pun.Demo.SlotRacer.Utils;


public class AIChatManager : MonoBehaviour
{
    public InputField inputAIChat;
    public Text textAIChat;
    public Button btnOpenAI;
    public GameObject imgAIChat;
    // Start is called before the first frame update
    void Start()
    {
        imgAIChat.SetActive(false);
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    public bool isOpen = false;
    public void OpenAIChat()
    {
        if (isOpen)
        {
           imgAIChat.SetActive(false);
            isOpen = false;
            print("CloseAiChat");

        }
        else
        {
            imgAIChat.SetActive(true);
            isOpen = true;
            textAIChat.text = "입력을 기다리는중...";
            print("OpenAiChat");
        }
    }

    //Request body
    void chatInfo()
    {
        /*{
            "user": "string",
            "prompt": "string"
        }*/
    }

    public void PostJsonAI()
    {

        // 사용자 정보를 Dictionary로 저장
        Dictionary<string, string> aiChat = new Dictionary<string, string>
        {

            { "user", "player" },
            { "prompt",  inputAIChat.text }

        };

        // 리스트를 JSON 으로 직렬화 (배열로 감싸기)
        string newUserInfo = JsonConvert.SerializeObject(aiChat, Formatting.Indented);
        newUserInfo = "[" + newUserInfo + "]";
        print("서버에보낼신규유저정보" + newUserInfo);

        //서버에 post 요청하기
        //StartCoroutine(PostJsonAI(newUserInfo));
        print("서버에요청");
        
        StartCoroutine(PostJsonAI(newUserInfo));

    }

    IEnumerator PostJsonAI(string jsonData)
    {
       
        //보낼주소
        string url = "https://equal-seasnail-stirred.ngrok-free.app/aichat"; // 서버에서 JSON 파일을 제공하는 URL

        //요청 초기화하기
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        // JSON 데이터를 담아 요청 생성
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");


        // 요청 보내기
        yield return request.SendWebRequest();

        // 요청 결과 확인
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            // 서버 응답 확인
            string responseText = request.downloadHandler.text;
            print("서버 응답: " + responseText);

            
        }

    }

    public void PostJsonAITest()
    {
        textAIChat.text = "AI의 답변을 기다리중...";
        string name = "윤석열";
        //string question = "최고의 축구선수는 누구야?";
        string question = inputAIChat.text;


        StartCoroutine(PostJsonAI(name , question));
        print("서버에 요청시작");

        //StartCoroutine(PostStringAI(name , question));

    }

    IEnumerator PostJsonAI(string user, string prompt)
    {

        //보낼주소
        string url = "https://equal-seasnail-stirred.ngrok-free.app/aichat/"; // 서버에서 JSON 파일을 제공하는 URL
        string urlTest = "https://equal-seasnail-stirred.ngrok-free.app/aichat/?user";
        string urlTest1 = "https://equal-seasnail-stirred.ngrok-free.app/aichat/?user=example_user&prompt=Hello%20world";
        // 요청 데이터 준비 (user와 prompt를 JSON으로 직렬화)
        var postData = new
        {
            user = user,
            prompt = prompt
        };

        string jsonData = JsonConvert.SerializeObject(postData);
        //print("제이슨데이터" + jsonData);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
        //print("제이슨보내기" + jsonToSend);

        // 요청 생성
        using (UnityWebRequest request = new UnityWebRequest(urlTest1, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            //request.SetRequestHeader("Accept", "application/json");

            // 요청을 보냄
            yield return request.SendWebRequest();

            // 오류가 없는 경우 응답 처리
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("서버 응답: " + responseText);

                textAIChat.text = responseText;

                // JSON 응답을 처리 (예: 서버가 JSON 형식으로 응답을 보냈다면)
                //var responseData = JsonConvert.DeserializeObject<ResponseFormat>(responseText);
                //Debug.Log("AI 응답: " + responseData.reply);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
                textAIChat.text = "AI의 연결불가...";
                
            }
        }
    }


    IEnumerator PostStringAI(string user, string prompt)
    {
        // 보낼 주소
        string url = "https://equal-seasnail-stirred.ngrok-free.app/aichat/";
        string urlTest = "https://equal-seasnail-stirred.ngrok-free.app/aichat/?user=example_user&prompt=Hello%20world";
        // 요청 데이터 준비 (단순 문자열로 결합)
        string postData = $"user={user}&prompt={prompt}";  // 이 형식은 예시입니다. 서버 요구사항에 따라 다르게 구성해야 할 수 있습니다.

        print("보내는 데이터: " + postData);
        byte[] dataToSend = Encoding.UTF8.GetBytes(postData);

        // 요청 생성
        using (UnityWebRequest request = new UnityWebRequest(urlTest, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(dataToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "text/plain");  // 서버가 요구하는 헤더로 변경
            request.SetRequestHeader("Accept", "application/json");  // JSON 응답을 받는 경우 유지

            // 요청을 보냄
            yield return request.SendWebRequest();

            // 서버 응답 처리
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                Debug.Log("서버 응답: " + responseText);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
                Debug.LogError("서버 요청: " + request.uploadHandler);
                Debug.LogError("서버 응답: " + request.downloadHandler.text);  // 서버 응답 확인
            }
        }
    }

}
