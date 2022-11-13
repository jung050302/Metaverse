using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hair : Closet
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Customizing")
        {
            cm = GameObject.Find("CostomizingManager").GetComponent<CostomizingManager>();
        }
        else
        {
            _cm = GameObject.Find("Closet").GetComponent<ClosetManager>();//.transform.Find("Coloset")
        }
    }
    public void HairButton()
    {
         
        if (SceneManager.GetActiveScene().name == "Customizing")
        {
            cm.playerHair = Hair.name;
            GameObject.Find("11_Helmet1").GetComponent<SpriteRenderer>().sprite = Hair;
        }
        else
        {
             
            _cm.playerHair = Hair.name;
            GameObject.Find("mannequin11_Helmet1").GetComponent<SpriteRenderer>().sprite = Hair;
        }
    }
}
