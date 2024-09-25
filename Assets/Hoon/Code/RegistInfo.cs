using System.Collections;
// Dictionary 사용을 위함
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
// JSON 변환(파싱)을 위해 필요 (Json.NET 라이브러리)
using Newtonsoft.Json;
using static MainUI;

public class RegistInfo : MonoBehaviour
{
    MainUI mainUI;
    MainUIObject mainUiObject;

    GameObject if_ID_Obejct;
    GameObject if_Pass_Obejct;
    GameObject if_Name_Obejct;
    GameObject if_Age_Obejct;

    InputField id_Regist_InputField;
    InputField pass_Regist_InputField;
    InputField name_Regist_InputField;
    InputField age_Regist_InputField;

    GameObject ph_Regist_ID_Object;
    GameObject ph_Rigist_Pass_Obejct;
    GameObject ph_Rigist_Name_Obejct;
    GameObject ph_Rigist_Age_Obejct;

    Text ph_Regist_ID_Text;
    Text ph_Regist_Pass_Text;
    Text ph_Regist_Name_Text;
    Text ph_Regist_Age_Text;

    string loadUserInfo;

    public string idText;
    public string passText;
    public string nameText;

    InputField inputRegistPass;

    // JSON 타입으로 정보 저장하기 위한 클래스
    [System.Serializable]
    public class UserInfo
    {
        public string userId;
        public string userPassword;
        public string userName;
        public string bigCategory;
        public string smallCategory;
        public string bigCategory2;
        public string smallCategory2;
        public string bigCategory3;
        public string smallCategory3;

       
    }

    // Start is called before the first frame update
    void Start()
    {

        mainUI = gameObject.GetComponent<MainUI>();
        mainUiObject = gameObject.GetComponent<MainUIObject>();

        //인풋필드 아이디 오브젝트
        if_ID_Obejct = GameObject.Find("IF_Regist_ID");
        //인풋필드 컴포넌트
        if (if_ID_Obejct != null)
        {
            //print("아이디 등록 인풋필드");
            id_Regist_InputField = if_ID_Obejct.GetComponent<InputField>();
        }

        //플레이스홀드 오브젝트
        ph_Regist_ID_Object = GameObject.Find("Ph_Regist_ID");
        //플레이스홀드 아이드 텍스트 
        if (ph_Regist_ID_Object != null) ph_Regist_ID_Text = ph_Regist_ID_Object.GetComponent<Text>();

        //인풋필드 패스 오브젝트
        if_Pass_Obejct = GameObject.Find("IF_Regist_Pass");
        //인풋필드 컴포넌트
        if (if_Pass_Obejct != null) pass_Regist_InputField = if_Pass_Obejct.GetComponent<InputField>();
        //플레이스홀드
        ph_Rigist_Pass_Obejct = GameObject.Find("Ph_Regist_Pass");
        //플레이스홀드 패스 텍스트
        if (ph_Rigist_Pass_Obejct != null) ph_Regist_Pass_Text = ph_Rigist_Pass_Obejct.GetComponent<Text>();

        if_Name_Obejct = GameObject.Find("IF_Regist_Name");
        if (if_Name_Obejct != null) name_Regist_InputField = if_Name_Obejct.GetComponent<InputField>();

        ph_Rigist_Name_Obejct = GameObject.Find("Ph_Regist_Name");
        if(ph_Rigist_Name_Obejct != null)ph_Regist_Name_Text = ph_Rigist_Name_Obejct.GetComponent<Text>();

        ph_Rigist_Name_Obejct = GameObject.Find("Ph_Regist_Name");

        inputRegistPass = GameObject.Find("IF_Regist_Pass").GetComponent<InputField>();

        //mainUI.imgLogin_Object.SetActive(true);

 

    }

    // Update is called once per frame
    /*void Update()
    {

    }*/
    
