using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    private int money;

    int Lv;
    int Exp;
    public int MaxExp;
    public Image expBar;
    public Text expText;

    public Text nickName;
    float scale;
    public Animator ani;
    public float speed;
    public string nickname;

    public bool stop;

    public string _name;
    
    public FixedJoystick joy;

    public GameObject _btn;

    bool coolTime;

    public int expSum;

    //static public Player Instance;

    public Notification notifixation;

    string userId;


    void Start()
     {
        money = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).point;//PlayerPrefs.HasKey("money") ? PlayerPrefs.GetInt("money") : 0;

        Exp = PlayerPrefs.HasKey("Exp") ? PlayerPrefs.GetInt("Exp") : 0;

        userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;


        joy = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();

        

        coolTime = false;
        //_interaction = GameObject.Find("InteractionButton").GetComponent<InteractionButton>().interaction;
       //PlayerPrefs.DeleteAll();
       _name = PlayerPrefs.GetString("NickName");
        expSum = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).exp;
        MaxExp = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).max_exp;
        Lv = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).level;


        stop = false;
        nickname = "["+Lv.ToString() + "LV]" +PlayerPrefs.GetString("NickName") ;
        speed = 5f;
        
        scale = this.transform.localScale.x;

         

         

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddExp(10);
        }

        //print(PlayerPrefs.HasKey("X"));

        nickname = "LV."+ Lv.ToString() +" "+ _name;
        nickName.text = nickname;
        expBar.fillAmount = (float)Exp / (float)MaxExp;
        expText.text = Exp.ToString() + "/" + MaxExp.ToString();
         
        //if(PlayerPrefs.HasKey(""))

        if (Exp >= MaxExp)
        {
            LvUp();
        }
        float x = joy.Horizontal;
        float y = joy.Vertical;
        
        if (!stop)
        {
            if (x != 0 || y != 0)
            {
                ani.SetFloat("RunState", 0.5f);

            }
            else
            {
                ani.SetFloat("RunState", 0f);
            }
            
            this.gameObject.transform.Translate(x * speed * Time.deltaTime, y * speed * Time.deltaTime, 0);
            if (x < 0)
            {
                gameObject.transform.localScale = new Vector2(scale, gameObject.transform.localScale.y);
            }
            else if(x>0)
            {
                gameObject.transform.localScale = new Vector2(-scale, gameObject.transform.localScale.y);
            }
        }

    }

    public void AddExp(int exp)
    {
        Exp += exp;
        PlayerPrefs.SetInt("Exp",Exp);
        PlayerPrefs.SetInt("MaxExp", MaxExp);
        expSum += exp;
        PlayerPrefs.SetInt("expSum", expSum);
        ExpInfo expInfo = new ExpInfo()
        {
            exp = expSum
        };
        string json = JsonUtility.ToJson(expInfo);
        StartCoroutine(RequestExp("http://43.201.142.6:5000/game/addExp/"+ userId, json));
    }

   

    void LvUp()
    {
        int save=0;
        if (Exp > MaxExp)
        {
            save=Exp-MaxExp;
        }
         
        Exp = 0;
        Exp += save;
        Lv++;
        MaxExp += (int)MaxExp * 7 / 100;
        PlayerPrefs.SetInt("LV", Lv);
        PlayerPrefs.SetInt("Exp", Exp);
        PlayerPrefs.SetInt("MaxExp", MaxExp);
        AddMoney(500);
        GameInfo info = new GameInfo()
        {
            level = Lv,
            exp = expSum,
            max_exp = MaxExp,
            user_id = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id,
            point = money
        };
        string json = JsonUtility.ToJson(info);
        StartCoroutine(LevelUp("http://43.201.142.6:5000/game/level_up/", json));

        
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("X");
        PlayerPrefs.DeleteKey("Y");
        PlayerPrefs.DeleteKey("Inventory");
    }
    public void Buy(int price)//아이템 구매했을때 
    {
        money -= price;
        PlayerPrefs.SetInt("money", money);
    }
    public int GetMoney()
    {
        return money;
    }
    public void AddMoney(int addMoney)
    {
        money += addMoney;
        PlayerPrefs.SetInt("money", money);
    }

    IEnumerator LevelUp(string URL, string json)
    {
        using (UnityWebRequest request = UnityWebRequest.Post(URL, json))
        {
            byte[] jsonTosend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonTosend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            //Dictionary<string, object> dic = JsonUtility.FromJson<Dictionary>();
            Data response = JsonUtility.FromJson<Data>(request.downloadHandler.text);

            notifixation.gameObject.SetActive(true);
            notifixation._text.text = "레벨업!!\n500포인트 획득!\nLV:"+Lv+"\npoint:"+money;
        }

    }

    IEnumerator RequestExp(string URL, string json)
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
            ExpResponse response = JsonUtility.FromJson<ExpResponse>(request.downloadHandler.text);
            print(response.code);

        }

    }

}

[System.Serializable]
public class ExpInfo
{
    public int exp;
}

[System.Serializable]
public class ExpResponse
{
    public int code;
    public string msg;
}

[System.Serializable]
public class GameInfo
{
    public int level;
    public int exp;
    public int max_exp;
    public string user_id;
    public int point;
}

