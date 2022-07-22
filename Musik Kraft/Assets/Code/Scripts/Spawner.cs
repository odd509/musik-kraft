using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawn;
    public float coolDown;

    private bool spawnAvailable = true;
    private bool startRaid = false;

    // startAfterSec : Kaç saniye bekleyip baþlatacaðýný giriyor.
    public void StartRaid(float startAfterSec, float duration)
    {
        StartCoroutine(TimeControl(startAfterSec, duration));
    }
    void StartEnemy()
    {
        if (startRaid && spawnAvailable)
        {
            InvokeRepeating("Spawn", 0f, coolDown);
            spawnAvailable = false;
        }
        else if (!startRaid)
        {
            CancelInvoke();
            spawnAvailable = true;
        }
    }

    void Spawn()
    {
        GameObject copy = Instantiate(spawn, transform.position, Quaternion.identity);
        copy.SetActive(true);
    }
    IEnumerator TimeControl(float startAfterSec, float duration)
    {
        yield return new WaitForSeconds(startAfterSec);
        startRaid = true;
        StartEnemy();
        yield return new WaitForSeconds(duration);
        startRaid = false;
        StartEnemy();
    }

}
