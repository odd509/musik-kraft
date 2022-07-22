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

    [Header("Accordion")] public AudioClip accordion1;
    
    [Header("Sax")] 
    
    public AudioClip sax1;
    public AudioClip sax2;
    
    [Header("Guitar")] public AudioClip guitar1;
    
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

    public void Accordion()
    {
        src.clip = accordion1;
        src.pitch = Random.Range(0.7f, 1.2f);
        src.Play();
    }

    public void Sax()
    {
        ArrayList list = new ArrayList();
        list.Add(sax1);
        list.Add(sax2);
        src.clip = (AudioClip) list[Random.Range(0, list.Count)];
        src.pitch = Random.Range(0.9f, 1.1f);
        src.Play();
    }

    public void Guitar()
    {
        src.clip = guitar1;
        src.pitch = 1f;
        
        src.Play();
    }
    
}
