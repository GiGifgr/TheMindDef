using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _maxLives = 3;

    [Header("UI Hearts")]
    [SerializeField] private Image[] _heartImages;

    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverCanvas;

    private int _currentLives;

    public bool IsAlive => _currentLives > 0;

    private void Start()
    {
        _currentLives = _maxLives;

        UpdateHearts();

        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(false);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        _currentLives -= damageAmount;

        if (_currentLives < 0)
        {
            _currentLives = 0;
        }

        UpdateHearts();

        Debug.Log("Vidas restantes: " + _currentLives);

        if (!IsAlive)
        {
            Die();
        }
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < _heartImages.Length; i++)
        {
            _heartImages[i].enabled = i < _currentLives;
        }
    }

    private void Die()
    {
        Debug.Log("GAME OVER");

        
        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(true);
        }

        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu Inicio");
    }
}