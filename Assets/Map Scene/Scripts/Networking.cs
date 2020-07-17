using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Networking : MonoBehaviour
{

    public string url;
    public Coroutine coroutine { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        /*coroutine = StartCoroutine(GET("http://localhost:65080/dungeon/joinDungeon",returnValue=>{
            Debug.Log(returnValue);
        
        }));
        Debug.Log(coroutine);*/
    }
    public IEnumerator PUT(string uri, string data, System.Action<string> callback = null)
    {
        UnityWebRequest www = UnityWebRequest.Put(url+uri, data);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            //Need to include some UI which tells the user they have lost connection.
            Debug.Log("There was an error processing command" + www.error);
        }
        else
        {
            callback(www.downloadHandler.text.ToString());
        }
    }


    public IEnumerator POST(string uri, string data, System.Action<string> callback = null)
    {

        UnityWebRequest www = UnityWebRequest.Post(url + uri, data);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
        www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.Log("There was an error processing command" + www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text.ToString());
            callback(www.downloadHandler.text.ToString());
        }
    }
    public IEnumerator GET(string uri, System.Action<string> callback = null)
    {

        //JSON statements/or userID
        UnityWebRequest www = UnityWebRequest.Get(url + uri);
        yield return www.SendWebRequest();
        if (www.isNetworkError)
        {
            Debug.Log("There was an error processing command" + www.error);
        }
        else
        {
            callback(www.downloadHandler.text.ToString());

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
