using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScript : MonoBehaviour
{
    [SerializeField]
    private GameObject cut;
    [SerializeField]
    private float cutDestroyTime;
    [SerializeField]
    private Camera cam;
    private bool dragging = false;
    private Vector3 swipeStart;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.dragging = true;
            this.swipeStart = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z * -1));
        }
        else if (Input.GetMouseButtonUp(0) && this.dragging)
        {
            this.createCut();
        }
    }
    private void createCut(){
        this.dragging = false;
        Vector3 swipeEnd = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z * -1));
        GameObject cut = Instantiate(this.cut, this.swipeStart, Quaternion.identity) as GameObject;
        cut.GetComponent<LineRenderer>().SetPosition(0, this.swipeStart);
        cut.GetComponent<LineRenderer>().SetPosition(1, swipeEnd);
        Vector2[] colliderPoints = new Vector2[2];
        colliderPoints[0] = new Vector2(0.0f, 0.0f);
        colliderPoints[1] = swipeEnd - this.swipeStart;
        Debug.Log(swipeStart);
        Debug.Log(swipeEnd);
        cut.GetComponent<EdgeCollider2D>().points = colliderPoints;
        Destroy(cut.gameObject, this.cutDestroyTime);
    }
}
