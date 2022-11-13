using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
 

public class SignUpManager : MonoBehaviour
{
    public GameObject login;//�α��� ȭ�� 

    
    public InputField id;//id �Է¶�
    public InputField pw;//pw�Է¶�
    public InputField pwCheck;//pw�ٽ��ѹ� �Է��ϴ� �Է��ʵ�
    public InputField schoolCode;//�޴�����ȣ
    public InputField _name;//�̸�
    public InputField Email;//�̸���
    public InputField classCode_;

    bool idCheckButton;//�ߺ�Ȯ�ι�ư�� �������� Ȯ�����ִ� ����
    bool _pwCheck;//pw�� ������ �ٸ��� Ȯ�����ִ� ����
    bool emailCheck;
    bool nameCheck;
    bool phoneCheck;//�޴�����ȣ Ȯ�����ִ� ����
    bool classCodeCheck;
     
    public GameObject warningMsg;//���޽���
    public GameObject warningMsg2;
    public GameObject notification;//�˸�â
    public Text notificationText;//�˸�â�� ��� �ؽ�Ʈ

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
            warningMsg.GetComponent<Text>().text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
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
                    warningMsg2.GetComponent<Text>().text = "�߸��� ��ȭ��ȣ �Դϴ�";
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
        //id�� �ߺ����� Ȯ�� ���� �α��� ȸ������ ������ ���̽��� ���� �����Ÿ� �ȸ��� �̰� ������� ����
        idCheckButton = true;
    }
    

    public void SignUpButton()
    {
        
        //PhoneCheck�����̸� �����ϱ�
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
        //    notificationText.text = "�ߺ�Ȯ�� ��ư�� �����ּ���";
        //}
        else if (!_pwCheck)
        {
            notification.SetActive(true);
            notificationText.text = "��й�ȣ�� Ȯ�����ּ���";
        }
        else if (!nameCheck)
        {
            notification.SetActive(true);
            notificationText.text = "�̸��� Ȯ���� �ּ���";
        }
        else if (!emailCheck)
        {
            notification.SetActive(true);
            notificationText.text = "�̸��ϸ� Ȯ���� �ּ���";
        }
        else if (!phoneCheck)
        {
            notification.SetActive(true);
            notificationText.text = "�޴�����ȣ�� Ȯ���� �ּ���";
        }
        else if (!classCodeCheck)
        {
            notification.SetActive(true);
            notificationText.text = "���ڵ带 �Է��� �ּ���";
        }
         
        else
        {
            notification.SetActive(true);
            notificationText.text = "�������� �ذ����ּ���";
        }
        
    }

    public void ToggleClick(bool isOn)
    {
        if (isOn)
        {
            //���簡 false �л��� true
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
                notificationText.text = "ȸ������ �Ϸ�";
                login.SetActive(true);
                gameObject.SetActive(false);
            }
        }

    }
}
