using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used by the Customization scene. 
/// This class handles the user input through the UI and saves the selections in a <see cref="Customization"/> object.
/// The sprites are set and changed through Unity3D where the script is run (CuztomizationPanel).
/// </summary>
public class CustomizationMenu : MonoBehaviour {
	private List<Color> colorList = new List<Color> () {		
		new Color32(255, 0, 0, 255),
		new Color32(0, 0, 255, 255),
		new Color32(0, 255, 255, 255),
		new Color32(128, 0, 128, 255),
		new Color32 (255, 255, 255, 255),
		new Color32 (255, 255, 0, 255),
		new Color32 (255, 250, 200, 255),
		new Color32 (255, 215, 180, 255),
		new Color32 (0, 255, 0, 255),
		new Color32 (230, 25, 75, 255),
		new Color32 (60, 180, 75, 255),
		new Color32 (0, 130, 200, 255),
		new Color32 (245, 130, 48, 255),
		new Color32 (145, 30, 80, 255),
		new Color32 (240, 50, 230, 255),
		new Color32 (0, 128, 128, 255),
		new Color32 (230, 190, 255, 255),
		new Color32 (170, 110, 40, 255),
		new Color32 (128, 0, 0, 255),
		new Color32 (170, 255, 195, 255),
		new Color32 (128, 128, 0, 255),
		new Color32 (0, 0, 128, 255),
		new Color32 (128, 128, 128, 255)
	};
	Customization custom;
	List<Color> tempColor;

	private Dropdown matDropdown;

    public Sprite ice;
	public Sprite marble;
	public Sprite paper;
    public Sprite wood;

    /// <summary>
    /// Starts this instance.
    /// </summary>
    void Start () {
		custom = new Customization ();
		custom.BuildCustom ();

		matDropdown = GameObject.Find ("MaterialDropdown").GetComponent<Dropdown> ();
		Button bgColorBtn = GameObject.Find ("BackgroundColorButton").GetComponent<Button> ();
		Button piece1Btn = GameObject.Find ("Piece1").GetComponent<Button> ();
		Button piece2Btn = GameObject.Find ("Piece2").GetComponent<Button> ();
		Button piece3Btn = GameObject.Find ("Piece3").GetComponent<Button> ();
		Button piece4Btn = GameObject.Find ("Piece4").GetComponent<Button> ();
		Button piece5Btn = GameObject.Find ("Piece5").GetComponent<Button> ();
		Button piece6Btn = GameObject.Find ("Piece6").GetComponent<Button> ();
		Button piece7Btn = GameObject.Find ("Piece7").GetComponent<Button> ();
		Button piece8Btn = GameObject.Find ("Piece8").GetComponent<Button> ();

		switch(custom.GetMaterial()) {
		case 1:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = ice;
			break;
		case 2:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = marble;
			break;
		case 3:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = paper;
			break;
		case 4:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = wood;
			break;
		default:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = null;
			break;
		}

		matDropdown.onValueChanged.AddListener (SetMaterial);


		tempColor = new List<Color> ();
		bgColorBtn.onClick.AddListener (() => CreateColorBoard (0));
		piece1Btn.onClick.AddListener (() => CreateColorBoard (1));
		piece2Btn.onClick.AddListener (() => CreateColorBoard (2));
		piece3Btn.onClick.AddListener (() => CreateColorBoard (3));
		piece4Btn.onClick.AddListener (() => CreateColorBoard (4));
		piece5Btn.onClick.AddListener (() => CreateColorBoard (5));
		piece6Btn.onClick.AddListener (() => CreateColorBoard (6));
		piece7Btn.onClick.AddListener (() => CreateColorBoard (7));
		piece8Btn.onClick.AddListener (() => CreateColorBoard (8));

	}
    /// <summary>
    /// Sets the material.
    /// </summary>
    /// <param name="num">The number.</param>
    public void SetMaterial(int num) {
		switch(num) {
		case 1:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = ice;
			break;
		case 2:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = marble;
			break;
		case 3:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = paper;
			break;
		case 4:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = wood;
			break;
		default:
			GameObject.Find ("MaterialDropdown").GetComponent<Image> ().sprite = null;
			break;
		}
		//int num = matDropdown.value;
		custom.SetMaterial (num);
		PlayerPrefs.Save ();
	}
    /// <summary>
    /// Creates the color board.
    /// </summary>
    /// <param name="flag">The flag.</param>
    public void CreateColorBoard (int flag) {
		GameObject[] colors = GameObject.FindGameObjectsWithTag ("Color");
		for (int i = colors.Length - 1; i >= 0; i--) {
			colors [i].tag = "Untagged";
			Destroy (colors [i]);
		}
		for (int i = 0; i < colorList.Count; i++) {
			tempColor.Add (colorList [i]);
		}
		List<PieceColor> pieceColors = custom.GetPieceColors ();
		if (flag != 0) {
			for (int i = pieceColors.Count - 1; i >= 0; i--) {
				tempColor.Remove (pieceColors [i].color);
			}
		}
		var board = GameObject.Find ("ColorBoard");
		for (int i = tempColor.Count - 1; i >= 0; i--) {
			GameObject a = new GameObject();
			a.AddComponent<Image> ();
			a.AddComponent<Button> ();
			a.tag = "Color";
			Instantiate (a);
			a.transform.SetParent (board.transform, false);
			a.GetComponent<Image> ().color = tempColor [i];
			tempColor.Remove (tempColor [i]);
			switch (flag) {
			case 0:
				a.GetComponent<Button> ().onClick.AddListener (() => SetBackground (a.GetComponent<Image> ().color));
				break;
			default:
				a.GetComponent<Button> ().onClick.AddListener (() => SetPieceColor (a.GetComponent<Image> ().color, flag));
				break;
			}
		}
	}
    /// <summary>
    /// Sets the background.
    /// </summary>
    /// <param name="color">The color.</param>
    public void SetBackground(Color color) {
		string bgcolor = color.r + "," + color.g + "," + color.b + "," + color.a;
		custom.SetBGColor (new GameColor (bgcolor));
		GameObject.Find ("BackgroundColorButton").GetComponent<Image> ().color = color;
		GameObject.Find ("ColorListPanel").SetActive (false);
		PlayerPrefs.Save ();
	}

