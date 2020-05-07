using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onMouseEvent : MonoBehaviour {
    public Text clickedText;
	// Use this for initialization
	void Start () {
        clickedText = GameObject.Find("checkClick").GetComponent<Text>();
	}
	void onMouseDown()
    {
        clickedText.text = "Clicked";
    }
    // Update is called once per frame
    void Update () {
		
	}
}
