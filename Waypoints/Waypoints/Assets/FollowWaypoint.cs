using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 10.0f;
    public float tracker_additionSpeed = 20;
    public GameObject[] waypoints;
    
    private GameObject tracker;
    private int wpLoc=0;
    // Update is called once per frame
    //
    //
    

    void ProgressTracker()
    {
        if(Vector3.Distance(tracker.transform.position, this.transform.position) >= 10) return;

        if(Vector3.Distance(tracker.transform.position, waypoints[wpLoc].transform.position) < 3) 
        {
            wpLoc +=1 ;
            
            if(wpLoc >= waypoints.Length) 
            {
                wpLoc = 0;
            }
        }
        tracker.transform.LookAt(waypoints[wpLoc].transform);
        tracker.transform.Translate(0, 0, (speed + tracker_additionSpeed) * Time.deltaTime);

    }
    void Start() 
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.transform.position = this.transform.position;
        tracker.GetComponent<MeshRenderer>().enabled =false;
        tracker.transform.rotation = this.transform.rotation;
            
    }
    void Update()
    {
        if(waypoints.Length != 0) {
            ProgressTracker();

            Quaternion wpLookat = Quaternion.LookRotation(tracker.transform.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, wpLookat, rotationSpeed * Time.deltaTime);
//          this.transform.LookAt(waypoints[wpLoc].transform);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
