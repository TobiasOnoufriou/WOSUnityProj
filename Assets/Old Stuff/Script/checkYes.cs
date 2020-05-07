using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class checkYes : MonoBehaviour {
	// Use this for initialization
	
   public void startDungeon()
    {
        SceneManager.LoadSceneAsync("DungeonScene");
        Debug.Log("Yes Clicked");
       
    }
    public void removeUI()
    {
        Destroy(gameObject);
    }
}
