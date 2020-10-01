using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    [Header("UC Game Manager")]

    public MovementController CubePlayerPrefab;
    public MovementController VRPlayerPrefab;
    [HideInInspector]
    public MovementController CubeLocalPlayer;
    [HideInInspector]
    public MovementController VRLocalPlayer;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            SceneManager.LoadScene("Menu");
            return;
        }
    }
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            MovementController.RefreshInstance(ref VRLocalPlayer, VRPlayerPrefab);
        }
        else
        {
            MovementController.RefreshInstance(ref CubeLocalPlayer, CubePlayerPrefab);
        }    
    }
}

