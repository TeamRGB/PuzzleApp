using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class creates the board used in the background. 
/// The colors of the pieces are in a list of color objects. 
/// This is a temporary background for the app.
/// </summary>
public class CreateBackground : MonoBehaviour {
    List<Color> colorList = new List<Color>() {
        new Color32(128, 0, 0, 200),
        new Color32(0, 128, 0, 200),
        new Color32(0, 0, 128, 200)
    };
    /// <summary>
    /// Starts this instance.
    /// </summary>
    void Start () {
        //The background board
		var board = GameObject.Find ("GameBoard");
        //Set the size of the background board.
		board.GetComponent<GridLayoutGroup> ().constraintCount = 8; 
		board.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (75f, 75f);
        //Get the first piece to clone
		GameObject boardPeice = GameObject.Find ("BoardPeice");
        //Create 102 other pieces and randomly assign the color
		for (int i = 0; i < 102; i++) {
			GameObject a = (GameObject)Instantiate (boardPeice);
			a.transform.SetParent (board.transform, false);
			a.GetComponent<Image> ().color = colorList[Random.Range(0, colorList.Count)];
		}
	}
}
