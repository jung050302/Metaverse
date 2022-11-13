using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
 

public class Test:MonoBehaviour
{
    public  int a = 1;
    private void Start()
    {
        print("A:" + a);
    }
    public void _A(int i)
    {
        a++;
        print(i+"¹ø¤ŠA:"+a);
    }
}
