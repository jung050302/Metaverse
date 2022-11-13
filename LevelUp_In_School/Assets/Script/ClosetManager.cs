using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosetManager : MonoBehaviour
{
    public string[] key;
    public Sprite[] value;

    public SpriteRenderer mannequinHair;//gpfapt헬멧
    public SpriteRenderer mannequinBody;//클로스바디
    public SpriteRenderer mannequinleftArm;//lcarm
    public SpriteRenderer mannequinrightArm;//Rcarm
    public SpriteRenderer mannequinrightLeg;//R_Cloth
    public SpriteRenderer mannequinleftLeg;//L_Cloth

    public SpriteRenderer Hair;//gpfapt헬멧
    public SpriteRenderer Body;//클로스바디
    public SpriteRenderer leftArm;//lcarm
    public SpriteRenderer rightArm;//Rcarm
    public SpriteRenderer rightLeg;//R_Cloth
    public SpriteRenderer leftLeg;//L_Cloth

    public GameObject canvas;
    public GameObject closet;
    public GameManager gameManager;
    public InputField nameText;
    public GameObject player;

    public string playerCloth;
    public string playerLArm;
    public string playerRArm;


    public string playerLPants;
    public string playerRPants;

    public string playerHair;



    public GameObject hairContentObj;
    public GameObject clothContentObj;
    public GameObject pantsContentObj;

    public GameObject hairObj;
    public GameObject clothObj;
    public GameObject pantsObj;

    public Notification notification;

    public GameObject aaaaa;
    List<GameObject> btnLst=new List<GameObject>();
    private void OnEnable()
    {
        canvas.SetActive(false);
        player.SetActive(false);
        nameText.text= PlayerPrefs.GetString("NickName");
        
        playerHair = PlayerPrefs.GetString("Hair");
        playerCloth = PlayerPrefs.GetString("Cloth");
        playerLArm = PlayerPrefs.GetString("LArm");
        playerRArm = PlayerPrefs.GetString("RArm");

        playerLPants = PlayerPrefs.GetString("LPants");
        playerRPants = PlayerPrefs.GetString("RPants");

        

        mannequinHair.sprite = ReturnValue(PlayerPrefs.GetString("Hair"));
        mannequinBody.sprite = ReturnValue(PlayerPrefs.GetString("Cloth"));
        mannequinleftArm.sprite = ReturnValue(PlayerPrefs.GetString("LArm"));
        mannequinrightArm.sprite = ReturnValue(PlayerPrefs.GetString("RArm"));
        mannequinrightLeg.sprite = ReturnValue(PlayerPrefs.GetString("LPants"));
        mannequinleftLeg.sprite = ReturnValue(PlayerPrefs.GetString("RPants"));
         
        for (int i = 0; i < PlayerPrefs.GetString("Inventory").Split(',').Length-1; i++)
        {



            GameObject btn;
            if (PlayerPrefs.GetString("Inventory").Split(',')[i] != "" && !FindLst(PlayerPrefs.GetString("Inventory").Split(',')[i] + "(Clone)"))
            {
                btn = Resources.Load<GameObject>(PlayerPrefs.GetString("Inventory").Split(',')[i]);
                 
                 
                if (btn.GetComponent<Closet>().type == "Cloth")
                {
                    
                    GameObject _btn = Instantiate(btn, clothContentObj.transform);
                    btnLst.Add(_btn);
                }
                else if (btn.GetComponent<Closet>().type == "Hair")
                {

                    GameObject _btn = Instantiate(btn, hairContentObj.transform);
                    btnLst.Add(_btn);
                }
                else if (btn.GetComponent<Closet>().type == "Pants")
                {

                    GameObject _btn = Instantiate(btn, pantsContentObj.transform);
                    btnLst.Add(_btn);
                }
            }
             
            
        }
        hairObj.SetActive(false);
        pantsObj.SetActive(false);
    }
    
    bool FindLst(string obj)
    {
        for(int i = 0; i < btnLst.Count; i++)
        {
            if (btnLst[i].gameObject.name == obj)
            {
                 
                return true;
            }
        }
         
        return false;
    }

    public void Cloth1()
    {
        print("산타복장 누름");
        mannequinBody.sprite = ReturnValue("cloth1");
        mannequinleftArm.sprite = ReturnValue("lArm1");
        mannequinrightArm.sprite = ReturnValue("RArm1");
        

        playerCloth = "cloth1";
        playerLArm = "lArm1";
        playerRArm = "RArm1";
    }
    public void Cloth2()
    {
        mannequinBody.sprite = ReturnValue("cloth2");
        mannequinleftArm.sprite = ReturnValue("lArm2");
        mannequinrightArm.sprite = ReturnValue("RArm2");
        playerCloth = "cloth2";
        playerLArm = "lArm2";
        playerRArm = "RArm2";
    }
    public void Cloth3()
    {
        mannequinBody.sprite = ReturnValue("cloth3");
        mannequinleftArm.sprite = ReturnValue("lArm3");
        mannequinrightArm.sprite = ReturnValue("RArm3");
        playerCloth = "cloth3";
        playerLArm = "lArm3";
        playerRArm = "RArm3";
    }
    public void Cloth4()
    {
        mannequinBody.sprite = ReturnValue("cloth4");
        mannequinleftArm.sprite = ReturnValue("lArm4");
        mannequinrightArm.sprite = ReturnValue("RArm4");
        playerCloth = "cloth4";
        playerLArm = "lArm4";
        playerRArm = "RArm4";
    }
    public void Pants1()
    {
        mannequinleftLeg.sprite = ReturnValue("lPants1");
        mannequinrightLeg.sprite = ReturnValue("RPants1");

        playerLPants = "lPants1";
        playerRPants = "RPants1";
    }
    public void Pants2()
    {
        mannequinleftLeg.sprite = ReturnValue("lPants2");
        mannequinrightLeg.sprite = ReturnValue("RPants2");
        playerLPants = "lPants2";
        playerRPants = "RPants2";
    }
    public void Pants3()
    {
        mannequinleftLeg.sprite = ReturnValue("lPants3");
        mannequinrightLeg.sprite = ReturnValue("RPants3");
        playerLPants = "lPants3";
        playerRPants = "RPants3";
    }
    public void Hair1Down()
    {
        mannequinHair.sprite = ReturnValue("hair1");
        playerHair = "hair1";

    }
    public void Hair2Down()
    {
        mannequinHair.sprite = ReturnValue("hair2");
        playerHair = "hair2";
    }
    void Save(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    public void CompletionButton()
    {
        Lst l = new Lst();
        string[] lst = l._arr;
        for(int i = 0; i < lst.Length; i++)
        {
            if (LookFor(nameText.text, lst[i]))
            {
                notification.gameObject.SetActive(true);
                notification._text.text = "이 닉네임을 사용하실수 없습니다.";


                return;
            }
        }
        clothObj.SetActive(true);
        hairObj.SetActive(false);
        pantsObj.SetActive(false);
        player.SetActive(true);
        player.GetComponent<Player>()._name = nameText.text;
        this.gameObject.SetActive(false);
        canvas.SetActive(true);
        Save("Cloth", playerCloth);
        Save("LArm", playerLArm);
        Save("RArm", playerRArm);
        Save("LPants", playerLPants);
        Save("RPants", playerRPants);
        Save("Hair", playerHair);
        Save("NickName", nameText.text);
        gameManager.Load();
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

    public void closetButton()
    {
        closet.SetActive(true);
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
