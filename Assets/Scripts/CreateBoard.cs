using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// This class creates the handles the GameBoard scene.
/// The class handles all UI elements, the gameboard, and user input.
/// The users customzation options, game options, and stats are all updated in this class.
/// The game board is created using a two dimensional array.
/// The empty or mover piece is a reference to one of the objects on the game board.
/// The mover piece has a zero alpha value making it transparent.
/// The game pieces are all buttons, which when clicked will update the color of themselves and the mover piece if they are next to the mover piece.
/// The time and score are updated on the UI thread using the Unity FixedUpdate API.
/// </summary>
public class CreateBoard : MonoBehaviour {
	private GameObject boardPiece;
	private GameObject solutionPiece;
	private GameObject pauseScreen;
	private GameObject scoreScreen;
	private GameObject solutionScreen;
	private GamePiece[,] pieces;
	private GamePiece[,] solutionPieces;
	private GamePiece mover;
	private bool running = false;
	private float time;
	private int type;
	private int boardSize;
	private int colors;
	private List<PieceColor> customColors;
	private Text timer;
	private Button pauseBtn;
	private int moves;
	private Hashtable colorCount;
	public Sprite ice;
	public Sprite marble;
	public Sprite paper;
	public Sprite wood;
	private int material;
	private GamePreferences options;
	private Customization custom;
	private GameModeStats overallStats;
	private GameModeStats modeStats;
	private GameModeStats boardStats;
	private GameModeStats overallBoardStats;

