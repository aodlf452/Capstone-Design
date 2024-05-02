using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStatus;

public class CombatStatusManager : MonoBehaviour
{
    [HideInInspector] public Player player;
    [HideInInspector] public PlayerStatus player_status;
    [HideInInspector] public Animator anim;


    // Start is called before the first frame update
    private void Awake()
    {
        player=GetComponent<Player>();
        anim=GetComponent<Animator>();
        player_status=GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {
        int result_damage = (int)(damage * (1 - (player_status.basicStats.def / (player_status.basicStats.def + player_status.combatStats.constant_def))));//������ = ������*���������(= ����/����+�����)
        player_status.basicStats.hp -= result_damage;
        //Debug.Log(result_damage);
        if (result_damage > 0 && player.dDown) //�÷��̾ ���� ���з� ���� ���̶��~
        {
            player.DefensingHit();
        }
    }

    public void HitResponse(float cctime=1.0f)       //�����ݿ� ���� �ǰ� ���� �ִϸ��̼� ���(cc�� �ð�)
    {
        anim.SetTrigger("getHit");
        player.isCC = true;
        Invoke("HitResponseEnd", cctime);
    }
    public void HitResponseEnd()
    {
        player.isCC = false;
    }
    // �˹�
    public void TakeKnockback(Vector3 force)
    {
        player.rigid.AddForce(force , ForceMode.Impulse);
    }
}
