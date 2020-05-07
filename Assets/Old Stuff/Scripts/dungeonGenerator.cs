using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dungeonGenerator : MonoBehaviour {
    public GameObject hallway, room, connectionPoints;
    
    public int roomCount;

    private List<GameObject> roomList = new List<GameObject>();
    private List<GameObject> hallwayList = new List<GameObject>();
    private List<string> lastConnectedNode = new List<string>();
    [SerializeField]
    private string direction;
    private bool initialSetup = false;

	// Use this for initialization
	void Start () {
        /*var initialRoom = Instantiate(room);
        

        var initialHallway = Instantiate(hallway);
        translateHallwayOnPosition(initialHallway);*/
        
        for(int i = 0; i <= roomCount; i++)
        {
            var roomTemp = Instantiate(room);
            roomList.Add(roomTemp);
            if (initialSetup)
                connectRoomToHallway(hallwayList[i - 1], roomList[i-1].transform,roomList[i], lastConnectedNode[i - 1]);
            hallway.transform.position = chooseRandomPoint(roomList[i]);
            var hallwayTemp = Instantiate(hallway);
            hallwayList.Add(hallwayTemp);
            translateHallwayOnPosition(hallwayList[i]);
            
        }
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/
    private void connectRoomToHallway(GameObject hallway,Transform lastRoom,GameObject cRoom, string lastConnectedNode)
    {
        cRoom.transform.position = lastRoom.position;
        switch (lastConnectedNode)
        {
            case "north":
                Debug.Log("North");
                cRoom.transform.Translate(new Vector3(0, 0, 14.74f));
                break;
            case "south":
                Debug.Log("South");

                cRoom.transform.Translate(new Vector3(0, 0, -14.74f));
                break;
            case "east":
                Debug.Log("East");
                cRoom.transform.Translate(new Vector3(14.74f, 0,0));
                break;
            case "west":
                Debug.Log("west");
                cRoom.transform.Translate(new Vector3(-14.74f, 0, 0));
                break;
        }
    }
    private void translateHallwayOnPosition(GameObject hallway)
    {
        switch (direction) {
            case "north":
                hallway.transform.Translate(new Vector3(0, 0, 2.39f));
                break;
            case "south":
                hallway.transform.Translate(new Vector3(0, 0, -2.39f));
                break;
            case "east":
                hallway.transform.Translate(new Vector3(2.39f, 0, 0));
                hallway.transform.Rotate(new Vector3(0, -90, 0));
                break;
            case "west":
                hallway.transform.Translate(new Vector3(-2.39f, 0, 0));
                hallway.transform.Rotate(new Vector3(0, -90, 0));             
                break;
        }
        this.initialSetup = true;
    }
    private Vector3 chooseRandomPoint(GameObject room)
    {
        int connectionPoints = 0;
        List<GameObject> pointArray = new List<GameObject>();
        foreach(Transform g in room.transform)
        { 
            if(g.tag == "connectPoint")
            {
                pointArray.Add(g.gameObject);
                connectionPoints++;
                
            }
        }
        int RandomGen = Random.Range(0, connectionPoints);
        direction = pointArray[RandomGen].GetComponent<proceduralPoint>().Direction;
        lastConnectedNode.Add(direction);
        return pointArray[RandomGen].gameObject.transform.position;
    }







}
