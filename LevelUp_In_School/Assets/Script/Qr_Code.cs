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

        //���ڵ� �۾�    //QRcode Make..

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

        //QRcode�� ���ܼ� ������ ���Ͽ� �ν��� �ϰ� �Ǵµ�, �ν��� �ϰ� �Ǹ� �ش� QRcode�� ����� �ؽ�Ʈ�� ���� ���� �� ������ Ȯ�� �ϴ� ���̴�. �̿� ���� QR �ڵ带 ���� ������ QRcode �ȿ� ������ �ؽ�Ʈ�� �Բ� ������Ų��.

    }



    public static Texture2D generateQR(string text)

    {

        //���ڵ� �۾��� ���� Encode �Լ� ȣ��

        var encoded = new Texture2D(256, 256);

        var color32 = Encode(text, encoded.width, encoded.height);

        encoded.SetPixels32(color32);

        encoded.Apply();



        //���ڵ带 �Ϸ��� PNG ���Ϸ� ����� ���� File �ý���.

        byte[] bytes = encoded.EncodeToPNG();

        File.WriteAllBytes("Assets/Resources/QrImg/" + "Qrimg" + ".png", bytes);
        imgname = text;
        //Application.persistentDataPath �� ������ �����Ͽ����� C:\Users\��������\AppData\LocalLow\�� ����Ƽ ���۴� ȸ�� �̸��� ���� �� ���̴�.

        return encoded;

    }



    void Start()
    {
        userId = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("user")).user_id;
        //10���� qr�ڵ带 ����� ���ؼ� for���� �̿��� ���� ���۵Ǹ� ���� �ǵ��� �Ͽ���.




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
