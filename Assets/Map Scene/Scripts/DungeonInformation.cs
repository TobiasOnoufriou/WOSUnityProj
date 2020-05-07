using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonInformation: MonoBehaviour
{
    [SerializeField] private int m_dungeonRarity;
    [SerializeField] private string m_dungeonID;
    public void Start()
    {
        
    }
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
}
