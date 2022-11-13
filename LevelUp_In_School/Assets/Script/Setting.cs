using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{

    public GameObject[] btnLst;
    public GameObject[] optionLst;

    public GameObject sound;
    public GameObject account;

    public Slider bgmVol;
    public Slider soundVol;
    public Text bgmVolText;
    public Text soundVolText;
    void Start()
    {
        if (PlayerPrefs.HasKey("BGMvol") == true)
        {
            bgmVol.value = PlayerPrefs.GetFloat("BGMvol");
        }
        if (PlayerPrefs.HasKey("SoundVol") == true)
        {
            soundVol.value = PlayerPrefs.GetFloat("SoundVol");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        bgmVolText.text = (Math.Round(bgmVol.value * 10,1)).ToString();
        soundVolText.text = (Math.Round(soundVol.value * 10, 1)).ToString();
        PlayerPrefs.SetFloat("BGMvol", bgmVol.value);
        PlayerPrefs.SetFloat("SoundVol", soundVol.value);
        
    }

    public void Sound()
    {
        BtnColor(this.gameObject.name);
        OptionActive();
        btnLst[0].GetComponent<Image>().color = new Color(0.6941177f, 1, 1, 1); ;
        sound.SetActive(true);
    }
    public void Account()
    {
        BtnColor(this.gameObject.name);
        OptionActive();
        btnLst[1].GetComponent<Image>().color = new Color(0.6941177f, 1, 1, 1); ;
        account.SetActive(true);
    }

    void BtnColor(string name)//버튼을 눌렀을때 매개변수에 해당하는 버튼을 제외한 모든 버튼을 1,1,1,1(하얀색)으로 바꿔줌
    {
        for(int i = 0; i < btnLst.Length; i++)
        {
            if (btnLst[i].gameObject.name!= name && btnLst[i].GetComponent<Image>().color != new Color(1, 1, 1, 1))
            {
                btnLst[i].GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                continue;
            }
        }
    }
    void OptionActive()//모든 옵션 창을 비활성화 해준다
    {
        for(int i=0;i< optionLst.Length; i++)
        {
            if (optionLst[i].activeSelf == true)
            {
                optionLst[i].SetActive(false);
            }
            else
            {
                continue;
            }
        }
    }

    
}
