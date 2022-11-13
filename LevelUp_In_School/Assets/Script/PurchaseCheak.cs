using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PurchaseCheak : MonoBehaviour
{
    public Image itemImg;
    public Text itemName;
    public Text itemPrice;

    public GameObject player;
    public Notification notification;
    public int price;

    public string itemTag;//파일 이름
    //public GameObject buyItem;

    public Text moneyText;
    string userId;
    private void Start()
    {
        
        userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
        StartCoroutine(Inventory("http://43.201.142.6:5000/game/buy/" + userId));
         
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerPrefs.DeleteKey("Inventory");

        }
        if (this.gameObject.name == "MoneyText")
        {
            moneyText.text = player.GetComponent<Player>().GetMoney() + " 포인트";

        }
    }

    public void BuyButton()
    {
        string[] lst = itemTag.Split(',');
        if (!BuyItemCheck(lst[0]))
        {
            if (player.GetComponent<Player>().GetMoney() < price)
            {
                notification.gameObject.SetActive(true);
                notification._text.text = "포인트가 부족합니다";
            }
            else
            {

                Save("Inventory", PlayerPrefs.GetString("Inventory") + itemTag + ",");


                player.GetComponent<Player>().Buy(price);
                BuyInfo info = new BuyInfo()
                {
                    item_name = itemTag,
                    point = player.GetComponent<Player>().GetMoney(),
                    //user_id = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id
                };
                string json = JsonUtility.ToJson(info);
                StartCoroutine(BuyItem("http://43.201.142.6:5000/game/buy/" + userId, json));
                this.gameObject.SetActive(false);

                notification.gameObject.SetActive(true);
                notification._text.text = itemName.text + " 구매 완료!";
            }
        }
        else
        {
            this.gameObject.SetActive(false);
            notification.gameObject.SetActive(true);
            notification._text.text = "이미 구매한 아이템 입니다";
        }

    }
    public void Save(string key,string value)
    {
         
        string[] lst = PlayerPrefs.GetString("Inventory").Split(',');
        for(int i = 0; i < lst.Length-1; i++)
        {
             
            if(lst[i]== itemTag)
            {
                 
                return;//아이템 중복
            }
             
        }
        PlayerPrefs.SetString(key, value);
    }
    public bool BuyItemCheck(string item)
    {
        string[] lst = PlayerPrefs.GetString("Inventory").Split(',');
        for (int i = 0; i < lst.Length - 1; i++)
        {
            print(i + " " + lst[i]);
            if (lst[i] == item)
            {
                 
                return true;//아이템 중복
            }

        }
         
        return false;
    }


    IEnumerator BuyItem(string URL, string json)
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
            Data response = JsonUtility.FromJson<Data>(request.downloadHandler.text);

            
        }

    }
    IEnumerator Inventory(string URL)
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
            StrInventory response = JsonUtility.FromJson<StrInventory>(request.downloadHandler.text);
            print("response.data: " + response.data);
            PlayerPrefs.SetString("Inventory", response.data);
            //request.downloadHandler.text
            print("리폰데타:"+response.data);
        }
    }

}


[System.Serializable]
public class BuyInfo
{
    public string item_name;
    public int point;
    //public string user_id;

}

[System.Serializable]
public class StrInventory
{
    public string data;
}
