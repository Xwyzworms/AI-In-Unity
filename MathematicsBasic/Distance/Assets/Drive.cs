using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// A very simplistic car driving on the x-z plane.

public class Drive : MonoBehaviour
{
    public float speed = 1000.0f;
    public float rotationSpeed = 100.0f;
    public GameObject fuel ; 
    private bool moveDude = false;


    private void CalculateDistance() 
    {
        float distance = Mathf.Sqrt(Mathf.Pow(this.transform.position.x - fuel.transform.position.x , 2) +
                                    Mathf.Pow(this.transform.position.y - fuel.transform.position.y, 2));

        Vector3 tankPosition = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        Vector3 fuelPosition = new Vector3(fuel.transform.position.x, fuel.transform.position.y, 0);

        Vector3 tankToFuel = tankPosition - fuelPosition;
        Debug.Log("Distance : " + distance);
        Debug.Log("Udistance : " + Vector3.Distance(tankPosition, fuelPosition) );
        Debug.Log("SqrMagnitude : " + tankToFuel.sqrMagnitude);
    }

    private void CalculateAngle() 
    {
        // transform.up Consider the Rotation, And that is important for calculating Angle
        Vector3 tankUp = this.transform.up;
        Vector3 fuelDistance = fuel.transform.position - this.transform.position ;
        Debug.DrawRay(this.transform.position, tankUp * 10, Color.green, 2);
        Debug.DrawRay(this.transform.position, fuelDistance, Color.red, 2);
        
        float dot = tankUp.x * fuelDistance.x + tankUp.y * fuelDistance.y;
        
        float beta = Mathf.Acos( dot / (tankUp.magnitude * fuelDistance.magnitude));

        Debug.Log("Angle : " + beta * Mathf.Rad2Deg);
        Debug.Log("UnityAngle : " + Vector3.Angle(tankUp, fuelDistance) );
        Debug.Log("CrossProduct" + CrossProduct(tankUp, fuelDistance));
        int clockWise = 1;
        if(CrossProduct(tankUp, fuelDistance).z > 0) 
        {
            clockWise = -1;
        }
        this.transform.Rotate(0, 0, beta * Mathf.Rad2Deg * clockWise  );
    }


    private Vector3 CrossProduct(Vector3 v, Vector3 w) 
    {
        float xMult = v.y * w.z - v.z * w.y;
        float yMult = v.x * w.z - v.z * w.x;
        float zMult = v.y * w.x - v.x * w.y;

        return new Vector3 ( xMult, yMult, zMult);
    }
    private void Move() 
    {
        CalculateAngle();
        Vector3 direction = fuel.transform.position - this.transform.position; 
        Debug.Log(direction.magnitude);
        if (direction.magnitude > 4) 
        {
            Vector3 velocity = direction.normalized * Time.deltaTime * speed;
            this.transform.position = this.transform.position + velocity;
            

        }

    }

    void LateUpdate()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, translation, 0);

        // Rotate around our y-axis
        transform.Rotate(0, 0, -rotation);

        if(moveDude) 
        {
            Move();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateDistance();
            CalculateAngle();
        }
        else if(Input.GetKeyDown(KeyCode.T)) 
        {
            // Do Stuff here
            // 1. Calculate Angle
            // 2. Turn the Agent
            // 3. Calculate Distance
            // 4. Move the Tank to Position
            moveDude = true;

        }

    }
}
