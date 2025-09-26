using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject gameWinScreen;
    public GameObject gameOverScreen;

    bool gameIsOver;

    void Start()
    {
        Enemy.OnPlayerSpotted += ShowGameOverUI;
    }

    void Update()
    {
        if (gameIsOver && Input.GetKeyDown(KeyCode.Space))
        {
            // reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void ShowGameWinUI()
    {
        OnGameOver(gameWinScreen);
    }

    void ShowGameOverUI()
    {
        OnGameOver(gameOverScreen);
    }

    void OnGameOver(GameObject screen)
    {
        screen.SetActive(true);
        gameIsOver = true;
        Enemy.OnPlayerSpotted += ShowGameOverUI;
    }

    void OnDestroy()
    {
        Enemy.OnPlayerSpotted += ShowGameOverUI;
    }
}
