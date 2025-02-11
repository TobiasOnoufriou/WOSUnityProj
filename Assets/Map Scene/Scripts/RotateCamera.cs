﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Ray hitMove = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (Physics.Raycast(hitMove, out hit))
            {
                if (hit.collider != null && hit.collider.tag == "RotateZone")
                {
                    var touch = Input.GetTouch(0);
                    x += touch.deltaPosition.x * xSpeed * 0.02f;
                    y -= touch.deltaPosition.y * ySpeed * 0.02f;
                    //x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                    //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                    y = ClampAngle(y, yMinLimit, yMaxLimit);

                    Quaternion rotation = Quaternion.Euler(y, x, 0);

                    distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
                    RaycastHit thing;

                    if (Physics.Linecast(target.position, transform.position, out thing))
                    {
                        distance -= thing.distance;
                    }
                    Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
                    Vector3 position = rotation * negDistance + target.position;

                    transform.rotation = rotation;
                    transform.position = position;
                }
            }
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}