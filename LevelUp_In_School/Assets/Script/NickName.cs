using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    //player
    [SerializeField] Camera _camera;
    public GameObject nameOBJ;
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        nameOBJ = GameObject.Find("name_");
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position != nameOBJ.transform.position)
        {
            this.transform.position = _camera.WorldToScreenPoint(new Vector2(nameOBJ.transform.position.x, nameOBJ.transform.position.y));
        }
         
    }
}
