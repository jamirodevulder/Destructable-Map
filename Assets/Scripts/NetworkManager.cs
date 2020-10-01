using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private string gameVersion = "v1";
    [SerializeField] private Text inputText;
    [SerializeField] private Text errorText;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ConnectToMaster();
    }
    public void ConnectToMaster()
    {
        PhotonNetwork.OfflineMode = false;           //true would "fake" an online connection
        PhotonNetwork.NickName = "RobotAlien" + Random.Range(0, 100).ToString() + Random.Range(0, 100).ToString();       //to set a player name
        PhotonNetwork.AutomaticallySyncScene = true; //to call PhotonNetwork.LoadLevel()
        PhotonNetwork.GameVersion = gameVersion;            //only people with the same game version can play together
        //PhotonNetwork.ConnectToMaster(ip,port,appid); //manual connection
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected to Master!");
    }
    public void OnClickJoin()
    {
        if (!PhotonNetwork.IsConnected)
        {
            print("misluktePoging");
            return;
        }
        //PhotonNetwork.CreateRoom("Peter's Game 1"); //Create a specific Room - Error: OnCreateRoomFailed
        PhotonNetwork.JoinRoom(inputText.text);   //Join a specific Room   - Error: OnJoinRoomFailed  
        //Join a random Room     - Error: OnJoinRandomRoomFailed  
        print("join");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        //no room available
        //create a room (null as a name means "does not matter")
        PhotonNetwork.CreateRoom(inputText.text, new RoomOptions { MaxPlayers = 20 });
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        Debug.Log(message);
        base.OnCreateRoomFailed(returnCode, message);
        errorText.text = message;
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Master: " + PhotonNetwork.IsMasterClient + " | Players In Room: " + PhotonNetwork.CurrentRoom.PlayerCount + " | RoomName: " + PhotonNetwork.CurrentRoom.Name);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("SampleScene");
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log(cause);
        PhotonNetwork.LoadLevel("JoinGame");
        ConnectToMaster();
    }
    public void SinglePlayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }
}
