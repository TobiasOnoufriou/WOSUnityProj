using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Networking : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(sendData());
    }

    IEnumerator sendData()
    {
        UnityWebRequest www = UnityWebRequest.Post("", "Yeet");
        yield return www.SendWebRequest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
