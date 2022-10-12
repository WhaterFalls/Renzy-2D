using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SessionManager : MonoBehaviour
{

    // Create one instance and don't destroy between scenes
    public static SessionManager Instance;

    #region variables
    string playerName;
    TMP_InputField nameField;
    TMP_Text highScoreText;
    
    #endregion

    #region accessors
    public string PlayerName { get; }
    public string HighScorePlayer { get { return PlayerPrefs.GetString("High Score Player"); } }

    public int HighScore { get { return PlayerPrefs.GetInt("High Score"); } }
    #endregion

    private void Awake()
    {
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TMP_Text>();
        highScoreText.text = $"High Score: {HighScore}\nBy: {HighScorePlayer}";

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        nameField = GameObject.Find("NameField").GetComponent<TMP_InputField>();
        nameField.onEndEdit.AddListener(SetPlayerName);
    }

    public void StartGame()
    {
        if (nameField.text != "")
        {
            SceneManager.LoadScene("Game");
        }
        else
        {
            Debug.Log("Please enter an alias before starting.");
        }
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    public void SetHighScore(int score)
    {
        if (PlayerPrefs.HasKey("High Score"))
        {
            if (score > PlayerPrefs.GetInt("High Score"))
            {
                PlayerPrefs.SetInt("High Score", score);
                PlayerPrefs.SetString("High Score Player", playerName);
            }
        }
        else
        {
            PlayerPrefs.SetInt("High Score", score);
            PlayerPrefs.SetString("High Score Player", playerName);
        }
    }

    public void ClearHighScore()
    {
        PlayerPrefs.SetInt("High Score", 0);
        PlayerPrefs.SetString("High Score Player", "");
        highScoreText.text = "High Score: 0\nBy: ";
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
