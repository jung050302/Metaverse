using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonManager : MonoBehaviour
{
    
    //�α���
    public GameObject loginObj;//�α��� �ý��� ������Ʈ
    public GameObject signupObj;//ȸ������ �ý��� ������Ʈ
    //------
    public GameObject white;//ȭ�� ��ȯ��
    //Ŀ��
    public GameObject hairimg;// 
    public GameObject clothimg;//Ŀ���͸���¡ ���̹���
     
    public GameObject pants;
    //-----
    public GameObject _text;

    public GameObject shopObj;
    
    public void LoginButtonDown()//�α��� ��ư�� ��������
    {
        
        _text.SetActive(true);
        _text.GetComponent<StartText>().loginCheak = true;


    }
    public void SignupButtonDown()//ȸ������ ��ư�� �����ٸ� ȸ������ â�� ������
    {
        signupObj.SetActive(true);
        loginObj.SetActive(false);
    }
    public void HairButtonDown()//Ŀ���͸���¡ ������ Face��ư�� ��������
    {
        pants.SetActive(false);
        clothimg.SetActive(false);
        hairimg.SetActive(true);
    }
    public void ClothButtonDown()//Ŀ���͸���¡ ������ Cloth��ư�� ��������
    {
        pants.SetActive(false) ;
        hairimg.SetActive(false);
        clothimg.SetActive(true);
    }
    public void PantsButtonDown()
    {
        hairimg.SetActive(false);
        clothimg.SetActive(false);
        pants.SetActive(true);
    }
     
    public void ShopButton()
    {
        shopObj.SetActive(true);
    }
}
