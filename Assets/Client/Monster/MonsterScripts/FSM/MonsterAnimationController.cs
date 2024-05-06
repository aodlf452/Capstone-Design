using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimationController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private AnimationClip[] attackClips;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        foreach (var clip in anim.runtimeAnimatorController.animationClips)
        {
            // walk �ִϸ��̼ǿ� ���ؼ��� �ڵ鷯�� �۵��ϴ� ��
            AnimationEvent endEvent = new AnimationEvent();
            endEvent.time = clip.length;
            endEvent.functionName = "AnimationEndHandler";
            endEvent.stringParameter = clip.name;

            clip.AddEvent(endEvent);
            Debug.Log($"event�� time:{endEvent.time}, event�� pram:{endEvent.stringParameter}");
        }
    }
    public void AnimationEndHandler(string name)
    {
        Debug.Log($"�̺�Ʈ ��� Ȯ�� - ���� Ŭ��: {name}");
    }
    public void testHandler(string name)
    {

        // �ν����� �󿡼� 1 �����̿� �θ� ȣ�� �ȵǰ�, 0.8 ���Ϸ� �θ� ȣ���.. ������ ã�ƾ� �� ��
        Debug.Log("���� �׽�Ʈ");
    } 
}
