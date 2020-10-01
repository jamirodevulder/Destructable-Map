using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Player, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
