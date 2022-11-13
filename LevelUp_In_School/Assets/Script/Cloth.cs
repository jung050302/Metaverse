using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cloth : Closet
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().name== "Customizing")
        {
            cm = GameObject.Find("CostomizingManager").GetComponent<CostomizingManager>();
        }
        else
        {
            _cm = GameObject.Find("Closet").GetComponent<ClosetManager>();//.transform.Find("Coloset")
        }
         
    }
    public void ClothButton()
    {
        if(SceneManager.GetActiveScene().name== "Customizing")
        {
            cm.playerCloth = cloth.name;
            cm.playerLArm = LArm.name;
            cm.playerRArm = RArm.name;
            GameObject.Find("ClothBody").GetComponent<SpriteRenderer>().sprite = cloth;
            GameObject.Find("21_LCArm").GetComponent<SpriteRenderer>().sprite = LArm;
            GameObject.Find("-19_RCArm").GetComponent<SpriteRenderer>().sprite = RArm;
        }
        else
        {

            _cm.playerCloth = cloth.name;
            _cm.playerLArm = LArm.name;
            _cm.playerRArm = RArm.name;
            GameObject.Find("mannequinClothBody").GetComponent<SpriteRenderer>().sprite = cloth;
            GameObject.Find("mannequin21_LCArm").GetComponent<SpriteRenderer>().sprite = LArm;
            GameObject.Find("mannequin-19_RCArm").GetComponent<SpriteRenderer>().sprite = RArm;
        }
         
    }
}
