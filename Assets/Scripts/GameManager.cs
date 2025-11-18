using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start the game at half speed. (speed reaches 1 as score increments)
    public const float TimeScaleAtStart = 0.5f;

    [SerializeField] private GameObject _gameOverCanvas;
    [SerializeField] private UnityEngine.UI.Button _playButton;

    public static GameManager instance;

    public event Action OnPlayerJoin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        Time.timeScale = TimeScaleAtStart;
    }

    /// <summary> invoked by PlayerInputManager </summary>
    public void OnPlayerJoined()
    {
        if (OnPlayerJoin != null)
        {
            OnPlayerJoin();
        }
    }

    public void GameOver()
    {
        // TODO: CrazyGames branch
        _gameOverCanvas.SetActive(true);
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(_playButton.gameObject);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
