using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    private UI_Manager _uiManager;
    [SerializeField]
    AudioSource _backgroundMusic;

    private void Start()
    {
        Application.targetFrameRate = 60;

        _backgroundMusic = GameObject.Find("Background_Music").GetComponent<AudioSource>();
        if (_backgroundMusic == null)
        {
            Debug.LogError("Backgrounf Music is null");
        }

        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
        if ( _uiManager == null)
        {
            Debug.LogError(" UI_Manager id NULL ");
        }

    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();

        }
    }


    public void GameOver()
    {
        _isGameOver = true;
    }

    
    public void GamePause()
    {

        _uiManager.PausePanelOn();
        _backgroundMusic.Pause();
        Time.timeScale = 0;     
                   
    }

    public void GameResume()
    {
        Time.timeScale = 1.0f;
        _backgroundMusic.Play();
        _uiManager.PausePanelOff();
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }




    


}
