using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Dictionary 사용을 위함
using System.Collections.Generic;
// JSON 변환을 위해 필요 (Json.NET 라이브러리)
using Newtonsoft.Json;
using UnityEngine.Networking;
using System;
//using UnityEditor.PackageManager.Requests;
using System.Text;
//파일불러오기
using System.IO;
using File = System.IO.File;
using WebSocketSharp;
using UnityEngine.XR;
using UnityEngine.Rendering.LookDev;
using static RegistInfo;
using static System.Net.WebRequestMethods;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using System.Linq;
using static MainUI;


public class MainUI : MonoBehaviour
{
    public static MainUI Instance;
    public MainUIObject mainUiObject;
    int likeStirngIdx = 0;

    

   

    //로비이동 버튼
    //public Button move_Lobby_Btn;
    //종료패널
    GameObject panel_Exit;
    
    GameObject panelMyLike_Object;

    public string idText;
    public string passText;
    public string userNameText;

    string phID_STR = "아이디(이메일)";
    string phPass_STR = "비밀번호";

    //로그출력
    string log;

    string enteredID;
    string enteredPass;
    //유저정보 불러오기
    string loadUserInfo;

    bool isRoomActive = false;
    bool isViewPass = false;
    bool isViewExitMenu = false;
    bool isViewMyInfo = false;
    bool isViewPanelLike = false;
    bool isWriteMyMemo = false;

    //로비를 제외하고 버튼은 계속 유지합니다.
    // Start is called before the first frame update

    GameObject scrollView_Culture;
    GameObject scrollView_Leiture;
    GameObject scrollView_Tech;
    GameObject scrollView_Life;
    GameObject scrollView_Health;

    GameObject text_Culture_Object;
    GameObject text_Leiture_Object;
    GameObject text_Tech_Object;
    GameObject text_Life_Object;
    GameObject text_Health_Object;

    Text culture_Text;
    Text leiture_Text;
    Text tech_Text;
    Text life_Text;
    Text health_Text;

    int likeCount = 0;
    public int loginCount = 0;

    [System.Serializable]
    public class HttpInfo
    {
        public string url = "";

        // Body 데이터
        public string body = "";

        // contentType
        public string contentType = "";

        // 통신 성공 후 호출되는 함수 담을 변수
        public Action<DownloadHandler> onComplete;
    }

    //userId 저장하는 변수
    public string saveUserId;

    //category List
    List<string> myBigCategory = new List<string>();
    List<string> mySmallCategory = new List<string>();

    //관심사 저장변수
    Text firstLikeText;
    Text secondLikeText;
    Text thirdLikeText;


