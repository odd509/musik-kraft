using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bombe : MonoBehaviour
{

    public Transform firePointFront;
    public GameObject bullet;
    public float projectileSpeed = 20f;
    

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 projectileVector;
        var proj = Instantiate(bullet, firePointFront.position, Quaternion.identity); 

        if ((Input.GetKey(KeyCode.UpArrow)))
        {
            projectileVector = Vector2.up;
            proj.transform.Rotate(0, 0,  90);

        }
        else
        {
            projectileVector = GetComponent<Movement>().isFacingRight ? (Vector2.right + Vector2.up).normalized : (Vector2.left + Vector2.up).normalized;

            float rotationAmount = GetComponent<Movement>().isFacingRight ? 45 : -45;
            
            proj.transform.Rotate(0, 0, rotationAmount);
        }

        
        proj.GetComponent<GuitarProjectileMovement>().velocityVector = projectileVector * projectileSpeed;

    }
}
