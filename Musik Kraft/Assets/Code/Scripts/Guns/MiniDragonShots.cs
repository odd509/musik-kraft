using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniDragonShots : MonoBehaviour
{
    public float thrust;
    public float damage;
    void Awake()
    {
        StartCoroutine(DestroyProjectile());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = other.GetComponent<Rigidbody2D>();
            if (playerRB != null)
            {
                other.GetComponent<PlayerStats>().TakeDamage(damage);
                Vector2 difference = (playerRB.transform.position - transform.position).normalized;
                difference = difference.normalized * thrust;
                playerRB.AddForce(difference, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
