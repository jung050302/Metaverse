using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FadeOut : MonoBehaviour
{
    //화면전환 효과 FadeOut효과
    public Image white;//하얀색 이미지;
    public string _scenename;//씬이름을 유니티엔진 inspector창에서 씬이름 입력을하여 변수를 할당
    int index;
    public GameObject bgm;
     
    void Start()
    {
        

        StartCoroutine(Fadeout());
    }

   
    public IEnumerator Fadeout()
    {

        if (bgm != null)
        {
            bgm.GetComponent<BGM>().stop = true;
        }

        while (true)
        {
            
            if (bgm != null)
            {
                if (white.color.a >= 1.0f && bgm.GetComponent<AudioSource>().volume <= 0)
                {
                    if (bgm != null)
                    {
                        bgm.GetComponent<BGM>().stop = false;
                    }
                    bgm.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("BGMvol");
                    break;
                }
            }
            else
            {
                if (white.color.a <= 0.0f)
                {
                    if (bgm != null)
                    {
                        bgm.GetComponent<BGM>().stop = false;
                    }
                    print("화면전환");
                    break;
                }
            }
            if (bgm != null)
            {
                bgm.GetComponent<BGM>().stop = true;
            }

            if (bgm != null)
            {
                if (bgm.GetComponent<AudioSource>().volume <= PlayerPrefs.GetFloat("BGMvol") && PlayerPrefs.GetFloat("BGMvol") != 0)
                {
                    bgm.GetComponent<AudioSource>().volume -= Time.deltaTime / 2;
                }
                else
                {
                    bgm.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("BGMvol");
                }
            }
            white.color = new Color(white.color.r, white.color.g, white.color.b, white.color.a + (Time.deltaTime / 2));


            yield return null;
        }
         
        if (_scenename!="")
        {
            //만약 변수가 비어있지 않다면 해당 씬으로 화면을 전환한다
            SceneManager.LoadScene(_scenename );
             
        }
    }
}
