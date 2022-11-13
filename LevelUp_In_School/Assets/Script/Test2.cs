using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    Test t = new Test();
    private void Start()
    {
       print( this.gameObject.name);
        t._A(1);
    }
}
