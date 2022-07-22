using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    [SerializeField] public GameObject projectile;
    
    public float attackTimer = 3f;
    public float flySpeed = 1f;
    public float fallSpeed = 1f;

    private Animator _animator;
    private GameObject _damageEffect;
    private Vector2 startPoint;
    private float radius, movespeed;
    
    private float timer;

    private void Awake()
    {
        timer = attackTimer;
        _animator = GetComponent<Animator>();
        _damageEffect = transform.Find("Area").gameObject;
        
        radius = 5f;
        movespeed = 15f;
    }

    private void Update()
    {

        if (timer < 0)
        {
            timer = attackTimer;
            StartCoroutine(Attack());
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }


    public IEnumerator Attack()
    {
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.up * flySpeed;
        yield return new WaitForSeconds(1.5f);

        
        GetComponent<Rigidbody2D>().velocity = Vector2.down * fallSpeed;
        _animator.SetTrigger("Fall");
        while (!GetComponent<IsGrounded>().onGround)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        
        //shake zart zort
        GetComponent<Shake>().ShakeCamera();
        StartCoroutine(AreaEffect());
        
        yield return new WaitForSeconds(1f);
        
        
        GetComponent<Rigidbody2D>().velocity = Vector2.up * flySpeed;
        _animator.SetTrigger("Fly");

        for (int i = 0; i < 5; i++)
        {
            SpawnProjectiles(7);
            yield return new WaitForSeconds(0.3f);
        }
        
        
        GetComponent<EnemyAI>().enabled = true;

    }

    IEnumerator AreaEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            _damageEffect.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _damageEffect.SetActive(false); 
            yield return new WaitForSeconds(0.1f);

        }

    }
    
    void SpawnProjectiles (int numberOfProjectiles){
        startPoint = transform.position;
        float angleStep = 360f / numberOfProjectiles;
        float angle = 90f;

        for(int i = 0; i <= numberOfProjectiles - 1; i++){

            float projectileDirXPosition = startPoint.x + Mathf.Sin ((angle* Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos ((angle* Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2 (projectileDirXPosition, projectileDirYPosition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * movespeed;

            var proj = Instantiate (projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2 (projectileMoveDirection.x, projectileMoveDirection.y);

            angle += angleStep;
        }
    }
    
}
