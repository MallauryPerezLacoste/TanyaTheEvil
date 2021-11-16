using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInGame : MonoBehaviour
{
    [SerializeField] List<KeyCode> keysCamera;
    [SerializeField] float speedTranslate;
    [SerializeField] float speedTranslateHigh;
    [SerializeField] float speedRotate;
    [SerializeField] float mouseSpeedTranslate;
    [SerializeField] float mouseSpeedRotate;
   

    void Start()
    {
        keysCamera = GameObject.FindGameObjectWithTag("keyMapping").GetComponent<KeyMapping>().keysCamera;
    }


    void Update()
    {
        deplacement();
    }


    void deplacement()
    {
        //avant arriere
        if (Input.GetKey(keysCamera[0]))
        {
            deplacementForward(1);
        }
        if (Input.GetKey(keysCamera[1]))
        {
            deplacementForward(-1);
        }
        //gauche droite
        if (Input.GetKey(keysCamera[2]))
        {
            deplacementSide(-1);
        }
        if (Input.GetKey(keysCamera[3]))
        {
            deplacementSide(1);
        }
        //turn
        if (Input.GetKey(keysCamera[4]))
        {
            rotating(-1);
        }
        if (Input.GetKey(keysCamera[5]))
        {
            rotating(1);
        }
        
        if (Input.GetKey(KeyCode.Mouse2))
        {
            rotate();
        }
        //High down
        High();
    }

    void deplacementForward(int speedMoving)
    {
        transform.Translate(0, 0, speedMoving * speedTranslate * Time.deltaTime);
    }
    void deplacementSide(int speedMoving)
    {
        transform.Translate(speedMoving * speedTranslate * Time.deltaTime, 0, 0);
    }
    void rotating(int speedRotating)
    {
        transform.Rotate(0, speedRotating * speedRotate * Time.deltaTime, 0);
    }
    void High()
    {
        transform.Translate(0, speedTranslateHigh * Time.deltaTime * Input.mouseScrollDelta.y, 0);
    }
    void rotate()
    {
        transform.Rotate(Time.deltaTime * speedRotate * Input.GetAxis("Mouse Y"), -Time.deltaTime * mouseSpeedRotate * Input.GetAxis("Mouse X"), 0);
        Vector3 newEulerAngles = transform.eulerAngles;
        newEulerAngles.z = 0;
        transform.eulerAngles = newEulerAngles;
    }
}