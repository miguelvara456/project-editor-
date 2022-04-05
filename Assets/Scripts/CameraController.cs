using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private KeyCode centralCamera;
    [SerializeField] private KeyCode moveUp;
    [SerializeField] private KeyCode moveDown;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode movefoward;
    [SerializeField] private KeyCode moveBackFoward;
    [SerializeField] private KeyCode rotate;
    [SerializeField] private KeyCode moverFast;
    [SerializeField] private float vel;
    [SerializeField] private float velInitial;
    [SerializeField] private float sensivility;
    [SerializeField] private Transform posCentral;
    [SerializeField] private float maxX,minX,maxY,minY;
    public Transform target;
    public Vector3 targetOffset;
    public float distance = 5.0f;
    public float maxDistance = 20;
    public float minDistance = .6f;
    public int yMinLimit = -80;
    public int yMaxLimit = 80;
    public int zoomRate = 40;
    public float panSpeed = 0.3f;
    public float zoomDampening = 5.0f;

    private float xDeg = 0.0f;
    private float yDeg = 0.0f;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;
    private float timeFast;
    void Start()
    {
        Init();
    }
    

     void Init()
    {
        ResetPosition();
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position + (transform.forward * distance);
            target = go.transform;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance;

        //be sure to grab the current rotations as starting points.
        position = transform.position;
        rotation = transform.rotation;
        desiredRotation = transform.rotation;

        xDeg = Vector3.Angle(Vector3.right, transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
    }

   
    //void LateUpdate()
    //{
    //    // If Control and Alt and Middle button? ZOOM!

    //    // If middle mouse and left alt are selected? ORBIT

    //    // otherwise if middle mouse is selected, we pan by way of transforming the target in screenspace
        

    //    ////////Orbit Position

    //    // affect the desired Zoom distance if we roll the scrollwheel
    //    //desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
    //    //clamp the zoom min/max
    //    //desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
    //    // For smoothing of the zoom, lerp distance
       

    //    // calculate position based on the new currentDistance 
    //    //position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
    //    //transform.position = position;
    //}


    void LateUpdate()
    {
         Inputs();
         ClampPositionCamera();
    }

    void Inputs()
    {
        if(Input.GetKey(rotate))
        {
            Rotate();
            Move();
            Move(moveUp, Vector3.up);
            Move(moveDown, -Vector3.up);
        }
      
          
            
        if(Input.GetKey(moverFast))
        {
            timeFast += Time.deltaTime;
            vel += timeFast; 
        }
        else
        {
            timeFast = 0;
            vel = velInitial;
        }

       


        if (Input.GetMouseButton(2))
        {
            OrbitCamera();
        }
        else if(Input.GetKey(KeyCode.LeftControl))
        {
            ZoomCamera();
        }
          

        if(Input.GetKeyDown(centralCamera))
        {
            ResetPosition();
        }

    }

     void ZoomCamera()
    {
        desiredDistance -= Input.GetAxis("Mouse Y") * Time.deltaTime * zoomRate * 0.125f * Mathf.Abs(desiredDistance);
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
    }

     void OrbitCamera()
    {
        
            //grab the rotation of the camera so we can move in a psuedo local XY space
            target.rotation = transform.rotation;
            target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
            target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);

        // calculate position based on the new currentDistance 
        position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        transform.position = position;
    }

     void Rotate()
    {
        xDeg += Input.GetAxis("Mouse X") * sensivility * 0.02f;
        yDeg -= Input.GetAxis("Mouse Y") * sensivility * 0.02f;

      

        //Clamp the vertical axis for the orbit
        yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
        // set camera rotation 
        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
        //currentRotation = transform.rotation;

        //rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = desiredRotation;
    }

    void Move()
    {
        float x = 0f;
        float z = 0f;
        Vector3 move;


        if (Input.GetKey(movefoward))
        {
            z = 1;
        }

        if (Input.GetKey(moveBackFoward))
        {
            z = -1;
        }

        if (Input.GetKey(moveRight))
        {
            x = 1;

        }

        if (Input.GetKey(moveLeft))
        {
            x = -1;     
        }

             move = Vector3.right * x + Vector3.forward * z;

            transform.Translate(move * vel * Time.deltaTime);
    }

    void Move(KeyCode _key, Vector3 _direcction)
    {
        if (Input.GetKey(_key))
        {
            transform.Translate(_direcction * vel * Time.deltaTime);
        }
    }
    float ClampAngle(float _angle, float _min, float _max)
    {
        if (_angle < -360)
            _angle += 360;
        if (_angle > 360)
            _angle -= 360;
        return Mathf.Clamp(_angle, _min, _max);
    }

    void ClampPositionCamera()
    {
        if(transform.position.x>=maxX || transform.position.x <= minX || transform.position.y >= maxY || transform.position.y <= minY)
        {
            ResetPosition();
        }

    }

    void ResetPosition()
    {
        transform.position = posCentral.position;
        transform.rotation = posCentral.rotation;
    }
}

