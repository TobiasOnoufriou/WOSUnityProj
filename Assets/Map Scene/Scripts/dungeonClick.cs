using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using Mapbox.Platform;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

public class dungeonInfo
{
    [JsonProperty("dungeonID")]
    public string dungeonID { get; set; }
    [JsonProperty("bossType")]
    public string bossType { get; set; }
    [JsonProperty("dungeonRarity")]
    public int dungeonRarity { get; set; }
    [JsonProperty("dungeonLevel")]
    public int dungeonLevel { get; set; }

   


}
public class dungeonClick : MonoBehaviour {

    public float speed;
    public Transform player; // Drag your player here
    public GameObject content;
    public GameObject dungeonItemButton;
    public Networking net;

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
    private async void setupDungeonHotbar()
    {
        string userdata = "{\"user\":\"bigboi1\"}";
        string response = "";
        StartCoroutine(net.POST("/inventory/openDungeonInventory",userdata,(value) => response = value));
        await Task.Delay(TimeSpan.FromSeconds(1.5f));
        var valueSet = JsonConvert.DeserializeObject(response);
        List<dungeonInfo> list = JsonConvert.DeserializeObject<List<dungeonInfo>>(response);
        
        //Test successful connection.
        //Get all dungeons in inventory.
        //adds it to dungeon hotbar
        //Assign all of the dungeons to an array list
        
        for (int i = 0; i < list.Count; i++)
        {
            setupButton(list[i]);
        }
    }
    private void setupButton(dungeonInfo info)
    {
        var button = Instantiate(dungeonItemButton);
        button.transform.SetParent(content.transform);
        button.transform.localScale = new Vector3(1, 1, 1);
        button.transform.localRotation = Quaternion.Euler(0, 0, 0);
        button.transform.localPosition = new Vector3(50, -50, 0);
        button.GetComponent<DungeonInformation>().dungeonRarity = info.dungeonRarity;
        button.GetComponent<DungeonInformation>().dungeonID = info.dungeonID;
        button.GetComponent<DungeonInformation>().setupButton();
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
    

    async void DungeonClick(GameObject dungeon)
    {
        string userdata = "{\"user\":\"bigboi1\"}";
        string response = "";
        //This will ask the server if they have enough slots to pick up the dungeon.
        StartCoroutine(net.POST("/inventory/collectDungeon", userdata, (value) => response = value));
        await Task.Delay(TimeSpan.FromSeconds(1.5f));
        Debug.Log(response);
       if(response == "No Pickup")
        {
            Debug.Log("Inventory full");
        }else{
            var valueSet = JsonConvert.DeserializeObject(response);
            dungeonInfo item = JsonConvert.DeserializeObject<dungeonInfo>(response);
            setupButton(item);
            Destroy(dungeon);
        }
        //Needs to be changed to not change to dungeon scene
        //SceneManager.LoadScene("DungeonScene", LoadSceneMode.Single);
        //dungeon.GetComponent<Animator>().SetBool("scale", true);
        //dungeon.GetComponent<Animator>().Play("Dungeon");
        //Vector3 dir = BagIcon.transform.position - dungeon.transform.position;
        //dir = dir.normalized;
        //StartCoroutine(moveDungeon(dungeon, dir));
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
