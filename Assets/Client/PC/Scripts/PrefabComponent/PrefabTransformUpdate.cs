using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTransformUpdate : MonoBehaviour
{
    PrefabCreator creator;               //������ ���� �� �߰��� ������Ʈ
    
    void Start()
    {
        creator=GetComponent<PrefabCreator>();
    }

    
    void Update()
    {
        //������ ������ ������Ʈ�� rotation�� ������ rotation ����ȭ
        transform.rotation=creator.creatorParentTransform.transform.rotation;
    }
}
