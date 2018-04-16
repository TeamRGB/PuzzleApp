using System;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class HighScore {

    /// <summary>
    /// Initializes a new instance of the <see cref="HighScore"/> class.
    /// </summary>
    /// <param name="score">The score.</param>
    /// <param name="moves">The moves.</param>
    /// <param name="gameTime">The game time.</param>
    /// <param name="day">The day.</param>
    /// <param name="time">The time.</param>
    public HighScore (int score, int moves, float gameTime, string day, string time) {
		this.Score = score;
		this.Moves = moves;
		this.GameTime = gameTime;
		this.Day = day;
		this.Time = time;
	}
    /// <summary>
    /// Gets the score.
    /// </summary>
    /// <value>
    /// The score.
    /// </value>
    public int Score { get; private set; }

    /// <summary>
    /// Gets the moves.
    /// </summary>
    /// <value>
    /// The moves.
    /// </value>
    public int Moves { get; private set; }

    /// <summary>
    /// Gets the game time.
    /// </summary>
    /// <value>
    /// The game time.
    /// </value>
    public float GameTime { get; private set; }

    /// <summary>
    /// Gets the day.
    /// </summary>
    /// <value>
    /// The day.
    /// </value>
    public string Day { get; private set; }

    /// <summary>
    /// Gets the time.
    /// </summary>
    /// <value>
    /// The time.
    /// </value>
    public string Time { get; private set; }
}