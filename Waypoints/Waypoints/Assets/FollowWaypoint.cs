using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    public float speed = 20.0f;
    public GameObject[] waypoints;
    private int wpLoc=0;
    // Update is called once per frame
    void Update()
    {
        if(waypoints.Length != 0) {
            float distance = Vector3.Distance(this.transform.position, waypoints[wpLoc].transform.position);
            if(distance < 3) 
            {
                wpLoc ++;
            }
            if(wpLoc >= waypoints.Length) wpLoc=0;

            this.transform.LookAt(waypoints[wpLoc].transform);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
