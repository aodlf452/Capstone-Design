using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Soul : MonoBehaviourPun
{
    private Monster.MonsterType monsterType;

    [SerializeField]private float distance = 0.5f;
    [SerializeField]private float frequency = 5.0f;
    private float originY;
    [SerializeField] private AudioClip audioClip; // �ҿ� ����
    // Start is called before the first frame update
    void Start()
    {
        //originY = transform.position.y;      
        originY = GetGroundYPosition(transform.position);
        transform.position = new Vector3(transform.position.x, originY, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float y = distance * Mathf.Sin(2 * Mathf.PI * frequency * Time.time) ;
        transform.position = new Vector3(transform.position.x, originY + y, transform.position.z);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GrowthSystem growthSystem = other.gameObject.GetComponent<GrowthSystem>();
            int soulAmount = GetSoulAmount(monsterType); // ���� Ÿ�Կ����� �ҿ� ���� ��ȯ
            growthSystem.GetSoul(soulAmount); // �÷��̾� ȹ�� ó��

            PlaySound();
            GameManager.Instance.ObjDelete(gameObject);
            //PhotonNetwork.Destroy(gameObject);
        }
    }
    /// <summary>
    /// ���� ������ ���� ��ȯ�� �ҿ� ���� ����
    /// </summary>
    /// <param name="type">���� Ÿ��</param>
    /// <returns>�ҿ� ����</returns>
    private int GetSoulAmount(Monster.MonsterType type)
    {
        int amount = 0;
        switch(type)
        {
            case Monster.MonsterType.Cyclops:
                amount = 25;
                break;
            case Monster.MonsterType.Goblin:
                amount = 5;
                break;
            case Monster.MonsterType.Hobgoblin:
                amount = 10;
                break;
            case Monster.MonsterType.Kobold: 
                amount = 5;
                break;  
            case Monster.MonsterType.Troll: 
                amount = 25;
                break;
        }
        return amount;
    }
    /// <summary>
    /// Setter: monsterType 
    /// </summary>
    /// <param name="monsterType"></param>
    public void SetMonsterType(Monster.MonsterType monsterType)
    {
        this.monsterType = monsterType;
    }

    private float GetGroundYPosition(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity))
        {
            return hit.point.y;
        }
        else
        {
            return position.y;
        }
    }
    private void PlaySound()
    {
        // �ӽ� ������Ʈ 
        GameObject tempObj = new GameObject("tempAudio");
        tempObj.transform.position = transform.position;

        // �ӽ� ������Ʈ�� AudioSource �߰�
        AudioSource audioSource = tempObj.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();

        // �ӽ� ������Ʈ �ı� ����
        Destroy(tempObj, audioSource.clip.length);

    }
}
