using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _ammoCountText;
    [SerializeField]
    private Text _restartGameText;
    [SerializeField]
    private Text _gameEndText;
    [SerializeField]
    private Text _winnerText;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _mainMenuButton;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _thrustBar;
    [SerializeField]
    private Text _thrustIndicator;

    private GameManager _gameManager;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _ammoCountText.text = "Ammo: " + 15 + "/15";
        _thrustIndicator.text = 100 + "%";
        _gameoverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
    }

    public void UpdateAmmo(int playerAmmo)
    {
        _ammoCountText.text = "Ammo: " + playerAmmo.ToString() + "/15";
    }

    public void GameOverTextFlicker()
    {
        StartCoroutine(FlickerTextRoutine());
    }

    IEnumerator FlickerTextRoutine()
    {
        while (_liveSprites[0])
        {
            _gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ButtonActivation()
    {
        _restartButton.gameObject.SetActive(true);
        _mainMenuButton.gameObject.SetActive(true);
    }

    public void WinningText()
    {
        _winnerText.gameObject.SetActive(true);
        _gameEndText.gameObject.SetActive(true);
    }

    public void RestartGameText()
    {
        if (_liveSprites[0])
        {
            _restartGameText.gameObject.SetActive(true);
            _gameManager.GameOver();
        }
    }


   public void UpdateThruster(float thrustAmount)
    {
        _thrustBar.fillAmount = thrustAmount / 100;

        if (thrustAmount > 50f)
        {
            _thrustBar.color = Color.blue;
        }
        else if (thrustAmount <= 50f)
        {
            _thrustBar.color = Color.yellow;
        }

        if (thrustAmount <= 25f)
        {
            StartCoroutine(RedWhiteWarning());
        }

        _thrustIndicator.text = thrustAmount + "%";
    }

    IEnumerator RedWhiteWarning()
    {
        while (_thrustBar.fillAmount <= 0.25f)
        {
            yield return new WaitForSeconds(0.15f);
            _thrustBar.color = Color.red;
            yield return new WaitForSeconds(0.15f);
            _thrustBar.color = Color.white;
        }
    }
}
