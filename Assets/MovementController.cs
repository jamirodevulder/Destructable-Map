using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementController : MonoBehaviourPun, IPunObservable
{
    private Quaternion playerRotation;
    private Vector3 playerPosition;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    [SerializeField] private float speed = 10;
    [SerializeField] private Camera[] objects;
    [SerializeField] private MonoBehaviourPun[] scripts;
    [SerializeField] private MonoBehaviour[] monoScripts;
    [SerializeField] private bool test = true;
    private Vector3 recievePosition;
    private Quaternion recieveRotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetPlayerMovement(transform.position, transform.rotation);
      //  if (!photonView.IsMine)
      //  {
          //  float step = speed * Time.deltaTime;
           // transform.position = Vector3.MoveTowards(transform.position, recievePosition, step);
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, recieveRotation, step);
     //   }
    }
    public void Awake()
    {
        if (!photonView.IsMine && test)
        {
            test = false;
            for(int i = 0; i < scripts.Length; i++)
            {
                Destroy(scripts[i]);
            }
            for (int i = 0; i < objects.Length; i++)
            {
                Destroy(objects[i]);
                print("camera weg!");
            }
            for (int i = 0; i < monoScripts.Length; i++)
            {
                Destroy(monoScripts[i]);
            }
        }
    }
    public static void RefreshInstance(ref MovementController player, MovementController Prefab)
    {
        var position = new Vector3(Random.Range(0,10),0,Random.Range(0, 10));
        var rotation = Quaternion.identity;
        if (player != null)
        {
            position = player.transform.position;
            rotation = player.transform.rotation;
            PhotonNetwork.Destroy(player.gameObject);
        }

        player = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation).GetComponent<MovementController>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerPosition);
            stream.SendNext(playerRotation);

        }
        else
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();

        }
    }

    public void SetPlayerMovement(Vector3 position, Quaternion rotation)
    {
        playerPosition = position;
        playerRotation = rotation;
    }
}
