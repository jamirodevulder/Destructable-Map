using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Valve.VR;
using Valve.VR.InteractionSystem;
[RequireComponent(typeof(LineRenderer))]
public class ObjectPlacer: MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean rightButtonInput;
    [SerializeField] private SteamVR_Action_Boolean ArightButtonInput;
    [SerializeField] private SteamVR_Input_Sources rigtButtonInputSource = SteamVR_Input_Sources.RightHand;
    [SerializeField] private float length;
    [SerializeField] private GameObject transparentArsenalObject;
    [SerializeField] private GameObject ArsenalObject;
    [SerializeField] private string groundTagName;
    private LineRenderer line;
    private PlaceObject placeObject;
    private ObjectPlaceAble objectPlaceAble;
    private bool placeIt = false;

    // Start is called before the first frame update
    void Start()
    {
        ArsenalObject = Instantiate(ArsenalObject, new Vector3(1000, 1000, 1000), Quaternion.identity);
        transparentArsenalObject = Instantiate(transparentArsenalObject, new Vector3(1000, 1000, 1000), Quaternion.identity);
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        placeObject = GetComponent<PlaceObject>();
        objectPlaceAble = transparentArsenalObject.GetComponent<ObjectPlaceAble>();


    }

    // Update is called once per frame
    void Update()
    {
        if (rightButtonInput.GetStateDown(rigtButtonInputSource) && !GetComponent<Occupie>().GetOccuPied())
        {
            line.enabled = true;
            transparentArsenalObject.SetActive(true);
        }
        if (rightButtonInput.GetState(rigtButtonInputSource) && !GetComponent<Occupie>().GetOccuPied())
        {
            UselineRenderer();
            line.enabled = true;
            transparentArsenalObject.SetActive(true);
            if (ArightButtonInput.GetStateDown(rigtButtonInputSource))
            {
                transparentArsenalObject.transform.Rotate(0,+90,0);
            }
        }
        else if(rightButtonInput.GetStateUp(rigtButtonInputSource) && !GetComponent<Occupie>().GetOccuPied())
        {
            if (placeIt)
            {
                ArsenalObject.transform.position = transparentArsenalObject.transform.position;
                ArsenalObject.transform.rotation = transparentArsenalObject.transform.rotation;
            }
            line.enabled = false;
            transparentArsenalObject.SetActive(false);
        }
        if(line.enabled && GetComponent<Occupie>().GetOccuPied())
        {
            line.enabled = false;
            transparentArsenalObject.SetActive(false);
        }
    }



    private void UselineRenderer()
    {
        line.SetPosition(0, transform.position);
        Vector3 endPosition = CalculateEnd();
        line.SetPosition(1, endPosition);
        transparentArsenalObject.transform.position = new Vector3(endPosition.x, endPosition.y + 0.2f, endPosition.z);
    }
    private Vector3 CalculateEnd()
    {
        RaycastHit hit = ForwardRayCast();
        Vector3 endposition = DefaultEnd();
        if(hit.collider)
        {
            endposition = hit.point;
        }
        CheckObjectAllowedToBePlaced(hit);
        return endposition;
    }
    private void CheckObjectAllowedToBePlaced(RaycastHit hit)
    {
        if(hit.collider == null || hit.collider.gameObject.tag != groundTagName || !objectPlaceAble.CheckIfObjectIsAllowedToBePlaced())
        {
            placeObject.CantPlaceHere();
            placeIt = false;
        }
        else if(objectPlaceAble.CheckIfObjectIsAllowedToBePlaced())
        {
            placeIt = true;
            placeObject.CanPlaceHere();
        }

    }
    private Vector3 DefaultEnd()
    {
        return transform.position + (transform.forward * length);
    }
    private RaycastHit ForwardRayCast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, length);
        return hit;
    }
}
