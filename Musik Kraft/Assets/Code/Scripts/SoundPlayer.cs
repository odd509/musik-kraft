using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource src;

    [Header("Drum")] 
    
    public AudioClip drum1;
    public AudioClip drum2;
    public AudioClip drum3;
    
    
    public void Drum()
    {
        ArrayList list = new ArrayList();
        list.Add(drum1);
        list.Add(drum2);
        list.Add(drum3);
        src.clip = (AudioClip) list[Random.Range(0, list.Count)];
        src.pitch = Random.Range(0.8f, 1.1f);
        src.Play();
    }
    
}
