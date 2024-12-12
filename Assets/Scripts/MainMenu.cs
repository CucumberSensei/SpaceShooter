using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI textMesh;
    public static string currentPlayerName;
    
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        currentPlayerName = textMesh.text;
        Debug.Log(currentPlayerName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("BestScore", 0);
        PlayerPrefs.SetString("BestPlayerName", string.Empty);
        PlayerPrefs.Save();
    }
    

    
}
