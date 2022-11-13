using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartText : MonoBehaviour
{
    //[SerializeField]private Text _text;
    public GameObject login;
    public bool loginCheak;
    public GameObject white;
    void Start()
    {
        loginCheak = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!loginCheak)
        {
             if (Input.touchCount > 0)
             {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    login.SetActive(true);
                    gameObject.SetActive(false);

                }
             }
                 
        }
        if (loginCheak)
        {
            login.SetActive(false);
            //Input.GetTouch(0).phase==TouchPhase.Ended
            if (Input.touchCount > 0 )
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {

                    //white.GetComponent<FadeOut>()._scenename = "Lobby";
                    white.SetActive(true);
                    gameObject.SetActive(false);


                }
            }
              
        }

        if (!loginCheak)
        {
            
                if (Input.GetMouseButtonDown(0))
                {
                    login.SetActive(true);
                    gameObject.SetActive(false);

                }
            

        }
        if (loginCheak)
        {
            login.SetActive(false);
            //Input.GetTouch(0).phase==TouchPhase.Ended
            
                if (Input.GetMouseButtonDown(0))
                {

                    
                    white.SetActive(true);
                    gameObject.SetActive(false);


                }
            

        }
    }

     
     
    
}
