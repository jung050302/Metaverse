using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Entrance : MonoBehaviour
{
    public Sprite img;
    public Collider2D _collider;
    public GameObject _btn;
    public GameObject player;
    public GameObject _camera;
    public Transform movePos;

    

    bool btn;

    public bool a;

    public bool _classScene;
    public static Transform outPos;
    void Start()
    {
        a = false;
        _btn = GameObject.Find("InteractionButton");
        
        //if (SceneManager.GetActiveScene().name == "Playground" && PlayerPrefs.HasKey("Xpos"))
        //{
        //    print("ฟ๖วม");

        //    player.transform.position = new Vector2(PlayerPrefs.GetFloat("Xpos"), PlayerPrefs.GetFloat("Ypos"));//this.gameObject.transform.position;
             

        //    PlayerPrefs.DeleteKey("Xpos");
        //    PlayerPrefs.DeleteKey("Ypos");

        //}
    }

    
    void Update()
    {
        //btn = _btn.GetComponent<InteractionButton>().Btn();
        
        if (a)
        {
            if (_classScene)
            {
                player.transform.position = outPos.position;

                _camera.transform.position = new Vector3(outPos.position.x, outPos.position.y, -10);
                outPos = null;
                a = false;
                 
            }
            else
            {
                player.transform.position = movePos.position;

                _camera.transform.position = new Vector3(movePos.position.x, movePos.position.y, -10);
                outPos = this.gameObject.transform;
                a = false;
            }
             

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.name == "Player")
        {
            
            _btn.GetComponent<InteractionButton>().img = img;
            _btn.GetComponent<InteractionButton>()._collider = this.gameObject;
        }
         
        _collider = collision;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Player")
        {
            
            _btn.GetComponent<InteractionButton>().img = null;
            _btn.GetComponent<InteractionButton>()._collider = null;
        }
        
        _collider = null;
    }
}
