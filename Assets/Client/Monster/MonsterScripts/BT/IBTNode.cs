using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// BT �ൿƮ�� �������̽�
public interface IBTNode{
    
    public enum NodeState
    {
        Running = 0,
        Success = 1,
        Failure = 2
    }
    // ��� ���� �� �� ���� ��ȯ
    public NodeState Evaluate();
}
