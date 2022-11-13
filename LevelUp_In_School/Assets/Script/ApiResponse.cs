using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ApiResponse
{
    public string code;
    public string msg;
    //public Data data;
}

[System.Serializable]
public class Data
{
    public string id;//데이터베이스에 저장되는 고유번호;
    public string user_id;
    //public string password;
    public bool isStudent;
    public string school_code;
    public string class_code;
    public string username;
    public int level;
    public int exp;
    public int max_exp;
    public int point;
}
public class AuthRes: ApiResponse
{
    public Data data;
}
public class GetPort :ApiResponse
{
    public string[] data;
}