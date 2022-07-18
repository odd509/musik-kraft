using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombeProj : MonoBehaviour
{
    public float damage;
    public float thrust;
    public float splashRange;
    
    private void OnTriggerEnter2D(Collider2D collusion){
        if (splashRange > 0){
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, splashRange);
            foreach (var hitCollider in hitColliders){
                if(hitCollider.CompareTag("Platform")){
                    continue;
                }
                var kill = hitCollider.GetComponent<EnemyStats>();
                var enemy = hitCollider.GetComponent<Rigidbody2D>();
                if (kill){
                    var closestPoint = hitCollider.ClosestPoint(transform.position);
                    var distance = Vector3.Distance(closestPoint, transform.position);

                    var damagePercent = Mathf.InverseLerp(splashRange,0,distance);
                    Vector2 difference = (enemy.transform.position - transform.position).normalized;
                    difference = difference.normalized * thrust;
                    enemy.AddForce(difference, ForceMode2D.Impulse);
                    kill.health -= (damagePercent * damage);
                }
            }
        }
        else{
            var enemy = collusion.GetComponent<EnemyStats>();
            if(enemy){
                enemy.health -= damage;
            }
        }
    }
}
