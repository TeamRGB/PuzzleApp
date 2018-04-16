using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the ui elements in the options scene.
/// It uses <see cref="GamePreferences"/> to handle the data.
/// All user input is handled using listeners.
/// </summary>
public class OptionsMenu : MonoBehaviour {

	GamePreferences options;

	private Button arcadeBtn;
	private Button patternBtn;
	private Button columnBtn;
	private Button rowBtn;

	private Slider sizeSlider;
	private Slider colorSlider;

	private Text sizeLabel;
	private Text colorLabel;

	// Use this for initialization
	void Start () {
		arcadeBtn = GameObject.Find ("ArcadeBtn").GetComponent<Button> ();
		patternBtn = GameObject.Find ("PatternBtn").GetComponent<Button> ();
		columnBtn = GameObject.Find ("ColumnBtn").GetComponent<Button> ();
		rowBtn = GameObject.Find ("RowBtn").GetComponent<Button> ();
		colorSlider = GameObject.Find ("ColorSlider").GetComponent<Slider> ();
		sizeSlider = GameObject.Find ("SizeSlider").GetComponent<Slider> ();
		sizeLabel = GameObject.Find ("BoardSizeText").GetComponent<Text> ();
		colorLabel = GameObject.Find ("ColorNumberText").GetComponent<Text> ();
		options = new GamePreferences ();
		options.BuildPreferences ();
		if (options.GetGameType().GameTypeIndex== 0) {
			arcadeBtn.interactable = false;
		} else if (options.GetGameType().GameTypeIndex== 1) {
			patternBtn.interactable = false;
		}else if (options.GetGameType().GameTypeIndex== 2) {
			columnBtn.interactable = false;
			colorSlider.interactable = false;
		}else if (options.GetGameType().GameTypeIndex== 3) {
			rowBtn.interactable = false;
			colorSlider.interactable = false;
		}
		sizeSlider.value = options.GetBoardSize ();
		colorSlider.value = options.GetColors ();
		colorSlider.maxValue = sizeSlider.value;

		arcadeBtn.onClick.AddListener (() => SetGameMode (0));
		patternBtn.onClick.AddListener (() => SetGameMode (1));
		columnBtn.onClick.AddListener (() => SetGameMode (2));
		rowBtn.onClick.AddListener (() => SetGameMode (3));

		sizeSlider.onValueChanged.AddListener (SetSizeSliderValue);
		colorSlider.onValueChanged.AddListener (setColorSliderValue);
		sizeLabel.text = options.GetBoardSize ().ToString ("0") +"x" + options.GetBoardSize ().ToString ("0");
		colorLabel.text = options.GetColors ().ToString ("0"); 

	}
	public void SetSizeSliderValue(float value) {
		if (!columnBtn.IsInteractable () || !rowBtn.IsInteractable ()) {
			colorSlider.maxValue = sizeSlider.value;
			colorSlider.value = sizeSlider.value;
			sizeLabel.text = sizeSlider.value.ToString ("0") + "x" + sizeSlider.value.ToString ("0");
		} else {
			colorSlider.maxValue = sizeSlider.value;
			sizeLabel.text = sizeSlider.value.ToString ("0") + "x" + sizeSlider.value.ToString ("0");
		}
		options.SetBoardSize ((int)sizeSlider.value);
		PlayerPrefs.Save ();
	}
	public void setColorSliderValue(float value) {
		colorLabel.text = colorSlider.value.ToString("0");
		options.SetColors ((int)colorSlider.value);
		PlayerPrefs.Save ();
	}
	public void SetGameMode (int mode) {
		switch(mode) {
		case 0:
			colorSlider.interactable = true;
			arcadeBtn.interactable = false;
			patternBtn.interactable = true;
			columnBtn.interactable = true;
			rowBtn.interactable = true;
			break;
		case 1:
			colorSlider.interactable = true;
			arcadeBtn.interactable = true;
			patternBtn.interactable = false;
			columnBtn.interactable = true;
			rowBtn.interactable = true;
			break;
		case 2:
			colorSlider.interactable = false;
			colorSlider.value = sizeSlider.value;
			arcadeBtn.interactable = true;
			patternBtn.interactable = true;
			columnBtn.interactable = false;
			rowBtn.interactable = true;
			break;
		case 3:
			colorSlider.interactable = false;
			colorSlider.value = sizeSlider.value;
			arcadeBtn.interactable = true;
			patternBtn.interactable = true;
			columnBtn.interactable = true;
			rowBtn.interactable = false;
			break;
		}
		options.SetGameType (new GameType(mode));
		PlayerPrefs.Save ();
	}
}
