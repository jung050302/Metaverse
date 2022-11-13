using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class LobbyManager : MonoBehaviour
{
    public GameObject Btn;
    public GameObject FadeOut;
    bool roomManager;
    public GameObject inputRoomNum;

    public GameObject miniMap;

    public InputField roomCode;

    public GameObject notification;
    public Text notificationText;


    void Start()
    {
        roomManager = false;
        roomCode.text = PlayerPrefs.GetString("classCode");
    }

     
    void Update()
    {
        
    }
    public void ParticipateButton()
    {
        roomManager = false;
        Save();
        Btn.SetActive(false);
        inputRoomNum.SetActive(true);
        //FadeOut.SetActive(true);
    }
    public void RoomMake()
    {

        roomManager = true;
        Save();
        FadeOut.SetActive(true);
         
    }
    public void StartButton()
    {
        RoomCodeManager code = new RoomCodeManager()
        {
            class_code = roomCode.text
        };
        string json = JsonUtility.ToJson(code);

        StartCoroutine(RoomCode("http://43.201.142.6:5000/game/before_connect/", json));
        //FadeOut.SetActive(true);
    }
    void Save()
    {
        PlayerPrefs.SetInt("RoomManager", System.Convert.ToInt16(roomManager));
    }

    public void MiniMap()
    {
        miniMap.SetActive(true);
    }

    IEnumerator RoomCode(string URL, string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, json))
        {
            byte[] jsonTosend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonTosend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("authorization", PlayerPrefs.GetString("token"));
            yield return request.SendWebRequest();
            //Dictionary<string, object> dic = JsonUtility.FromJson<Dictionary>();
            GetPort response = JsonUtility.FromJson<GetPort>(request.downloadHandler.text);

            Debug.Log(response.data);//포트번호
            
            if (request.isNetworkError || request.isHttpError || response.code == "-1")
            {

                notification.SetActive(true);
                notificationText.text = response.msg;
            }
            else
            {

                PlayerPrefs.SetInt("move_port", int.Parse(response.data[0]));//move포트
                print("포트:"+response.data[0]);
                PlayerPrefs.SetInt("chat_port", int.Parse(response.data[1]));//채팅포트
                Debug.Log("성공 "+ response.msg);
                SceneManager.LoadScene("Customizing");
            }
        }
    }

}

[System.Serializable]
public class RoomCodeManager
{
    public string class_code;
}
