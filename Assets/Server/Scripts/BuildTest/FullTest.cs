using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullTest : MonoBehaviour
{
    // Start is called before the first frame update
    int phase = 0;
    bool isGame = true;
    void Start()
    {
        

        Screen.SetResolution(1920, 1080, true);
        Screen.fullScreen = true;
        StartCoroutine("Defense");
        Cursor.lockState = CursorLockMode.Locked;
    }
    IEnumerator Defense()
    {
        while (true)
        {
            Debug.Log(phase + "�ڷ�ƾ" + isGame);
            if (phase ==3)
            {
                Debug.Log(phase + "��");
                yield break;
            }
            else if (!isGame)
            {
                Debug.Log(phase + "��");

                yield return new WaitForSeconds(5);

                phase++;
            }
            else if (isGame)
            {
                Debug.Log(phase + "�޽�");
                yield return new WaitForSeconds(2);
            }
            isGame = !isGame;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("����");
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("�� ����");
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

    }
}
