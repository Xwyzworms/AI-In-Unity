using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject goal;
    public Vector3 direction;
    public float speeds;

    public void Start() 
    {
    }


    public void LateUpdate() 
    {

        direction = goal.transform.position - this.transform.position;
        if(direction.magnitude > 2) 
        {   
            this.transform.LookAt(goal.transform);
            Vector3 velocity = direction.normalized * speeds * Time.deltaTime;
            this.transform.position = this.transform.position + velocity;

        }
    }

}

