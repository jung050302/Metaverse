using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Text _text;
    
    public void OkButtonDown()
    {
        gameObject.SetActive(false);//Ȯ�ι�ư�� ������ �˸�â�� ����
    }
}
