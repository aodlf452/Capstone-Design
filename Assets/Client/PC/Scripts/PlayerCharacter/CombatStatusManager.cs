using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerStatus;
using Photon.Pun;

public class CombatStatusManager : MonoBehaviourPun
{
    [HideInInspector] public Player player;
    [HideInInspector] public PlayerStatus player_status;
    [HideInInspector] public Animator anim;

    public float knockbackPower = 8f; // �˹� �Ŀ�
    public float knockbackDuration = 0.2f; // �˹� ���� �ð�
    PhotonView pv;


    // Start is called before the first frame update
    private void Awake()
    {
        player=GetComponent<Player>();
        anim=GetComponent<Animator>();
        player_status=GetComponent<PlayerStatus>();
        pv= GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        if (player.isJump) return;              //������ ���̶�� ����
        Debug.Log("������ ����");
        int result_damage = (int)(damage * (1 - (player_status.basicStats.def / (player_status.basicStats.def + player_status.combatStats.constant_def))));//������ = ������*���������(= ����/����+�����)
        //player_status.basicStats.hp -= result_damage; // ĸ��ȭ �̿��� ������ �� ����
        player_status.DecreaseHP(result_damage);
        //Debug.Log(result_damage);
        if (result_damage > 0 && player.isDefense) //�÷��̾ ���� ���з� ���� ���̶�� ����� ��Ʈ��
        {
            player.DefensingHit();
        }
        //pv.RPC("RPCDamage", RpcTarget.All, damage);
        //���� �������� �ʾ��� �� �Ʒ� �Լ� ���
    }

    [PunRPC]
    public void RPCDamage(int damage)
    {
        if (player.isJump) return;              //������ ���̶�� ����

        int result_damage = (int)(damage * (1 - (player_status.basicStats.def / (player_status.basicStats.def + player_status.combatStats.constant_def))));//������ = ������*���������(= ����/����+�����)
        //player_status.basicStats.hp -= result_damage; // ĸ��ȭ �̿��� ������ �� ����
        player_status.DecreaseHP(result_damage); 
        //Debug.Log(result_damage);
        if (result_damage > 0 && player.isDefense) //�÷��̾ ���� ���з� ���� ���̶�� ����� ��Ʈ��
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

    /// <summary>
    /// ĳ���Ͱ� �˹�Ǵ� �Լ�
    /// </summary>
    /// <param name="direction">�˹� ���⺤��</param>
    public void TakeKnockback(Vector3 direction)
    {
        // �޾ƿ� ���⺤���� y������ �����ϰų�, ����ȭ�� �ȵǾ��ִ� ��츦 ����
        if(direction.y != 0) direction.y = 0; 
        if(direction.magnitude > 1f) direction = direction.normalized; // ����ȭ

        StartCoroutine(OnKnockback(direction));
    }
    private IEnumerator OnKnockback(Vector3 direction)
    {
        float elapsed = 0;
        while (elapsed < knockbackDuration)
        {
            elapsed += Time.deltaTime;
            transform.Translate(direction * knockbackPower * Time.deltaTime);
            yield return null;
        }
    }
}
