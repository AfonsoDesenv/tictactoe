using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour {

	public float tree_deph = 1f;
	public string player_choice = "X";
	public SpriteRenderer iconX;
	public SpriteRenderer iconO;
	public Button button_X;
	public Button button_O;
	public Slider slider;
	public Text tree_deph_text;

	void Awake() {
		LoadSettings();
	}

	void Update () 
	{
		tree_deph_text.text = "Tree Deph: " + tree_deph;
	}

	public void SaveSettings() 
	{
		SaveSystem.SaveSettings(this);
		goToMenu();
	}

	public void LoadSettings() 
	{
		Settings settings =	SaveSystem.LoadSettings();
		if (settings != null)
		{
			this.player_choice = settings.player_choice;
			if (this.player_choice == "X")
			{
				iconX.gameObject.SetActive(true);
				iconO.gameObject.SetActive(false);
				button_X.gameObject.SetActive(false);
				button_O.gameObject.SetActive(true);
			} else 
			{
				iconX.gameObject.SetActive(false);
				iconO.gameObject.SetActive(true);
				button_X.gameObject.SetActive(true);
				button_O.gameObject.SetActive(false);
			}
			this.tree_deph = settings.tree_deph;
			slider.value = settings.tree_deph;
		}
	}

	public void Change_TreeLevel (float newDeph) 
	{
			tree_deph = newDeph;
			tree_deph_text.text = "Tree Deph: " + tree_deph;
	}

	public void ChoosePiece (string piece){
			this.player_choice = piece;
	}

	public void goToMenu () {
		SceneManager.LoadScene("Menu");
	}
}
