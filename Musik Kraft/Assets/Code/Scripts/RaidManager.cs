using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidManager : MonoBehaviour
{
    
    public GameObject spawn;
    public float coolDown;
    public bool startRaid = false;
    public float startTime;
    public float duration;

    private bool spawnAvailable = true;

    void Awake(){
        
        StartCoroutine(spawnTimes());
    }
    void startEnemy(){
        if(startRaid && spawnAvailable){
            InvokeRepeating("Spawn",0f,coolDown);
            spawnAvailable = false;
        }
        else if(!startRaid){
            CancelInvoke();
            spawnAvailable = true;
        }
    }

    void Spawn(){

        GameObject copy = Instantiate(spawn, transform.position, Quaternion.identity);
        copy.SetActive(true);

    }
    IEnumerator spawnTimes(){
        yield return new WaitForSeconds(startTime);
        startRaid = true;
        startEnemy();
        yield return new WaitForSeconds(duration);
        startRaid = false;
        startEnemy();
        

    }
    
}
