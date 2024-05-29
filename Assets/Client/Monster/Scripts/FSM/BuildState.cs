using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : IMonsterState
{
    Monster monster;
    float currentTime = 0;
    float attackInterval = 3f;
    public BuildState(Monster monster)
    {
        this.monster = monster;
    }

    public void EnterState()
    {
        Debug.Log("Attack: Enter");
        monster.transform.LookAt(monster.TargetPlayer);
        monster.Anim.SetTrigger("doAttack");
        monster.Anim.SetInteger("randomValue", Random.Range(0, 3));
        //monster.MSound.PlaySound(0);
        //monster.Anim.SetInteger("randomValue", 2);
    }

    public void ExitState()
    {

    }

    public void ExecuteState()
    {
        //AnimatorStateInfo animatorStateInfo = monster.Anim.GetCurrentAnimatorStateInfo(0);
        //if (animatorStateInfo.IsName("attack1"))
        //{
        //    Debug.Log($"normalizedTime: {animatorStateInfo.normalizedTime}");
        //}
        currentTime += Time.deltaTime;
        if (currentTime >= attackInterval)
        {
            monster.transform.LookAt(monster.TargetPlayer);
            monster.TransformTrigger();
            monster.Anim.SetTrigger("doAttack");
            monster.Anim.SetInteger("randomValue", Random.Range(0, 3));
            currentTime = 0f;
            //monster.MSound.PlaySound(0);
            //monster.Anim.SetInteger("randomValue", 0);
            //monster.Anim.SetInteger("randomValue", 2);
        }

        // ������� �ƴҶ��� �ִϸ��̼� ���
        //if (!IsAnimationRunning(monster.Anim, "attack1"))
        //{
        //    Debug.Log("�ִϸ��̼� ����" + count);
        //    monster.Anim.SetTrigger("doAttack");
        //    monster.Anim.SetInteger("randomValue", 0);
        //    count++;
        //}
        //Debug.Log("=========================================================");
    }
    bool IsAnimationRunning(Animator animator, string animationStateName)
    {
        if (animator == null) return false;

        // �ִϸ����� ù��° ���̾� ��ȯ 
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorClipInfo[] animatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (animatorStateInfo.IsName(animationStateName)) // �Ű������� �޾ƿ� �̸��� �����̸�
        {
            Debug.Log($"normalizedTime :{animatorStateInfo.normalizedTime}");
            if (animatorStateInfo.normalizedTime <= 1f)
            {

                //Debug.Log($"normalizedTime :{animatorStateInfo.normalizedTime}");
                //Debug.Log("�������Դϴ�.");
                return true;
            }
            else
            {
                Debug.Log("������ �������ϴ�.");
                return false;
            }
        }
        else // �Ű������� �޾ƿ� �̸��� ���°� �ƴϸ�
        {
            Debug.Log($"�ִϸ��̼� ����: {animator.GetCurrentAnimatorClipInfo(0)[0].clip.name}");
            Debug.Log($"�ִϸ��̼� ����: {animator.GetCurrentAnimatorClipInfo(0)[0].clip.length}");
            return false; // �ƴϸ� false
        }
        //if(animatorStateInfo.IsName(animationStateName) && (animatorStateInfo.normalizedTime % 1.0f) < 1f)
        //{
        //    return true;
        //}
        //else
        //{
        //    return false;
        //}
    }
}
