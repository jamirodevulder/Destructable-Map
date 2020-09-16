using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class VRPlayerMovement : MonoBehaviour
{

    [SerializeField] private SteamVR_Action_Vector2 leftControllerInput;

    [SerializeField] private SteamVR_Action_Boolean turnRightInput;
    [SerializeField] private SteamVR_Input_Sources turnRightInputSource = SteamVR_Input_Sources.RightHand;

    [SerializeField] private SteamVR_Action_Boolean turnLeftInput;
    [SerializeField] private SteamVR_Input_Sources turnLeftInputSource = SteamVR_Input_Sources.RightHand;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject cameraParent;
    [SerializeField] private float speed = 1;
    [SerializeField] private int snapIncrement = 45;
    float smooth = 5.0f;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = camera.transform.TransformDirection(new Vector3(leftControllerInput.axis.x, 0, leftControllerInput.axis.y));
        transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
        
         if (turnLeftInput.GetStateDown(turnLeftInputSource))
            this.transform.RotateAround(camera.transform.position, Vector3.up, -Mathf.Abs(snapIncrement));
        if (turnRightInput.GetStateDown(turnRightInputSource))
            this.transform.RotateAround(camera.transform.position, Vector3.up, Mathf.Abs(snapIncrement));
        

    }
    void Start()
    {
        camera.transform.position = transform.position;
    }
}
