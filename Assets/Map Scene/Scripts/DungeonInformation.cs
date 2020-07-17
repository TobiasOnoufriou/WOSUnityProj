using Mapbox.Examples.Voxels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class Rarity
{
    [JsonProperty("dungeonRarity")]
    public int dungeonRarity { get; set; }
}

public class DungeonInformation: MonoBehaviour
{
    public Button scrap;
    public Button upgrade;

    public Texture green;
    public Texture blue;
    public Texture purple;
    public Texture orange;

    public Networking net;


    public GameObject dungeonSmash;

    [SerializeField] private int m_dungeonRarity;
    [SerializeField] private string m_dungeonID;
    public int dungeonRarity
    {
        get { return m_dungeonRarity; }
        set { m_dungeonRarity = value; }
    }
    public string dungeonID
    {
        get { return m_dungeonID; }
        set { m_dungeonID = value; }
    }
    public void Start()
    {
        
    }
   
    public void setupButton()
    {
        setIconandButtonPrices();
    }
    //Sets the icon of the button.
    public void setIconandButtonPrices()
    {
        switch (m_dungeonRarity)
        {
            case 0:
                scrap.GetComponentInChildren<Text>().text = "25";
                upgrade.GetComponentInChildren<Text>().text = "50";
                GetComponent<RawImage>().texture = green;
                break;
            case 1:
                scrap.GetComponentInChildren<Text>().text = "50";
                upgrade.GetComponentInChildren<Text>().text = "75";
                GetComponent<RawImage>().texture = blue;
                break;
            case 2:
                scrap.GetComponentInChildren<Text>().text = "150";
                upgrade.GetComponentInChildren<Text>().text = "300";
                GetComponent<RawImage>().texture = blue;
                break;
            case 3:
                scrap.GetComponentInChildren<Text>().text = "500";
                Destroy(upgrade.gameObject);
                GetComponent<RawImage>().texture = orange;
                break;
        }
    }   
    public async void openDungeon()
    {
        MapObjectManager.Instance.openDungeonSmash(m_dungeonRarity,m_dungeonID);
        
    }
    public async void scrapDungeon()
    {
        string json = "{\"user\":\"bigboi1\", \"dungeonID\":\"" + dungeonID + "\"}";
        var response = "";
        StartCoroutine(net.POST("/inventory/scrapDungeon", json, (value)=>response = value));
        await Task.Delay(TimeSpan.FromSeconds(1.5f));
        Debug.Log(response);
        Destroy(this.gameObject);
    }
    public async void upgradeDungeon()
    {
        string json = "{\"user\":\"bigboi1\", \"dungeonID\":\"" + dungeonID + "\"}";
        var response = "";
        StartCoroutine(net.POST("/inventory/upgradeDungeon", json, (value) => response = value));
        await Task.Delay(TimeSpan.FromSeconds(1.5f));
        if (response == "NoScrap")
        {
            Debug.Log("Not Enough Scrap");
        }
        else
        {
            var valueSet = JsonConvert.DeserializeObject(response);
            Rarity item = JsonConvert.DeserializeObject<Rarity>(response);
            Debug.Log(item.dungeonRarity);
            this.dungeonRarity = item.dungeonRarity;
            setIconandButtonPrices();
        }
    }

}
