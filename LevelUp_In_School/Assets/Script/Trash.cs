using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trash : MonoBehaviour
{
    public Sprite img;
    GameObject trashManager;

    public bool interaction;
    public TrashManager quest;
    AudioSource _audio;
    public AudioClip trashSound;
    SpriteRenderer sp;
    GameObject btn;
    void Start()
    {
        trashManager = GameObject.Find("TrashManager");
        sp = GetComponent<SpriteRenderer>();
        quest = GameObject.Find("TrashManager").GetComponent<TrashManager>();
        interaction = false;
        _audio = GetComponent<AudioSource>();
        btn = GameObject.Find("InteractionButton");
    }

    // Update is called once per frame
    Collider2D _collider;
    void Update()
    {
        if (PlayerPrefs.HasKey("SoundVol") == true)
        {
            _audio.volume = PlayerPrefs.GetFloat("SoundVol");
        }
         
        if (interaction)
        {

            quest.trashCount++;
            _audio.PlayOneShot(trashSound);
            sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, 0);
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Invoke("Del", 1f);
            interaction = false;
             
        }
        if (_collider != null)
        {
            if (_collider.gameObject.name == "Player")
            {
                btn.GetComponent<InteractionButton>().img = img;
            }
        }
        
         

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _collider = collision;
            trashManager.GetComponent<TrashManager>().trashLst.Add(gameObject);
            //btn.GetComponent<InteractionButton>().img = img;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trashManager.GetComponent<TrashManager>().trashLst.Remove(gameObject);
            btn.GetComponent<InteractionButton>().img = null;
            _collider = null;
        }
    }
    void Del()
    {
        Destroy(gameObject);
    }
}
