using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GuitarWeapon : MonoBehaviour
{

    public Transform firePointFront;
    public GameObject bullet;
    public float projectileSpeed = 20f;
    private bool canShoot = true;
    public float cooldown;

    private SpriteRenderer sprite;
    
    private void Awake()
    {
        firePointFront = transform.Find("FirePointFront");
        sprite = transform.Find("Instruments").transform.Find("guitar").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) && canShoot)
        {
            Shoot();
            canShoot= false;
            StartCoroutine(shootTimer());
        }
    }

    void Shoot()
    {
        StartCoroutine(spriteToggle());
        Vector2 projectileVector;
        var proj = Instantiate(bullet, firePointFront.position, Quaternion.identity); 
        
        

        if (Input.GetKey(KeyCode.UpArrow) && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            projectileVector = GetComponent<Movement>().isFacingRight ? (Vector2.right + Vector2.up).normalized : (Vector2.left + Vector2.up);

            float rotationAmount = GetComponent<Movement>().isFacingRight ? 45 : -45;
            
            proj.transform.Rotate(0, 0, rotationAmount);
        }
        else if ((Input.GetKey(KeyCode.UpArrow)))
        {
            projectileVector = Vector2.up;
            proj.transform.Rotate(0, 0,  90);

        }
        else
        {
            projectileVector = GetComponent<Movement>().isFacingRight ? Vector2.right : Vector2.left;
        }

        
        proj.GetComponent<GuitarProjectileMovement>().velocityVector = projectileVector * projectileSpeed;

    }
    IEnumerator shootTimer(){
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
    
    IEnumerator spriteToggle()
    {
        sprite.enabled = true;
        yield return new WaitForSeconds(1);
        sprite.enabled = false;

    }
}
