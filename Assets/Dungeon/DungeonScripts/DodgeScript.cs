using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DodgeScript : MonoBehaviour {

    public GameObject LeftPos, RightPos;
    public GameObject LeftButton, RightButton;

    private Vector3 OriginalPosition;
    private Rigidbody rb;

    private int lane = 1;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<DungeonInteraction>().slidePart)
        {
            this.OriginalPosition = rb.position;
            GetComponent<DungeonInteraction>().slidePart = false;
        }
    }
    public void RightButtonTrigger()
    {
        
        StartCoroutine(RightMove());
    }
    public void LeftButtonTrigger()
    {
        StartCoroutine(LeftMove());
    }

    IEnumerator LeftMove()
    {
        LeftButton.GetComponent<Button>().interactable = false;
        transform.position = LeftPos.transform.position;
        DungeonManager.Instance.pLane = 0;
        yield return new WaitForSeconds(0.5f);
        DungeonManager.Instance.pLane = 1;
        transform.position = OriginalPosition;
        LeftButton.GetComponent<Button>().interactable = true;
    }
    IEnumerator RightMove()
    {
        RightButton.GetComponent<Button>().interactable = false;
        transform.position = RightPos.transform.position;
        DungeonManager.Instance.pLane = 2;
        yield return new WaitForSeconds(0.5f);
        DungeonManager.Instance.pLane = 1;
        transform.position = OriginalPosition;
        RightButton.GetComponent<Button>().interactable = true;
    }
}
