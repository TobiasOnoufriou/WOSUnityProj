using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {
    private int itemLevel;
    private string itemName;
    private int itemID;
    private string itemType;
    private int armourStats;
    private float weaponDamage;

    public Item()
    {
        Debug.Log("Constructed");
    }
    public void setArmourStats(int newarmourStats)
    {
        this.armourStats = newarmourStats;
    }
    public int getArmourStats()
    {
        return this.armourStats;
    }
    public void setweaponDamge(float damage)
    {
        this.weaponDamage = damage;
    }
    public float getweaponDamage()
    {
        return this.weaponDamage;
    }
    public void setItemLevel(int userLevel)
    {
        int min = userLevel - 3, max = userLevel + 1;
        this.itemLevel = Random.RandomRange(min, max);
    }
    public int getItemLevel(int userLevel)
    {
        return this.itemLevel;
    }
    public void setItemType(string newitemType)
    {
        this.itemType = newitemType;
    }
    public string getItemType()
    {
        return this.itemType;
    }
    public int getItemId()
    {
        return this.itemID;
    }
    public void setItemId(int newItemId)
    {
        this.itemID = newItemId;
    }
    public void setItemName(string newitemName)
    {
        this.itemName = newitemName;
    }
    public string getItemName()
    {
        return this.itemName;
    }

}
