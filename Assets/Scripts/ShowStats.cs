using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the user input in the Stats scene and displays information based on that input. 
/// The user's statistics are stored in <see cref="GameModeStats"/> objects.
/// The buttons call the single method SetStatsText(gametype, boardsize)
/// </summary>
public class ShowStats : MonoBehaviour {
	private int gameMode;
	private int boardSize;

	private Text highScoreTxt;
	private Text highScoreMovesTxt;
	private Text highScoreGameTimeTxt;
	private Text highScoreDayTxt;
	private Text highScoreTimeTxt;
	private Text gamesPlayedTxt;
	private Text gamesWonTxt;
	private Text percentCompletedTxt;
	private Text lowestMovesTxt;
	private Text highestMovesTxt;
	private Text avgMovesTxt;
	private Text lowestTimeTxt;
	private Text highestTimeTxt;
	private Text avgTimeTxt;

	private Button overallBtn;
	private Button arcadeBtn;
	private Button patternBtn;
	private Button columnBtn;
	private Button rowButton;

	private Button nBtn;
	private Button threeBtn;
	private Button fourBtn;
	private Button fiveBtn;
	private Button sixBtn;
	private Button sevenBtn;
	private Button eightBtn;

	// Use this for initialization
	void Start () {
		highScoreTxt = GameObject.Find ("HighScoreText").GetComponent<Text> ();
		highScoreMovesTxt = GameObject.Find ("HighScoreMovesText").GetComponent<Text> ();
		highScoreGameTimeTxt = GameObject.Find ("HighScoreGameTimeText").GetComponent<Text> ();
		highScoreDayTxt = GameObject.Find ("HighScoreDayText").GetComponent<Text> ();
		highScoreTimeTxt = GameObject.Find ("HighScoreTimeText").GetComponent<Text> ();
		gamesPlayedTxt = GameObject.Find ("GamesPlayedText").GetComponent<Text> ();
		gamesWonTxt = GameObject.Find ("GamesWonText").GetComponent<Text> ();
		percentCompletedTxt = GameObject.Find ("PercentGamesCompletedText").GetComponent<Text> ();
		lowestMovesTxt = GameObject.Find ("LowestMovesText").GetComponent<Text> ();
		highestMovesTxt = GameObject.Find ("HighestMovesText").GetComponent<Text> ();
		avgMovesTxt = GameObject.Find ("AverageMovesText").GetComponent<Text> ();
		lowestTimeTxt = GameObject.Find ("LowestTimeText").GetComponent<Text> ();
		highestTimeTxt = GameObject.Find ("HighestTimeText").GetComponent<Text> ();
		avgTimeTxt = GameObject.Find ("AverageTimeText").GetComponent<Text> ();

		overallBtn = GameObject.Find ("OverallButton").GetComponent<Button> ();
		arcadeBtn = GameObject.Find ("ArcadeButton").GetComponent<Button> ();
		patternBtn = GameObject.Find ("PatternButton").GetComponent<Button> ();
		columnBtn = GameObject.Find ("ColumnButton").GetComponent<Button> ();
		rowButton = GameObject.Find ("RowButton").GetComponent<Button> ();

		nBtn = GameObject.Find ("nxnButton").GetComponent<Button> ();
		threeBtn = GameObject.Find ("3x3Button").GetComponent<Button> ();
		fourBtn = GameObject.Find ("4x4Button").GetComponent<Button> ();
		fiveBtn = GameObject.Find ("5x5Button").GetComponent<Button> ();
		sixBtn = GameObject.Find ("6x6Button").GetComponent<Button> ();
		sevenBtn = GameObject.Find ("7x7Button").GetComponent<Button> ();
		eightBtn = GameObject.Find ("8x8Button").GetComponent<Button> ();

		SetStatsText (99, 99);
		overallBtn.onClick.AddListener (() => SetStatsText (99, boardSize));
		arcadeBtn.onClick.AddListener (() => SetStatsText (0, boardSize));
		patternBtn.onClick.AddListener (() => SetStatsText (1, boardSize));
		columnBtn.onClick.AddListener (() => SetStatsText (2, boardSize));
		rowButton.onClick.AddListener (() => SetStatsText (3, boardSize));
		overallBtn.enabled = false;

		nBtn.onClick.AddListener (() => SetStatsText (gameMode, 99));
		threeBtn.onClick.AddListener (() => SetStatsText (gameMode, 3));
		fourBtn.onClick.AddListener (() => SetStatsText (gameMode, 4));
		fiveBtn.onClick.AddListener (() => SetStatsText (gameMode, 5));
		sixBtn.onClick.AddListener (() => SetStatsText (gameMode, 6));
		sevenBtn.onClick.AddListener (() => SetStatsText (gameMode, 7));
		eightBtn.onClick.AddListener (() => SetStatsText (gameMode, 8));
		nBtn.enabled = false;

	}
    /// <summary>
    /// Sets the stats text.
    /// </summary>
    /// <param name="a">Game type</param>
    /// <param name="b">Board size</param>
    private void SetStatsText(int a, int b) {
		gameMode = a;
		boardSize = b;
		switch (gameMode) {
		case 99:
			overallBtn.enabled = false;
			arcadeBtn.enabled = true;
			patternBtn.enabled = true;
			columnBtn.enabled = true;
			rowButton.enabled = true;
			break;
		case 0:
			overallBtn.enabled = true;
			arcadeBtn.enabled = false;
			patternBtn.enabled = true;
			columnBtn.enabled = true;
			rowButton.enabled = true;
			break;
		case 1:
			overallBtn.enabled = true;
			arcadeBtn.enabled = true;
			patternBtn.enabled = false;
			columnBtn.enabled = true;
			rowButton.enabled = true;
			break;
		case 2:
			overallBtn.enabled = true;
			arcadeBtn.enabled = true;
			patternBtn.enabled = true;
			columnBtn.enabled = false;
			rowButton.enabled = true;
			break;
		case 3:
			overallBtn.enabled = true;
			arcadeBtn.enabled = true;
			patternBtn.enabled = true;
			columnBtn.enabled = true;
			rowButton.enabled = false;
			break;
		}
		switch (boardSize) {
		case 99:
			nBtn.enabled = false;
			threeBtn.enabled = true;
			fourBtn.enabled = true;
			fiveBtn.enabled = true;
			sixBtn.enabled = true;
			sevenBtn.enabled = true;
			eightBtn.enabled = true;
			break;
		case 3:
			nBtn.enabled = true;
			threeBtn.enabled = false;
			fourBtn.enabled = true;
			fiveBtn.enabled = true;
			sixBtn.enabled = true;
			sevenBtn.enabled = true;
			eightBtn.enabled = true;
			break;
		case 4:
			nBtn.enabled = true;
			threeBtn.enabled = true;
			fourBtn.enabled = false;
			fiveBtn.enabled = true;
			sixBtn.enabled = true;
			sevenBtn.enabled = true;
			eightBtn.enabled = true;
			break;
		case 5:
			nBtn.enabled = true;
			threeBtn.enabled = true;
			fourBtn.enabled = true;
			fiveBtn.enabled = false;
			sixBtn.enabled = true;
			sevenBtn.enabled = true;
			eightBtn.enabled = true;
			break;
		case 6:
			nBtn.enabled = true;
			threeBtn.enabled = true;
			fourBtn.enabled = true;
			fiveBtn.enabled = true;
			sixBtn.enabled = false;
			sevenBtn.enabled = true;
			eightBtn.enabled = true;
			break;
		case 7:
			nBtn.enabled = true;
			threeBtn.enabled = true;
			fourBtn.enabled = true;
			fiveBtn.enabled = true;
			sixBtn.enabled = true;
			sevenBtn.enabled = false;
			eightBtn.enabled = true;
			break;
		case 8:
			nBtn.enabled = true;
			threeBtn.enabled = true;
			fourBtn.enabled = true;
			fiveBtn.enabled = true;
			sixBtn.enabled = true;
			sevenBtn.enabled = true;
			eightBtn.enabled = false;
			break;
		}
		GameModeStats stats = new GameModeStats (gameMode, boardSize);
		stats.BuildStats ();
		highScoreTxt.text = "High Score: " + stats.GetHighScore ().Score.ToString ("0");
		highScoreMovesTxt.text = "Moves: " + stats.GetHighScore ().Moves.ToString ("0");
		float gt = stats.GetHighScore ().GameTime ;
		int gtMin = (int)gt / 60;
		int gtSec = (int)gt % 60;
		highScoreGameTimeTxt.text = "Time : " + gtMin.ToString ("0") + ":" + gtSec.ToString("00");
		highScoreDayTxt.text = stats.GetHighScore ().Day;
		highScoreTimeTxt.text = stats.GetHighScore ().Time;
		gamesPlayedTxt.text = stats.GetGamesPlayed().ToString("0");
		gamesWonTxt.text = stats.GetGamesWon ().ToString("0");
		float percentCompleted = 0f;
		if (stats.GetGamesPlayed () > 0) {
			percentCompleted = (float)stats.GetGamesWon () / stats.GetGamesPlayed () * 100;
		}
		percentCompletedTxt.text = percentCompleted.ToString ("0") + "%";
		lowestMovesTxt.text = stats.GetLowestMoves ().ToString("0");
		highestMovesTxt.text = stats.GetHighestMoves ().ToString("0");
		avgMovesTxt.text = stats.GetAvgMoves ().ToString("0");
		float lowTime = stats.GetLowestTime();
		int lMin = (int)lowTime / 60;
		int lSec = (int)lowTime % 60;
		lowestTimeTxt.text = lMin.ToString ("0") + ":" + lSec.ToString ("00");
		float highTime = stats.GetHighestTime ();
		int hMin = (int)highTime / 60;
		int hSec = (int)highTime % 60;
		highestTimeTxt.text = hMin.ToString ("0") + ":" + hSec.ToString ("00");
		float avgTime = stats.GetAvgTime ();
		int aMin = (int)avgTime / 60;
		int aSec = (int)avgTime % 60;
		avgTimeTxt.text = aMin.ToString ("0") + ":" + aSec.ToString ("00");
	}
}