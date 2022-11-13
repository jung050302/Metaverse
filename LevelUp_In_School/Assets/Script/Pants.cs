using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pants : Closet
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
     
    public void PantsButton()
    {
         
        if (SceneManager.GetActiveScene().name == "Customizing")
        {
            cm.playerLPants = LPants.name;
            cm.playerRPants = RPants.name;
            GameObject.Find("_9R_Cloth").GetComponent<SpriteRenderer>().sprite = RPants;
            GameObject.Find("_4L_Cloth").GetComponent<SpriteRenderer>().sprite = LPants;
        }
        else
        {
            _cm.playerLPants = LPants.name;
            _cm.playerRPants = RPants.name;
            GameObject.Find("mannequin_9R_Cloth").GetComponent<SpriteRenderer>().sprite = RPants;
            GameObject.Find("mannequin_4L_Cloth").GetComponent<SpriteRenderer>().sprite = LPants;
        }
    }
}
