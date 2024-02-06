namespace TicTacToeConsole;

public class PlayerSetEventArgs : EventArgs
{
    public bool User { get; private set; }
    public int Row { get; private set; }
    public int Column { get; private set; }

    public PlayerSetEventArgs(bool user, int row, int columns)
    {
        User = user;
        Row = row;
        Column = columns;
    }
}