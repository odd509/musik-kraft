using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float damage, health;
    
    // Update is called once per frame
    void Update()
    {
        if (health <= 0f){
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            Movement player = collider.GetComponent<Movement>();
            player.playerHP -= damage;
        }
    }

}
