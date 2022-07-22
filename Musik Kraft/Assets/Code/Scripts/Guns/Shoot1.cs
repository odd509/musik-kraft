using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1 : MonoBehaviour
{
    [SerializeField] public int numberOfProjectiles;

    [SerializeField] public GameObject projectile;

    private Vector2 startPoint;
    public GameObject player;
    private float radius, movespeed;
    private bool canShoot = true;
    public float cooldown;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake()
    {
        radius = 5f;
        movespeed = 15f;
        sprite = transform.Find("Instruments").transform.Find("acordion").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && canShoot){
            canShoot = false;
            startPoint = player.transform.position;
            SpawnProjectiles (numberOfProjectiles);
            StartCoroutine(spriteToggle());
            StartCoroutine(shootTimer());
        }
    }
    void SpawnProjectiles (int numberOfProjectiles){
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
