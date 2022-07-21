using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public float attackTimer = 3f;
    public float flySpeed = 1f;
    public float fallSpeed = 1f;

    private Animator _animator;
    
    
    private float timer;

    private void Awake()
    {
        timer = attackTimer;
        _animator = GetComponent<Animator>();
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
        yield return new WaitForSeconds(2);

        
        GetComponent<Rigidbody2D>().velocity = Vector2.down * fallSpeed;
        _animator.SetTrigger("Fall");
        while (!GetComponent<IsGrounded>().onGround)
        {
            yield return new WaitForSeconds(0.1f);
        }
        
        GetComponent<Shake>().ShakeCamera();

        yield return new WaitForSeconds(1f);
        //shake zart zort
        
        GetComponent<Rigidbody2D>().velocity = Vector2.up * flySpeed;
        _animator.SetTrigger("Fly");
        yield return new WaitForSeconds(0.5f);
        GetComponent<EnemyAI>().enabled = true;

    }
    
}
