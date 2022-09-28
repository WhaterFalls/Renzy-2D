using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameControl : MonoBehaviour {

	public static GameControl instance;
	public GameObject gameOverText;
	public Text scoreText;
	TMP_Text highScoreText;
	public bool gameOver = false;
	private int score = 0;

	// Use this for initialization
	void Awake () {
		if (instance == null) {
			instance = this;
			highScoreText = GameObject.Find("HighScoreText").GetComponent<TMP_Text>();
			highScoreText.text = $"High Score: {SessionManager.Instance.HighScore}";
		} else if (instance != this) {
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		CheckForHighScore();
		if (gameOver) {
			StartCoroutine ("Reset");
		}
	}

	IEnumerator Reset() {
		yield return new WaitForSeconds(2);
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.RightArrow)) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		} else if (Input.touchCount > 0) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	public void PlayerScored() {
		if (gameOver){
			return;
		}
		score++;
		scoreText.text = "Score: " + score.ToString() ;
	}

	public void PlayerDied() {
		gameOverText.SetActive (true);
		gameOver = true;
	}

	void CheckForHighScore() {
		if (score > SessionManager.Instance.HighScore)
        {
			SessionManager.Instance.SetHighScore(score);
			highScoreText.text = $"High Score: {SessionManager.Instance.HighScore}";
		}
	}
}
