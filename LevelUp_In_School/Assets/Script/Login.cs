using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
public class Login : MonoBehaviour
{
    public InputField _id;
    public InputField _pw;
    public GameObject _text;

    public GameObject notification;
    public Text notificationText;

    public void LoginButtonDown()//로그인 버튼을 눌렀을때
    {
        LoginInfo info = new LoginInfo()
        {
            user_id = _id.text,
            password = _pw.text
        };
        string json = JsonUtility.ToJson(info);
        
        StartCoroutine(_Login("http://43.201.142.6:5000/auth/login/", json));


    }
    IEnumerator _Login(string URL,string json)
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
            Token token = new Token()
            {
                access_token = request.GetResponseHeader("authorization")
                
            };
           
            if (request.isNetworkError || request.isHttpError || response.code == "-1" || token.access_token==null)
            {
                notification.SetActive(true);
                notificationText.text = response.msg;
            }
            else
            {
                PlayerPrefs.SetString("token", token.access_token);
                Data info = new Data()
                {
                    user_id = response.data.user_id,
                    id = response.data.id,
                    class_code = response.data.class_code,
                    school_code = response.data.school_code,
                    isStudent = response.data.isStudent,
                    //password = response.data.password,
                    username = response.data.username,
                    level = response.data.level,
                    exp=response.data.exp,
                    max_exp=response.data.max_exp,
                    point=response.data.point
                };

                string userJson = JsonUtility.ToJson(info);
                PlayerPrefs.SetString("user", userJson);
                _text.GetComponent<StartText>().loginCheak = true;
                _text.SetActive(true);
            }
        }
    }
}

[System.Serializable]
public class LoginInfo
{
    public string user_id;
    public string password;
    
}

 