    //세이브로컬레지스트제이슨
    public void SaveLocalRegistJSON()
    {
         idText = id_Regist_InputField.text;
        print("회원등록ID" +  idText);
         passText = pass_Regist_InputField.text;
        print("회원등록pass" + passText);
         nameText = name_Regist_InputField.text;
        print("회원등록name" + nameText);
        // 파일 저장 경로
        string path = Application.dataPath + "/Resources/SaveRegist.json";
        string like = "미지정";
        // 사용자 정보를 Dictionary로 저장
        Dictionary<string, string> userInfo = new Dictionary<string, string>
        {
            { "userId", idText },
            { "userPassword", passText },
            { "userNickName", nameText },
            { "likeCount", "0" },
            { "bigCategory", like },
            { "smallCategory", like },
            { "bigCategory2", like },
            { "smallCategory2", like },
            { "bigCategory3", like },
            { "smallCategory3", like }

        };

        //신규유저 정보를 Json 으로 변경
        string newUser = JsonConvert.SerializeObject(userInfo, Formatting.Indented);
        print("신규유저정보" + newUser);

        // 기존 파일이 존재하는지 확인
        if (File.Exists(path))
        {
            // 파일의 내용을 읽어온다.
            loadUserInfo = File.ReadAllText(path);

            // JSON을 Dictionary 리스트로 변환
            List<Dictionary<string, string>> userInfoList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(loadUserInfo);

            // 동일한 ID가 있는지 확인
            foreach (var user in userInfoList)
            {
                if(idText =="")
                {

                    ph_Regist_ID_Text.text = "아이디를 입력하세요";
                    ph_Regist_ID_Text.color = Color.red;
                    print("아이디 미입력");
                    return;
                }
                if (user["userId"] == idText)
                {
                    id_Regist_InputField.text = "";
                    ph_Regist_ID_Text.text = "아이디가 중복됩니다";
                    ph_Regist_ID_Text.color = Color.red;
                    print("아이디 중복");
                    return;
                }
                if (passText == "")
                {
                    ph_Regist_Pass_Text.text = "비밀번호를 입력하세요";
                    ph_Regist_Pass_Text.color = Color.red;
                    print("패스워드 미입력");
                    return;
                }
                if (nameText == "")
                {
                    ph_Regist_Name_Text.text = "이름을를 입력하세요";
                    ph_Regist_Name_Text.color = Color.red;
                    print("이름 미입력");
                    return;
                }

                
                
            }

            // 새로운 유저 정보를 리스트에 추가
            userInfoList.Add(userInfo);

            print("회원가입정보" + userInfo);

            // 리스트를 다시 JSON 문자열로 변환
            loadUserInfo = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);

        }
        else
        {
            // 파일이 없으면 새 리스트를 만들고 추가
            List<Dictionary<string, string>> userInfoList = new List<Dictionary<string, string>>();
            userInfoList.Add(userInfo);

            // 리스트를 JSON 문자열로 변환
            loadUserInfo = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);
        }

        // JSON 데이터를 파일에 저장
        File.WriteAllText(path, loadUserInfo);
        print("SaveComplete" + newUser);
        mainUI.NewRegistComplite();

        //서버에 post 요청하기
        //StartCoroutine(SaveServerRegistJSON(loadUserInfo));
        //print("서버에요청");

    }


    public void SaveServerRegistJSON()
    {

        idText = id_Regist_InputField.text;
        passText = pass_Regist_InputField.text;
        nameText = name_Regist_InputField.text;
        string like = "미지정";

        // 파일 저장 경로
        string path = Application.dataPath + "/Resources/SaveRegist.json";
        
        // 사용자 정보를 Dictionary로 저장
        Dictionary<string, string> userInfo = new Dictionary<string, string>
        {
            { "userId", idText },
            { "userPassword", passText },
            { "userName", nameText },
            { "smallCategory", like },
            { "smallCategory2", like },
            { "smallCategory3", like }

        };

        //신규유저 정보를 Json 으로 변경
        string newUser = JsonConvert.SerializeObject(userInfo, Formatting.Indented);
        print("신규유저정보" + newUser);

        // 기존 파일이 존재하는지 확인
        if (File.Exists(path))
        {
            // 파일의 내용을 읽어온다.
            loadUserInfo = File.ReadAllText(path);

            // JSON을 Dictionary 리스트로 변환
            List<Dictionary<string, string>> userInfoList = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(loadUserInfo);

            // 동일한 ID가 있는지 확인
            foreach (var user in userInfoList)
            {
                if (user["userId"] == idText)
                {
                    id_Regist_InputField.text = "";
                    ph_Regist_ID_Text.text = "아이디가 중복됩니다";
                    ph_Regist_ID_Text.color = Color.red;
                    return;
                }
            }

            // 새로운 유저 정보를 리스트에 추가
            userInfoList.Add(userInfo);

            print("회원가입정보" + userInfo);

            // 리스트를 다시 JSON 문자열로 변환, 로드저장
            loadUserInfo = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);

        }
        else
        {
            // 파일이 없으면 새 리스트를 만들고 추가
            List<Dictionary<string, string>> userInfoList = new List<Dictionary<string, string>>();
            userInfoList.Add(userInfo);

            // 리스트를 JSON 문자열로 변환
            loadUserInfo = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);
        }

        // JSON 데이터를 파일에 저장
        File.WriteAllText(path, loadUserInfo);
        print("SaveComplete" + newUser);

       
        // 사용자 정보를 Dictionary로 저장
        Dictionary<string, string> userInfoTest = new Dictionary<string, string>
        {
             
            { "userId", idText },
            { "userPassword", passText },
            { "userNickname", nameText },
            //{ "birthday", "19" },
            //{ "gender", "m" }
            { "smallCategory", like },
            { "smallCategory2", like },
            { "smallCategory3", like }
        };



        // 리스트를 JSON 으로 직렬화 (배열로 감싸기)
        string newUserInfo = JsonConvert.SerializeObject(userInfoTest, Formatting.Indented);
        newUserInfo = "[" + newUserInfo + "]";
        print("서버에보낼신규유저정보" + newUserInfo);

        //서버에 post 요청하기
        StartCoroutine(RegistPostJSON(newUserInfo));
        print("서버에요청");

    }

    // HTTP POST 요청을 보내는 메소드
    IEnumerator RegistPostJSON(string jsonData)
    {
        string url = "http://192.168.0.76:8080/user"; // 서버 URL 변경 필요
        string urlPot = "https://jsonplaceholder.typicode.com/posts/1";
        string urlPostTest = "http://192.168.0.76:8080/user"; //같은아이피일때
        string urlPostUser = "http://125.132.216.190:5544/user";

        // HTTP POST 요청 준비
        //UnityWebRequest request = new UnityWebRequest(url, "POST");
        //UnityWebRequest request = new UnityWebRequest(urlPot, "POST");
        //UnityWebRequest request = new UnityWebRequest(urlPostTest, "POST");
        UnityWebRequest request = new UnityWebRequest(urlPostUser, "POST");

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



        // 응답 확인
        /*if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("POST 성공: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("POST 실패: " + request.error);
        }*/

    }

    public void testLGH()
    {
       // FurnitureData_GH fd = list_Furniture[i].GetComponent<FurnitureData_GH>();
        HttpInfo info = new HttpInfo();
        info.url = "http://192.168.0.76:8080/ground-furniture";
        //info.body = JsonUtility.ToJson(fd.furnitureInfo);
        info.contentType = "application/json";
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
        };
       // StartCoroutine(NetworkManager_GH.GetInstance().Post(info));
    }

    public void SaveTest()
    {
        string idText = id_Regist_InputField.text;
        string passText = pass_Regist_InputField.text;
        string nameText = name_Regist_InputField.text;

        //아이디 입력한게 없고 null이면
        if (string.IsNullOrEmpty(idText))
        {
            ph_Regist_ID_Text.text = "아이디를입력하세요";
            ph_Regist_ID_Text.color = Color.red;
        }
        //비밀번호 입력한게 없고 null이면
        if (string.IsNullOrEmpty(passText))
        {
            ph_Regist_Pass_Text.text = "비밀번호를입력하세요";
            ph_Regist_Pass_Text.color = Color.red;
        }
        if (string.IsNullOrEmpty(passText))
        {
            ph_Regist_Name_Text.text = "이름을입력하세요.";
            ph_Regist_Name_Text.color = Color.red;
        }

        //입력한게 있다면
        else
        {
            print("입력한거 있음");
            //문자열로 저장할경로 + 파일이름.
            //C:\Users\Admin\AppData\LocalLow\DefaultCompany\front-end
            string path = Application.dataPath + "/Resources/" + "/SaveRegist.txt";
            //파일에 저장될 모양과 값.
            string saveUserInfo = "ID" + ":" + idText + "," + "Password" + ":" + passText + "Password" + ":" + nameText + "\n";

            // 파일이 존재하는지, 그리고 동일한 내용이 있는지 확인
            if (File.Exists(path))
            {
                //pah의 모든 택스트를 가져오자.
                loadUserInfo = File.ReadAllText(path);

                //content가 포함되어 있다면
                if (loadUserInfo.Contains(saveUserInfo))
                {
                    id_Regist_InputField.text = "";
                    ph_Regist_ID_Text.text = "아이디가중복됩니다";
                    ph_Regist_ID_Text.color = Color.red;
                    return;

                }


                loadUserInfo = loadUserInfo + saveUserInfo;


            }

            //using System.IO; 인클루드 해줘야함. //파일을 텍스트에 저장해주자.
            File.WriteAllText(path, loadUserInfo);
            print("SaveComplite");
        }


    }

    public void SaveReistInfo()
    {
        string loginId_Text = id_Regist_InputField.text;
    }




}//클래스끝






