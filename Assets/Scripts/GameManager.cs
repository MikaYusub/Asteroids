using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private bool gameOver = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static GameManager GetInstance()
    {
        return instance;
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
