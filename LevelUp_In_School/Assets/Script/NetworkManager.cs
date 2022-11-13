using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine.SceneManagement;
using System.Text;
public class NetworkManager : MonoBehaviour
{
  
    public SoketNetwork socketNetwork;
    public GameObject player;
    public FixedJoystick joy;

    public GameObject multiUserPre;
    //public List<GameObject> multiUser=new List<GameObject>();
    public Dictionary<string, GameObject> multiUser ;
    public MultiUser _multiUser;

    public string receive;
    public string data;

    static public NetworkManager networkManager;


    public Queue<string> queue = new Queue<string>();
    public Thread t1;

    public bool b;

    public Thread t2;
    public SendInfo data2;
    public SendInfo sendInfo;

    public string _userId;

    public bool a;

    public List<string> keyLst;

    string[] DestroyPlayer;
    void Start()
    {

        multiUser = new Dictionary<string, GameObject>();

        a = true;
        b = false;
        player = GameObject.Find("Player");
        joy = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();

        if (networkManager != null)
        {
            Destroy(gameObject);

        }
        else
        {

            DontDestroyOnLoad(this.gameObject);
            networkManager = this;

        }

        socketNetwork = new SoketNetwork(PlayerPrefs.GetInt("move_port"), "3.34.210.37");
        socketNetwork.FirstDo();
        _userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;

        sendInfo = new SendInfo()
        {
            userPosition_x = player.transform.position.x.ToString(),
            userPosition_y = player.transform.position.y.ToString(),

            head = PlayerPrefs.GetString("Hair"),
            cloth = PlayerPrefs.GetString("Cloth"),
            LArm = PlayerPrefs.GetString("LArm"),
            RArm = PlayerPrefs.GetString("RArm"),
            LPants = PlayerPrefs.GetString("LPants"),
            RPants = PlayerPrefs.GetString("RPants"),

            JoyPosition_x = (joy.Horizontal).ToString(),
            JoyPosition_y = (joy.Vertical).ToString(),
            userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id,
            nickName = PlayerPrefs.GetString("NickName"),

            scaleX = (player.transform.localScale.x).ToString(),
            lv = PlayerPrefs.GetInt("LV").ToString()
        };

        t1 = new Thread(()=>send(sendInfo));
        t1.IsBackground = true;
         
        t2 = new Thread(()=> Receive());
        t2.IsBackground = true;
        t1.Start();
        t2.Start();
        //t1.Join();
        //t2.Join();
    }

    
    void Update()
    {



        if (!a)
        {
            multiUser = new Dictionary<string, GameObject>();
            if (SceneManager.GetActiveScene().name != "Lobby" && SceneManager.GetActiveScene().name != "Customizing"&&!t1.IsAlive&&!t2.IsAlive)
            {
                socketNetwork = new SoketNetwork(PlayerPrefs.GetInt("move_port"), "3.34.210.37");
                socketNetwork.FirstDo();
                _userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
                print("실행!!");
                player = GameObject.Find("Player");
                joy = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();

                sendInfo = new SendInfo()
                {
                    userPosition_x = player.transform.position.x.ToString(),
                    userPosition_y = player.transform.position.y.ToString(),

                    head = PlayerPrefs.GetString("Hair"),
                    cloth = PlayerPrefs.GetString("Cloth"),
                    LArm = PlayerPrefs.GetString("LArm"),
                    RArm = PlayerPrefs.GetString("RArm"),
                    LPants = PlayerPrefs.GetString("LPants"),
                    RPants = PlayerPrefs.GetString("RPants"),

                    JoyPosition_x = (joy.Horizontal).ToString(),
                    JoyPosition_y = (joy.Vertical).ToString(),
                    userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id,
                    nickName = PlayerPrefs.GetString("NickName"),

                    scaleX = (player.transform.localScale.x).ToString(),
                    lv = PlayerPrefs.GetInt("LV").ToString()
                };

                t1 = new Thread(() => send(sendInfo));
                 
                 
                t2 = new Thread(() => Receive());
                t1.Start();
                t2.Start();


                a = true;
            }
            //else if(SceneManager.GetActiveScene().name == "Lobby")
            //{
            //    //print(multiUser.ContainsKey(data2.userId));//true
            //    //multiUser.Remove(data2.userId));
            //    //print(multiUser.ContainsKey(data2.userId));//false
            //}



        }
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            b = false;
            a = false;
            //if (multiUser[_userId] != null)
            //{

            //    Destroy(multiUser[_userId]);
            //    multiUser.Remove(_userId);

            //}
             
            t1.Abort();
            t2.Abort();
             
            socketNetwork.close();
             

            //print("1소켓닫음:"+t1.IsAlive);
            //print("2소켓닫음:" + t2.IsAlive);
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            print("client.Close");
            t1.Abort();
            t2.Abort();
            socketNetwork.client.Close();
        }


