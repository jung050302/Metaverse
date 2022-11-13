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
    

    public SpriteRenderer Hair;//gpfapt���
    public SpriteRenderer Body;//Ŭ�ν��ٵ�
    public SpriteRenderer leftArm;//lcarm
    public SpriteRenderer rightArm;//Rcarm
    public SpriteRenderer rightLeg;//R_Cloth
    public SpriteRenderer leftLeg;//L_Cloth

    public bool roomManager;
    

    //��ųʸ��� ����Ƽ �ν����Ϳ��� �����Ҽ���� ���� ���
    public List<string> key;
    public List<Sprite> value;

    void Start()
    {
        roomManager= System.Convert.ToBoolean(PlayerPrefs.GetInt("RoomManager"));
        
        Load();

        
        
        
    }

    
    public void Load()
    {
        print("�ε� ȣ��");
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
        //string������ key ����Ʈ�� �ִ� ���ڿ��� �Ű������� �Է¹��� keys
        //�� ���ڿ��� ���ٸ� keys�� ���� key����Ʈ���ִ� ���ڿ�
        //�� ���� ��ġ���ִ� sprite(�̹���)������ ����Ʈ�� ����� ��������Ʈ�� �����´�
        
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