    private void Awake()
    {
        //인스턴스가 없으면
        if (Instance == null)
        {
            //나를 생성
            Instance = this;
            // 오브젝트를 파괴하지 않고 유지. // 필요에 따라 추가
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            //인스턴스가 잇으면 삭제
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainUiObject = GameObject.Find("MainUIObject").GetComponent<MainUIObject>();
        //if (mainUiObject == null) print("메인오브젝트없음");

        scrollView_Culture = GameObject.Find("Scroll View_Culture");
        scrollView_Leiture = GameObject.Find("Scroll View_Leiture");
        scrollView_Tech = GameObject.Find("Scroll View_Tech");
        scrollView_Life = GameObject.Find("Scroll View_Life");
        scrollView_Health = GameObject.Find("Scroll View_Health");

        //테마오브젝트
        text_Culture_Object = GameObject.Find("Text_Culture");
        text_Leiture_Object = GameObject.Find("Text_Leisure");
        text_Tech_Object = GameObject.Find("Text_Tech ");
        text_Life_Object = GameObject.Find("Text_Life");
        text_Health_Object = GameObject.Find("Text_Health");

        //테마텍스트
        culture_Text = text_Culture_Object.GetComponent<Text>();
        leiture_Text = text_Leiture_Object.GetComponent<Text>();
        tech_Text = text_Tech_Object.GetComponent<Text>();
        life_Text = text_Life_Object.GetComponent<Text>();
        health_Text = text_Health_Object.GetComponent<Text>();

        //스크롤뷰
        scrollView_Culture.SetActive(false);
        scrollView_Leiture.SetActive(false);
        scrollView_Tech.SetActive(false);
        scrollView_Life.SetActive(false);
        scrollView_Health.SetActive(false);

        //종료패널
        panel_Exit = GameObject.Find("Panel_Exit");
        panel_Exit.SetActive(false);

       

        //관심사
        panelMyLike_Object = GameObject.Find("Panel_MyLike");
        panelMyLike_Object.SetActive(false);

        //OFFIMG(); // 이미지 오브젝트 끄기


    }

    void Update()
    {
       
    }

    public class Mymemo
    {
        public string userId;
        public string MyMemo;
    }

    
    public void SaveJsonMyMemo()
    {
        //메모인풋켜기
        if(isWriteMyMemo == false)
        {
            mainUiObject.write_MyMemo_Object.SetActive(true);
            mainUiObject.writeMomo_Input.text = mainUiObject.myMemo_TextComp.text;
            isWriteMyMemo = true;
        }
        //메모인풋끄기
        else
        {
            mainUiObject.write_MyMemo_Object.SetActive(false);
            //텍스트를 갱신
            mainUiObject.myMemo_TextComp.text = mainUiObject.writeMomo_Input.text;
            //결과를 저장하자.
            string path = Application.dataPath + "/Resources/MyMeMo.json";
            string loadUserInfo = System.IO.File.ReadAllText(path);
            //정보를 Json으로 불러오기
            List<Dictionary<string, string>> userInfoList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(loadUserInfo);

            //파일에 내용 없으면
            if (loadUserInfo == "")
            {
                print("파일에 내용 없음");

                // 파일에내용 없으면 신규작성
                //newUserInfoList  Dictionary 만들기
                List<Dictionary<string, string>> newUserInfoList = new List<Dictionary<string, string>>();
                //newUserInfoList에 저장할 Dictionary 초기화
                Dictionary<string, string> newUserInfo = new Dictionary<string, string>
                {
                     //idText = userID 값
                    { "userId", idText },
                     //MyMemo 는  mainUiObject.writeMomo_Input.text 값
                    { "MyMemo", mainUiObject.writeMomo_Input.text }
                };
                //newUserInfoList 에 Dictionary 추가
                newUserInfoList.Add(newUserInfo);

                // JSON으로 변환하여 파일에 저장
                string saveJson = JsonConvert.SerializeObject(newUserInfoList, Formatting.Indented);
                System.IO.File.WriteAllText(path, saveJson);
                print("신규 저장 완료: " + saveJson);

            }
            //파일내용 있으면
            else
            {
                print("파일에 내용 있음" + loadUserInfo);


                // 유저 정보가 있는지 확인
                bool isUserFound = false;
                
                //리스트 순회
                foreach (var userInfo in userInfoList)
                {
                    // 리스트에 일치하는지 값이 잇는지 확인하기
                    if ((userInfo["userId"] == idText))
                    {
                        //일치하는 값이 있다면 mymemo value 값을 갱신
                        userInfo["MyMemo"] = mainUiObject.writeMomo_Input.text;
                        //userInfoList.Add(userInfo);

                        string saveJson = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);
                        //saveJson = loadUserInfo + saveJson;
                        System.IO.File.WriteAllText(path, saveJson);
                        print("신규 유저정보 업데이트: " + saveJson);

                        //유저찾음
                        isUserFound = true;
                        //나가기
                        break;


                    }


                }
                //일치하는 유저가 없다면
                if(!isUserFound)
                {
                    print("일치하는유저없음");

                    //신규저장
                    Dictionary<string, string> newUserInfo = new Dictionary<string, string>
                    {
                     //idText = userID 값
                        { "userId", idText },
                     //MyMemo 는  mainUiObject.writeMomo_Input.text 값
                        { "MyMemo", mainUiObject.writeMomo_Input.text }
                    };
                    //기존 리스트에 신규정보 추가
                    userInfoList.Add(newUserInfo);

                    // JSON으로 변환하여 파일에 저장
                    string saveJson = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);
                    //기존 string 에 신규유저 string을 더하고 저장
                    //saveJson = loadUserInfo + saveJson;
                    System.IO.File.WriteAllText(path, saveJson);
                    print("신규 유저정보 업데이트: " + saveJson);


                }
            
            
            }

            //인풋박스 끄기
            isWriteMyMemo = false;

        }
       
    }




    //Http 통신 서버에 Get 요청
    public void GetJSONUserInfo()
    {
        
        print("Get 로그인 버튼");
        
        if(mainUiObject == null)
        {
            mainUiObject = GameObject.Find("MainUIObject").GetComponent<MainUIObject>();
        }
        // 입력된 아이디와 패스워드 가져오기
        idText = mainUiObject.id_InputField.text;
        passText = mainUiObject.pass_InputField.text;

        // 코루틴을 사용하여 서버로부터 데이터를 가져옴
        StartCoroutine(CheckLoginFromServer(idText, passText));
    }

    
  
    // JSON 데이터를 담을 클래스
    public class User
    {
        public int id;
        public string userId;
        public string userPassword;
        public string userNickname;
        public string birthday;
        public string gender;
        public string[] interestList;
    }
   //
    private IEnumerator CheckLoginFromServer(string idText, string passText)
    {
        // 서버에서 JSON 데이터를 가져오기 위한 URL
        string urlGetTest = "http://192.168.0.76:8080/user?userId="+ idText; //같은아이피일때
        string urlGetUser = "http://125.132.216.190:15530/user?userId=" + idText;

        print("Get userInfo" +  "id" + idText + "pass" + passText);
        //Get 서버요청
        UnityWebRequest request = UnityWebRequest.Get(urlGetUser);

        //콜백이 올때까지 기다린다.
        yield return request.SendWebRequest();

        //요청 결과 확인
        //연결오류, 프로토콜 오류 발생시
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            print("서버 연결 오류: " + request.error);
        }
        //문제가 없다면
        else
        {
            print("서버 연결 성공");

            // 서버로부터 받은 응답 데이터를 문자열로 변환
            string strResponse = request.downloadHandler.text;
           
            // 서버에서 받은 JSON 데이터 출력
            print("Get 응답 데이터: " + strResponse);

            if (strResponse.Contains(idText))
            {
                if(idText.IsNullOrEmpty())
                {
                    print("idText 비었음" + idText);
                    print("로그인 실패");
                    print("아이디가 틀림");
                    mainUiObject.id_InputField.text = "";
                    mainUiObject.phID_Text.text = "아이디(필수)";
                    mainUiObject.phID_Text.color = Color.red;

                    print("비밀번호가 틀림");
                    mainUiObject.pass_InputField.text = "";
                    mainUiObject.phPass_Text.text = "비밀번호(필수)";
                    mainUiObject.phPass_Text.color = Color.red;

                }
                else
                {
                    // JSON 데이터를 C# 객체로 변환
                    User user = JsonConvert.DeserializeObject<User>(strResponse);
                    //print("제이슨 -> 구조체" + user);

                    //이름변수에 이름을 저장
                    userNameText = user.userNickname;
                    print("내이름" + userNameText);
                    print(" mainUiObject.nameText.text" + mainUiObject.nameTextComp.text);

                    //MyInfo UserName을 갱신
                    mainUiObject.nameTextComp.text = userNameText;

                    saveUserId = idText;
                    print("내아이디" + saveUserId);

                    StartCoroutine(ServerGetLike());

                    // 로그인 성공
                    Login();
                    print("로그인 성공");
                }
                
            }
            else
            {
                print("로그인 실패");
                print("아이디가 틀림");
                mainUiObject.id_InputField.text = "";
                mainUiObject.phID_Text.text = "아이디가 틀림";
                mainUiObject.phID_Text.color = Color.red;

                print("비밀번호가 틀림");
                mainUiObject.pass_InputField.text = "";
                mainUiObject.phPass_Text.text = "비밀번호 틀림";
                mainUiObject.phPass_Text.color = Color.red;

            }
        }

    }// 서버 코루틴 끝
    // Get Like 
    public IEnumerator ServerGetLike()
    {
        string type = "/interest-v2?userId=";
        string url = "http://125.132.216.190:15530" + type + idText;

        UnityWebRequest request = UnityWebRequest.Get(url);

        //콜백이 올때까지 기다린다.
        yield return request.SendWebRequest();

        // 요청 결과 확인
        //연결오류, 프로토콜 오류 발생시
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            print("서버 연결 오류: " + request.error);
        }
        //문제가 없다면
        else
        {
            print("Get Server Like 성공");

            // 서버로부터 받은 응답 데이터를 문자열로 변환
            string strResponse = request.downloadHandler.text;
            // 서버에서 받은 JSON 데이터 출력
            print("Get Like 서버 응답 데이터: " + strResponse);

            //----------------------------------------------------------------------------------------
            //post가 쌓이는 문제로 patch로 변경해서 해결해야함.
            // JSON 문자열을 파싱하여 ResponseData 배열로 변환
            //PostUserLike[] responseDataArray = JsonUtility.FromJson<PostUserLike[]>(strResponse);

            // 배열의 첫 번째 요소에서 smallCategory 값을 가져와 firstLikeText에 설정
            /* if (responseDataArray.Length > 0)
             {
                 Text firstLikeText = mainUiObject.firstLikeObject.GetComponent<Text>();
                 Text secondLikeText = mainUiObject.secondLikeObject.GetComponent<Text>();
                 Text thirdLikeText = mainUiObject.thirdLikeObject.GetComponent<Text>();

                 firstLikeText.text = responseDataArray[0].smallCategory;
                 secondLikeText.text = responseDataArray[0].smallCategory2;
                 thirdLikeText.text = responseDataArray[0].smallCategory3;
             }*/
            //----------------------------------------------------------------------

            //로컬데이터에 저정된 값으로 세팅해보자.
            string path = Application.dataPath + "/Resources/SaveRegist.json";
            string loadUserInfo = System.IO.File.ReadAllText(path);
            print("loadUserInfo" + loadUserInfo);

            //정보를 Json으로 불러오기
            List<Dictionary<string, string>> userInfoList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(loadUserInfo);
            print("userInfoList 크기" + userInfoList.Count);
            print("userInfoList 값" + userInfoList);


            //리스트 순회
            foreach (var userInfo in userInfoList)
            {


                // 리스트에 일치하는지 값이 잇는지 확인하기
                if ((userInfo["userId"] == idText))
                {
                    print("아이디 찾았다" + idText);

                    //관심사 텍스트 캐싱
                    Text firstLikeText = mainUiObject.firstLikeObject.GetComponent<Text>();
                    Text secondLikeText = mainUiObject.secondLikeObject.GetComponent<Text>();
                    Text thirdLikeText = mainUiObject.thirdLikeObject.GetComponent<Text>();
                    print("관심사 텍스트 캐싱");

                    //print("smallCategory" + userInfo["smallCategory"]);

                    //strResponse 에서 smallCategory, smallCategory2, smallCategory3 만 가져오기
                   
                    if (userInfo.ContainsKey("smallCategory"))
                    {    //firstLikeText.text = smallCategory; 갱신
                        firstLikeText.text = userInfo["smallCategory"];
                    }
                    else
                    {
                        firstLikeText.text = "미지정";
                    }
                    if (userInfo.ContainsKey("smallCategory2"))
                    {     //secondLikeText.text = smallCategory2; 갱신
                          secondLikeText.text = userInfo["smallCategory2"];
                    }
                    if (userInfo.ContainsKey("smallCategory3"))
                    {
                        //thirdLikeText.text = smallCategory3; 갱신
                        thirdLikeText.text = userInfo["smallCategory3"];
                    }




                    break;

                }

            }



          
            
           
            


        }
    }
    //
    string likeObjectName;
    string likeObjectNameText;
    public void OnLikeText(GameObject likeTextObject)
    {
        likeObjectName = likeTextObject.name;
        likeObjectNameText = likeTextObject.GetComponent<Text>().text;
        //print("OnLikeText" + likeObjectNameText);
        //선택한 이름과, 테마이름을 저장.
        OnLikeChoice(likeObjectName, themeTextChoice);
    }
    //관심사를 선택하기
    void OnLikeChoice(string objectName, string themeName)
    {
        string likeText;
        GameObject textTheater_Object = GameObject.Find(objectName);
        Text textTheater_Text = textTheater_Object.GetComponent<Text>();
        likeText = textTheater_Text.text;

        //텍스트 저장
        SaveLikeText(likeText);

        if (likeCount == 0)
        {

            GameObject btnFirst_Like_Object = GameObject.Find("Text_First_Like");
            Text btnt_Like_Text = btnFirst_Like_Object.GetComponent<Text>();

            //myBigCategory에 값이 저장되어 있는지 확인.
            if (myBigCategory.Count == 1)
            {
                btnt_Like_Text.text = myBigCategory[0] + likeText;
            }
            else if (myBigCategory.Count == 2)
            {
                btnt_Like_Text.text = myBigCategory[1] + likeText;
            }
            else if (myBigCategory.Count == 3)
            {
                btnt_Like_Text.text = myBigCategory[2] + likeText;
            }

            likeCount++;
        }
        else if (likeCount == 1)
        {

            GameObject btnFirst_Like_Object = GameObject.Find("Text_Second_Like");
            Text btnt_Like_Text = btnFirst_Like_Object.GetComponent<Text>();
            if (myBigCategory.Count == 1)
            {
                btnt_Like_Text.text = myBigCategory[0] + likeText;
            }
            else if (myBigCategory.Count == 2)
            {
                btnt_Like_Text.text = myBigCategory[1] + likeText;
            }
            else if (myBigCategory.Count == 3)
            {
                btnt_Like_Text.text = myBigCategory[2] + likeText;
            }

            likeCount++;
        }
        else
        {

            GameObject btnFirst_Like_Object = GameObject.Find("Text_Third_Like");
            Text btnt_Like_Text = btnFirst_Like_Object.GetComponent<Text>();
            if (myBigCategory.Count == 1)
            {
                btnt_Like_Text.text = myBigCategory[0] + likeText;
            }
            else if (myBigCategory.Count == 2)
            {
                btnt_Like_Text.text = myBigCategory[1] + likeText;
            }
            else if (myBigCategory.Count == 3)
            {
                btnt_Like_Text.text = myBigCategory[2] + likeText;
            }

            likeCount = 0;
        }


    }

    //관심사 저장하기
    public void SaveLikeText(string likeText)
    {
        //이전텍스트
        string beforeText;
        //파일이름. 확장자 제외
        string fileName = "TestSaceLike";
        // Resources 폴더에서 파일 불러오기
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        // 저장할 파일 경로
        // Application.dataPath = Asset/Resources + 파일이름 + 확장자
        string path = Application.dataPath + "/Resources/" + fileName + ".txt";


        //파일이 없으면 
        if (textAsset == null)
        {
            //생성하기
            System.IO.File.CreateText(path);
            print("텍스트가 생성하기");

        }
        //파일있으면 
        else
        {
            List<string> newString = new List<string>();

            // 기존 텍스트를 줄별로 나누어 배열로 가져오기
            string[] beforeStringLines = System.IO.File.ReadAllLines(path);

            // 배열의 각 줄을 리스트에 추가
            newString.AddRange(beforeStringLines);
            //print("newString 개수" + newString.Count);

            //카운트가 3보다 작으면 텍스트를 추가
            if (newString.Count < 3)
            {
                newString.Add(likeText);

            }
            //카운트가 3이되면 idx를 초기화 idx 에 추가
            else if (newString.Count == 3)
            {
                //print("카운트 3");
                if (likeStirngIdx == 0)
                {
                    newString[likeStirngIdx] = likeText;
                    likeStirngIdx++;
                }
                //idx 2보다 작으면
                else if (likeStirngIdx == 1)
                {

                    //idx에 추가
                    newString[likeStirngIdx] = likeText;
                    likeStirngIdx++;
                }
                else if (likeStirngIdx == 2)
                {

                    //idx에 추가
                    newString[likeStirngIdx] = likeText;
                    likeStirngIdx = 0;
                }


            }

            // 리스트를 파일에 다시 쓰기 (줄별로 합쳐서 작성)
            System.IO.File.WriteAllLines(path, newString);

            // 새로 추가할 텍스트도 리스트에 추가
            //newString.Add(likeText);

            // 리스트를 파일에 다시 쓰기 (줄별로 합쳐서 작성)
            //File.WriteAllLines(path, newString);

            //File.WriteAllText(path, likeText);
            //print("신규 라이크 저장" + likeText);

        }

    }

    string[] themeNameArray;
    string[] objectNameTextArray;
    // 관심사를 Json으로 저장하기
    public void OnLikeTextJson()
    {
        print("관심사저장버튼");
        Text firstLikeText = mainUiObject.firstLikeObject.GetComponent<Text>();
        Text secondLikeText = mainUiObject.secondLikeObject.GetComponent<Text>();
        Text thirdLikeText = mainUiObject.thirdLikeObject.GetComponent<Text>();
        if (mySmallCategory.Count != 0)
        {

            mySmallCategory[0] = firstLikeText.text;
            //print("첫번째관심사" + mySmallCategory[0]);
            mySmallCategory[1] = secondLikeText.text;
            //print("두번째관심사" + secondLikeText.text);
            mySmallCategory[2] = thirdLikeText.text;
            //print("세번째관심사" + thirdLikeText.text);
            print("관심사 저장");
        }
        else
        {
            mySmallCategory.Add(firstLikeText.text);
            mySmallCategory.Add(secondLikeText.text);
            mySmallCategory.Add(thirdLikeText.text);

            print("관심사배열크기" + mySmallCategory.Count);
            
           
        }

        //배열초기화
        themeNameArray = new string[mySmallCategory.Count];
        objectNameTextArray = new string[mySmallCategory.Count];

        for (int i = 0; i < mySmallCategory.Count; i++)
        {
            // 문자열을 '>' 기준으로 분리
            string[] splitResult = mySmallCategory[i].Split(':');

            // 분리된 문자열을 각각 themeName과 objectNameText에 할당
            if (splitResult.Length == 2)
            {
                themeNameArray[i] = splitResult[0];       // "문화"
                objectNameTextArray[i] = splitResult[1];  // "음악"

                //출력
                //print("Theme Name: " + themeNameArray[i]);
                //print("Object Name Text: " + objectNameTextArray[i]);
            }

        }
        //local에 저장
        SaveLocalLikeTextJson(themeNameArray, objectNameTextArray);

        //선택한 이름과, 테마이름을 저장.
        //OnLikeChoiceJson(likeObjectName, themeName, objectNameText);
        //print("OnLikeTextJson" + themeTextChoice);

    }

    /*Request body()
    {
      "userId": "string",
      "likeCount": "string",
      "bigCategory": "string",
      "smallCategory": "string",
      "bigCategory2": "string",
      "smallCategory2": "string",
      "bigCategory3": "string",
      "smallCategory3": "string"
    }*/

    //유저데이터 클래스
    [System.Serializable]
    public class UserData
    {
        public string userId;
        public string userPassword;
        public string userNickName;
        public int likeCount;
        public string bigCategory;
        public string smallCategory;
        public string bigCategory2;
        public string smallCategory2;
        public string bigCategory3;
        public string smallCategory3;
    }

   
    //관심사, Json, 로컬저장  
    void SaveLocalLikeTextJson(string[] themeNames, string[] objectNameTexts)
    {
        //파일 경로를 불러옵니다.
        string path = Application.dataPath + "/Resources/SaveRegist.json";
        //파일이 있으면
        if (System.IO.File.Exists(path))
        {
            // 파일의 모든 텍스트를 가져온다 (JSON 파일).
            loadUserInfo = System.IO.File.ReadAllText(path);

            //파일을 잘 가져왔다면
            if (loadUserInfo != null)
            {
                print("JSON 파일 읽기 완료" + loadUserInfo);
                // UserData를 List로 만들고 JSON 데이터를 리스트로 파싱
                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(loadUserInfo);
                print("JSON 파싱" + users);

                //유저아이디확인
                string userId = saveUserId;
                print("유저 아이디 확인" + saveUserId);

                // ID와 일치하는 부분 찾기
                UserData matchingUser = users.Find(user => user.userId == saveUserId);

                //ID 가 포함되어 있다면
                if (matchingUser != null)
                {

                    //라이크 카운트가 없으면 신규추가
                    if (matchingUser.likeCount == 0)
                    {

                        // bigCategory에 themeText, smallCategory에 likeText 설정
                        matchingUser.bigCategory = themeNames[0];
                        matchingUser.smallCategory = objectNameTexts[0];
                        matchingUser.likeCount++;

                        // JSON으로 신규저장
                        string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                        System.IO.File.WriteAllText(path, updatedJson);
                        print("JSON 파일 저장 완료 0: " + updatedJson);

                    }
                    //라이크 카운트가 0이 아니면 기존정보에 추가
                    if (matchingUser.likeCount == 1)
                    {
                        // bigCategory에 themeText, smallCategory에 likeText 설정
                        matchingUser.bigCategory2 = themeNames[1];
                        matchingUser.smallCategory2 = objectNameTexts[1];
                        matchingUser.likeCount++;

                        // JSON으로 신규저장
                        string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                        System.IO.File.WriteAllText(path, updatedJson);
                        print("JSON 파일 저장 완료 1: " + updatedJson);

                    }
                    //라이크 카운트가 0이 아니면 기존정보에 추가
                    if (matchingUser.likeCount == 2)
                    {
                        // bigCategory에 themeText, smallCategory에 likeText 설정
                        matchingUser.bigCategory3 = themeNames[2];
                        matchingUser.smallCategory3 = objectNameTexts[2];
                        matchingUser.likeCount = 0;

                        // JSON으로 신규저장
                        string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                        System.IO.File.WriteAllText(path, updatedJson);
                        print("JSON 파일 저장 완료 2: " + updatedJson);

                    }

                    //서버에 Post하기
                    SaveSeverLikeJsonTest();
                    print("서버에 Post");

                }
                else
                {
                    print("userId 불일치");
                }
            }
            else
            {
                print("파일없음");
            }

        }
    }

    //포스트할 데이터타입
    [System.Serializable]
    public class PostUserLike
    {
        public string userId;
        public int likeCount;
        public string bigCategory;
        public string smallCategory;
        public string bigCategory2;
        public string smallCategory2;
        public string bigCategory3;
        public string smallCategory3;
    }
    //Post Like 하기
    public void SaveSeverLikeJsonTest()
    {

        //파일 경로를 불러옵니다.
        string path = Application.dataPath + "/Resources/SaveRegist.json";

        // 파일이 있으면
        if (System.IO.File.Exists(path))
        {
            // 파일의 모든 텍스트를 가져온다 (JSON 파일).
            loadUserInfo = System.IO.File.ReadAllText(path);

            // 파일을 잘 가져왔다면
            if (loadUserInfo != null)
            {
                print("JSON 파일 읽기 완료: " + loadUserInfo);

                // UserData를 List로 만들고 JSON 데이터를 리스트로 파싱
                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(loadUserInfo);
                //print("JSON 파싱 완료: " + users);

                // 유저아이디확인
                string userStr = saveUserId;

                // ID와 일치하는 부분 찾기
                UserData matchingUser = users.Find(user => user.userId == saveUserId);

                // ID가 포함되어 있다면
                if (matchingUser != null)
                {
                    // PostUserLike 데이터 구조로 변환
                    PostUserLike postUserLike = new PostUserLike
                    {
                        userId = matchingUser.userId,
                        likeCount = matchingUser.likeCount,
                        bigCategory = matchingUser.bigCategory,
                        smallCategory = matchingUser.smallCategory,
                        bigCategory2 = matchingUser.bigCategory2,
                        smallCategory2 = matchingUser.smallCategory2,
                        bigCategory3 = matchingUser.bigCategory3,
                        smallCategory3 = matchingUser.smallCategory3
                    };

                    // 서버에 Post 요청
                    StartCoroutine(PostLikeTextToServer(postUserLike));
                }
                else
                {
                    print("userId 불일치");
                }
            }
            else
            {
                print("파일 없음");
            }
        }
    }
    // 서버에 사용자 관심사 정보 POST하기
    IEnumerator PostLikeTextToServer(PostUserLike postUserLike)
    {
        string type = "/interest-v2";
        string url = "http://125.132.216.190:15530" + type; // 실제 서버의 API URL로 변경

        // JSON으로 직렬화
        string jsonData = JsonConvert.SerializeObject(postUserLike);
        print("서버로 보낼 데이터: " + jsonData);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        // POST 요청 생성
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // 요청 전송
            yield return request.SendWebRequest();

            // 서버 응답 처리
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("서버 응답 성공: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("서버 응답 실패: " + request.error);
            }
        }



    }

    //관심사 Post 하기
    IEnumerator SaveSeverLikeTextJson()
    {
        string test = "";
        //v2로 관심사 post하기
        string urlLike = "http://125.132.216.190:5544/interest-v2";
        UnityWebRequest request = new UnityWebRequest(urlLike, "POST");
        print("URL확인중");

        // JSON으로 직렬화
        string jsonData = JsonConvert.SerializeObject(test);
        print("서버로 보낼 데이터: " + jsonData);
        byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);

        // JSON 데이터를 담아 요청 생성
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // 요청 보내기
        yield return request.SendWebRequest();
        print("요청받기");

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

            // 서버 응답과 newUser가 같은지 확인
            if (responseText == jsonData)
            {
                Debug.Log("서버 응답과 신규 유저 정보가 일치합니다.");
            }
            else
            {
                Debug.LogWarning("서버 응답과 신규 유저 정보가 일치하지 않습니다.");
            }
        }

    }

    //버튼 누르면 호출하고  SaveLikeTextJson로 이동
    void OnLikeChoiceJson(string objectName, string themeName, string objectNameText)
    {

        string likeText;
        GameObject textTheater_Object = GameObject.Find(objectName);
        if (textTheater_Object != null)
        {
            Text textTheater_Text = textTheater_Object.GetComponent<Text>();
            likeText = textTheater_Text.text;

            //텍스트 저장
            SaveLikeTextJson(objectNameText, themeName);
            //print("OnLikeChoiceJson" + themeName);
            //print("OnLikeChoiceJson" + objectNameText);
        }
        else
        {
            print("저장안됨");
            return;
        }


    }

    //OnLikeChoiceJson 을 저장하고 종료
    public void SaveLikeTextJson(string likeText, string themeText)
    {
        //파일 경로를 불러옵니다.
        string path = Application.dataPath + "/Resources/SaveRegist.json";
        //파일이 있으면
        if (System.IO.File.Exists(path))
        {
            // 파일의 모든 텍스트를 가져온다 (JSON 파일).
            loadUserInfo = System.IO.File.ReadAllText(path);

            //파일을 잘 가져왔다면
            if (loadUserInfo != null)
            {
                print("JSON 파일 읽기 완료" + loadUserInfo);
                // UserData를 List로 만들고 JSON 데이터를 리스트로 파싱
                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(loadUserInfo);

                //유저아이디확인
                string userStr = saveUserId;
                //print("유저 아이디 확인" + userStr);

                // ID와 일치하는 부분 찾기
                UserData matchingUser = users.Find(user => user.userId == saveUserId);

                //ID 가 포함되어 있다면
                if (matchingUser != null)
                {

                    //라이크 카운트가 없으면 신규추가
                    if (matchingUser.likeCount == 0)
                    {

                        // bigCategory에 themeText, smallCategory에 likeText 설정
                        matchingUser.bigCategory = themeText;
                        matchingUser.smallCategory = likeText;
                        matchingUser.likeCount++;

                        // JSON으로 신규저장
                        string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                        System.IO.File.WriteAllText(path, updatedJson);
                        print("JSON 파일 저장 완료: " + updatedJson);
                    }
                    //라이크 카운트가 0이 아니면 기존정보에 추가
                    else if (matchingUser.likeCount == 1)
                    {
                        // bigCategory에 themeText, smallCategory에 likeText 설정
                        matchingUser.bigCategory2 = themeText;
                        matchingUser.smallCategory2 = likeText;
                        matchingUser.likeCount++;

                        // JSON으로 신규저장
                        string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                        System.IO.File.WriteAllText(path, updatedJson);
                        print("JSON 파일 저장 완료: " + updatedJson);

                    }
                    //라이크 카운트가 0이 아니면 기존정보에 추가
                    else if (matchingUser.likeCount == 2)
                    {
                        // bigCategory에 themeText, smallCategory에 likeText 설정
                        matchingUser.bigCategory3 = themeText;
                        matchingUser.smallCategory3 = likeText;
                        matchingUser.likeCount++;

                        // JSON으로 신규저장
                        string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                        System.IO.File.WriteAllText(path, updatedJson);
                        print("JSON 파일 저장 완료: " + updatedJson);

                    }
                    //라이크 카운트가 0이 아니면 기존정보에 추가
                    else if (matchingUser.likeCount == 3)
                    {
                        // bigCategory에 themeText, smallCategory에 likeText 설정
                        matchingUser.bigCategory = themeText;
                        matchingUser.smallCategory = likeText;
                        matchingUser.likeCount = 0;

                        // JSON으로 신규저장
                        string updatedJson = JsonConvert.SerializeObject(users, Formatting.Indented);
                        System.IO.File.WriteAllText(path, updatedJson);
                        print("JSON 파일 저장 완료: " + updatedJson);

                    }


                }
                else
                {
                    print("userId 불일치");
                }
            }
            else
            {
                print("파일없음");
            }

        }



    }
    //id가 일치하는 부분만 가져오자.
    //가져온 부분에 bigCategory 에 themeText 를 smallCategory likeText 를 넣습니다.
    //다시 저장합니다.



   /* string likeText;
    public void OnLikeTheme(GameObject likeThemeText)
    {

        likeText = likeThemeText.name;
        print("선택된테마" + likeText);
    }*/

    //테마가 여기에 저장됩니다.
    string themeTextChoice;
    int bigChategoryCount = 0;
    //테마 선택하기
    public void OnLikeTheme(int val)
    {

        GameObject[] go = { scrollView_Culture, scrollView_Leiture, scrollView_Tech, scrollView_Life, scrollView_Health };
        //print("문화" + culture_Text.text);
        Text[] themeTexts = { culture_Text, leiture_Text, tech_Text, life_Text, health_Text };

        for (int i = 0; i < go.Length; i++)
        {
            if (i == val)
            {
                go[val].SetActive(true);
                //테마를 문자로 저장하자.
                themeTextChoice = themeTexts[val].text;
                //print("선택된테마" + themeTextChoice);

            }
            else
            {
                go[i].SetActive(false);
            }

        }

        //카운트가 0이면 새로추가
        if (myBigCategory.Count == 0)
        {
            myBigCategory.Add(themeTextChoice + ":");
            //0번 내용을 출력
            //print("나의빅카테고리0" + myBigCategory[0]);
            bigChategoryCount++;

        }
        //이미 값이 있으면 덮어쓰기
        else
        {
            myBigCategory[0] = themeTextChoice + ":";
            //print("나의빅카테고리0" + myBigCategory[0]);
        }
     
    }


    //로그인, 회원정보 이미지를 끕니다.

    /*public void LoadJSONTest()
    {

        print("로그인 버튼");
        //입력된 아이디 가져오기
        string idText = mainUiObject.id_InputField.text;
        //입력된 패스워드 가져오기
        string passText = mainUiObject.pass_InputField.text;
        // 파일명과 경로 설정 (JSON 파일)
        string fileName = "SaveRegist";
        string path = Application.dataPath + "/Resources/" + fileName + ".json";

        // 파일이 존재하는지 확인
        if (System.IO.File.Exists(path))
        {
            // 파일의 모든 텍스트를 가져온다 (JSON 파일).
            loadUserInfo = System.IO.File.ReadAllText(path);
            print("JSON 파일 읽기 완료");

            // JSON 파일을 Dictionary 리스트로 변환
            List<Dictionary<string, string>> userInfoList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(loadUserInfo);

            // 유저 정보가 있는지 확인
            bool isUserFound = false;
            foreach (var userInfo in userInfoList)
            {

                // 일치하는지 값이 잇는지 확인하기
                if ((userInfo["ID"] == idText && userInfo["Password"] == passText))
                {
                    // 로그인 성공
                    Login();
                    print("로그인 성공");
                    isUserFound = true;
                    break;
                }


            }
            //유저가 없으면
            if (!isUserFound)
            {
                // 아이디 또는 비밀번호 불일치
                print("불일치");
                bool isIdFound = false;

                // ID가 있는지 확인
                foreach (var userInfo in userInfoList)
                {
                    if (userInfo["ID"] == idText)
                    {
                        isIdFound = true;
                        break;
                    }
                }
                //아이디 없으면
                if (!isIdFound)
                {
                    print("아이디가 틀림");
                    mainUiObject.id_InputField.text = "";
                    mainUiObject.phID_Text.text = "아이디가 틀림";
                    mainUiObject.phID_Text.color = Color.red;
                }
                else
                {
                    print("비밀번호가 틀림");
                    mainUiObject.pass_InputField.text = "";
                    mainUiObject.phPass_Text.text = "비밀번호 틀림";
                    mainUiObject.phPass_Text.color = Color.red;
                }
            }
        }
        else
        {
            // 파일이 없으면 새로 생성 (빈 파일)
            System.IO.File.Create(path).Dispose();
            print("JSON 파일 생성됨");
        }

    }*/
    //파일로드 테스트 
    /*public void LoadTest()
    {
        //입력된 아이디 가져오기
        string idText = mainUiObject.id_InputField.text;
        //입력된 패스워드 가져오기
        string passText = mainUiObject.pass_InputField.text;

        //문자열로 저장할경로 + 파일이름.
        //C:\Users\Admin\AppData\LocalLow\DefaultCompany\front-end
        string path = Application.persistentDataPath + "/SaveRegist.txt";
        //파일에 저장될 모양과 값.
        string content = "ID" + ":" + idText + "," + "Password" + ":" + passText + "\n";

        // 파일이 존재하는지, 그리고 동일한 내용이 있는지 확인
        if (System.IO.File.Exists(path))
        {
            //pah의 모든 택스트를 가져오자.
            loadUserInfo = System.IO.File.ReadAllText(path);

            //content가 포함되어 있다면
            if (loadUserInfo.Contains(content))
            {
                *//* id_Regist_InputField.text = "";
                 ph_Regist_ID_Text.text = "아이디가중복됩니다";
                 ph_Regist_ID_Text.color = Color.red;
                 return;*//*

            }

            //갱신
            //loadUserInfo = loadUserInfo + content;


        }

        //using System.IO; 인클루드 해줘야함. //파일을 텍스트에 저장해주자.
        //File.WriteAllText(path, loadUserInfo);
        print("LoadComplite");

    }*/
    //로컬 로그인테스트 JSON
    public void TestLocalLoginJson()
    {
        //null이라 다시 캐싱
        if (mainUiObject == null)
        {
            mainUiObject = GameObject.Find("MainUIObject").GetComponent<MainUIObject>();
        }
        
        print("로컬 로그인 버튼 클릭");
        print(" mainUiObject.id_InputField.text" + mainUiObject.id_InputField.text);
        //입력된 아이디 가져오기
        idText = mainUiObject.id_InputField.text;
        //입력된 패스워드 가져오기
        passText = mainUiObject.pass_InputField.text;

        // 파일명과 경로 설정 (JSON 파일)
        string path = Application.dataPath + "/Resources/SaveRegist.json";

        // 파일이 존재하는지, 그리고 동일한 내용이 있는지 확인
        if (System.IO.File.Exists(path))
        {
            //pah의 모든 택스트를 가져오자.
            loadUserInfo = System.IO.File.ReadAllText(path);
            print("JSON 파일 읽기 완료" + loadUserInfo);

            // JSON 파일을 Dictionary 리스트로 변환
            List<Dictionary<string, string>> userInfoList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(loadUserInfo);
            //print("유저정보량" + userInfoList.Count);

            // 유저 정보가 있는지 확인하는 변수
            bool isUserFound = false;
            
            //리스트파일을 순차검사
            foreach (var userInfo in userInfoList)
            {
                
                //아이디와 패스워드가 일치하는지 확인
                if (userInfo["userId"] == idText && userInfo["userPassword"] == passText)
                {
                    //정보표시해주기
                    print("id, pass 일치");
                    //userId 저장하기
                    saveUserId = idText;
                    
                    //이름변수에 이름을 저장
                    userNameText = userInfo["userNickName"];
                    print("내이름" + userNameText);
                    //MyInfo UserName을 갱신
                    mainUiObject.nameTextComp.text = userNameText;

                    //smallCategory 를 포함하는 경우에만
                    if (userInfo.ContainsKey("smallCategory"))
                    {
                        //small Category가져오기
                        if (userInfo["smallCategory"] != null)
                        {
                            string smallCategory = userInfo["smallCategory"];
                            //print("smallCategory" + smallCategory);
                            mySmallCategory.Add(smallCategory);
                        }
                        if (userInfo["smallCategory2"] != null)
                        {
                            string smallCategory = userInfo["smallCategory2"];
                            //print("smallCategory2" + smallCategory);
                            mySmallCategory.Add(smallCategory);
                        }
                        if (userInfo["smallCategory3"] != null)
                        {
                            string smallCategory = userInfo["smallCategory3"];
                            //print("smallCategory3" + smallCategory);
                            mySmallCategory.Add(smallCategory);
                        }

                        Text firstLikeText = mainUiObject.firstLikeObject.GetComponent<Text>();
                        if (mySmallCategory.Count != 0)
                        {
                            firstLikeText.text = mySmallCategory[0];
                            //print("첫번째관심사" + firstLikeText.text);

                            Text secondLikeText = mainUiObject.secondLikeObject.GetComponent<Text>();
                            secondLikeText.text = mySmallCategory[1];
                            //print("두번째관심사" + secondLikeText.text);

                            Text thirdLikeText = mainUiObject.thirdLikeObject.GetComponent<Text>();
                            thirdLikeText.text = mySmallCategory[2];
                            //print("세번째관심사" + thirdLikeText.text);

                        }

                    }

                    //유저찾음
                    isUserFound = true;
                    //로그인처리하기
                    Login();
                    print("로그인 완료");
                    //루틴 나가기
                    break;

                }
                //유저가 없으면
                else if (!isUserFound)
                {
                    //print("아이디가 틀림");
                    mainUiObject.id_InputField.text = "";
                    mainUiObject.phID_Text.text = "아이디가 틀림";
                    mainUiObject.phID_Text.color = Color.red;

                    //print("비밀번호가 틀림");
                    mainUiObject.pass_InputField.text = "";
                    mainUiObject.phPass_Text.text = "비밀번호 틀림";
                    mainUiObject.phPass_Text.color = Color.red;

                }

            }

        }
        //파일 없으면 생성하기
        else
        {
            // 파일이 없으면 새로 생성 (빈 파일)
            System.IO.File.Create(path).Dispose();
            print("JSON 파일 생성됨");
        }

    }//TestLocalLoginJson end
    //관심사 선택
    public void OnLikeChoice()
    {
        if (!isViewPanelLike)
        {
            panelMyLike_Object.SetActive(true);
            isViewPanelLike = true;
        }
        else
        {
            panelMyLike_Object.SetActive(false);
            isViewPanelLike = false;
        }

    }
    //로그인 img 와 회원가입 img 끄자.
    void OFFIMG()
    {
        //로그인 오브젝트 끄기
        mainUiObject.imgLogin_Object.SetActive(false);
        //회원가입 오브젝트 끄기
        mainUiObject.imgRegist_Object.SetActive(false);
    }
    //로컬 로그인 스트링 타입
    public void TestLocalLoginStirng()
    {
        print("로그인 버튼 클릭");
        //입력된 아이디 가져오기
        idText = mainUiObject.id_InputField.text;
        //입력된 패스워드 가져오기
        passText = mainUiObject.pass_InputField.text;
        print("ID" + idText + "," + "pass" + passText);

        //문자열로 저장할경로 + 파일이름.
        //C:\Users\Admin\AppData\LocalLow\DefaultCompany\front-end
        string fileName = "SaveRegist";
        //string path = Application.persistentDataPath + "/SaveRegist.txt";

        //asset/Resources
        string path = Application.dataPath + "/Resources/" + fileName + ".txt";
        //파일에 저장될 모양과 값.
        string content = "ID" + ":" + idText + "," + "Password" + ":" + passText;

        // 파일이 존재하는지, 그리고 동일한 내용이 있는지 확인
        if (System.IO.File.Exists(path))
        {
            //pah의 모든 택스트를 가져오자.
            loadUserInfo = System.IO.File.ReadAllText(path);
            print("텍스트 가져오기");

            //content가 포함되어 있다면
            if (loadUserInfo.Contains(content))
            {
                //로그인해주기
                Login();
                print("LoadComplite");
                return;
                /* id_Regist_InputField.text = "";
                 ph_Regist_ID_Text.text = "아이디가중복됩니다";
                 ph_Regist_ID_Text.color = Color.red;
                 return;*/

            }
            else
            {
                print("불일치");
                if (!loadUserInfo.Contains(idText))
                {
                    print("아이디가 틀림");
                    //빈문자열로 해줍니다. //영어로만 들어 갑니다.
                    mainUiObject.id_InputField.text = "";
                    mainUiObject.phID_Text.text = "아이디가 틀림";
                    mainUiObject.phID_Text.color = Color.red;
                }
                if (!loadUserInfo.Contains(passText))
                {
                    print("비밀번호가 틀림");
                    mainUiObject.pass_InputField.text = "";
                    mainUiObject.phPass_Text.text = "비밀번호 틀림";
                    mainUiObject.phPass_Text.color = Color.red;
                }
            }


        }
        else
        {
            System.IO.File.Create(path);
        }

    }
    //내
    public void ViewExitPanel()
    {
        if (isViewExitMenu == false)
        {
            panel_Exit.SetActive(true);
            isViewExitMenu = true;
        }
        else
        {
            panel_Exit.SetActive(false);
            isViewExitMenu = false;
        }

    }
    //내정보보기
    public void ViewMyInfo()
    {
        if (isViewMyInfo == false)
        {
            mainUiObject.panel_MyInfo_Object.SetActive(true);
            isViewMyInfo = true;
        }
        else
        {
            mainUiObject.panel_MyInfo_Object.SetActive(false);
            isViewMyInfo = false;
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 원하는 오브젝트 비활성화
        mainUiObject.imgLogin_Object.SetActive(false);
        mainUiObject.imgRegist_Object.SetActive(false);

        // 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    //로그인 텍스트 리셋
    public void ResetLoginText()
    {
        //아이디 텍스트 초기화
        mainUiObject.id_InputField.text = "";
        //비밀번호 텍스트 초기화
        mainUiObject.pass_InputField.text = "";
        //아이디 홀드 텍스트 초기화
        mainUiObject.phID_Text.text = phID_STR;
        mainUiObject.phID_Text.color = Color.gray;
        //패스 홀드 텍스트 초기화
        mainUiObject.phPass_Text.text = phPass_STR;
        mainUiObject.phPass_Text.color = Color.gray;
    }

    public void NewRegistComplite()
    {
        mainUiObject.img_RegistComplite_Object.SetActive(true);
    }

    //회원가입
    public void MoveNewRegist()
    {
        if(mainUiObject == null)
        {
            mainUiObject = GameObject.Find("MainUIObject").GetComponent<MainUIObject>();
        }
        //로그인이미지를 꺼주자.
        mainUiObject.imgLogin_Object.SetActive(false);
        ResetLoginText();

    }
    //로그아웃 하면 로그인 img 켜주자
    public void MoveLogin()
    {
        //img_Regist_Object.SetActive(false);
        //로그인이미지를 켜주자.
        mainUiObject.imgLogin_Object.SetActive(true);
    }
    /*public void CheckLogin()
    {
        //아이디 필드의 텍스트 가져오기
        enteredID = mainUiObject.idText.text;
        //패스 필드의 텍스트 가져오기
        enteredPass = mainUiObject.passText.text;

        if (enteredID == current_Id)
        {
            print(111);
        }
        else
        {
            print(222);
        }


    }*/

    //패스워드 인풋 필드의 컨텐츠 타입 변경,
    public void ViewPass()
    {
        if (mainUiObject == null)
        {
            mainUiObject = GameObject.Find("MainUIObject").GetComponent<MainUIObject>();
        }
        //print("비밀번호 보기");
        if (isViewPass == false)
        {
            mainUiObject.pass_InputField.contentType = InputField.ContentType.Standard;

            isViewPass = true;
        }
        else
        {
            //print(2222);
            mainUiObject.pass_InputField.contentType = InputField.ContentType.Password;
            isViewPass = false;
        }
        // 텍스트를 강제로 재설정하여 변경된 contentType 반영
        mainUiObject.pass_InputField.ForceLabelUpdate();
        string currentText = mainUiObject.pass_InputField.text;
        mainUiObject.pass_InputField.text = "";
        mainUiObject.pass_InputField.text = currentText;

    }
    //로그아웃하기
    public void LogOut()
    {
        print("나가기");
        //변수이름 잘 확인하자.
        if (MainUI.Instance != null)
        {
            GameObject canVas = GameObject.Find("HoonCanvas");
            //if (canVas != null) print(1111);
            GameObject imgLogin = canVas.transform.Find("Img_Login").gameObject;
            imgLogin.SetActive(true);
            mainUiObject.imgRegist_Object.SetActive(true);
            //MainUI.Instance.imgLogin_Object = GameObject.Find("Img_Login");
            //MainUI.Instance.imgLogin_Object.SetActive(true);

        }

    }
    //로그인 img 회원가입 img 를 비활성

    public void LoadJsonMyMemo()
    {
        string userId = idText;
        string fileName = "MyMeMo.json";
        string path = Application.dataPath + "/Resources/" + fileName;

       
        //파일이 없으면
        if(!System.IO.File.Exists(path))
        {
            //파일생성
            using (System.IO.FileStream fs = System.IO.File.Create(path))
            {
                // 파일을 닫아주기 위해 using 사용
                print("신규메모생성");
            }
          

        }
        //파일이 있으면
        else
        {
            //모든텍스트 가져오기
            string loadUserMemo = System.IO.File.ReadAllText(path);
            print("모든 메모 가져오기" + loadUserMemo);
            print("idText 확인하기" + idText);
            
            // JSON 파일을 Dictionary 리스트로 변환
            List<Dictionary<string, string>> userMemoList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(loadUserMemo);
            //print("유저정보량" + userInfoList.Count);

            // 유저 정보가 있는지 확인하는 변수
            bool isUserFound = false;

            //리스트파일을 순차검사
            foreach (var memo in userMemoList)
            {

                //아이디를 포함하고 있고 idText를 포함하고 있다면
                if (memo.ContainsKey("userId") && memo["userId"] == idText)// && memo.ContainsKey("MyMemo"))
                {
                    print("아이디 있음");
                   
                    //유저찾음
                    isUserFound = true;

                    // 일치하는 유저 메모를 텍스트에 할당
                    mainUiObject.myMemo_TextComp.text = memo["MyMemo"];
                    print("상태메시지 " + memo["MyMemo"]);

                    //나가기
                    break;

                }

            }
            if(!isUserFound)
            {
                mainUiObject.myMemo_TextComp.text = "상태메시지를 입력하세요";
                print("상태메시지 없음");
            }
          
        }
      
    }



    public void Login()
    {
        loginCount++;
        if (mainUiObject.imgLogin_Object != null) mainUiObject.imgLogin_Object.SetActive(false);
        //로그인에 입력한 텍스트 초기화
        ResetLoginText();
        //상태메시지 로드
        LoadJsonMyMemo();


        //이미지 회원가입 끄기
        mainUiObject.imgRegist_Object.SetActive(false);
        //내정보패널을 끄기
        mainUiObject.panel_MyInfo_Object.SetActive(false);

        RoomUIManager_GH.instance.roomUserIdSet(saveUserId);
        //서버가 응답하지 않으면 null 임시주석
       //RoomUIManager_GH.instance.OnLoad();




    }
    //메인키를 누르면 모든 UI를 보여주게하자.
    public void ViewMain()
    {
        //BG를 끄자.
        mainUiObject.bg_Object.SetActive(true);
        //mainRoom 끄자
        mainUiObject.mainRoom_Object.SetActive(true);
        //myInfo 끄자
        mainUiObject.myInfo_Object.SetActive(true);
        //PlayerImg 끄자
        mainUiObject.playerImg_Object.SetActive(true);


    }

    //Room 버튼을 누르면 모든 UI를 끄고 Room으로 이동
    public void ViewRoom()
    {

        //BG를 끄자.
        mainUiObject.bg_Object.SetActive(false);
        //mainRoom 끄자
        mainUiObject.mainRoom_Object.SetActive(false);
        //myInfo 끄자
        mainUiObject.myInfo_Object.SetActive(false);
        //PlayerImg 끄자
        mainUiObject.playerImg_Object.SetActive(false);
        //room을 켜주자.
        isRoomActive = true;


    }

    public void MoveLobby()
    {
        SceneManager.LoadScene("HoonLobbyScene");

    }

    public void MoveChat()
    {

    }

    public void PrintLog(string message)
    {
        log += message + '\n';
        //text_logText.text = log;
    }
    public void ChatSceneLoad()
    {
        SceneManager.LoadScene(2);
        //ChatManager_GH.instance.roomUserIdSet(saveUserId);
    }
    public void MainSceneLoad()
    {
        SceneManager.LoadScene(0);
    }

}//클래스끝
