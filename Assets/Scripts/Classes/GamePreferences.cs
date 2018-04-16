using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class loads, holds, and saves all of the user game options.
/// The game options are loaded from PlayerPrefs Unity API using the BuildPreferences method.
/// The board size, colors, and material index are held in an int. The game type is held in a <see cref="GameType" /> object.
/// The user's game options are set using the Unity API PlayerPrefs.
/// After any change to the user's game options, the PlayerPrefs.Save() method must be called to commit the changes.
/// </summary>
public class GamePreferences {
	private GameType gameType;
	private int boardSize;
	private int colors;
	private int material;
	public void BuildPreferences() {
		gameType = new GameType(PlayerPrefs.GetInt ("GameType", 0));
		boardSize = PlayerPrefs.GetInt ("BoardSize", 4);
		colors = PlayerPrefs.GetInt ("ColorCount", 4);
		material = PlayerPrefs.GetInt ("Material", 0);
	}
    /// <summary>
    /// Gets the type of the game.
    /// </summary>
    /// <returns>The game type</returns>
    public GameType GetGameType() {
		return gameType;
	}
    /// <summary>
    /// Gets the size of the board.
    /// </summary>
    /// <returns>The size of the board</returns>
    public int GetBoardSize() {
		return boardSize;
	}
    /// <summary>
    /// Gets the number of colors.
    /// </summary>
    /// <returns>The number of colors</returns>
    public int GetColors() {
		return colors;
	}
    /// <summary>
    /// Gets the material.
    /// </summary>
    /// <returns>The material of the pieces</returns>
    public int GetMaterial() {
		return material;
	}
    /// <summary>
    /// Sets the type of the game.
    /// </summary>
    /// <param name="gameType">Type of the game.</param>
    public void SetGameType(GameType gameType) {
		this.gameType = gameType;
		PlayerPrefs.SetInt ("GameType", gameType.GameTypeIndex);
	}
    /// <summary>
    /// Sets the size of the board.
    /// </summary>
    /// <param name="boardSize">Size of the board.</param>
    public void SetBoardSize(int boardSize) {
		this.boardSize = boardSize;
		PlayerPrefs.SetInt ("BoardSize", boardSize);
	}
    /// <summary>
    /// Sets the colors.
    /// </summary>
    /// <param name="colors">The colors.</param>
    public void SetColors(int colors) {
		this.colors = colors;
		PlayerPrefs.SetInt ("ColorCount", colors);
	}
    /// <summary>
    /// Sets the material.
    /// </summary>
    /// <param name="material">The material.</param>
    public void SetMaterial(int material) {
		this.material = material;
		PlayerPrefs.SetInt ("Material", material);
	}
}