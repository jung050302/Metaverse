using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class QuestManager : MonoBehaviour//����Ʈ ���� Ŭ����
{
     
    public string questName;//����Ʈ ����
    
    public abstract void Quest_();//����Ʈ �߻��Լ�(�� Ŭ������ ��ӹ��� Ŭ�������� ����Ʈ ������ �����ؼ� ���)
    public abstract bool CheckQuest();//����Ʈ ���൵ üũ �߻��Լ�(�� Ŭ������ ��ӹ��� Ŭ�������� �Լ� ������ �����ؼ� ���)

    public abstract void QuestReset();//����Ʈ ���൵ ���� �߻��Լ�(�� Ŭ������ ��ӹ��� Ŭ�������� �Լ� ������ �����ؼ� ���)
    
}
