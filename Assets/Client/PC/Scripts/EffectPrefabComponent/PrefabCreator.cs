using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabCreator : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform creatorParentTransform;                    //������ ������ ������Ʈ�� Transform
    public PrefabTransformUpdate update;                        //�����տ� �ִ� PrefabTransformUpdate ������Ʈ(��Ȱ��ȭ ����)
    public bool isStrong=false;
    public int attackNum=0;
    public Weapon weapon;


    private void Awake()
    {
        update= GetComponent<PrefabTransformUpdate>();
        if (update == null) { return; }
        update.enabled = true;
    }
}
