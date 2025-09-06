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
    public TMP_Text coinsText;
    private int coins;

    public Immortality immortality;
    public Magnet magnet;

    public void CoinCollected(int value = 1)
    {
        coins += value;
        PlayerPrefs.SetInt("Coins", coins);
    }

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
        immortality.isActive = false;
        magnet.isActive = false;

        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }
        else
        {
            coins = 0;
            PlayerPrefs.SetInt("Coins", 0);
        }
    }

    public void ImmortalityCollected()
    {
        if (immortality.isActive)
        {
            CancelInvoke("CancelImmortality");
            CancelImmortality();
        }

        immortality.isActive = true;
        worldScrollingSpeed += immortality.GetSpeedBoost();
        Invoke("CancelImmortality", immortality.GetDuration());
    }

    void CancelImmortality()
    {
        worldScrollingSpeed -= immortality.GetSpeedBoost();
        immortality.isActive = false;
    }

    public void MagnetCollected()
    {
        if (magnet.isActive)
        {
            CancelInvoke("CancelMagnet");
        }

        magnet.isActive = true;
        Invoke("CancelMagnet", magnet.GetDuration());
    }

    public void CancelMagent()
    {
        magnet.isActive = false;
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
        coinsText.text = coins.ToString("0");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
