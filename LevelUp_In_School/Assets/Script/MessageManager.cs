using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading;

public class MessageManager : QuestManager
{
    public GameObject _quest;

    public GameObject joy;
    public GameObject messagebutton;
    public GameObject chatting;
    public InputField input;
    public GameObject player;
    public Text message;
    public GameObject content;
    //string PlayerPrefs.GetString("NickName");
    public bool quest;
    ChatNetwork socket;
    public bool greetings;
    string userId;

    Thread t1;
    Thread t2;
    string msg_;


    bool test;
    void Start()
    {
        test = false;
        userId= JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
        greetings = false;
        _quest = GameObject.Find("QuestManager");
        player = GameObject.Find("Player");
        //PlayerPrefs.GetString("NickName");// = PlayerPrefs.GetString("NickName");
        quest = false;
        questName = "채팅으로 선생님께 공손하게 인사하기";
    }

    // Update is called once per frame
    void Update()
    {

        if (test)
        {
            multiText.text = msgData;

            test = false;
        }
        
        if (quest)
        {
            questName = "채팅으로 선생님께 공손하게 인사하기";
        }
        if (multiText.text != "")
        {
            Instantiate(multiText, content.transform);
            
            multiText.text = "";
        }
        
        if (chatting.activeSelf == true)
        {
            player.GetComponent<Player>().stop = true;
            joy.SetActive(false);
        }
        else
        {
            player.GetComponent<Player>().stop = false;
            joy.SetActive(true);
        }
    }
    
    public void SendButtonDown()
    {
        if (input.text != "")
        {

            msg_= PlayerPrefs.GetString("NickName") + "(" + userId + ")" + " : " + Change();
            message.text = Change();
            socket.HandleSend(msg_);
            //t2 = new Thread(() => Send(msg_));
            //t2.IsBackground = true;
            //t2.Start();
            Instantiate(message, content.transform);
            


            if ((quest && input.text == "안녕하세요 선생님") || (quest && input.text == "선생님 안녕하세요")|| (quest && input.text == "선생님 안녕하세요 ")|| (quest && input.text == "안녕하세요 선생님 "))
            {
                if (GameObject.Find("quest3").GetComponent<quest3>().quest)
                {
                    GameObject.Find("quest3").GetComponent<quest3>().count++;
                }
                greetings = true;
            }
            input.text = "";
            message.text = "";
        }
    }



    public string msgData;
    public Text multiText;
    public void MessageButton()
    {
        socket = new ChatNetwork(PlayerPrefs.GetInt("chat_port"), "13.112.64.176");
        socket.FirstDo();
        chatting.SetActive(true);
        messagebutton.gameObject.SetActive(false);

        t1 = new Thread(() => Receive());
        t1.IsBackground = true;


        t1.Start();


    }

    public void Send(string msg)
    {
        socket.HandleSend(msg);
    }
    public void Receive()
    {
        while (true)
        {
            try
            {
                msgData = socket.HandleReceive();
                print(msgData);
                if (!msgData.Contains(userId))
                {
                    //multiText.text = msgData;
                    test = true;
                    //Instantiate(multiText, content.transform);
                }
            }
            catch(ThreadAbortException e)
            {

                break;
            }

            //else
            //{
            //    message.text = msgData.Split(':')[1];
                 
            //    //Instantiate(message, content.transform);
            //}

        }
        return;
    }


    public void ExitButtonDown()
    {
        messagebutton.gameObject.SetActive(true);
        chatting.SetActive(false);
        socket.HandleSend("/END");
        t1.Abort();


        socket.close();

    }
    //욕 검열해주는 함수
    public string Change()
    {
        string text = "";
        //욕설 모음 txt파일에 있는 욕설이 채팅에 포함되어있으면 검열을 해준다
        //string file = "Assets/Resources/txt/swearList.txt";
        //string[] arr = System.IO.File.ReadAllText(file).Split(',');
        Lst l = new Lst();
        string[] arr = l._arr;
        string Test = "";
        string index = "";

        string _input = input.text;

        for (int i = 0; i < arr.Length; i++)
        {

            if (Search(_input, arr[i]) != "")
            {
                if (i == arr.Length - 1)
                {
                    Test += Search(_input, arr[i]);
                }
                else
                {
                    Test += Search(_input, arr[i]) + ",";
                }

            }


        }

        string[] indexArr = Test.Split(',');

        bool a = true;
        for (int i = 0; i < _input.Length; i++)
        {

            a = true;

            for (int j = 0; j < indexArr.Length; j++)
            {
                if (i.ToString() == indexArr[j].ToString())
                {
                    text += "*";
                    a = false;
                }

            }
            if (a == true)
            {
                text += _input[i];
                a = false;
            }
        }



        return text;

    }
    //특정 단어 찾아주는 함수
    public string Search(string a, string b)
    {
        string Demo = "";
        string test = "";
        string index = "";
        bool c = false;


        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == b[0])
            {

                Demo += i + ",";

                c = true;
                for (int j = 1; j < b.Length; j++)
                {
                    
                    if (i+j<a.Length&& a[i + j] == b[j])
                    {
                        Demo += (i + j) + ",";
                        c = true;

                    }
                     
                    else
                    {
                        Demo = "";
                        c = false;
                    }
                }

            }
            if (c == true)
            {
                test += Demo;
                Demo = "";
                c = false;
            }
        }
        for (int i = 0; i < test.Length - 1; i++)
        {
            index += test[i];
        }
        return index;


    }

    public override void Quest_()
    {
        quest = true;
         
    }
    public override bool CheckQuest()
    {
        if (greetings)
        {
            quest = false;
             
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void QuestReset()
    {
        quest = false;
        greetings = false;
    }

    private void OnApplicationQuit()
    {

        if (t1.IsAlive)
        {
            socket.HandleSend("/END");

            t1.Abort();
            

            socket.close();

        }

    }

}
