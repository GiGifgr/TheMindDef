using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMusicManager : MonoBehaviour
{
    private static LevelMusicManager instance;
    private static string currentSceneName;

    [SerializeField] private string menuSceneName = "Menu Inicio";

    private void Awake()
    {
        string newSceneName = SceneManager.GetActiveScene().name;

        if (newSceneName == menuSceneName)
        {
            Destroy(gameObject);
            return;
        }

        if (instance != null)
        {
            if (currentSceneName == newSceneName)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Destroy(instance.gameObject);
            }
        }

        instance = this;
        currentSceneName = newSceneName;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += CheckScene;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckScene;
    }

    private void CheckScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == menuSceneName)
        {
            Destroy(gameObject);
        }
    }
}