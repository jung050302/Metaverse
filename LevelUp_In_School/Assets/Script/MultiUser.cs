using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiUser : MonoBehaviour
{
    public string[] key;
    public Sprite[] value;

    public int Lv;

    public SpriteRenderer Hair;//gpfapt
    public SpriteRenderer Body;//Ŭ�ν��ٵ�
    public SpriteRenderer leftArm;//lcarm
    public SpriteRenderer rightArm;//Rcarm
    public SpriteRenderer rightLeg;//R_Cloth
    public SpriteRenderer leftLeg;//L_Cloth

    public float joyX;
    public float joyY;

    public float scale_x;

    public Animator ani;

    public string _name;//�г���
    public GameObject nameText;//Instantiate �� ������ Text UI
    public GameObject PreNameText;//Text������

    [SerializeField] Camera _camera;
    //public GameObject nameOBJ;

    public GameObject nameBox;//�г��� ������
    private void Awake()
    {
        nameText = Instantiate(PreNameText, GameObject.Find("MultiUserNameText").transform);
    }
    void Start()
    {

        //nameText = Instantiate(PreNameText,GameObject.Find("Canvas").transform);

        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //nameBox = GameObject.Find("name_");
    }

    // Update is called once per frame
    void Update()
    {
        //nameText.transform.position = nameBox.transform.position;
        nameText.GetComponent<Text>().text = "LV." + Lv.ToString() + " " + _name;

        if (nameText.gameObject.transform.position != nameBox.transform.position)
        {
            nameText.transform.position = _camera.WorldToScreenPoint(new Vector2(nameBox.transform.position.x, nameBox.transform.position.y));
        }

        if (joyX != 0 || joyY != 0)
        {
            ani.SetFloat("RunState", 0.5f);

        }
        else
        {
            ani.SetFloat("RunState", 0f);
        }

        //this.gameObject.transform.Translate(joyX * 5 * Time.deltaTime, joyY * 5 * Time.deltaTime, 0);
        if (joyX < 0)
        {
            
            this.gameObject.transform.localScale = new Vector2(scale_x, gameObject.transform.localScale.y);
        }
        else if (joyX > 0)
        {

            this.gameObject.transform.localScale = new Vector2(-scale_x, gameObject.transform.localScale.y);
        }
    }

    public Sprite ReturnValue(string _key)
    {
        for (int i = 0; i < key.Length; i++)
        {
            if (key[i] == _key)
            {
                return value[i];
            }
        }
        return null;

    }

}
