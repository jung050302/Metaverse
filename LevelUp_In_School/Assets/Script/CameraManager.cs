using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    public Transform pos4;

    public GameObject player;
    public float speed;
    Vector3 playerPos;
    //static public CameraManager Instance;

    void Start()
    {
        speed = 5f;
        //if (Instance != null)
        //{
        //    Destroy(gameObject);
             
        //}
        //else
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
         
    }


    void Update()
    {

        if (player != null)
        {
            playerPos.Set(player.transform.position.x, player.transform.position.y, this.transform.position.z);
            this.transform.position = Vector3.Lerp(this.transform.position, playerPos, speed * Time.deltaTime);
        }
        else
        {
            player = GameObject.Find("Player");
        }

        //if (pos1.position.x > this.transform.position.x)
        //{
        //    this.gameObject.transform.position = new Vector3(pos1.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        //}
        //if (pos2.position.x < this.transform.position.x)
        //{
        //    this.gameObject.transform.position = new Vector3(pos2.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);

        //}
        //if (pos3.position.y < this.transform.position.y)
        //{
        //    this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, pos3.position.y, this.gameObject.transform.position.z);
        //}
        //if (pos4.position.y > this.transform.position.y)
        //{
        //    this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, pos4.position.y, this.gameObject.transform.position.z);
        //}

    }
}
