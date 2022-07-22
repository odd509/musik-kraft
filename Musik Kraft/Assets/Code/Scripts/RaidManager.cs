using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidManager : MonoBehaviour
{
    
    public GameObject spawn;
    public float coolDown;
    public bool startRaid = false;
    private bool spawnAvailable = true;
    public float startTime, endTime;

    void Update(){
        
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

        Instantiate(spawn, transform.position, Quaternion.identity);

    }
    IEnumerator spawnTimes(){
        yield return new WaitForSeconds(startTime);
        startRaid = true;
        startEnemy();
        yield return new WaitForSeconds(endTime - startTime);
        startRaid = false;
        

    }
    
}
