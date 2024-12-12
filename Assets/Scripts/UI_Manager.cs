using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bestScoreText;


    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _liveImage;
    [SerializeField]
    private Text _gameOver;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _pausePanel;

    private Player _player;



    void Start()
    {   
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null ) 
        {
            Debug.LogError("Player is NULL");
        }


        _gameOver.gameObject.SetActive(false);
        _pausePanel.SetActive(false);
        _scoreText.text = MainMenu.currentPlayerName + " Score: " + 0;
        _bestScoreText.text = "Best: " + _player.BestScore();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        

        if (_gameManager == null ) 
        {
            Debug.LogError("Game_Manager is NULL");
        }
        
        UpdateBestScoreText(PlayerPrefs.GetInt("BestScore"));
        UpdateScoreText(0);
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = MainMenu.currentPlayerName + " : " + score;
    }

    public void UpdateBestScoreText(int bestScore)
    {
        _bestScoreText.text = "Best: " + PlayerPrefs.GetString("BestPlayerName", string.Empty) + " : " + bestScore;
    }



    public void UpdateImgLives(int currentLives)
    {
        _liveImage.sprite = _livesSprites[currentLives];
    }

    public void OnGameOver()
    {
        _gameOver.gameObject.SetActive(true);
        StartCoroutine(GameOverFlicker());
        _restartText.gameObject.SetActive(true);

        _gameManager.GameOver();
    }

    IEnumerator GameOverFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOver.gameObject.SetActive(true);
            
        }
        
    }

    public void PausePanelOn()
    {
        _pausePanel.SetActive(true);
    }
    public void PausePanelOff()
    {
        _pausePanel.SetActive(false);
    }

    

}
