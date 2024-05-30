using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip lobbyAndMainMenuClip; // Lobbyscene�� Mainmenuscene���� ����� Ŭ��
    public AudioClip mainSceneClip; // Mainscene���� ����� Ŭ��

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // ���� �ٲ� �ı����� �ʵ��� ����
            audioSource = GetComponent<AudioSource>();
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "LobbyScene":
            case "MainMenuScene":
                if (audioSource.clip != lobbyAndMainMenuClip)
                {
                    audioSource.clip = lobbyAndMainMenuClip;
                    audioSource.Play();
                }
                break;
            case "MainScene":
                Destroy(gameObject);
                break;
        }
    }
}
