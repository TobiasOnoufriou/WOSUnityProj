using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDungeon : MonoBehaviour
{
    [SerializeField] private Light dungeonLight;
    private int clickAmount = 0;
    [SerializeField] public string m_dungeonID;
    public GameObject setupDungeon;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void changeColourLight(int dungeonRarity)
    {
        switch (dungeonRarity)
        {
            case 0:
                dungeonLight.color = Color.green;
                break;
            case 1:
                dungeonLight.color = Color.blue;
                break;
            case 2:
                dungeonLight.color = Color.cyan;
                break;
            case 3:
                dungeonLight.color = Color.red;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            
            //Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray raycast = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                //Debug.Log("Hit Collider" + raycastHit.collider.tag);
                if (raycastHit.collider.CompareTag("OpenDungeon"))
                {
                    clickAmount++;
                    Debug.Log("Soccer Ball clicked " + clickAmount);
                    if (clickAmount < 3)
                    {
                        dungeonLight.intensity *= 2.5f + clickAmount;
                    }
                    else
                    {
                        //Start loading the dungeon and play the animation
                        //Will need some sort of handshake that the user online.
                        setupDungeon.GetComponent<SetupInformation>().Dungeon_ID = m_dungeonID;
                        GameObject dungeonSetup = Instantiate(setupDungeon);
                        DontDestroyOnLoad(dungeonSetup);
                        SceneManager.LoadScene("DungeonScene");
                    }
                }
            }
        }
    }

    //Will reset everything.
   public void closeDungeonWindow()
    {
        m_dungeonID = null;
        clickAmount = 0;
        dungeonLight.intensity = 1;
    }
    
}
