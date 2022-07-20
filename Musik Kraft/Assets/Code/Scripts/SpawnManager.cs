using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawn;

    void Start()
    {
        InvokeRepeating("Spawn", 3f, 2f);
    }

    void Spawn(){

        Instantiate(spawn, transform.position, Quaternion.identity);

    }

    
}
