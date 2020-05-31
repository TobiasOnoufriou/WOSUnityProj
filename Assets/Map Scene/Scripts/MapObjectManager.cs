using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class MapObjectManager : MonoBehaviour
{
    public static MapObjectManager Instance { get; private set; }

    public GameObject bagButton;
    public GameObject DungeonSmash;
    public GameObject worldEventThing;
    public GameObject dungeonHotbar;
    public GameObject closeDungeonButton;


    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }
    }
    public void openDungeonSmash(int rarity, string dungeon_ID)
    {
        bagButton.SetActive(false);
        worldEventThing.SetActive(false);
        dungeonHotbar.SetActive(false);
       
        DungeonSmash.GetComponent<OpenDungeon>().changeColourLight(rarity);
        DungeonSmash.GetComponent<OpenDungeon>().m_dungeonID = dungeon_ID;
        DungeonSmash.SetActive(true);
        closeDungeonButton.SetActive(true);
    }
    public void closeDungeonSmash()
    {
        bagButton.SetActive(true);
        worldEventThing.SetActive(true);
        dungeonHotbar.SetActive(true);

        DungeonSmash.GetComponent<OpenDungeon>().closeDungeonWindow();
        DungeonSmash.SetActive(false);
        closeDungeonButton.SetActive(false);
    }
    

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}


