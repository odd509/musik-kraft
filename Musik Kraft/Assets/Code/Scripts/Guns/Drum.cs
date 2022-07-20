using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public GameObject damageSprite;

    public float effectDuration = 0.2f;
    public float groundDamage = 40f;
    public float areaDamage = 15f;
    public float knockbackForce = 2f;
    public float radius = 10f;

    private GameObject camera;

    bool keyPressed = false;

    private void Start()
    {
        camera = GameObject.FindWithTag("MainCamera");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            camera.GetComponent<Shake>().ShakeCamera();

            StartCoroutine(DamageEffect());

            Collider2D[] results = Physics2D.OverlapCircleAll(GetComponent<Transform>().position, radius);
            Debug.Log(results[0].gameObject.name);
            foreach (Collider2D collider in results)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("Enemy");
                    GameObject enemy = collider.gameObject;

                    enemy.GetComponent<EnemyStats>().TakeDamage(groundDamage);


                    Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                    Vector2 difference = (rb.transform.position - transform.position).normalized;
                    difference = difference.normalized * knockbackForce;
                    rb.AddForce(difference, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GetComponent<Transform>().position, radius);
    }

    private IEnumerator DamageEffect()
    {
        damageSprite.SetActive(true);
        yield return new WaitForSeconds(effectDuration);
        damageSprite.SetActive(false);
    }
}
