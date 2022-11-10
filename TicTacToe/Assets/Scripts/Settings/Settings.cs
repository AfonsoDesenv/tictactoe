using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
	public float tree_deph;
	public string player_choice;

    public Settings (SettingsUI settings) {
        this.tree_deph = settings.tree_deph;
        this.player_choice = settings.player_choice;
    }
}
