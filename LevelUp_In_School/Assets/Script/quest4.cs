using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest4 : QuestManager
{
    public Sprite btnImg;
    public GameObject img;
    bool quest;
    public InteractionButton _btn;
    Collider2D col;
    bool completion;
    void Start()
    {
        completion = false;
        quest = false;
        questName = "Ä¥ÆÇ ´Û±â";
    }
    private void Update()
    {
        if (quest)
        {
            
            if (_btn.Btn()&&col.gameObject.name=="Player")
            {
                _btn.GetComponent<InteractionButton>().img = null;
                img.SetActive(false);
                completion = true;
                quest = false;
            }
        }
    }
    public override void Quest_()
    {
         
        img.SetActive(true);
        quest = true;
    }
    public override bool CheckQuest()
    {
         
        return completion;
    }
    public override void QuestReset()
    {
         
        completion = false;
        quest = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _btn.img = btnImg;
        col = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _btn.img = null;
        col = null;
    }
}
