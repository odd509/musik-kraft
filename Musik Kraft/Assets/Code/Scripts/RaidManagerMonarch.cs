using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidManagerMonarch : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject frog;
    public GameObject eagle;
    public GameObject dragon;

    [Header("Spawners")]
    public RaidManager SpawnerTopLeft;
    public RaidManager SpawnerTopRight;
    public RaidManager SpawnerBottomLeft;
    public RaidManager SpawnerBottomRight;

    [Header("Raid Types")]

    [Header("Raid 1")]
    public bool isActiveRaid1 = true;
    public float startTimeRaid1 = 0f;
    public float durationRaid1 = 10f;

    [Header("Raid 2")]
    public bool isActiveRaid2 = true;
    public float startTimeRaid2 = 0f;
    public float durationRaid2 = 10f;

    [Header("Raid 3")]
    public bool isActiveRaid3 = true;
    public float startTimeRaid3 = 0f;
    public float durationRaid3 = 10f;

    private void Awake()
    {
        if (isActiveRaid1) Raid1();
    }

    void Raid1() {
        SpawnerBottomLeft.spawn = frog;
        SpawnerBottomLeft.startRaid = true;

    }
    void Raid2() { }
    void Raid3() { }

}
