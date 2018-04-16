using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// This class is a helper script which changes the scene on a button click.
/// </summary>
public class LoadSceneOnClick : MonoBehaviour {
    /// <summary>
    /// Loads the scene of the index.
    /// </summary>
    /// <param name="SceneIndex">Index of the scene.</param>
    public void LoadByIndex(int SceneIndex) {
		SceneManager.LoadScene(SceneIndex);
	}
}