using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Ranking : MonoBehaviour
{
    public Sprite _img;
    public GameObject _btn;
    public GameObject ranking;
    public bool collision = false;

    public Text rankingText;

    public GameObject content;
    List<GameObject> rankingLst;

    string userId;
    void Start()
    {
        rankingLst = new List<GameObject>();
        userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
    }

    // Update is called once per frame
    void Update()
    {
        if (_btn.GetComponent<InteractionButton>().Btn()&& collision)
        {
            ranking.SetActive(true);
            print("·©Å·");
            StartCoroutine(GetRanking("http://43.201.142.6:5000/game/ranking/"+PlayerPrefs.GetString("schoolCode")+"/"+PlayerPrefs.GetString("classCode")));
            collision = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _btn.GetComponent<InteractionButton>().img = _img;
            this.collision = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            _btn.GetComponent<InteractionButton>().img = null;
            this.collision = false;
        }
    }
    bool FindRankingObj(string objName)
    {
        for (int i = 0; i < rankingLst.Count; i++)
        {
            if (rankingLst[i].gameObject.name == objName)
            {

                return true;
            }
        }

        return false;
    }
    IEnumerator GetRanking(string URL)
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
            RankingInfo response = JsonUtility.FromJson<RankingInfo>(request.downloadHandler.text);
            if (response.code == 1)
            {
                for (int i = 0; i < response.whole_rank.Length; i++)
                {
                    if (!FindRankingObj(userId + "Ranking"+i))
                    {
                        Text _text = Instantiate(rankingText, content.transform);
                        _text.gameObject.name = userId + "Ranking"+i;
                        rankingLst.Add(_text.gameObject);
                        _text.text = (i + 1) + "µî " + response.whole_rank[i].user_id;
                    }
                    else
                    {
                        Destroy(GameObject.Find(userId + "Ranking"+i));
                        rankingLst.Remove(GameObject.Find(userId + "Ranking"+i).gameObject);
                        Text _text = Instantiate(rankingText, content.transform);
                        _text.gameObject.name = userId + "Ranking"+i;
                        rankingLst.Add(_text.gameObject);
                        _text.text = (i + 1) + "µî " + response.whole_rank[i].user_id;
                    }
                }
            }
             

        }
    }

}

[System.Serializable]
public class RankingInfo
{
    public int code;
    public string msg;
    public Ranking_[] whole_rank;
}

[System.Serializable]
public class Ranking_
{
    public string user_id;
    public int exp;
    public int point;

}
