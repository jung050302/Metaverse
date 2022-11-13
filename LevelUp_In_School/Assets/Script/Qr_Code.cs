using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
public class Qr_Code : MonoBehaviour
{
    public GameObject qrObj;
    public Image qrimg;
    static string imgname="";
    public RawImage qrraw;
     

    string userId;
    private static Color32[] Encode(string textForEncoding, int width, int height)

    {

        //인코딩 작업    //QRcode Make..

        var writer = new BarcodeWriter

        {

            Format = BarcodeFormat.QR_CODE,

            Options = new QrCodeEncodingOptions

            {

                Height = height,

                Width = width

            }

        };

        return writer.Write(textForEncoding);

        //QRcode는 적외선 센서를 통하여 인식을 하게 되는데, 인식을 하게 되면 해당 QRcode에 저장된 텍스트를 실행 시켜 그 내용을 확인 하는 것이다. 이에 따라 QR 코드를 만들 때에는 QRcode 안에 저장할 텍스트와 함께 생성시킨다.

    }



    public static Texture2D generateQR(string text)

    {

        //인코딩 작업을 위한 Encode 함수 호출

        var encoded = new Texture2D(256, 256);

        var color32 = Encode(text, encoded.width, encoded.height);

        encoded.SetPixels32(color32);

        encoded.Apply();



        //인코드를 완료후 PNG 파일로 만들기 위한 File 시스템.

        byte[] bytes = encoded.EncodeToPNG();

        File.WriteAllBytes("Assets/Resources/QrImg/" + "Qrimg" + ".png", bytes);
        imgname = text;
        //Application.persistentDataPath 에 파일을 저장하였으니 C:\Users\유저네임\AppData\LocalLow\의 유니티 컴퍼니 회사 이름에 저장 될 것이다.

        return encoded;

    }



    void Start()
    {
        userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
        //10개의 qr코드를 만들기 위해서 for문을 이용해 씬이 시작되면 실행 되도록 하였다.




    }
    
    public void QrButtonDown()
    {
        qrObj.SetActive(true);
        StartCoroutine(GetQr("http://43.201.142.6:5000/qr/"+ userId));
        
    }

    public void ExitButton()
    {
        StartCoroutine(UpdateInfo("http://43.201.142.6:5000/qr/updateExp/" + userId));
        qrObj.SetActive(false);
    }

    IEnumerator GetQr(string URL)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        request.SetRequestHeader("authorization", PlayerPrefs.GetString("token"));
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            QrResponse response = JsonUtility.FromJson<QrResponse>(request.downloadHandler.text);
            print(response.qr);
             
            Texture2D myQR = generateQR(response.qr);
            //qrimg.sprite = Sprite.Create(myQR, new Rect(0,0,myQR.width,myQR.height), new Vector2(0.5f, 0.5f));//myQR;
            qrraw.texture = myQR;

        }
    }

    IEnumerator UpdateInfo(string URL)
    {
        UnityWebRequest request = UnityWebRequest.Get(URL);
        request.SetRequestHeader("authorization", PlayerPrefs.GetString("token"));
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            GameInfo_ response = JsonUtility.FromJson<GameInfo_>(request.downloadHandler.text);

            GameObject.Find("Player").GetComponent<Player>().AddExp(response.exp-PlayerPrefs.GetInt("expSum"));
            PlayerPrefs.SetInt("MaxExp", response.max_exp);
            PlayerPrefs.SetInt("MaxExp", response.max_exp);
            PlayerPrefs.SetInt("Lv", response.level);
            PlayerPrefs.SetInt("money", response.point);

        }
    }

}

[System.Serializable]
public class QrResponse
{
    public int code;
    public string msg;
    public string qr;
}

[System.Serializable]
public class GameInfo_
{
    public int exp;
    public int level;
    public int max_exp;
    public int point;
    
}
