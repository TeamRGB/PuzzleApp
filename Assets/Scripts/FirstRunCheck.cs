using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is run in the Main Menu scene and sets default values for the PlayerPrefs.
/// The script is run in the canvas element in the Main Menu.
/// </summary>
public class FirstRunCheck : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (!PlayerPrefs.HasKey ("Run")) {
			List<PieceColor> colorList = new List<PieceColor>() {
				new PieceColor(new Color32(255, 0, 0, 255), 0),
				new PieceColor(new Color32(0, 0, 255, 255), 1),
				new PieceColor(new Color32(0, 255, 255, 255), 2),
				new PieceColor(new Color32(128, 0, 128, 255), 3),
				new PieceColor(new Color32 (255, 255, 255, 255), 4),
				new PieceColor(new Color32 (255, 255, 0, 255), 5),
				new PieceColor(new Color32 (255, 250, 200, 255), 6),
				new PieceColor(new Color32 (255, 215, 180, 255), 7)
			};
			GamePreferences pref = new GamePreferences ();
			Customization custom = new Customization ();
			custom.SetPieceColors(colorList);
			custom.SetMaterial (0);
			custom.SetBGColor(new GameColor("0,130,200,255"));
			pref.SetBoardSize (4);
			pref.SetColors (4);
			pref.SetGameType (new GameType(0));
			pref.SetMaterial(0);
			PlayerPrefs.SetInt ("Run", 1);
			PlayerPrefs.Save ();
		}
		
	}
}
