using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxing : MonoBehaviour
{

    public Transform[] backgrounds;         //Array(list) of all the back and foreground to be parallexed.
    private float[] parallaxScales;          // The properstion of the camera's movement to move the background by.
    public float smoothing = 1f;          //How smooth the parallax is going to be. Set this above 0.

    private Transform cam;                  // Reference to the main camera transform.
    private Vector3 previousCamPos;         // The position of the camera in previous frame.


    
    void Awake()
    {
        //set up the camera referance.
        cam = Camera.main.transform;
        
    }

    void Start()
    {
        //the previous frame had the current frame's camera position.
        previousCamPos = cam.position;

        // assigning coresponding paraxxedScales
        parallaxScales = new float[backgrounds.Length];
        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z* -1;
        }
    }

    void Update()
    {

        // for each background
        for (int i = 0; i < backgrounds.Length; i++)
        {
            //the parallax is the opposite of the camera movement because the previous frame multiplied by the scale.
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            //set a target x position which is the current position plus parallax.
            float backgroundTragetPosX = backgrounds[i].position.x + parallax;

            // create a target position which is the background's current position with it's target x position.
            Vector3 backgroundTargetPos = new Vector3(backgroundTragetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between cureent position and the target position using lerp.
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);


        }
        //set the previousCamPos to the camera's position at the end of the frame.
        previousCamPos = cam.position;



    }
}
