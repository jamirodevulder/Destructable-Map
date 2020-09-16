using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] private Color placeAbleColor;
    [SerializeField] private Color NotPlacableColor;

    // Update is called once per frame
    public void CanPlaceHere()
    {
        material.color = placeAbleColor;
    }

    public void CantPlaceHere()
    {
        material.color = NotPlacableColor;
    }
}
