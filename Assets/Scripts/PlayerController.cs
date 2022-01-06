using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float Sensitivity;
    [SerializeField] float DeadZone;
    [SerializeField] float massIncreaseAmt;
    [SerializeField] float maxVelocity;
    Gyroscope Gyro;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //Initialize Gyroscope
        Gyro = Input.gyro;
        Gyro.enabled = true;
    }

    void FixedUpdate()
    {
        Move();
        PlayerLimiter();
    }

    void Move()
    {
        Vector2 Rotation;
        Rotation.x = Gyro.attitude.eulerAngles.x;
        Rotation.y = Gyro.attitude.eulerAngles.y;
        //Their range is from 270 to 90 (goes to 360 and turns to 0)
        if(Rotation.x > 270)
        {
            //Go left (scaled down bc it's huge)
            Rotation.x = - ( 360 - Rotation.x )/ 90;
        }
        else
        {
            //Go right
            Rotation.x = Rotation.x / 90;
        }

        if(Rotation.y > 270)
        {
            //Go up (scaled down bc it's huge)
            Rotation.y = - ( 360 - Rotation.y )/ 90;
        }
        else
        {
            //Go left
            Rotation.y = Rotation.y / 90;
        }


        Vector3 pos = transform.position;
        pos = Vector3.zero;
        if(Mathf.Abs(Rotation.x)>DeadZone/90)
            pos.x -= Rotation.x;
        if(Mathf.Abs(Rotation.y)>DeadZone/90)
            pos.z -= Rotation.y;
        rigidbody.AddForce( pos * Sensitivity, ForceMode.Force);
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
    }
    
    void PlayerLimiter()
    {
        float zLimit = camera.orthographicSize;
        float xLimit = zLimit * camera.aspect;
        //Check position
        if( (transform.position.x > xLimit  ) ||
            (transform.position.x < -xLimit ) ||
            (transform.position.z > zLimit  ) || 
            (transform.position.z < -zLimit ) )
            {
                GameManager.Instance.Lose();
            }

    }
    
    public void IncreasePlayerMass()
    {
        rigidbody.mass += massIncreaseAmt;
    }

    public void ResetPlayer() => transform.position = Vector3.zero;

}