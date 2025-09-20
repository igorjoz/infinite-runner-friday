using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI highscoreValue;
    public TextMeshProUGUI coinsValue;
    public TextMeshProUGUI soundButtonText;

    private int highscore = 0;
    private int coins = 0;

    public GameObject mainMenuPanel;
    public GameObject upgradesStorePanel;

    public Text magnetLevelText;
    public Text magnetButtonText;

    public Text immortalityLevelText;
    public Text immortalityButtonText;

    public Powerup magnet;
    public Powerup immortality;


    void Start()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
        }

        if (PlayerPrefs.HasKey("Coins"))
        {
            coins = PlayerPrefs.GetInt("Coins");
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        highscoreValue.text = highscore.ToString();
        coinsValue.text = coins.ToString();

        if (SoundManager.instance.GetMuted())
        {
            soundButtonText.text = "Music ON";
        }
        else
        {
            soundButtonText.text = "Music OFF";
        }

        immortalityLevelText.text = immortality.ToString();
        immortalityButtonText.text = immortality.UpgradeCostString();

        magnetLevelText.text = magnet.ToString();
        magnetButtonText.text = magnet.UpgradeCostString();
    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("Game");
    }

    public void SoundButtonClicked()
    {
        SoundManager.instance.ToggleMuted();
        UpdateUI();
    }

    public void OpenUpgradesStore()
    {
        mainMenuPanel.SetActive(false);
        upgradesStorePanel.SetActive(true);
    }

    public void CloseUpgradesStore()
    {
        mainMenuPanel.SetActive(true);
        upgradesStorePanel.SetActive(false);
    }

    public void UpgradeImmortalityButton()
    {
        UpgradePowerup(immortality);
    }

    public void UpgradeMagnetButton()
    {
        UpgradePowerup(magnet);
    }
    private void UpgradePowerup(Powerup powerup)
    {
        if (coins >= powerup.GetNextUpgradeCost() && !powerup.IsMaxedOut())
        {
            ReduceCoinsAmount(powerup.GetNextUpgradeCost());
            powerup.Upgrade();
            UpdateUI();
        }
    }
    private void ReduceCoinsAmount(int amount)
    {
        coins -= amount;
        PlayerPrefs.SetInt("Coins", coins);
    }


    void Update()
    {

    }
}