    /// <summary>
    /// Sets the color of the piece.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="flag">The flag.</param>
    public void SetPieceColor(Color color, int flag) {
		//string bgcolor = color.r + "," + color.g + "," + color.b + "," + color.a;
		//List<PieceColor> pieceColors = custom.getPieceColors ();
		switch (flag) {
		case 1:
			custom.GetPieceColors () [0] = new PieceColor (color, 0);
			//pieceColors [0] = new PieceColor (color, 0);
			GameObject.Find ("Piece1").GetComponent<Image> ().color = color;
			break;
		case 2:
			custom.GetPieceColors () [1] = new PieceColor (color, 1);
			//pieceColors [1] = new PieceColor (color, 1);
			GameObject.Find ("Piece2").GetComponent<Image> ().color = color;
			break;
		case 3:
			custom.GetPieceColors () [2] = new PieceColor (color, 2);
			//pieceColors [2] = new PieceColor (color, 2);
			GameObject.Find ("Piece3").GetComponent<Image> ().color = color;
			break;
		case 4:
			custom.GetPieceColors () [3] = new PieceColor (color, 3);
			//pieceColors [3] = new PieceColor (color, 3);
			GameObject.Find ("Piece4").GetComponent<Image> ().color = color;
			break;
		case 5:
			custom.GetPieceColors () [4] = new PieceColor (color, 4);
			//pieceColors [4] = new PieceColor (color, 4);
			GameObject.Find ("Piece5").GetComponent<Image> ().color = color;
			break;
		case 6:
			custom.GetPieceColors () [5] = new PieceColor (color, 5);
			//pieceColors [5] = new PieceColor (color, 5);
			GameObject.Find ("Piece6").GetComponent<Image> ().color = color;
			break;
		case 7:
			custom.GetPieceColors () [6] = new PieceColor (color, 6);
			//pieceColors [6] = new PieceColor (color, 6);
			GameObject.Find ("Piece7").GetComponent<Image> ().color = color;
			break;
		case 8:
			custom.GetPieceColors () [7] = new PieceColor (color, 7);
			//pieceColors [7] = new PieceColor (color, 7);
			GameObject.Find ("Piece8").GetComponent<Image> ().color = color;
			break;
		}
		custom.SavePieces ();
		PlayerPrefs.Save ();
		GameObject.Find ("ColorListPanel").SetActive (false);
	}
}
