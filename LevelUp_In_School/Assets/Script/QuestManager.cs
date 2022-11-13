using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class QuestManager : MonoBehaviour//퀘스트 관리 클래스
{
     
    public string questName;//퀘스트 내용
    
    public abstract void Quest_();//퀘스트 추상함수(이 클래스를 상속받은 클래스에서 퀘스트 내용을 정의해서 사용)
    public abstract bool CheckQuest();//퀘스트 진행도 체크 추상함수(이 클래스를 상속받은 클래스에서 함수 내용을 정의해서 사용)

    public abstract void QuestReset();//퀘스트 진행도 리셋 추상함수(이 클래스를 상속받은 클래스에서 함수 내용을 정의해서 사용)
    
}
