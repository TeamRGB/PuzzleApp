using UnityEngine;

/// <summary>
/// PieceColor
/// </summary>
public class PieceColor : GameColor {
    public Color32 color;
    public int colorIndex;
    public string colorString;

    /// <summary>
    /// Initializes a new instance of the <see cref="PieceColor"/> class.
    /// </summary>
    public PieceColor() {
		this.colorIndex = -1;
	}
    /// <summary>
    /// Initializes a new instance of the <see cref="PieceColor"/> class.
    /// </summary>
    /// <param name="color">The color.</param>
    /// <param name="colorIndex">Index of the color.</param>
    public PieceColor(Color color, int colorIndex) {
		this.color = color;
		this.colorIndex = colorIndex;
		this.colorString = color.r + "," + color.g + "," + color.b + "," + color.a;
	}
    /// <summary>
    /// Initializes a new instance of the <see cref="PieceColor"/> class.
    /// </summary>
    /// <param name="colorString">The color string.</param>
    /// <param name="colorIndex">Index of the color.</param>
    public PieceColor(string colorString, int colorIndex) {
		this.colorIndex = colorIndex;
		this.colorString = colorString;
		this.color = GetColor ();
	}
    /// <summary>
    /// Gets the color.
    /// </summary>
    /// <returns>The Color object of the PieceColor</returns>
    public new Color GetColor() {
		string[] rgba = colorString.Split (",".ToCharArray (), 4);
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
    public new string ColorString
    {
        get
        {
            return colorString;
        }
    }

	public bool Equals(PieceColor a) {
		return colorIndex == a.colorIndex;
	}
}