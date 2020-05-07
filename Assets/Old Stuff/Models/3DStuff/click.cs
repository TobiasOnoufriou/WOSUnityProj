﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}

    void OnMouseDown()
    {
        
    }

    // Update is called once per frame
    void Update () {
        int layerMask = 1 << 8;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
