using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public Text _text;
    
    public void OkButtonDown()
    {
        gameObject.SetActive(false);//확인버튼을 누르면 알림창을 닫음
    }
}
