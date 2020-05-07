using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class collectCoin : MonoBehaviour {
    private float speed = 10f;
    private bool towardsPlayer = false;
    public Transform player;
    public Text goldAmount;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
	}
	public void changeAmount(string amount)
    {
        goldAmount.text = amount + " Gold";
    }
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(player.position, this.transform.position);
        if(distance <= 20f)
        {
            floatTowardsPlayer();
        }

        //floatTowardsPlayer();
	}
    void floatTowardsPlayer()
    {
       
        float step = speed * Time.deltaTime; // calculate distance to move
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(player.position.x, player.position.y + 4.5f, player.position.z), step);
        float distance = Vector3.Distance(player.position, this.transform.position);
        Debug.Log(distance);
        if (distance <= 7.5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
       
    }
    private void OnTriggerExit(Collider other)
    {
     
    }
}
