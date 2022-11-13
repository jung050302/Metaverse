using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HomeWorkQuest : MonoBehaviour
{
    public GameObject homeWorkScrollView;
    public Text homeWorkQuestText;
    public GameObject content;
    string userId;

    
    public QuestText questText;

    List<GameObject> questLst=new List<GameObject>();
    void Start()
    {
        userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Refresh()
    {
        homeWorkScrollView.SetActive(false);
        homeWorkScrollView.SetActive(true);
        StartCoroutine(GetQuest("http://43.201.142.6:5000/quest/game/" + userId));
    }

    public void ButtonDown()
    {
        homeWorkScrollView.SetActive(true);

        StartCoroutine(GetQuest("http://43.201.142.6:5000/quest/game/"+ userId));

    }
    
    public void CompletButton(string text,int id)
    {
        if (text=="완료")
        {
            SendId sendId = new SendId()
            {
                id = id
            };
            string json = JsonUtility.ToJson(sendId);
            StartCoroutine(RequestDone("http://43.201.142.6:5000/quest/game/" + userId, json));
            homeWorkScrollView.SetActive(false);
            ButtonDown();
            //btn.BtnText.text = "요청보냄";
        }
        
        else if (text == "보상수령")
        {
            SendId sendId = new SendId()
            {
                id = id
            };
            string json = JsonUtility.ToJson(sendId);
            StartCoroutine(Reward("http://43.201.142.6:5000/quest/game/reward/" + userId, json));
            Destroy(GameObject.Find(userId + id));
            homeWorkScrollView.SetActive(false);
            ButtonDown();
            
        }
    }

    bool FindQuestObj(string objName)
    {
        for(int i = 0; i < questLst.Count; i++)
        {
            if (questLst[i].gameObject.name == objName)
            {
                
                return true;
            }
        }
        
        return false;
    }


    IEnumerator RequestDone(string URL,string json)
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
            Done response = JsonUtility.FromJson<Done>(request.downloadHandler.text);

            print("코드:"+response.code);
            print("메시지:" + response.msg);
        }

    }
    IEnumerator GetQuest(string URL)
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
            QuestResponse response = JsonUtility.FromJson<QuestResponse>(request.downloadHandler.text);
            print("code:" + response.data.Length);

            if (response.code == 1)
            {
                for(int i = 0; i < response.data.Length; i++)
                {
                    if (!FindQuestObj(userId + response.data[i].id))
                    {
                        int _id = response.data[i].id;
                        QuestText quest = Instantiate(questText, content.transform);
                        quest.titleText.text = response.data[i].title;
                        quest.descriptionText.text = response.data[i].description;
                        quest.rewardText.text = response.data[i].exp + "exp\n" + response.data[i].point + "p";
                        quest.dateText.text = response.data[i].start_date + "~" + response.data[i].end_date;
                        quest.teacherIdText.text = response.data[i].teacher_id+ " 선생님";

                        print("rewardText:"+ quest.rewardText);

                        print("done:" + response.data[i].done);
                        if (response.data[i].done == true && response.data[i].check == false)
                        {
                            quest.BtnText.text = "요청보냄";
                        }
                        else if (response.data[i].done == true && response.data[i].check == true)
                        {
                            quest.BtnText.text = "보상수령";
                        }
                        else if (response.data[i].done == false && response.data[i].check == false)
                        {
                            quest.BtnText.text = "완료";
                            //quest.thisBtn.interactable = false;
                        }
                        print("flepxk:"+response.data.Length);
                        print("i:"+i);
                        quest.thisBtn.onClick.AddListener(() => CompletButton(quest.BtnText.text, _id));
                         
                        quest.gameObject.name = userId + response.data[i].id;
                        questLst.Add(quest.gameObject);
                    }
                    else
                    {
                        Destroy(GameObject.Find(userId + response.data[i].id));
                        questLst.Remove(GameObject.Find(userId + response.data[i].id));
                        int _id = response.data[i].id;
                        QuestText quest = Instantiate(questText, content.transform);
                        quest.titleText.text = response.data[i].title;
                        quest.descriptionText.text = response.data[i].description;
                        quest.rewardText.text = response.data[i].exp + "exp\n" + response.data[i].point + "p";
                        quest.dateText.text = response.data[i].start_date + "~" + response.data[i].end_date;
                        quest.teacherIdText.text = response.data[i].teacher_id + " 선생님";


                        print("done:" + response.data[i].done);
                        if (response.data[i].done == true && response.data[i].check == false)
                        {
                            quest.BtnText.text = "요청보냄";
                        }
                        else if (response.data[i].done == true && response.data[i].check == true)
                        {
                            quest.BtnText.text = "보상수령";
                        }
                        else if (response.data[i].done == false && response.data[i].check == false)
                        {
                            quest.BtnText.text = "완료";
                            //quest.thisBtn.interactable = false;
                        }
                        print("flepxk:" + response.data.Length);
                        print("i:" + i);
                        quest.thisBtn.onClick.AddListener(() => CompletButton(quest.BtnText.text, _id));

                        quest.gameObject.name = userId + response.data[i].id;
                        questLst.Add(quest.gameObject);
                    }

                }
            }
        }
    }

    IEnumerator Reward(string URL, string json)
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
            RewardInfo response = JsonUtility.FromJson<RewardInfo>(request.downloadHandler.text);
             
            GameObject.Find("Player").GetComponent<Player>().AddExp(response.data.exp) ;
            GameObject.Find("Player").GetComponent<Player>().AddMoney(response.data.point);
        }

    }

}




[System.Serializable]
public class QuestResponse
{
    public int code;
    public string msg;
    public Quest_[] data;
    
}

[System.Serializable]
public class Done
{
    public int code;
    public string msg;
}

[System.Serializable]
public class Quest_
{
    public int id;
    public string title;
    public string description;
    public string start_date;
    public string end_date;
    public string user_id;
    public string teacher_id;
    public int exp;
    public int point;

    public bool done;
    public bool check;

}

[System.Serializable]
public class SendId
{
    public int id;
    
}

[System.Serializable]
public class RewardInfo
{
    public int code;
    public string msg;
    public Rewards data;
}

[System.Serializable]
public class Rewards
{
    public int exp;
    public int point;
}
