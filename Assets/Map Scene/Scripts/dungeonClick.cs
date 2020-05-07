using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class dungeonClick : MonoBehaviour {

    public float speed;
    public Transform player; // Drag your player here
    public GameObject content;
    public GameObject dungeonItemButton;


    List<GameObject> dungeonButtons = new List<GameObject>();
    

    private float angle;
    float swipeSpeed = 0.05F;
    float inputX;
    float inputY;
    
    private string username;


    //public GameObject dungeonUI;
    public GameObject playerTarget;
    public GameObject BagIcon;
    protected bool dungeonClicked;


	// Use this for initialization
	void Start () {
        Screen.orientation = ScreenOrientation.Portrait;
        //this.dungeonUI.SetActive(false);
        this.dungeonClicked = false;
        setupDungeonHotbar();
	}
    private void setupDungeonHotbar()
    {

        //Test successful connection.
        //Get all dungeons in inventory.
        //adds it to dungeon hotbar
        //Assign all of the dungeons to an array list
        for (int i = 0; i < 10; i++)
        {
            dungeonButtons.Add(Instantiate(dungeonItemButton));
            dungeonButtons[i].transform.SetParent(content.transform);
            dungeonButtons[i].transform.localScale = new Vector3(1, 1, 1);
            dungeonButtons[i].transform.localRotation = Quaternion.Euler(0, 0, 0);
            dungeonButtons[i].transform.localPosition = new Vector3(50,-50, 0);
            
        }
    }

    private IEnumerator clickWait()
    {
        yield return new WaitForSeconds(2f); 
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        { 
            Debug.Log("Click");
            Debug.Log(dungeonClicked);
            RaycastHit hit;
            if (EventSystem.current.IsPointerOverGameObject())
                return;
          
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit) && dungeonClicked == false)
            {
                if(hit.collider.tag == "Dungeon")
                {
                    DungeonClick(hit.collider.gameObject);
                }
            }
        }
    }
    

    void DungeonClick(GameObject dungeon)
    {
        //Needs to be changed to not change to dungeon scene
        SceneManager.LoadScene("DungeonScene", LoadSceneMode.Single);
        dungeon.GetComponent<Animator>().SetBool("scale", true);
        //dungeon.GetComponent<Animator>().Play("Dungeon");
        Vector3 dir = BagIcon.transform.position - dungeon.transform.position;
        dir = dir.normalized;
        StartCoroutine(moveDungeon(dungeon, dir));
        //dungeonClicked = true;
    }
    IEnumerator moveDungeon(GameObject dungeon, Vector3 dir)
    {
        while (dungeon.transform.position != BagIcon.transform.localPosition)
        {
            dir = BagIcon.transform.position - dungeon.transform.position;
            dir = dir.normalized;
            dungeon.transform.position += dir * Time.deltaTime * 100.0f;

            yield return null;
        }
    }
    void FixedUpdate()
    {
       // OrbitAround();
    }

    public void startDungeon()
    {
        SceneManager.LoadScene("DungeonScene",LoadSceneMode.Single);
        Debug.Log("Yes Clicked");

    }
    public void removeUI()
    {
        dungeonClicked = false;
        /*var tempObject = GameObject.Find("EnterDungeonUI 1(Clone)");
        Destroy(tempObject);*/
        //this.dungeonUI.SetActive(false);
        //Will call an animation to make a map go into the inventory.
       
    }
    void OrbitAround()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //this.dungeonClicked = true;
            Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            inputX += touchDeltaPosition.x * swipeSpeed;
            inputY += touchDeltaPosition.y * swipeSpeed;
            this.transform.RotateAround(player.position, player.up, inputX * Time.deltaTime / 2);
        }

    }
    


}
