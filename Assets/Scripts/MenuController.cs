using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
   
    [Header("Scene Names")]
    [SerializeField] private string _gameSceneName = "Inicio";
    [SerializeField] private string _creditsSceneName = "Creditos";
    [SerializeField] private string _menuSceneName = "Menu Inicio";

   
    public void PlayGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(_creditsSceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(_menuSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego");

        Application.Quit();
    }
}