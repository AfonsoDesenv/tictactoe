using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPVP : MonoBehaviour {

	public Text EndGamePanelTitle;
	public SpriteRenderer Icon;
	public Sprite[] IconSprites;
	public static GameObject PausePanel;
	public static GameObject EndGamePanel;
	public static bool GameIsPaused = false;
	public static bool Is_GameOver;
	[SerializeField] private BoardPVP board;

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
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
		Time.timeScale = 1f;
		GameIsPaused = false;
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
		if (board.player_one_wins) {
			Icon.sprite = IconSprites[0];
			Icon.color = Color.green;
			EndGamePanelTitle.text = "Player 1 Win"; 
		}
		else if (board.player_two_wins) {
			Icon.sprite = IconSprites[0];
			Icon.color = Color.green;
			EndGamePanelTitle.text = "Player 2 Win"; 
		}
		else {
			Icon.sprite = IconSprites[1];
			Icon.color = Color.blue;
			EndGamePanelTitle.text = "Draw"; 
		}
	}
}
