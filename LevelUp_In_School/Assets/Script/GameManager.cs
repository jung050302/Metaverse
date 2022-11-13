using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using System;

public class GameManager : MonoBehaviour
{
    public GameObject roomnum;
    public Text roomNumText;
    

    public SpriteRenderer Hair;//gpfapt헬멧
    public SpriteRenderer Body;//클로스바디
    public SpriteRenderer leftArm;//lcarm
    public SpriteRenderer rightArm;//Rcarm
    public SpriteRenderer rightLeg;//R_Cloth
    public SpriteRenderer leftLeg;//L_Cloth

    public bool roomManager;
    

    //딕셔너리를 유니티 인스펙터에서 정의할수없어서 쓰는 방법
    public List<string> key;
    public List<Sprite> value;

    void Start()
    {
        roomManager= System.Convert.ToBoolean(PlayerPrefs.GetInt("RoomManager"));
        
        Load();

        
        
        
    }

    
    public void Load()
    {
        print("로드 호출");
        Hair.sprite = ReturnValue(PlayerPrefs.GetString("Hair"));
        Body.sprite = ReturnValue(PlayerPrefs.GetString("Cloth"));
        leftArm.sprite = ReturnValue(PlayerPrefs.GetString("LArm"));
        rightArm.sprite = ReturnValue(PlayerPrefs.GetString("RArm"));
        leftLeg.sprite = ReturnValue(PlayerPrefs.GetString("LPants"));
        rightLeg.sprite = ReturnValue(PlayerPrefs.GetString("RPants"));
        //nickname.GetComponent<Text>().text = (PlayerPrefs.GetString("NickName") + " " + Lv.ToString() + "LV");
    }
    
     
    Sprite ReturnValue(string keys)
    {
        //string형식의 key 리스트에 있는 문자열이 매개변수로 입력받은 keys
        //의 문자열과 같다면 keys와 같은 key리스트에있는 문자열
        //과 같은 위치에있는 sprite(이미지)형식의 리스트에 저장된 스프라이트를 가져온다
        
         for(int i = 0; i < key.Count; i++)
         {
            if (key[i] == keys)
            {
                if (value[i] == null)
                {
                    
                }
                return value[i];
            }
         }
        
        return null;
    }

}
