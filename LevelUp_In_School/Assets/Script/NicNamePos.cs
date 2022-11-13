using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NicNamePos : MonoBehaviour
{
    public GameObject player;
    public Camera _camera;
    void Start()
    {
        this.gameObject.transform.position =_camera.WorldToScreenPoint(new Vector2(player.gameObject.transform.position.x, player.gameObject.transform.position.y-1.5f));
    }

    
}
