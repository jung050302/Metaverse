using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public GameObject[] true_gameObj;
    public GameObject[] false_gameObj;
    void Start()
    {
        
    }

    public void ButtonDown()//x버튼
    {
        if (true_gameObj.Length!=0)//버튼을 눌렀을때 리스트에 있는 모든 오브젝트들을 활성ㅇ화해줌
        {
            for (int i = 0; i < true_gameObj.Length; i++)
            {
                true_gameObj[i].SetActive(true);
            }
        }
        if (false_gameObj.Length!=0)//리스트에 있는 모든 오브젝트들을 비활성화 해준다
        {
            for (int i = 0; i < false_gameObj.Length; i++)
            {
                false_gameObj[i].SetActive(false);
            }
        }
         
    }
}
