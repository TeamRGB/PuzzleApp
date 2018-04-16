using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class loads, holds, and saves all user set customization information.
/// The customization options are loaded from PlayerPrefs Unity API using the BuildCustom method.
/// The material is held in an int which represents the index of the material in the game board. The background color is held in a <see cref="GameColor" /> object.
/// The Colors of the pieces are held in a List of <see cref="PieceColor" /> objects.
/// The user's customization options are set using the Unity API PlayerPrefs.
/// After any change to the user's customization choices, the PlayerPrefs.Save() method must be called to commit the changes.
/// <seealso cref="CustomizationMenu"/>
/// <seealso cref="PieceColor"/>
/// <seealso cref="GameColor"/>
/// <seealso href="https://docs.unity3d.com/ScriptReference/PlayerPrefs.html">PlayerPrefs</seealso>
/// </summary>
public class Customization
{
    private int material;
    private GameColor bgColor;
    private List<PieceColor> pieceColors;

    /// <summary>
    /// Initializes a new instance of the <see cref="Customization"/> class.
    /// </summary>
    public Customization()
    {
        pieceColors = new List<PieceColor>();
    }

    /// <summary>
    /// Builds the user customization options.
    /// </summary>
    public void BuildCustom()
    {
        //Default color to use
        string defaultString = "255,255,255,255";
        //Get material from PlayerPrefs
        material = PlayerPrefs.GetInt("Material", 0);
        //Get background color from PlayerPrefs
        bgColor = new GameColor(PlayerPrefs.GetString("BackgroundColor", defaultString));
        //Process Piece colors
        for (int i = 1; i <= 8; i++)
        {
            string pieceNum = "Piece" + i;
            string pieceColor = PlayerPrefs.GetString(pieceNum, defaultString);
            if (!pieceColor.Equals(defaultString))
            {
                pieceColors.Add(new PieceColor(pieceColor, i));
            }
        }
    }

    /// <summary>
    /// Gets the material.
    /// </summary>
    /// <returns>The int index of the material</returns>
    public int GetMaterial()
    {
        return material;
    }

    /// <summary>
    /// Gets the color of the bg.
    /// </summary>
    /// <returns>The background color</returns>
    public GameColor GetBGColor()
    {
        return bgColor;
    }

    /// <summary>
    /// Gets the piece colors.
    /// </summary>
    /// <returns>The list of piece colors</returns>
    public List<PieceColor> GetPieceColors()
    {
        return pieceColors;
    }

    /// <summary>
    /// Sets the material.
    /// </summary>
    /// <param name="material">The material.</param>
    public void SetMaterial(int material)
    {
        this.material = material;
        PlayerPrefs.SetInt("Material", material);
    }

    /// <summary>
    /// Sets the color of the bg.
    /// </summary>
    /// <param name="bgColor">Color of the bg.</param>
    public void SetBGColor(GameColor bgColor)
    {
        this.bgColor = bgColor;
        PlayerPrefs.SetString("BackgroundColor", bgColor.ColorString);
    }

    /// <summary>
    /// Sets the piece colors.
    /// </summary>
    /// <param name="pieceColors">The piece colors.</param>
    public void SetPieceColors(List<PieceColor> pieceColors)
    {
        this.pieceColors = pieceColors;
        for (int i = 0; i < 8; i++)
        {
            int num = i + 1;
            string pieceNum = "Piece" + num;
            PlayerPrefs.SetString(pieceNum, pieceColors[i].colorString);
        }
    }

    /// <summary>
    /// Saves the pieces.
    /// </summary>
    public void SavePieces()
    {
        for (int i = 0; i < 8; i++)
        {
            int num = i + 1;
            string pieceNum = "Piece" + num;
            PlayerPrefs.SetString(pieceNum, pieceColors[i].colorString);
        }
    }
}