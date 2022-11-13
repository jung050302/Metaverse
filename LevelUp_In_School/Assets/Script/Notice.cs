using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Notice : MonoBehaviour
{
    public GameObject content;
    public NoticeText noticeText;
    public GameObject noticeObj;
    List<GameObject> noticeLst;
    string userId;
    void Start()
    {
        noticeLst = new List<GameObject>();
        userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        noticeObj.SetActive(false);
        noticeObj.SetActive(true);
        StartCoroutine(GetNotice("http://43.201.142.6:5000/quest/game/" + userId));
    }

    public void NoticeButtonDown()
    {
        noticeObj.SetActive(true);
        StartCoroutine(GetNotice("http://43.201.142.6:5000/notice/game/"+userId));
    }

    bool FindQuestObj(string objName)
    {
        for (int i = 0; i < noticeLst.Count; i++)
        {
            if (noticeLst[i].gameObject.name == objName)
            {

                return true;
            }
        }

        return false;
    }

    IEnumerator GetNotice(string URL)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        request.SetRequestHeader("authorization", PlayerPrefs.GetString("token"));
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            NoticeInfo response = JsonUtility.FromJson<NoticeInfo>(request.downloadHandler.text);

            if (response.code == 1)
            {
                for (int i = 0; i < response.data.Length; i++)
                {
                    if (!FindQuestObj(userId + response.data[i].id))
                    {
                        NoticeText notice_ = Instantiate(noticeText, content.transform);
                        notice_.gameObject.name = userId + response.data[i].id;
                        noticeLst.Add(notice_.gameObject);
                        notice_.title.text = response.data[i].title;
                        notice_.description.text = response.data[i].content;
                        notice_.date.text = response.data[i].current_date;
                        notice_.teacherId.text = response.data[i].teacher_id+" ¼±»ý´Ô";
                    }
                    else
                    {
                        Destroy(content.transform.Find(userId + response.data[i].id));
                        noticeLst.Remove(content.transform.Find(userId + response.data[i].id).gameObject);
                        NoticeText notice_ = Instantiate(noticeText, content.transform);
                        notice_.gameObject.name = userId + response.data[i].id;
                        noticeLst.Add(notice_.gameObject);
                        notice_.title.text = response.data[i].title;
                        notice_.description.text = response.data[i].content;
                        notice_.date.text = response.data[i].current_date;
                        notice_.teacherId.text = response.data[i].teacher_id + " ¼±»ý´Ô";
                    }
                }
            }
                        

            
        }
    }
}

[System.Serializable]
public class NoticeInfo
{
    public int code;
    public string msg;
    public NoticeData[] data;
}

[System.Serializable]
public class NoticeData
{
    public int id;
    public string title;
    public string content;
    public string current_date;
    public string teacher_id;
    public string class_code;
}
