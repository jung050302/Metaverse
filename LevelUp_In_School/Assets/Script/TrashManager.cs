using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashManager : QuestManager
{
    

    public bool quest;
    public GameObject[] _trashLst = new GameObject[10];
    public List<GameObject> trashLst;
    public GameObject _btn;
    bool coolTime;

    public int trashCount;
    public GameObject trash;
    void Start()
    {
        quest = false;
        coolTime = false;
        questName = "운동장에 떯어진 쓰레기 줍기/" + trashCount;
        //questName = "구구단 출력하는 프로그램 작성후 코드제출하기";
    }

    // Update is called once per frame
    void Update()
    {
         

        if (quest)
        {
            questName = "운동장에 떯어진 쓰레기 줍기/" + trashCount;
            //questName = "운동장에 떯어진 쓰레기 줍기/" + trashCount;
            if (_btn.GetComponent<InteractionButton>().Btn())
            {
                if (!coolTime)
                {
                    if (trashLst.Count > 0)
                    {
                        trashLst[0].GetComponent<Trash>().interaction = true;
                        coolTime = true;
                    }
                }
            }
            else
            {
                coolTime = false;
            }
        }
         
    }

    public override void Quest_()
    {
        if (SceneManager.GetActiveScene().name == "Playground")
        {
            quest = true;
            for (int i = 0; i < 10; i++)
            {
                //-39,21 34,-21
                GameObject trashPre = Instantiate(trash);
                trashPre.transform.position = new Vector2(UnityEngine.Random.Range(-27.56f, 25.19f), UnityEngine.Random.Range(14.93f, -14.93f));
                trashPre.gameObject.name = "trash" + i.ToString();
                _trashLst[i] = trashPre;
            }
            for (int i = 0; i < _trashLst.Length; i++)
            {
                _trashLst[i] = GameObject.Find("trash" + i.ToString());
            }
        }
        else
        {
            return;
        }
    }
    public override bool CheckQuest()
    {
        if (trashCount == 10)
        {
            if (GameObject.Find("quest3").GetComponent<quest3>().quest)
            {
                GameObject.Find("quest3").GetComponent<quest3>().count++;
            }
            quest = false;
             
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void QuestReset()
    {
         
        for (int i = 0; i < _trashLst.Length; i++)
        {
            print("쓰레기 초기화");
            Destroy(GameObject.Find("trash" + i.ToString()));
        }
        trashCount = 0;
        quest = false;
    }
}