    /// <summary>
    /// Starts this instance.
    /// </summary>
    void Start () {
		options = new GamePreferences ();
		options.BuildPreferences ();
		custom = new Customization ();
		custom.BuildCustom ();
		material = custom.GetMaterial ();
		Image bg = GameObject.Find ("Canvas").GetComponent<Image> ();
		bg.color = custom.GetBGColor ().GetColor ();
		boardSize = options.GetBoardSize();
		type = options.GetGameType ().GameTypeIndex;
		colors = options.GetColors ();
		customColors = custom.GetPieceColors ();
		overallStats = new GameModeStats (99, 99);
		modeStats = new GameModeStats (type,99);
		overallBoardStats = new GameModeStats (99, boardSize);
		boardStats = new GameModeStats (type, boardSize);
		overallStats.BuildStats ();
		modeStats.BuildStats ();
		overallBoardStats.BuildStats ();
		boardStats.BuildStats ();
		int overAllGames = overallStats.GetGamesPlayed ();
		overAllGames++;
		overallStats.SetGamesPlayed (overAllGames);
		int modeGames = modeStats.GetGamesPlayed ();
		modeGames++;
		modeStats.SetGamesPlayed (modeGames);
		int overAllBoardGames = overallBoardStats.GetGamesPlayed ();
		overAllBoardGames++;
		overallBoardStats.SetGamesPlayed (overAllBoardGames);
		int boardGames = boardStats.GetGamesPlayed ();
		boardGames++;
		boardStats.SetGamesPlayed (boardGames);
		GameObject board = GameObject.Find ("GameBoard");
		GameObject solutionBoard = GameObject.Find ("SolutionBoard");
		Text highScoreBITxt = GameObject.Find ("HighScoreBoardInfoText").GetComponent<Text> ();
		Text highScoreTxt = GameObject.Find ("HighScoreText").GetComponent<Text> ();
		Text highScoreMTxt = GameObject.Find ("HighScoreMovesText").GetComponent<Text> ();
		Text highScoreGTTxt = GameObject.Find ("HighScoreGameTimeText").GetComponent<Text> ();
		Text highScoreDTxt = GameObject.Find ("HighScoreDayText").GetComponent<Text> ();
		Text highScoreTTxt = GameObject.Find ("HighScoreTimeText").GetComponent<Text> ();
		string mode;
		if (type == 0)
			mode = "Arcade";
		else if (type == 1)
			mode = "Pattern";
		else if (type == 2)
			mode = "Coulumn";
		else
			mode = "Row";
		highScoreBITxt.text = "Your best score in " + mode + " mode for a " + boardSize + "x" + boardSize + " size board";
		highScoreTxt.text = "High Score: " + boardStats.GetHighScore ().Score.ToString ("0");
		highScoreMTxt.text = "Moves: " + boardStats.GetHighScore ().Moves .ToString("0");
		int hsGTMin = (int)boardStats.GetHighScore ().GameTime / 60;
		int hsGTSec = (int)boardStats.GetHighScore ().GameTime % 60;
		highScoreGTTxt.text = "Time: " + hsGTMin.ToString ("0") +":" + hsGTSec.ToString ("00");
		highScoreDTxt.text = boardStats.GetHighScore ().Day;
		highScoreTTxt.text = boardStats.GetHighScore ().Time;
		pauseScreen = GameObject.Find ("PauseBoard");
		solutionScreen = GameObject.Find ("SolutionPauseBoard");
		scoreScreen = GameObject.Find ("Scoreboard");
		boardPiece = GameObject.Find ("BoardPiece");
		solutionPiece = GameObject.Find ("SolutionPiece");
		scoreScreen.SetActive (false);
		moves = 0;
		colorCount = new Hashtable ();
		if (type != 1) {
			solutionScreen.SetActive (false);
		} else {
			solutionScreen.SetActive (true);
		}
		board.GetComponent<GridLayoutGroup> ().constraintCount = boardSize;
		switch (boardSize) {
		case 8:
			board.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (56.25f, 56.25f);
			break;
		case 7:
			board.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (64.3f, 64.3f);
			break;
		case 6:
			board.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (75f, 75f);
			break;
		case 5:
			board.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (90f, 90f);
			break;
		case 4:
			board.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (112.5f, 112.5f);
			break;
		case 3:
			board.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (150f, 150f);
			break;
		}
		time = 0.00f;
		pauseBtn = GameObject.Find ("PauseBtn").GetComponent<Button> ();
		pauseBtn.onClick.AddListener (() => Pause ());
		if (type == 1) {
			Text countdownTxt = GameObject.Find ("CountdownTxt").GetComponent<Text> ();
			countdownTxt.enabled = false;
			pauseScreen.SetActive (true);
			do {
				solutionPieces = new GamePiece[boardSize, boardSize];
				MakeBoard (solutionBoard, solutionPiece, solutionPieces, true);
				pieces = new GamePiece[boardSize, boardSize];
				MakeBoard (board, boardPiece, pieces, false);
				if(IsComplete()) {
					colorCount.Clear();
				}
			} while(IsComplete ());
			solutionBoard.GetComponent<GridLayoutGroup> ().constraintCount = boardSize;
			switch (boardSize) {
			case 8:
				solutionBoard.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (40f, 40f);
				break;
			case 7:
				solutionBoard.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (50f, 50f);
				break;
			case 6:
				solutionBoard.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (55f, 55f);
				break;
			case 5:
				solutionBoard.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (65f, 65f);
				break;
			case 4:
				solutionBoard.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (80f, 80f);
				break;
			case 3:
				solutionBoard.GetComponent<GridLayoutGroup> ().cellSize = new Vector2 (115f, 115f);
				break;
			}
			pauseScreen.SetActive (false);
			Button pauseBtn = GameObject.Find ("PauseBtn").GetComponent<Button> ();
			pauseBtn.enabled = false;
			Button resumeBtn = GameObject.Find ("SolutionResumeButton").GetComponent<Button> ();
			resumeBtn.onClick.AddListener (() => SolutionPause ());
		} else {
			solutionBoard.SetActive (false);
			solutionScreen.SetActive (false);
			do {
				pieces = new GamePiece[boardSize, boardSize];
				MakeBoard (board, boardPiece, pieces, false);
				if(IsComplete()) {
					colorCount.Clear();
				}
			} while(IsComplete ());
			Button resumeBtn = GameObject.Find ("PauseResumeButton").GetComponent<Button> ();
			resumeBtn.onClick.AddListener (() => Pause ());
			pauseScreen.SetActive (false);
			StartCoroutine (Countdown ());
		}

	}
    /// <summary>
    /// Makes the board.
    /// </summary>
    /// <param name="board">The board.</param>
    /// <param name="piece">The piece.</param>
    /// <param name="pieces">The pieces.</param>
    /// <param name="isSolBoard">if set to <c>true</c> [is sol board].</param>
    private void MakeBoard(GameObject board, GameObject piece, GamePiece[,] pieces, bool isSolBoard) {
		GameObject[] oldPieces = GameObject.FindGameObjectsWithTag ("GamePiece");
		foreach (GameObject obj in oldPieces) {
			Destroy (obj);
		}
			
			int numOfBoxes = boardSize * boardSize - 1;
			List<PieceColor> pieceColors = new List<PieceColor> ();
			for (int i = 0; i < numOfBoxes; i++) {
				pieceColors.Add (customColors [i % colors]);
			}
		for (int i = 0; i < boardSize && numOfBoxes > 0; i++) {
			for (int j = 0; j < boardSize && numOfBoxes > 0; j++) {
				if (i == 0 && j == 0) {
					int fpColor = Random.Range (0, pieceColors.Count);
					GamePiece firstpiece = new GamePiece (0, 0, pieceColors [fpColor], piece);
					if (colorCount.ContainsKey (pieceColors [fpColor].colorIndex)) {
						colorCount [pieceColors [fpColor].colorIndex] = (int)colorCount [pieceColors [fpColor].colorIndex] + 1;
					} else {
						colorCount.Add (pieceColors [fpColor].colorIndex, 1);
					}
					firstpiece.RefreshColor ();
					if (!isSolBoard) {
						firstpiece.obj.GetComponent<Button> ().onClick.AddListener (() => MovePiece (0, 0));
					} else {
						firstpiece.obj.GetComponent<Button> ().enabled = false;
					}
					pieces [0, 0] = firstpiece;
					pieceColors.Remove (pieceColors [fpColor]);
					switch (material) {
					case 1:
						firstpiece.obj.GetComponent<Image> ().sprite = ice;
						break;
					case 2:
						firstpiece.obj.GetComponent<Image> ().sprite = marble;
						break;
					case 3:
						firstpiece.obj.GetComponent<Image> ().sprite = paper;
						break;
					case 4:
						firstpiece.obj.GetComponent<Image> ().sprite = wood;
						break;
					}
				} else if (numOfBoxes == 1) {
					GameObject a = (GameObject)Instantiate (piece); 
					GamePiece temp = new GamePiece (); 
					temp.x = j;
					temp.y = i;
					temp.obj = a;
					temp.color = new PieceColor ();
					a.transform.SetParent (board.transform, false);
					pieces [j, i] = temp;
					mover = pieces [boardSize - 1, boardSize - 1];
					mover.RefreshColor ();
					if (!isSolBoard) {
						a.GetComponent<Button> ().onClick.AddListener (() => MovePiece (temp.x, temp.y)); 
					} else {
						a.GetComponent<Button> ().enabled = false;
					}
					if (!isSolBoard) {
						a.tag = "GamePiece";
					}
					numOfBoxes--;
				} else {
					GameObject a = (GameObject)Instantiate (piece); 
					GamePiece temp = new GamePiece (); 
					temp.x = j;
					temp.y = i;
					temp.obj = a;
					int colorNum = Random.Range (0, pieceColors.Count);
					temp.color = pieceColors [colorNum];
					if (colorCount.ContainsKey (pieceColors [colorNum].colorIndex)) {
						colorCount [pieceColors [colorNum].colorIndex] = (int)colorCount [pieceColors [colorNum].colorIndex] + 1;
					} else {
						colorCount.Add (pieceColors [colorNum].colorIndex, 1);
					}
					a.transform.SetParent (board.transform, false);
					pieces [j, i] = temp;
					a.GetComponent<Image> ().color = pieceColors [colorNum].color;
                    //Set the material
					switch (material) {
					case 1:
						a.GetComponent<Image> ().sprite = ice;
						break;
					case 2:
						a.GetComponent<Image> ().sprite = marble;
						break;
					case 3:
						a.GetComponent<Image> ().sprite = paper;
						break;
					case 4:
						a.GetComponent<Image> ().sprite = wood;
						break;
					}
					numOfBoxes--;
					if (!isSolBoard) {
						a.GetComponent<Button> ().onClick.AddListener (() => MovePiece (temp.x, temp.y)); 
					} else {
						a.GetComponent<Button> ().enabled = false;
					}
					pieceColors.Remove (pieceColors [colorNum]);
					if (!isSolBoard) {
						a.tag = "GamePiece";
					}
				}
			}
		}
	}
    /// <summary>
    /// Moves the piece at the specified x,y.
    /// Used for the button listener.
    /// This is the method that handles all movement on the board.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    private void MovePiece(int x, int y) {
		