        //string receive = socketNetwork.HandleReceive();


        //SendInfo data2 = JsonUtility.FromJson<SendInfo>(data[1]);
        if(SceneManager.GetActiveScene().name=="Class"|| SceneManager.GetActiveScene().name == "Playground")
        {



            sendInfo.userPosition_x = player.transform.position.x.ToString();
            sendInfo.userPosition_y = player.transform.position.y.ToString();

            sendInfo.head = PlayerPrefs.GetString("Hair");
            sendInfo.cloth = PlayerPrefs.GetString("Cloth");
            sendInfo.LArm = PlayerPrefs.GetString("LArm");
            sendInfo.RArm = PlayerPrefs.GetString("RArm");
            sendInfo.LPants = PlayerPrefs.GetString("LPants");
            sendInfo.RPants = PlayerPrefs.GetString("RPants");

            sendInfo.JoyPosition_x = (joy.Horizontal).ToString();
            sendInfo.JoyPosition_y = (joy.Vertical).ToString();
            sendInfo.userId = _userId;
            sendInfo.nickName = PlayerPrefs.GetString("NickName");

            sendInfo.scaleX = (player.transform.localScale.x).ToString();
            sendInfo.lv = PlayerPrefs.GetInt("LV").ToString();
            //print("!receive.Contains(_userId):" + !receive.Contains(_userId));

            DestroyPlayer = receive.Split(':');
            //print(DestroyPlayer[0]);
            //print(DestroyPlayer[1]);
            //print(GameObject.Find(DestroyPlayer[1]));
            //print(multiUser.ContainsKey(DestroyPlayer[1]));
            //print(DestroyPlayer[0] == "destroy" && GameObject.Find(DestroyPlayer[1]) != null);

            if (DestroyPlayer[0] == "destroy" && GameObject.Find(DestroyPlayer[1]) != null)
            {
                print(multiUser[DestroyPlayer[1]]);
                //Destroy(GameObject.Find(DestroyPlayer[1]).GetComponent<MultiUser>().nameText);
                Destroy(GameObject.Find(DestroyPlayer[1]));
                Destroy(GameObject.Find(DestroyPlayer[1] + "Text"));
                multiUser.Remove(DestroyPlayer[1]);
                print("삭크제됨");
                print(multiUser.ContainsKey(DestroyPlayer[1]));

            }

            if (receive !=""&& !receive.Contains(_userId)&& DestroyPlayer[0] != "destroy")//만약 받은 데이터의 userId가 내 userid와 다르다면 캐릭터 생성
            {



                //print("다름");

                //keyLst = new List<string>(multiUser.Keys);
                //print("ContainsKey:" + multiUser.ContainsKey(data2.userId));
                //print("dataUserId:" + data2.userId);
                //print("_userId:" + _userId);
                //multiUser[data2.userId]!=null&& 

                if (multiUser.ContainsKey(data2.userId)&&data2.userId!=_userId&& DestroyPlayer[0] != "destroy")
                {

                    print(multiUser[data2.userId]);
                    _multiUser = multiUser[data2.userId].GetComponent<MultiUser>();
                     
                    _multiUser._name = data2.nickName;

                    _multiUser.Hair.sprite = _multiUser.ReturnValue(data2.head);
                    _multiUser.Body.sprite = _multiUser.ReturnValue(data2.cloth);
                    _multiUser.leftArm.sprite = _multiUser.ReturnValue(data2.LArm);
                    _multiUser.rightArm.sprite = _multiUser.ReturnValue(data2.RArm);
                    _multiUser.rightLeg.sprite = _multiUser.ReturnValue(data2.RPants);
                    _multiUser.leftLeg.sprite = _multiUser.ReturnValue(data2.LPants);

                    print("기존"+ multiUser[data2.userId].gameObject.name);

                     
                    multiUser[data2.userId].transform.position = new Vector3(float.Parse(data2.userPosition_x), float.Parse(data2.userPosition_y), 0);
                    _multiUser.joyX = float.Parse(data2.JoyPosition_x);
                    _multiUser.joyY = float.Parse(data2.JoyPosition_y);
                    _multiUser.scale_x = float.Parse(data2.scaleX);

                    _multiUser.Lv = int.Parse(data2.lv);

                }
                else if(data2.userId != _userId&&!multiUser.ContainsKey(data2.userId)&& DestroyPlayer[0] != "destroy")
                {
                     
                    //multiUser[data2.userId] = Instantiate(multiUserPre);
                    multiUser.Add(data2.userId, Instantiate(multiUserPre));//

                    _multiUser = multiUser[data2.userId].GetComponent<MultiUser>();
                    _multiUser._name = data2.nickName;
                    multiUser[data2.userId].gameObject.name = data2.userId;
                    multiUser[data2.userId].GetComponent<MultiUser>().nameText.gameObject.name = data2.userId + "Text";
                    _multiUser.Hair.sprite = _multiUser.ReturnValue(data2.head);
                    _multiUser.Body.sprite = _multiUser.ReturnValue(data2.cloth);
                    _multiUser.leftArm.sprite = _multiUser.ReturnValue(data2.LArm);
                    _multiUser.rightArm.sprite = _multiUser.ReturnValue(data2.RArm);
                    _multiUser.rightLeg.sprite = _multiUser.ReturnValue(data2.RPants);
                    _multiUser.leftLeg.sprite = _multiUser.ReturnValue(data2.LPants);

                    print("새로운유저 접속"+ multiUser[data2.userId].gameObject.name);

                    //이동문제발생시 아래 코드 주석 해제
                    multiUser[data2.userId].transform.position = new Vector3(float.Parse(data2.userPosition_x), float.Parse(data2.userPosition_y), 0);
                    _multiUser.joyX = float.Parse(data2.JoyPosition_x);
                    _multiUser.joyY = float.Parse(data2.JoyPosition_y);
                    _multiUser.scale_x = float.Parse(data2.scaleX);

                    _multiUser.Lv = int.Parse(data2.lv);
                }
                 

                //if (!b)
                //{

                //    if (multiUser.ContainsKey(data2.userId))
                //    {
                //        multiUser[data2.userId]=Instantiate(multiUserPre);
                //    }
                //    else
                //    {
                //        multiUser.Add(data2.userId, Instantiate(multiUserPre));
                //    }

                //    b = true;

                //    print("유저접속");

                //}
                 
                 
            }

             
        }

         
       

    }

    

    public void send(SendInfo sendInfo)
    {
        
        print("샌드 성공");
        
            while (true)
            {


                try
                {
                    if (float.Parse(sendInfo.userPosition_x) != 0 && float.Parse(sendInfo.userPosition_y) != 0)
                    {
                        socketNetwork.HandleSend(sendInfo);
                        
                        Thread.Sleep(250);
                    }
                }
                catch (ThreadAbortException e)
                {
                    //Thread.ResetAbort();

                    socketNetwork.EndMessage("/END : "+_userId);
                    print("종크료");
                    break;
                }

            }
            return;
        
         
    }

    public void Receive()
    {
        print("리시브 성공");
        while (true)
        {
             

            try
            {
                
                receive = socketNetwork.HandleReceive();
                //print(receive);
                if (!receive.Contains("destroy"))
                {
                    
                    if (receive != "" && receive != null && DestroyPlayer[0] != "destroy")
                    {
                        data = '{' + receive.Split('{')[1];
                        data2 = JsonUtility.FromJson<SendInfo>(data);
                    }
                }
                  
                


                
            }
            catch (ThreadAbortException e)
            {
                //Thread.ResetAbort();
                break;
                 
            }
            
            //Thread.Sleep(100);
        }
        return;
         
        
    }
    private void OnApplicationQuit()
    {

        if (t1.IsAlive && t2.IsAlive)
        {
            
            t1.Abort();
            t2.Abort();

            socketNetwork.close();

        }

    }
}



