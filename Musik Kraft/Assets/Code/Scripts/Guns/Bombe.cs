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
    private bool canShoot = true;
    public float cooldown;

    private SpriteRenderer sprite;
    private GameObject soundManager;

    private void Awake()
    {
        firePointFront = transform.Find("FirePointFront");
        sprite = transform.Find("Instruments").transform.Find("sax").GetComponent<SpriteRenderer>();
        soundManager = transform.Find("SoundManager").gameObject;

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canShoot)
        {
            Shoot();
            canShoot = false;
            StartCoroutine(shootTimer());
        }
    }

    void Shoot()
    {
        StartCoroutine(spriteToggle());
        Vector2 projectileVector;
        soundManager.GetComponent<SoundPlayer>().Sax();
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
