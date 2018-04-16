using UnityEngine;
/// <summary>
/// This class loads, holds, and saves all of the stats for a particular game mode and board size.
/// The stats are loaded from PlayerPrefs Unity API using the BuildStats method.
/// The HighScore for the mode and board size is stored in a <see cref="HighScore"/> object. The rest of the stats are held in an int or float.
/// After any change to the user's stats, the PlayerPrefs.Save() method must be called to commit the changes.
/// 
/// <seealso cref="GetUserStats"/>
/// <seealso cref="CreateBoard"/>
/// <seealso cref="HighScore"/>
/// </summary>
public class GameModeStats {

	private HighScore highScore;
	private int gamesPlayed;
	private int gameMode;
	private int boardSize;
	private int gamesWon;
	private int lowestMoves;
	private int highestMoves;
	private int avgMoves;
	private float lowestTime;
	private float highestTime;
	private float avgTime;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameModeStats"/> class.
    /// </summary>
    /// <param name="gameMode">The game mode.</param>
    /// <param name="boardSize">Size of the board.</param>
    public GameModeStats(int gameMode, int boardSize) {
		this.gameMode = gameMode;
		this.boardSize = boardSize;
	}

    /// <summary>
    /// Builds the stats using Unity APi PlayerPrefs.
    /// </summary>
    public void BuildStats() {		
		int score = PlayerPrefs.GetInt (gameMode + ":" + boardSize + ":HighScore", 0);
		int moves = PlayerPrefs.GetInt (gameMode + ":" + boardSize + ":HighScoreMoves", 0);
		string scoreDay = PlayerPrefs.GetString (gameMode + ":" + boardSize + ":HighScoreDay", "");
		string scoreTime = PlayerPrefs.GetString (gameMode + ":" + boardSize + ":HighScoreTime", ""); 
		float gameTime = PlayerPrefs.GetFloat (gameMode + ":" + boardSize + ":HighScoreGameTime", 0f);
		highScore = new HighScore (score, moves, gameTime, scoreDay, scoreTime);
		gamesPlayed = PlayerPrefs.GetInt (gameMode + ":" + boardSize + ":GamesPlayed", 0);
		gamesWon = PlayerPrefs.GetInt (gameMode + ":" + boardSize + ":GamesWon", 0);
		lowestMoves = PlayerPrefs.GetInt (gameMode + ":" + boardSize + ":LowestMoves", 0);
		highestMoves = PlayerPrefs.GetInt (gameMode + ":" + boardSize + ":HighestMoves", 0);
		avgMoves = PlayerPrefs.GetInt (gameMode + ":" + boardSize + ":AverageMoves", 0);
		lowestTime = PlayerPrefs.GetFloat (gameMode + ":" + boardSize + ":LowestTime", 0f);
		highestTime = PlayerPrefs.GetFloat (gameMode + ":" + boardSize + ":HighestTime", 0f);
		avgTime = PlayerPrefs.GetFloat (gameMode + ":" + boardSize + ":AverageTime", 0f);
	}
    /// <summary>
    /// Gets the high score.
    /// </summary>
    /// <returns>The HighScore object for the current high score</returns>
    public HighScore GetHighScore() {
		return highScore;
	}
    /// <summary>
    /// Gets the game mode.
    /// </summary>
    /// <returns>An int index of the game mode</returns>
    public int GetGameMode() {
		return gameMode;
	}
    /// <summary>
    /// Gets the games played.
    /// </summary>
    /// <returns>The number of games played</returns>
    public int GetGamesPlayed() {
		return gamesPlayed;
	}
    /// <summary>
    /// Gets the games won.
    /// </summary>
    /// <returns>The number of games won</returns>
    public int GetGamesWon() {
		return gamesWon;
	}
    /// <summary>
    /// Gets the lowest moves.
    /// </summary>
    /// <returns>The number of lowest moves</returns>
    public int GetLowestMoves() {
		return lowestMoves;
	}
    /// <summary>
    /// Gets the highest moves.
    /// </summary>
    /// <returns>The number of highest moves</returns>
    public int GetHighestMoves() {
		return highestMoves;
	}
    /// <summary>
    /// Gets the average moves.
    /// </summary>
    /// <returns>The average number of moves</returns>
    public int GetAvgMoves() {
		return avgMoves;
	}
    /// <summary>
    /// Gets the lowest time.
    /// </summary>
    /// <returns>The lowest time</returns>
    public float GetLowestTime() {
		return lowestTime;
	}
    /// <summary>
    /// Gets the highest time.
    /// </summary>
    /// <returns>The highest time</returns>
    public float GetHighestTime() {
		return highestTime;
	}
    /// <summary>
    /// Gets the average time.
    /// </summary>
    /// <returns>The average time</returns>
    public float GetAvgTime() {
		return avgTime;
	}
    /// <summary>
    /// Sets the high score.
    /// </summary>
    /// <param name="highScore">The high score.</param>
    public void SetHighScore(HighScore highScore) {
		this.highScore = highScore;
		PlayerPrefs.SetInt (gameMode + ":" + boardSize + ":HighScore", highScore.Score);
		PlayerPrefs.SetInt (gameMode + ":" + boardSize + ":HighScoreMoves", highScore.Moves);
		PlayerPrefs.SetFloat (gameMode + ":" + boardSize + ":HighScoreGameTime", highScore.GameTime);
		PlayerPrefs.SetString (gameMode + ":" + boardSize + ":HighScoreDay", highScore.Day);
		PlayerPrefs.SetString (gameMode + ":" + boardSize + ":HighScoreTime", highScore.Time);
	}
    /// <summary>
    /// Sets the games played.
    /// </summary>
    /// <param name="gamesPlayed">The games played.</param>
    public void SetGamesPlayed(int gamesPlayed) {
		this.gamesPlayed = gamesPlayed;
		PlayerPrefs.SetInt (gameMode + ":" + boardSize + ":GamesPlayed", gamesPlayed);
	}
    /// <summary>
    /// Sets the games won.
    /// </summary>
    /// <param name="gamesWon">The games won.</param>
    public void SetGamesWon(int gamesWon) {
		this.gamesWon = gamesWon;
		PlayerPrefs.SetInt (gameMode + ":" + boardSize + ":GamesWon", gamesWon);
	}
    /// <summary>
    /// Sets the lowest moves.
    /// </summary>
    /// <param name="lowestMoves">The lowest moves.</param>
    public void SetLowestMoves(int lowestMoves) {
		this.lowestMoves = lowestMoves;
		PlayerPrefs.SetInt (gameMode + ":" + boardSize + ":LowestMoves", lowestMoves);
	}
    /// <summary>
    /// Sets the highest moves.
    /// </summary>
    /// <param name="highestMoves">The highest moves.</param>
    public void SetHighestMoves(int highestMoves) {
		this.highestMoves = highestMoves;
		PlayerPrefs.SetInt (gameMode + ":" + boardSize + ":HighestMoves", highestMoves);
	}
    /// <summary>
    /// Sets the average moves.
    /// </summary>
    /// <param name="avgMoves">The average moves.</param>
    public void SetAvgMoves(int avgMoves) {
		this.avgMoves = avgMoves;
		PlayerPrefs.SetInt (gameMode + ":" + boardSize + ":AverageMoves", avgMoves);
	}
    /// <summary>
    /// Sets the lowest time.
    /// </summary>
    /// <param name="lowestTime">The lowest time.</param>
    public void SetLowestTime(float lowestTime) {
		this.lowestTime = lowestTime;
		PlayerPrefs.SetFloat (gameMode + ":" + boardSize + ":LowestTime", lowestTime);
	}
    /// <summary>
    /// Sets the highest time.
    /// </summary>
    /// <param name="highestTime">The highest time.</param>
    public void SetHighestTime(float highestTime) {
		this.highestTime = highestTime;
		PlayerPrefs.SetFloat (gameMode + ":" + boardSize + ":HighestTime", highestTime);
	}
    /// <summary>
    /// Sets the average time.
    /// </summary>
    /// <param name="avgTime">The average time.</param>
    public void SetAvgTime(float avgTime) {
		this.avgTime = avgTime;
		PlayerPrefs.SetFloat (gameMode + ":" + boardSize + ":AverageTime", avgTime);
	}
}