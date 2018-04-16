using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// This class holds the position, color, and object reference for each game piece on the game board.
/// The position is stored using an x and a y coordinate.
/// The color is a <see cref="PieceColor"/> object.
/// The object holds the GameObject reference.
/// 
/// <seealso cref="CreateBoard"/>
/// <seealso cref="PieceColor"/>
/// <seealso href="https://docs.unity3d.com/ScriptReference/GameObject.html">GameObject</seealso>
/// </summary>
public class GamePiece
{
    public int x;
    public int y;
    public PieceColor color;
    public GameObject obj;
    /// <summary>
    /// Initializes a new instance of the <see cref="GamePiece"/> class.
    /// </summary>
    public GamePiece()
    {
        this.x = -1;
        this.y = -1;
        this.color = new PieceColor(new Color(0, 0, 0, 0), -1);
        this.obj = new GameObject();
    }
    /// <summary>
    /// Initializes a new instance of the <see cref="GamePiece"/> class.
    /// </summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="color">The color.</param>
    /// <param name="obj">The object.</param>
    public GamePiece(int x, int y, PieceColor color, GameObject obj)
    {
        this.x = x;
        this.y = y;
        this.color = color;
        this.obj = obj;
    }
    /// <summary>
    /// Refreshes the color.
    /// </summary>
    public void RefreshColor()
    {
        obj.GetComponent<Image>().color = color.color;
    }

}