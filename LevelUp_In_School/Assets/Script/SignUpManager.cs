using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
 

public class SignUpManager : MonoBehaviour
{
    public GameObject login;//로그인 화면 

    
    public InputField id;//id 입력란
    public InputField pw;//pw입력란
    public InputField pwCheck;//pw다시한번 입력하는 입력필드
    public InputField schoolCode;//휴대폰번호
    public InputField _name;//이름
    public InputField Email;//이메일
    public InputField classCode_;

    bool idCheckButton;//중복확인버튼을 눌렀는지 확인해주는 변수
    bool _pwCheck;//pw가 같은지 다른지 확인해주는 변수
    bool emailCheck;
    bool nameCheck;
    bool phoneCheck;//휴대폰번호 확인해주는 변수
    bool classCodeCheck;
     
    public GameObject warningMsg;//경고메시지
    public GameObject warningMsg2;
    public GameObject notification;//알림창
    public Text notificationText;//알림창에 띄울 텍스트

    //bool teacher;
    //bool student;

    bool job;
    void Start()
    {
        
        job = false;
        //teacher = true;
        //student = false;

        emailCheck = false;
        nameCheck = false;
        idCheckButton = false;
        _pwCheck = false;
         
        phoneCheck = false;
    }

    
    void Update()
    {

        if (pwCheck.text!=""&&pw.text != pwCheck.text)
        {
            warningMsg.SetActive(true);
            warningMsg.GetComponent<Text>().text = "비밀번호가 일치하지 않습니다.";
            _pwCheck = false;
        }
        if (pwCheck.text!=""&&pw.text != "" && pw.text == pwCheck.text)
        {
            _pwCheck = true;
            warningMsg.SetActive(false);
        }
        if (schoolCode.text != "")
        {
            for(int i = 0; i < schoolCode.text.Length; i++)
            {
                if (schoolCode.text[i] == '-')
                {
                    warningMsg2.SetActive(true);
                    warningMsg2.GetComponent<Text>().text = "잘못된 전화번호 입니다";
                    phoneCheck = false;
                    return;

                }
                warningMsg2.SetActive(false);
                phoneCheck = true;
            }
        }
        else if(schoolCode.text=="")
        {
            phoneCheck = false;
        }
        if (Email.text == "")
        {
            emailCheck = false;
        }
        else
        {
            emailCheck = true;
        }
        if (_name.text == "")
        {
            nameCheck = false;
        }
        else
        {
            nameCheck = true;
        }

        if (classCode_.text == "")
        {
            classCodeCheck = false;
        }
        else
        {
            classCodeCheck = true;
        }
        
    }
    public void IdCheckButton()
    {
        //id가 중복인지 확인 아직 로그인 회원가입 데이터 베이스나 서버 같은거를 안만들어서 이걸 만들수가 없음
        idCheckButton = true;
    }
    

    public void SignUpButton()
    {
        
        //PhoneCheck변수이름 변경하기
        if ( _pwCheck  && phoneCheck&&emailCheck&&nameCheck&& classCodeCheck)
        {
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("email", Email.text);
            //dic.Add("job", job);
            //dic.Add("password",pw.text);
            //dic.Add("school_code", schoolCode.text);
            //dic.Add("user_id", id.text);
            //dic.Add("username", _name.text);
            User user = new User
            {
                email = Email.text,
                isStudent = job,
                password = pw.text,
                school_code = schoolCode.text,
                user_id = id.text,
                username = _name.text,
                class_code = classCode_.text
            };
            PlayerPrefs.SetString("classCode", user.class_code);
            PlayerPrefs.SetString("schoolCode", user.school_code);
            string json = JsonUtility.ToJson(user);
            StartCoroutine(SignUp("http://43.201.142.6:5000/auth/singnup/",json));
             
        }
        //else if (!idCheckButton)
        //{
        //    notification.SetActive(true);
        //    notificationText.text = "중복확인 버튼을 눌러주세요";
        //}
        else if (!_pwCheck)
        {
            notification.SetActive(true);
            notificationText.text = "비밀번호를 확인해주세요";
        }
        else if (!nameCheck)
        {
            notification.SetActive(true);
            notificationText.text = "이름을 확인해 주세요";
        }
        else if (!emailCheck)
        {
            notification.SetActive(true);
            notificationText.text = "이메일를 확인해 주세요";
        }
        else if (!phoneCheck)
        {
            notification.SetActive(true);
            notificationText.text = "휴대폰번호를 확인해 주세요";
        }
        else if (!classCodeCheck)
        {
            notification.SetActive(true);
            notificationText.text = "반코드를 입력해 주세요";
        }
         
        else
        {
            notification.SetActive(true);
            notificationText.text = "오류들을 해결해주세요";
        }
        
    }

    public void ToggleClick(bool isOn)
    {
        if (isOn)
        {
            //교사가 false 학생이 true
            job = false;
            //teacher = true;
            //student = false;
             
        }
        else
        {
            job = true;
            //teacher = false;
            //student = true;
             
        }
    }
    
    IEnumerator SignUp(string URL, string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, json))
        {
            byte[] jsonTosend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonTosend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            //Dictionary<string, object> dic = JsonUtility.FromJson<Dictionary>();
            AuthRes response = JsonUtility.FromJson<AuthRes>(request.downloadHandler.text);
            Debug.Log(response.msg);

            if (request.isNetworkError || request.isHttpError || response.code == "-1")
            {
                notification.SetActive(true);
                notificationText.text = response.msg;

            }

            else
            {
                notification.SetActive(true);
                notificationText.text = "회원가입 완료";
                login.SetActive(true);
                gameObject.SetActive(false);
            }
        }

    }
}
