using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("�浹");
        if (coll.gameObject.tag == "Portal" && Countdown.mode == 1)
        {
            Countdown.mode = 2;
            Debug.Log("ī��Ʈ �ٿ� ����");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
