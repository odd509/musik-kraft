using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarWeapon : MonoBehaviour
{

    public Transform firePointFront;
    public GameObject bullet;
    

    private void Awake()
    {
        firePointFront = transform.Find("FirePointFront");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        var proj = Instantiate(bullet, firePointFront.position, firePointFront.rotation);
    }
}
