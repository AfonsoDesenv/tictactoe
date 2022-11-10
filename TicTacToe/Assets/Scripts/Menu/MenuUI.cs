using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
	public void goToVsComputer () {
		SceneManager.LoadScene("PlayerVsComputer");
	}

	public void goToPvP () {
		SceneManager.LoadScene("PlayerVsPlayer");
	}
    
	public void goToSettings () {
		SceneManager.LoadScene("Settings");
	}
}
