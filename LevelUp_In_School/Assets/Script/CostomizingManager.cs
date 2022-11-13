using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//using System.IO;

public class CostomizingManager : MonoBehaviour
{
    public GameObject white;//화면 전환용
    public GameObject player;//커스텀한 캐릭터를 프리팹으로 만들어주기 위한 변수

    public string[] key;
    public Sprite[] value;

    public SpriteRenderer Hair;//gpfapt
    public SpriteRenderer Body;//클로스바디
    public SpriteRenderer leftArm;//lcarm
    public SpriteRenderer rightArm;//Rcarm
    public SpriteRenderer rightLeg;//R_Cloth
    public SpriteRenderer leftLeg;//L_Cloth

    public GameObject hairContentObj;
    public GameObject clothContentObj;
    public GameObject pantsContentObj;

    public string playerHair;

    public string playerCloth;
    public string playerLArm;
    public string playerRArm;

    public string playerLPants;
    public string playerRPants;

    public GameObject Notification;
    public Text Notificationtext;
    public InputField nickName;


    //public GameObject clothScrollContent;
    //public GameObject pantsScrollContent;
    //public GameObject hairScrollContent;
    string userId;

    private void Awake()
    {
        print("test1:" + PlayerPrefs.GetString("Inventory"));
        print("test1.5:" + PlayerPrefs.HasKey("Inventory"));
        userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
        StartCoroutine(Inventory("http://43.201.142.6:5000/game/buy/" + userId));
         
    }
    void Start()
    {
        print("test2:" + PlayerPrefs.GetString("Inventory"));
        print("test2.5:" + PlayerPrefs.HasKey("Inventory"));
        playerHair = "";
        playerCloth = "";
        playerLArm = "";
        playerRArm = "";
         
        playerLPants = "";
        playerRPants = "";
        //print(LookFor("qwer", "qudtls"));
        
        for (int i = 0; i < PlayerPrefs.GetString("Inventory").Split(',').Length; i++)
        {
             
            GameObject btn;
            if (PlayerPrefs.GetString("Inventory").Split(',')[i] != "")
            {
                btn=Resources.Load<GameObject>(PlayerPrefs.GetString("Inventory").Split(',')[i]);
                //print(PlayerPrefs.GetString("Inventory").Split(',')[i]);
                
                if (btn.GetComponent<Closet>().type == "Cloth")
                {
                    Instantiate(btn, clothContentObj.transform);
                    
                }
                else if (btn.GetComponent<Closet>().type == "Hair")
                {
                    Instantiate(btn, hairContentObj.transform);
                    
                }
                else if (btn.GetComponent<Closet>().type == "Pants")
                {
                    Instantiate(btn, pantsContentObj.transform);
                   
                }
            }
             
        }
    }

    // Update is called once per frame
    
    public void StartButton()//커스터마이징 씬에서 완료 버튼을 눌렀다면
    {
        Lst l = new Lst();
        string[] lst = l._arr;
        
         

        if (nickName.text != "")
        {
            for (int i = 0; i < lst.Length; i++)
            {

                if (LookFor(nickName.text, lst[i]))
                {

                    Notification.SetActive(true);
                    Notificationtext.text = "이 닉네임을 사용하실수 없습니다.";
                    return;

                }
            }
            Save("Cloth", playerCloth);
            Save("LArm", playerLArm);
            Save("RArm", playerRArm);
            Save("LPants", playerLPants);
            Save("RPants", playerRPants);
            Save("Hair", playerHair);
            Save("NickName", nickName.text);
            white.SetActive(true);
        }
        
        else
        {
            Notification.SetActive(true);
            Notificationtext.text = "닉네임을 입력해주세요";
        }
         
         
    }

    private void OnApplicationQuit()
    {
        
        PlayerPrefs.DeleteKey("Inventory");
    }
    public void Cloth1()
    {
        Body.sprite = ReturnValue("cloth1");
        leftArm.sprite = ReturnValue("lArm1");
        rightArm.sprite = ReturnValue("RArm1");
         
        
        playerCloth = "cloth1";
        playerLArm = "lArm1";
        playerRArm = "RArm1";
    }
    public void Cloth2()
    {
        Body.sprite = ReturnValue("cloth2");
        leftArm.sprite = ReturnValue("lArm2");
        rightArm.sprite = ReturnValue("RArm2");
        playerCloth = "cloth2";
        playerLArm = "lArm2";
        playerRArm = "RArm2";
    }
    public void Cloth3()
    {
        Body.sprite = ReturnValue("cloth3");
        leftArm.sprite = ReturnValue("lArm3");
        rightArm.sprite = ReturnValue("RArm3");
        playerCloth = "cloth3";
        playerLArm = "lArm3";
        playerRArm = "RArm3";
    }
    public void Cloth4()
    {
        Body.sprite = ReturnValue("cloth4");
        leftArm.sprite = ReturnValue("lArm4");
        rightArm.sprite = ReturnValue("RArm4");
        playerCloth = "cloth4";
        playerLArm = "lArm4";
        playerRArm = "RArm4";
    }
    public void Pants1()
    {
        leftLeg.sprite = ReturnValue("lPants1");
        rightLeg.sprite = ReturnValue("RPants1");
         
        playerLPants = "lPants1";
        playerRPants = "RPants1";
    }
    public void Pants2()
    {
        leftLeg.sprite = ReturnValue("lPants2");
        rightLeg.sprite = ReturnValue("RPants2");
        playerLPants = "lPants2";
        playerRPants = "RPants2";
    }
    public void Pants3()
    {
        leftLeg.sprite = ReturnValue("lPants3");
        rightLeg.sprite = ReturnValue("RPants3");
        playerLPants = "lPants3";
        playerRPants = "RPants3";
    }
    public void Hair1Down()
    {
        Hair.sprite = ReturnValue("hair1");
        playerHair = "hair1";
        
    }
    public void Hair2Down()
    {
        Hair.sprite = ReturnValue("hair2");
        playerHair = "hair2";
    }
    void Save(string key, string value)
    {
        PlayerPrefs.SetString(key,value);
    }

    public Sprite ReturnValue(string _key)
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (key[i] == _key)
            {
                return value[i];
            }
        }
        return null;

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
            print("리폰데타:" + response.data);
        }
    }
    bool LookFor(string a, string b)
    {
        bool c = false;
        int index = 0;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == b[0])
            {
                index++;

                    for (int j = 1; j < b.Length; j++)
                    {
                        //print(i + j);
                        //print("bool:"+(i + j > a.Length));
                        if (i + j < a.Length && a[i + j] == b[j])
                        {
                            index++;
                        }
                        else
                        {
                            break;
                        }
                    
                }
                
                 
                if (index == b.Length)
                {
                    c = true;
                    index = 0;
                    break;
                }
                else
                {
                    index = 0;
                    c = false;
                }

            }

        }

        return c;
    }
}
