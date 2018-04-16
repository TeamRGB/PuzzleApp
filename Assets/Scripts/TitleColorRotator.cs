using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class rotates the colors in the title on the main page.
/// </summary>
public class TitleColorRotator : MonoBehaviour {
	Text cTxt;
	Text oTxt;
	Text lTxt;
	Text o2Txt;
	Text rTxt;

    /// <summary>
    /// Starts this instance.
    /// Gets the text component of each element in the title.
    /// Sets the starting color of each element in the title.
    /// Starts the rotation.
    /// </summary>
    void Start () {

        //Get all text components
		cTxt = GameObject.Find ("CText").GetComponent<Text> ();
		oTxt = GameObject.Find ("OText").GetComponent<Text> ();
		lTxt = GameObject.Find ("LText").GetComponent<Text> ();
		o2Txt = GameObject.Find ("O2Text").GetComponent<Text> ();
		rTxt = GameObject.Find ("RText").GetComponent<Text> ();

        //Set the initial colors
		cTxt.color = new Color (255, 0, 0, 255);
		oTxt.color = new Color (0, 255, 0, 255);
		lTxt.color = new Color (0, 0, 255, 255);
		o2Txt.color = new Color (255, 255, 0, 255);
		rTxt.color = new Color (255, 0, 255, 255);

        //Start the rotation
		StartCoroutine (Rotate ());
	}

    /// <summary>
    /// Rotates this instance.
    /// </summary>
    /// <returns>A one second wait on the thread</returns>
    IEnumerator Rotate() {
        //Advances the colors with a one second wait time.
		while (true) {
			Color32 temp = rTxt.color;
			rTxt.color = o2Txt.color;
			o2Txt.color = lTxt.color;
			lTxt.color = oTxt.color;
			oTxt.color = cTxt.color;
			cTxt.color = temp;
			yield return new WaitForSecondsRealtime (1);
		}
	}
	
}
