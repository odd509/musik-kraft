using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float maxHealth = 100;
    float health;

    [Header("iFrames")]
    [SerializeField] private float invulnerabilityDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("HealthBar")]
    public HealthBarBehavior healthBar;

    private void Awake()
    {
        health = maxHealth;
        healthBar.setHealth(health,maxHealth);
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.setHealth(health,maxHealth);
        
        StartCoroutine(Invulnerability());
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    public void Heal(float heal)
    {
        health = Mathf.Min(maxHealth, (heal + health));
    }

    public float getHP() { return health; }

    private IEnumerator Invulnerability(){
        Physics2D.IgnoreLayerCollision(3,9,true);
        for (int i = 0; i <numberOfFlashes; i++){
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(3,9,false);
    }
}
