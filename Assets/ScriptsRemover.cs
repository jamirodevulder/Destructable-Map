using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsRemover : MonoBehaviourPun
{
    [SerializeField] private Camera[] objects;
    [SerializeField] private MonoBehaviourPun[] scripts;
    [SerializeField] private MonoBehaviour[] monoScripts;
    [SerializeField] private bool test = true;
    public void Awake()
    {
        if (!photonView.IsMine && test)
        {
            test = false;
            for (int i = 0; i < scripts.Length; i++)
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
}