		if (!running) {
			running = true;
			Text btnTxt = GameObject.Find ("PauseBtnTxt").GetComponent<Text> ();
			btnTxt.text = "Pause";
		}
		if (x == (mover.x + 1) && y == mover.y)
        {
            mover.color = pieces [x, y].color;
			mover.RefreshColor ();
			mover = pieces [x, y];
			mover.color = new PieceColor();
			mover.RefreshColor ();
			moves++;
		} else if (y == (mover.y + 1) && x == mover.x)
        {
            mover.color = pieces [x, y].color;
			mover.RefreshColor ();
			mover = pieces [x, y];
			mover.color = new PieceColor();
			mover.RefreshColor ();
			moves++;
		} else if (x == (mover.x - 1) && y == mover.y)
        {
            mover.color = pieces [x, y].color;
			mover.RefreshColor ();
			mover = pieces [x, y];
			mover.color = new PieceColor();
			mover.RefreshColor ();
			moves++;
		} else if (y == (mover.y - 1) && x == mover.x)
        {
            mover.color = pieces [x, y].color;
			mover.RefreshColor ();
			mover = pieces [x, y];
			mover.color = new PieceColor();
			mover.RefreshColor ();
			moves++;
		}
		if (IsComplete ()) {		
			Text topScoreTxt = GameObject.Find ("ScoreText").GetComponent<Text> ();
			topScoreTxt.text = GetScore ().ToString ("0");
			Text movesTxt = GameObject.Find ("MovesText").GetComponent<Text> ();
			movesTxt.text = moves.ToString ("0");
			scoreScreen.SetActive (true);
			running = false;
			pauseBtn.enabled = false;
			StartCoroutine (SolveAnimation ());
		}
	}
    /// <summary>
    /// Updates the stats for the game mode param.
    /// </summary>
    /// <param name="stats">The stats to update.</param>
    private void UpdateStats(GameModeStats stats) {
		int gamesWon = stats.GetGamesWon () + 1;
		stats.SetGamesWon (gamesWon);
		int lMoves = stats.GetLowestMoves ();
		int hMoves = stats.GetHighestMoves ();
		int aMoves = stats.GetAvgMoves ();
		float lTime = stats.GetLowestTime ();
		float hTime = stats.GetHighestTime ();
		float aTime = stats.GetAvgTime ();
		if (moves < lMoves || lMoves == 0) {
			stats.SetLowestMoves (moves);
		}
		if (moves > hMoves) {
			stats.SetHighestMoves (moves);
		}
		aMoves = aMoves + ((moves - aMoves) / gamesWon);
		stats.SetAvgMoves (aMoves);
		if (time < lTime || lTime == 0f) {
			stats.SetLowestTime (time);
		}
		if (time > hTime) {
			stats.SetHighestTime (time);
		}
		aTime = aTime + ((time - aTime) / gamesWon);
		stats.SetAvgTime (aTime);
	}
    /// <summary>
    /// Countdowns this instance.
    /// Moves the board off screen, then counts down the CountdownTxt element.
    /// After the countdown is down it moves the board on screen.
    /// </summary>
    /// <returns></returns>
    IEnumerator Countdown() {
		pauseBtn.enabled = false;
		GameObject board = GameObject.Find ("GameBoard");
		Vector3 oldBoard = board.transform.position;
		Vector3 outsideVector = new Vector3 (-1000, -1000, 0);
		board.transform.position = outsideVector;
		Text countdownTxt = GameObject.Find ("CountdownTxt").GetComponent<Text> ();
		yield return new WaitForSecondsRealtime (1);
		countdownTxt.color = new Color32 (255, 255, 0, 255);
		countdownTxt.text = "2";
		yield return new WaitForSecondsRealtime (1);
		countdownTxt.color = new Color32 (0, 255, 0, 255);
		countdownTxt.text = "1";
		yield return new WaitForSecondsRealtime (1);
		countdownTxt.enabled = false;
		board.transform.position = oldBoard;
		running = true;
		Text btnTxt = GameObject.Find ("PauseBtnTxt").GetComponent<Text> ();
		btnTxt.text = "Pause";
		pauseBtn.enabled = true;
	}
    /// <summary>
    /// Update at a fixed screen rate.
    /// Updates the time and score if the game is running.
    /// </summary>
    void FixedUpdate () {
		Text timer = GameObject.Find ("PauseBtnTxt").GetComponent<Text> ();
		Text scoreTxt = GameObject.Find ("ScoreText").GetComponent<Text> ();
		Text movesTxt = GameObject.Find ("MovesText").GetComponent<Text> ();
		if (running) {
			time += Time.deltaTime;
			if (timer.text != null) {
				int mins = (int)time / 60;
				int sec = (int)time % 60;
				timer.text = mins.ToString ("0") + ":" + sec.ToString ("00");
				scoreTxt.text = GetScore ().ToString ("0");
				movesTxt.text = moves.ToString ("0");
			}
		}
	}
    /// <summary>
    /// Pause for the first time solution mode is run.
    /// </summary>
    public void SolutionPause() {
		Button resumeBtn = GameObject.Find ("SolutionResumeButton").GetComponent<Button> ();
		Text resumeBtnTxt = GameObject.Find ("SolutionResumeBtnTxt").GetComponent<Text> ();
		resumeBtn.onClick.RemoveAllListeners ();
		resumeBtn.onClick.AddListener(() => Pause());
		resumeBtnTxt.text = "Resume";
		Pause ();
	}
    /// <summary>
    /// Pauses this instance.
    /// </summary>
    public void Pause() {
		Button pauseBtn = GameObject.Find ("PauseBtn").GetComponent<Button> ();
        //If the game is running
		if (running) {
			if (type != 1) {
				pauseScreen.SetActive (true);
			} else {
				solutionScreen.SetActive (true);
			}
			running = false;
			pauseBtn.enabled = false;
			if (type != 1) {
				Text infoTxt = GameObject.Find ("GameInfoText").GetComponent<Text> ();
				string mode;
				if (type == 0) {
					mode = "Arcade";
				} else if (type == 2) {
					mode = "Column";
				} else {
					mode = "Row";
				}
				infoTxt.text = mode + " Mode";
			}
		}
        //Else we are paused
        else {
			if (type != 1) {
				pauseScreen.SetActive (false);
			} else {
				solutionScreen.SetActive (false);
			}
			running = true;
			pauseBtn.enabled = true;
		}
	}
    /// <summary>
    /// Determines whether the board is complete.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if the board is complete; otherwise, <c>false</c>.
    /// </returns>
    private bool IsComplete() {
        //Arcade Mode
        //This mode uses a recursive depth first search using the NumOfBlocksConnected function
        //TODO: Add Hashmap of colors as they are encountered
		if (type == 0) {
			Hashtable currentStatus = new Hashtable ();
			for (int i = 0; i < boardSize; i++) {
				for (int j = 0; j < boardSize; j++) {
					if (pieces [j, i] != mover) {
						if (currentStatus.ContainsKey (pieces [j, i].color.colorIndex)) {
							continue;
						} else {
							int connected = NumOfBlocksConnected (pieces [j, i], new List<GamePiece> ());
							currentStatus.Add (pieces [j, i].color.colorIndex, connected);
							if (currentStatus [pieces [j, i].color.colorIndex].Equals (colorCount [pieces [j, i].color.colorIndex])) {
								continue;
							} else {
								return false;
							}
						}
					}
				}
			}
			return true;
		}
        //Pattern Mode
        //Simple linear search with early exit conditions
        else if (type == 1) {
			for (int i = 0; i < boardSize; i++) {
				for (int j = 0; j < boardSize; j++) {
					if (i == boardSize - 1 && j == boardSize - 1 && pieces[j,i].Equals(mover)) {
						return true;
					} else if (!pieces [j, i].color.colorIndex.Equals (solutionPieces [j, i].color.colorIndex) || pieces[j,i].Equals(mover)) {
						return false;
					}
				}
			}
			return true;
		}
        //Column Mode
        //Simple linear search with early exit conditions
        else if (type == 2) {
			for (int j = 0; j < boardSize; j++) {
				for (int i = 0; i < boardSize - 1; i++) {
					if (pieces [j, i].Equals (mover)) {
						continue;
					}
					if (pieces [j, i + 1].Equals (mover) && i + 2 < boardSize) {
						if (!pieces [j, i].color.colorIndex.Equals (pieces [j, i + 2].color.colorIndex)) {
							return false;
						}
					} else if (!pieces [j, i].color.colorIndex.Equals (pieces [j, i + 1].color.colorIndex) && !(pieces [j, i].Equals (mover) || pieces [j, i + 1].Equals(mover))) {
						return false;
					} else if (pieces [j, i + 1].Equals (mover) && i + 2 >= boardSize) {
						continue;
					}
				}
			}
			return true;
		}
        //Row Mode
        //Simple linear search with early exit conditions
        else
        {
			for (int i = 0; i < boardSize; i++) {
				for (int j = 0; j < boardSize - 1; j++) {
					if (pieces [j, i].Equals (mover)) {
						continue;
					}
					if (pieces [j + 1, i].Equals (mover) && j + 2 < boardSize) {
						if (!pieces [j, i].color.colorIndex.Equals (pieces [j + 2, i].color.colorIndex)) {
							return false;
						}
					} else if (!pieces [j, i].color.colorIndex.Equals (pieces [j + 1, i].color.colorIndex) && !(pieces [j, i].Equals (mover) || pieces [j + 1, i].Equals(mover))) {
						return false;
					} else if (pieces [j + 1, i].Equals (mover) && j + 2 >= boardSize) {
						continue;
					}
				}
			}
			return true;
		} 
	}
    /// <summary>
    /// Recursive depth first search that returns the number of blocks connected of the same color.
    /// </summary>
    /// <param name="piece">The piece.</param>
    /// <param name="checkedPieces">The checked pieces.</param>
    /// <returns>The number of blocks connected of the color.</returns>
    private int NumOfBlocksConnected(GamePiece piece, List<GamePiece> checkedPieces) {
		int connected = 1;
		checkedPieces.Add (piece);
		if (piece.x + 1 < boardSize && piece.color.colorIndex.Equals (pieces [piece.x + 1, piece.y].color.colorIndex) && !checkedPieces.Contains(pieces[piece.x + 1, piece.y]))
			connected += NumOfBlocksConnected (pieces [piece.x + 1, piece.y], checkedPieces);
		if (piece.y + 1 < boardSize && piece.color.colorIndex.Equals (pieces [piece.x, piece.y + 1].color.colorIndex) && !checkedPieces.Contains(pieces[piece.x, piece.y + 1]))
			connected += NumOfBlocksConnected (pieces [piece.x, piece.y + 1], checkedPieces);
		if (piece.x - 1 >= 0 && piece.color.colorIndex.Equals (pieces [piece.x - 1, piece.y].color.colorIndex) && !checkedPieces.Contains(pieces[piece.x - 1, piece.y]))
			connected += NumOfBlocksConnected (pieces [piece.x - 1, piece.y], checkedPieces);
		if (piece.y - 1 >= 0 && piece.color.colorIndex.Equals (pieces [piece.x, piece.y - 1].color.colorIndex) && !checkedPieces.Contains(pieces[piece.x, piece.y - 1]))
			connected += NumOfBlocksConnected (pieces [piece.x, piece.y - 1], checkedPieces);
		return connected;
	}

    /// <summary>
    /// Gets the score.
    /// </summary>
    /// <returns>The current score</returns>
    private int GetScore() {
		int scoreFactor;
		int reductionFactor;
		switch (type) {
		case 0:
			scoreFactor = 1000;
			reductionFactor = 50;
			break;
		case 1:
			scoreFactor = 5000;
			reductionFactor = 30;
			break;
		default:
			scoreFactor = 1500;
			reductionFactor = 40;
			break;
		}
		int high = (int)(scoreFactor * Mathf.Pow(boardSize,2) * colors);
		int moveFactor = (reductionFactor * 5 * (moves + 1));
		int timeFactor = (reductionFactor * (int)(time + 1));
		int score = high - moveFactor - timeFactor;
		if (score < 0) {
			return 0;
		} else {
			return score;
		}
	}
    /// <summary>
    /// Animation for the results board.
    /// </summary>
    /// <returns></returns>
    IEnumerator SolveAnimation() {
		int score = GetScore ();
		Text finalMovesTxt = GameObject.Find ("FinalMovesText").GetComponent<Text> ();
		Text finalTimeTxt = GameObject.Find ("FinalTimeText").GetComponent<Text> ();
		Text finalScoreTxt = GameObject.Find ("FinalScoreText").GetComponent<Text> ();
		finalTimeTxt.enabled = false;
		finalScoreTxt.enabled = false;
		yield return StartCoroutine(TextCountUp (finalMovesTxt, moves, "Moves: "));
		finalTimeTxt.enabled = true;
		yield return StartCoroutine(TimeTextCountUp (finalTimeTxt, time, "Time: "));
		finalScoreTxt.enabled = true;
		yield return StartCoroutine(TextCountUp (finalScoreTxt, score, "Score: "));
		int oaHS = overallStats.GetHighScore ().Score;
		int gmHS = modeStats.GetHighScore ().Score;
		int oabHS = overallBoardStats.GetHighScore ().Score ;
		int bmHS = boardStats.GetHighScore ().Score ;
        //If the score is higher than the board size and type high score
		if (score > bmHS) {
			string highScoreDay = System.DateTime.Now.ToString ("d");
			string highScoreTime = System.DateTime.Now.ToString("hh:mm tt");
			HighScore highScore = new HighScore (score, moves, time, highScoreDay, highScoreTime);
			boardStats.SetHighScore (highScore);
			Text highScoreTxt = GameObject.Find ("HighScoreTxt").GetComponent<Text> ();
			highScoreTxt.text = "New " + boardSize + "x" + boardSize + " " + options.GetGameType().Name + "Mode High Score!";
		}
        //If the score is higher than the current game type high score
		if (score > gmHS) {
			string highScoreDay = System.DateTime.Now.ToString ("d");
			string highScoreTime = System.DateTime.Now.ToString("hh:mm tt");
			HighScore highScore = new HighScore (score, moves, time, highScoreDay, highScoreTime);
			modeStats.SetHighScore (highScore);
			Text highScoreTxt = GameObject.Find ("HighScoreTxt").GetComponent<Text> ();
			highScoreTxt.text = "New Overall " + options.GetGameType().Name + " High Score!";
		}
        //If the score is higher than the current board size high score
		if (score > oabHS) {
			string highScoreDay = System.DateTime.Now.ToString ("d");
			string highScoreTime = System.DateTime.Now.ToString("hh:mm tt");
			HighScore highScore = new HighScore (score, moves, time, highScoreDay, highScoreTime);
			overallBoardStats.SetHighScore (highScore);
			Text highScoreTxt = GameObject.Find ("HighScoreTxt").GetComponent<Text> ();
			highScoreTxt.text = "New " + boardSize + "x" + boardSize + " High Score!";
		}
        //If the score if higher than the overall high score
		if (score > oaHS) {
			string highScoreDay = System.DateTime.Now.ToString ("d");
			string highScoreTime = System.DateTime.Now.ToString("hh:mm tt");
			HighScore highScore = new HighScore (score, moves, time, highScoreDay, highScoreTime);
			overallStats.SetHighScore (highScore);
			Text highScoreTxt = GameObject.Find ("HighScoreTxt").GetComponent<Text> ();
			highScoreTxt.text = "New Overall High Score!";
		}
        //Update stats
		UpdateStats (overallStats);
		UpdateStats (modeStats);
		UpdateStats (boardStats);
		UpdateStats (overallBoardStats);
        //Save stats
		PlayerPrefs.Save ();
	}
    /// <summary>
    /// Increases the text paramter in integer format.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="value">The value.</param>
    /// <param name="startString">The start string.</param>
    /// <returns></returns>
    IEnumerator TextCountUp(Text text, int value, string startString) {
		int i = 0;
		int speed = value / 100;
		if (speed <= 0) {
			speed = 1;
		}
		Debug.Log (speed);
		while (i <= value - speed) {
			text.text = startString + i.ToString ("0");
			i += speed;
			yield return new WaitForSecondsRealtime (0.01f);
		}
		text.text = startString + value.ToString ("0");
	}
    /// <summary>
    /// Increases the text parameter in time format.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="value">The value.</param>
    /// <param name="startString">The start string.</param>
    /// <returns></returns>
    IEnumerator TimeTextCountUp(Text text, float value, string startString) {
		float i = 0f;
		int mins = 0;
		int sec = 0;
		float speed = value / 100;
		Debug.Log (speed);
		while (i <= value - speed) {
			mins = (int)i / 60;
			sec = (int)i % 60;
			text.text = startString + mins.ToString ("0") + ":" + sec.ToString ("00");
			i += speed;
			yield return new WaitForSecondsRealtime (0.01f);
		}
		mins = (int)value / 60;
		sec = (int)value % 60;
		text.text = startString + mins.ToString ("0") + ":" + sec.ToString ("00");
	}
}
