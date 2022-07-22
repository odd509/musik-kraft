using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidManagerMonarch : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject frogTemp;
    public GameObject eagleTemp;
    // Gerçek Ejderhayý koyun
    public GameObject dragon;

    [Header("Spawners")]
    public Spawner SpawnerTopLeft;
    public Spawner SpawnerTopRight;
    public Spawner SpawnerBottomLeft;
    public Spawner SpawnerBottomRight;

    [Header("Raid Types")]

    [Header("Raid 1")]
    public bool isActiveRaid1 = true;
    public float startTimeRaid1 = 0f;
    public float durationRaid1 = 10f;

    [Header("Raid 2")]
    public bool isActiveRaid2 = true;
    public float startTimeRaid2 = 10f;
    public float durationRaid2 = 10f;

    [Header("Raid 3")]
    public bool isActiveRaid3 = true;
    public float startTimeRaid3 = 20f;
    public float durationRaid3 = 10f;

    private void Awake()
    {
        if (isActiveRaid1) Raid1(startTimeRaid1, durationRaid1);
        if (isActiveRaid2) Raid2(startTimeRaid2, durationRaid2);
        if (isActiveRaid3) Raid3(startTimeRaid3, durationRaid3);
    }

    void Raid1(float startTime, float duration) {
        SpawnerBottomLeft.spawn = frogTemp;
        SpawnerBottomLeft.StartRaid(startTime, duration);

        SpawnerBottomRight.spawn = frogTemp;
        SpawnerBottomRight.StartRaid(startTime, duration);
    }
    void Raid2(float startTime, float duration) {
        Raid1(startTime, duration);

        SpawnerTopLeft.spawn = eagleTemp;
        SpawnerTopLeft.StartRaid(startTime, duration);

        SpawnerTopRight.spawn = eagleTemp;
        SpawnerTopRight.StartRaid(startTime, duration);
    }
    void Raid3(float startTime, float duration) {
        StartCoroutine(Dragon(startTime));
        
    }
    IEnumerator Dragon(float startTime)
    {
        yield return new WaitForSeconds(startTime);
        dragon.SetActive(true);
    }
}
