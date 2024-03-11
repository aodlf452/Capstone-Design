using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHP;
    public int curHP;
    public int atkPower; // ���ݷ�

    Rigidbody rigid;
    BoxCollider boxCollider;
    Material mat;
    Color originalColor;
    Animator anim;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();

        mat = GetComponentInChildren<SkinnedMeshRenderer>().material;
        originalColor = mat.color;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Melee") // ����
        {
            // 1. �浹�� other�� ��ũ��Ʈ�� �����´�(ex. ����)

            Weapon weapon = other.GetComponent<Weapon>();
            // 2. �ش� ��ũ��Ʈ�� ���� ���ݷ��� �̿��ؼ� ü�� �谨
            curHP -= weapon.damage;
            // 3, ���� ��ġ - �ǰ� ��ġ = ���ۿ� ����
            Vector3 reactVec = transform.position - other.transform.position;
            // 4. �ڷ�ƾ ����
            StartCoroutine(OnDamage(reactVec));
            
        }
    }
    IEnumerator OnDamage(Vector3 reactVec)
    {
        mat.color = Color.red; // ����) ������
        yield return new WaitForSeconds(0.1f); // 0.1s ���

        if (curHP > 0)
        {
            // ���� ���͸��� �������� ������
            mat.color = originalColor;
        }
        else // ���� ��� ��
        {
            mat.color = Color.gray;
            gameObject.layer = 9;

            reactVec = reactVec.normalized;
            reactVec += Vector3.up;
            rigid.AddForce(reactVec*5, ForceMode.Impulse);
            Destroy(gameObject, 5); // 5���� ����
        }
    }
}
