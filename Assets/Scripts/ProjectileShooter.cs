using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject gun;

    [SerializeField] private SteamVR_Action_Boolean rightButtonInput;
    [SerializeField] private SteamVR_Input_Sources rightButtonInputSource = SteamVR_Input_Sources.RightHand;
    [SerializeField] private bool inhand = false;
    void Update()
    {
        if (inhand)
        {
            if (rightButtonInput.GetStateDown(rightButtonInputSource))
            {
                GameObject shot = Instantiate(bullet) as GameObject;
                shot.transform.position = transform.position + gun.transform.forward;
                Rigidbody rb = shot.GetComponent<Rigidbody>();
                rb.velocity = gun.transform.forward * 30;
            }
        }
    }
}
