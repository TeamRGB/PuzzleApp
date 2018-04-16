using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class sets the user's current high score and the day the user got that high score in the leaderboard scene.
/// </summary>
public class GetUserStats : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        GameModeStats stats = new GameModeStats(99, 99);
        stats.BuildStats();
        HighScore highScore = stats.GetHighScore();
        string highScoreInfo = highScore.Day;
        Text highScoreTxt = GameObject.Find("HighScoreText").GetComponent<Text>();
        Text highScoreInfoTxt = GameObject.Find("HighScoreInfoText").GetComponent<Text>();
        highScoreTxt.text = "High Score: " + highScore.Score.ToString();
        highScoreInfoTxt.text = highScoreInfo;
    }

}
