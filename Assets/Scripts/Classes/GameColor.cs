using UnityEngine;
/// <summary>
/// This class holds the colors used in the game. 
/// The colors are stored in a string using PlayerPrefs and this class coverts the string to a Color object. 
/// The string is stored in the r,g,b,a format.
/// The string is then split, coverted to float, and used to create a Color object using the Color(float, float, float, float) constructor.
/// 
/// <seealso cref="GamePiece"/>
/// <seealso cref="Customization"/>
/// <seealso href="https://docs.unity3d.com/ScriptReference/Color.html">Color</seealso>
/// <seealso href="https://docs.unity3d.com/ScriptReference/PlayerPrefs.html">PlayerPrefs</seealso>
/// </summary>
public class GameColor	{

    /// <summary>
    /// Initializes a new instance of the <see cref="GameColor"/> class and sets the color string to null.
    /// </summary>
    public GameColor ()	{
		ColorString = null;
	}
    /// <summary>
    /// Initializes a new instance of the <see cref="GameColor"/> class.
    /// </summary>
    /// <param name="colorString">The r,g,b,a color string.</param>
    public GameColor(string colorString) {
		this.ColorString = colorString;
	}
    /// <summary>
    /// Gets the color.
    /// </summary>
    /// <returns>A Color object of the color</returns>
    public Color GetColor() {
		string[] rgba = ColorString.Split (",".ToCharArray (), 4);
		float r = 0f;
		float g = 0f;
		float b = 0f;
		float a = 0f;
		float.TryParse (rgba [0], out r);
		float.TryParse (rgba [1], out g);
		float.TryParse (rgba [2], out b);
		float.TryParse (rgba [3], out a);
		return new Color (r, g, b, a);
	}
    /// <summary>
    /// Gets the color string.
    /// </summary>
    /// <value>
    /// The color string.
    /// </value>
    public string ColorString { get; private set; }
}