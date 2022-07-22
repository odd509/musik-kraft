using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1Projectile : MonoBehaviour
{
    public float thrust;
    public float damage;
    void Awake(){
        StartCoroutine(DestroyProjectile());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRB = other.GetComponent<Rigidbody2D>();
            if (enemyRB != null)
            {
                EnemyStats kill = other.GetComponent<EnemyStats>();
                kill.TakeDamage(damage);
                Vector2 difference = (enemyRB.transform.position - transform.position).normalized;
                difference = difference.normalized * thrust;
                enemyRB.AddForce(difference, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyProjectile(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
