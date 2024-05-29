using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSound : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips; // ����� Ŭ�� �迭
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
   
    public void PlaySound(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }
    public AudioClip GetClip(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            return audioClips[index];
        }
        return null;
    }
    
}
