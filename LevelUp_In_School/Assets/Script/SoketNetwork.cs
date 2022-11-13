using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
 
public class SoketNetwork
{
    public Data user;
    public byte[] firstSend;
    public IPEndPoint ipep;
    public Socket client;
    public byte[] buff;
    public string data;


    public SoketNetwork(int data,string ip) 
    {
        user = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user"));
        firstSend = Encoding.UTF8.GetBytes(user.user_id);
        buff = new byte[1024];
        ipep = new IPEndPoint(IPAddress.Parse(ip), data);
        client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        

    }
    
    public void FirstDo()
    {
        //print("firstdo실행완료");
        client.Connect(ipep);
        client.Send(firstSend);
         
    }
    
    public void HandleSend(SendInfo sendInfo)
    {
        string json = JsonUtility.ToJson(sendInfo);
        byte[] encodedJson = Encoding.UTF8.GetBytes(json);
        try
        {
            client.Send(encodedJson);
            
        }
        catch
        {
            return;
        }
        
    }
    
    public string HandleReceive()
    {
        try
        {
            int n = client.Receive(buff);
            data = Encoding.UTF8.GetString(buff, 0, n);
        }
        catch
        {

            return "";
        }
         
        //print(data);
        return data;

    }

    public void close()
    {
        
        client.Close();
        
    }
    
    public void EndMessage(string msg)
    {
        client.Send(Encoding.UTF8.GetBytes(msg));
       
    }
    
}

[Serializable]
public class SendInfo
{
    //플레이어 위치
    public string userPosition_x;
    public string userPosition_y;
    //옷정보
    public string head;
    public string cloth;
    public string LArm;
    public string RArm;
    public string LPants;
    public string RPants;
    
    public string JoyPosition_x;//조이스틱 포지션
    public string JoyPosition_y;//조이스틱 포지션
    public string userId;//아이디
    public string nickName;//닉네임

    public string scaleX;

    public string lv;

    
}
