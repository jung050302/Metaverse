using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Quest : MonoBehaviour
{
    public GameObject comPlet1;
    public GameObject comPlet2;
    public GameObject comPlet3;

    public GameObject completion;
    public GameObject questObj;

    public GameObject trash;
    public Text questText1;
    public Text questText2;
    public Text questText3;

    public GameObject player;
    public GameObject quest;

    public List<QuestManager> checkQuest = new List<QuestManager>();

    //쓰레기줍기 퀘스트 변수
    public int trashCount = 0;
    public bool greetings;
    public GameObject message;

    bool CoolTime;
    bool _CoolTime;

    bool quest1;
    bool quest2;
    bool quest3;
    int rdm;
    int rdm2;
    int rdm3;
    DateTime startTime;
    DateTime nowTime;

    string day;
    string nowDate;

    bool dailyQuest;

    //static public Quest Instance;


    void Awake()
    {
        
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Rdm1") == true && PlayerPrefs.HasKey("Rdm2") == true && PlayerPrefs.HasKey("Rdm3") == true)
        {
            rdm = PlayerPrefs.GetInt("Rdm1");
            rdm2 = PlayerPrefs.GetInt("Rdm2");
            rdm3 = PlayerPrefs.GetInt("Rdm3");
             
        }

        dailyQuest = Load("dailyQuest");
        if (PlayerPrefs.HasKey("TodayDay") == false)
        {
            day = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("dd") + " 00:00:00";
            PlayerPrefs.SetString("TodayDay", day);
        }
        if (PlayerPrefs.HasKey("TodayDay") == true)
        {
            day = PlayerPrefs.GetString("TodayDay");
        }
        //print(day);
        startTime = Convert.ToDateTime(day);
         
        quest1 = Load("quest1");
        quest2 = Load("quest2");
        quest3 = Load("quest3");

        CoolTime = false;//Load("CoolTime");
        //print("쿨"+CoolTime);
        //_CoolTime = Load("_CoolTime");
         
        if (quest1)
        {
            
            questText1.text = checkQuest[rdm].questName;

            questText1.color = new Color(questText1.color.r, questText1.color.g, questText1.color.b, 0.25f);
            comPlet1=Instantiate(completion, questObj.transform);
            comPlet1.transform.position = questText1.gameObject.transform.position;

        }
        if (quest2)
        {
             
            questText2.text = checkQuest[rdm2].questName;

            questText2.color = new Color(questText2.color.r, questText2.color.g, questText2.color.b, 0.25f);
            comPlet2=Instantiate(completion, questObj.transform);
            comPlet2.transform.position = questText2.gameObject.transform.position;

        }
        if (quest3)
        {
             
            questText3.text = checkQuest[rdm3].questName;

            questText3.color = new Color(questText3.color.r, questText3.color.g, questText3.color.b, 0.25f);
            comPlet3=Instantiate(completion, questObj.transform);
            comPlet3.transform.position = questText3.gameObject.transform.position;

        }
        greetings = false;

        //if (Instance != null)
        //{
        //    Destroy(gameObject);
             
        //}
        //else
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
         
    }
    void Update()
    {
        //테스트용
        if (Input.GetKeyDown(KeyCode.O))
        {
            QuestReset();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            print("데이터 지움");
            PlayerPrefs.DeleteAll();
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.T))
        {
            print("날짜 데이터 지움");
            PlayerPrefs.DeleteKey("TodayDay");
        }
        nowDate = DateTime.Now.ToString("yyyy")+"-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("dd") + " " + DateTime.Now.ToString("HH")+":" + DateTime.Now.ToString("mm") + ":" + DateTime.Now.ToString("ss");
       
        nowTime = Convert.ToDateTime(nowDate);
        
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            QuestReset();
        }
        if (nowTime >= startTime.AddDays(+1))
        {
            QuestReset();
            //print("12시 지남");
        }
        else
        {
            //print("안지남");
        }
        if (!dailyQuest)
        {
            QuestManager();
            //print("다 안깸");
        }
        else
        {
            //print("다깸");
        }

    }
    
    public void QuestButton()
    {
        player.GetComponent<Player>().stop = true;
        quest.SetActive(true);
    }
    public void ExitButton()
    {
        player.GetComponent<Player>().stop = false;
        quest.SetActive(false);
    }
    void QuestManager()
    {
        if (!CoolTime)
        {
            Rdm1();

            

            questText1.text = checkQuest[rdm].questName;
            questText2.text = checkQuest[rdm2].questName;
            questText3.text = checkQuest[rdm3].questName;
            if (!quest1)
            {
                //print(checkQuest[rdm].questObj.name);
                checkQuest[rdm].Quest_();
                //Invoke(dic[rdm],0);
            }
            if (!quest2)
            {
                //print(checkQuest[rdm2].questObj.name);
                checkQuest[rdm2].Quest_();
                //Invoke(dic[rdm2], 0);
            }
           
            if (!quest3)
            {
                //print(checkQuest[rdm3].questObj.name);
                checkQuest[rdm3].Quest_();
                //Invoke(dic[rdm3],0);
            }
            
            CoolTime = true;

        }
        if (!_CoolTime)
        {

            if (!quest1)
            {
                questText1.text = checkQuest[rdm].questName;
                
                if (checkQuest[rdm].CheckQuest())
                {
                    player.GetComponent<Player>().AddExp((int)player.GetComponent<Player>().MaxExp * 7 / 100);
                    questText1.color = new Color(questText1.color.r, questText1.color.g, questText1.color.b, 0.25f);
                    comPlet1 = Instantiate(completion, questObj.transform);
                    comPlet1.transform.position = questText1.gameObject.transform.position;
                    quest1 = true;
                    Save("quest1", quest1);
                }
            }
            if (!quest2)
            {
                questText2.text = checkQuest[rdm2].questName;
                if (checkQuest[rdm2].CheckQuest())
                {
                    player.GetComponent<Player>().AddExp((int)player.GetComponent<Player>().MaxExp * 7 / 100);
                    questText2.color = new Color(questText2.color.r, questText2.color.g, questText2.color.b, 0.25f);
                    comPlet2 = Instantiate(completion, questObj.transform);
                    comPlet2.transform.position = questText2.gameObject.transform.position;
                    quest2 = true;
                    Save("quest2", quest2);
                }
            }
            if (!quest3)
            {
                questText3.text = checkQuest[rdm3].questName;
                if (checkQuest[rdm3].CheckQuest())
                { 
                    player.GetComponent<Player>().AddExp((int)player.GetComponent<Player>().MaxExp * 7 / 100);
                    questText3.color = new Color(questText3.color.r, questText3.color.g, questText3.color.b, 0.25f);
                    comPlet3 = Instantiate(completion, questObj.transform);
                    comPlet3.transform.position = questText3.gameObject.transform.position;
                    quest3 = true;
                    Save("quest3", quest3);
                }
            }
            if (quest1 && quest2 && quest3)
            {
                //_CoolTime = true;
                //Save("_CoolTime", _CoolTime);
                dailyQuest = true;
                Save("dailyQuest", dailyQuest);
                Save("CoolTime", CoolTime);
            }
        }
    }
    void Rdm1()
    {
        if (PlayerPrefs.HasKey("Rdm1") == true&& PlayerPrefs.HasKey("Rdm2") == true&& PlayerPrefs.HasKey("Rdm3") == true)
        {
            //rdm = PlayerPrefs.GetInt("Rdm1");
            //rdm2= PlayerPrefs.GetInt("Rdm2");
            //rdm3= PlayerPrefs.GetInt("Rdm3");
            return;
        }
        else
        {
            List<int> lst = new List<int>();
            //for (int i = 0; i < questLst.Length; i++)
            //{
            //    lst.Add(i);
            //}
            for (int i = 0; i < checkQuest.Count; i++)
            {
                lst.Add(i);
            }

            rdm = lst[UnityEngine.Random.Range(0, lst.Count)];
            lst.Remove(rdm);
            rdm2 = lst[UnityEngine.Random.Range(0, lst.Count)];
            lst.Remove(rdm2);
            rdm3 = lst[UnityEngine.Random.Range(0, lst.Count)];
            lst.Remove(rdm3);

            PlayerPrefs.SetInt("Rdm1", rdm);
            PlayerPrefs.SetInt("Rdm2", rdm2);
            PlayerPrefs.SetInt("Rdm3", rdm3);
        }
        
    }

    void Save(string Key,bool value)
    {

        PlayerPrefs.SetInt(Key, System.Convert.ToInt16(value));
    }
    private bool Load(string a)
    {
        return System.Convert.ToBoolean(PlayerPrefs.GetInt(a));
        
    }
    
    void QuestReset()
    {
        checkQuest[rdm].QuestReset();
        checkQuest[rdm2].QuestReset();
        checkQuest[rdm3].QuestReset();
        PlayerPrefs.DeleteKey("quest1");

        PlayerPrefs.DeleteKey("quest2");
        PlayerPrefs.DeleteKey("quest3");
         
        quest1 = false;
        quest2 = false;
        quest3 = false;
        
        //Save("quest1", quest1);
        //Save("quest2", quest2);
        //Save("quest3", quest3);

        PlayerPrefs.DeleteKey("Rdm1");
        
        PlayerPrefs.DeleteKey("CoolTime");
        CoolTime = false;
        //Save("CoolTime", CoolTime);
        PlayerPrefs.DeleteKey("_CoolTime");
        PlayerPrefs.DeleteKey("TodayDay");
        PlayerPrefs.DeleteKey("dailyQuest");
        dailyQuest = false;

        //Save("dailyQuest", dailyQuest);
        day = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.ToString("MM") + "-" + DateTime.Now.ToString("dd") + " 00:00:00";
        if (!quest1 && comPlet1 != null)
        {
            Destroy(comPlet1);
            questText1.color = new Color(questText1.color.r, questText1.color.g, questText1.color.b, 1);
            
        }
        if (!quest2 && comPlet2 != null)
        {
            Destroy(comPlet2);
            questText2.color = new Color(questText1.color.r, questText1.color.g, questText1.color.b, 1);
            
        }
        if (!quest3 && comPlet3 != null)
        {
            Destroy(comPlet3);
            questText3.color = new Color(questText1.color.r, questText1.color.g, questText1.color.b, 1);
            
        }
        startTime = Convert.ToDateTime(day);
        PlayerPrefs.SetString("TodayDay", day);
        
    }
}
