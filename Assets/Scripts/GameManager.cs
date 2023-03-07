using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private bool gameOver = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            Debug.Log("Game over!");
            // TODO GameOverUI
        }
    }
}
