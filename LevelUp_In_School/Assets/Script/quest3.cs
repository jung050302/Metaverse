using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest3 : QuestManager
{
    public int count;
    public bool quest = false;
    void Start()
    {

        questName = "��������Ʈ 2�� �Ϸ��ϱ�/" + count;
    }
    private void Update()
    {
        if (quest)
        {
            questName = "��������Ʈ 2�� �Ϸ��ϱ�/" + count;
        }
    }

    public override void Quest_()
    {
        quest = true;
    }
    public override bool CheckQuest()
    {
        if (quest)
        {
             
            if (count == 2)
            {
                quest = false;
                return true;
            }
             
        }
        return false;
    }
    public override void QuestReset()
    {
        count = 0;
        quest = false;
    }
}
