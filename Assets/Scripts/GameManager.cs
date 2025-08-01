using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float worldScrollingSpeed = 0.2f;
    public TMP_Text scoreText;
    private float score;

    public bool isInGame;
    public GameObject resetButton;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        InitializeGame();
    }

    void InitializeGame()
    {
        isInGame = true;
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.isInGame)
        {
            return;
        }

        score += worldScrollingSpeed;
        UpdateOnScreenScore();
    }

    public void HandleGameOver()
    {
        isInGame = false;
        resetButton.SetActive(true);
    }

    void UpdateOnScreenScore()
    {
        scoreText.text = score.ToString("0");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
