using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour {

    public static DungeonManager Instance { get; private set; }

    public  int pLane { get; set; }
    public  bool[] bAttack { get; set; }
    public float pHealth { get; set; }
    public float bHealth { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }


	// Use this for initialization
	void Start () {
		bAttack = new bool[] { false, false, false};
        pLane = 1; //Defualt lane in which the player is which is the middle lane.
        pHealth = 100;
        bHealth = 500;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
