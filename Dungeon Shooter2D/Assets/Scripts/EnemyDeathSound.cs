using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathSound : MonoBehaviour
{
    public AudioClip[] clips;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        int randomNumber = Random.Range(0, clips.Length);
        source.clip = clips[randomNumber];
        source.Play();
    }
}
