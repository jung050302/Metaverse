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

    public void ButtonDown()//x��ư
    {
        if (true_gameObj.Length!=0)//��ư�� �������� ����Ʈ�� �ִ� ��� ������Ʈ���� Ȱ����ȭ����
        {
            for (int i = 0; i < true_gameObj.Length; i++)
            {
                true_gameObj[i].SetActive(true);
            }
        }
        if (false_gameObj.Length!=0)//����Ʈ�� �ִ� ��� ������Ʈ���� ��Ȱ��ȭ ���ش�
        {
            for (int i = 0; i < false_gameObj.Length; i++)
            {
                false_gameObj[i].SetActive(false);
            }
        }
         
    }
}
