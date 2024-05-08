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
    
}
