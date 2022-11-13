using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonManager : MonoBehaviour
{
    
    //로그인
    public GameObject loginObj;//로그인 시스템 오브젝트
    public GameObject signupObj;//회원가입 시스템 오브젝트
    //------
    public GameObject white;//화면 전환용
    //커마
    public GameObject hairimg;// 
    public GameObject clothimg;//커스터마이징 옷이미지
     
    public GameObject pants;
    //-----
    public GameObject _text;

    public GameObject shopObj;
    
    public void LoginButtonDown()//로그임 버튼을 눌렀을때
    {
        
        _text.SetActive(true);
        _text.GetComponent<StartText>().loginCheak = true;


    }
    public void SignupButtonDown()//회원가입 버튼을 누른다면 회원가입 창을 보여줌
    {
        signupObj.SetActive(true);
        loginObj.SetActive(false);
    }
    public void HairButtonDown()//커스터마이징 씬에서 Face버튼을 눌렀을떄
    {
        pants.SetActive(false);
        clothimg.SetActive(false);
        hairimg.SetActive(true);
    }
    public void ClothButtonDown()//커스터마이징 씬에서 Cloth버튼을 눌렀을떄
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
