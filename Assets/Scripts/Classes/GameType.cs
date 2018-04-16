/// <summary>
/// This class holds the int index and the string name of the game type.
/// </summary>
public class GameType {
    /// <summary>
    /// Initializes a new instance of the <see cref="GameType"/> class.
    /// </summary>
    /// <param name="gameType">Type of the game.</param>
    public GameType (int gameType) {
		this.GameTypeIndex = gameType;
		switch (gameType) {
		case 0:
			Name = "Arcade";
			break;
		case 1:
			Name = "Pattern";
			break;
		case 2:
			Name = "Column";
			break;
		case 3:
			Name = "Row";
			break;
		}		
	}
    /// <summary>
    /// Gets the type.
    /// </summary>
    /// <value>
    /// The index of the game type.
    /// </value>
    public int GameTypeIndex { get; private set; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; private set; }
}

