using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public enum WeaponType { Melee,Range};
    public WeaponType type;
    public int damage; //atk
    public float rate;   //���ݼӵ�
    public BoxCollider meleeArea;   //���ݹ���
    public TrailRenderer trailEffect; //��������Ʈ
   public void Use()
    {
        if(type == WeaponType.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.6f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.2f);
        trailEffect.enabled = false;
    }
}
