using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public bool stop;
    private void Awake()
    {
        
        
        stop = false;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            if (PlayerPrefs.HasKey("BGMvol") == true)
            {
                this.gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("BGMvol");
            }
        }
         
         
    }
}
