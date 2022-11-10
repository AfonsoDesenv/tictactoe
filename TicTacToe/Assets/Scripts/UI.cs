using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text EndGamePanelTitle;
	public SpriteRenderer Icon;
	public Sprite[] IconSprites;
	public static GameObject PausePanel;
	public static GameObject EndGamePanel;
	public static bool GameIsPaused = false;
	public static bool Is_GameOver;
	[SerializeField] private BoardMinimax board;

	void Awake () {
		PausePanel = GameObject.Find("PausePanel");
		EndGamePanel = GameObject.Find("EndGamePanel");
		PausePanel.SetActive(false);
		EndGamePanel.SetActive(false);
	}
	// Use this for initialization
	void Start () {
		GameIsPaused = false;
	}

	// Update is called once per frame
	void Update () {
		if (Is_GameOver) {
			Is_GameOver = false;
			EndGame();
		}
	}

	public void pause () {
		PausePanel.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}

	public void resume () {
		PausePanel.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}

	public void restart() {
		Time.timeScale = 1f;
		GameIsPaused = false;
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void goToMenu () {
		Time.timeScale = 1f;
		GameIsPaused = false;
		SceneManager.LoadScene("Menu");
	}

	public void EndGame() {
		EndGamePanel.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
		if (board.player_wins) {
			Icon.sprite = IconSprites[0];
			Icon.color = Color.green;
			EndGamePanelTitle.text = "You Win"; 
		}
		else if (board.enemy_wins) {
			Icon.sprite = IconSprites[1];
			Icon.color = Color.red;
			EndGamePanelTitle.text = "You Lose"; 
		}
		else {
			Icon.sprite = IconSprites[2];
			Icon.color = Color.blue;
			EndGamePanelTitle.text = "Draw"; 
		}
	}
}
