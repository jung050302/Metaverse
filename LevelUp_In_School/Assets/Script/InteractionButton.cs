using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractionButton : MonoBehaviour
{
    public Sprite img;
    bool btn;

    public GameObject player;
    public Sprite basicImg;

    public GameObject _collider;
    void Start()
    {
        btn = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (img != null)
        {
            GetComponent<Image>().sprite = img;
        }
        else
        {
            GetComponent<Image>().sprite = basicImg;
        }
    }
    public bool Btn()
    {
        return btn;
    }

    public void BtnDown()
    {
        btn = true;
        

    }
    public void BtnUp()
    {
        btn = false;
    }
    public void ButtonDown()
    {
        if (_collider != null)
        {
            if (_collider.gameObject.tag == "Entrance")
            {
                _collider.GetComponent<Entrance>().a = true;
            }
        }
    }
}
