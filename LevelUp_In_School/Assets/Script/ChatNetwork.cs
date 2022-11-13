using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class ChatNetwork
{
    public Data user;
    public byte[] firstSend;
    public IPEndPoint ipep;
    public Socket client;
    public byte[] buff;
    public string data;


    public ChatNetwork(int data, string ip)
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

    public void HandleSend(string msg)
    {
        //string json = JsonUtility.ToJson(msg);
        byte[] encodedMsg = Encoding.UTF8.GetBytes(msg);
        try
        {
            client.Send(encodedMsg);

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

