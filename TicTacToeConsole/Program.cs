using Spectre.Console;
using TicTacToeConsole;

TicTacToe game = new TicTacToe();
game.Start(); //Invoke the method to manage the game

// game.DrawTable();
game.PlayerSet += GameOnPlayerSet;
game.GameEnd += GameOnGameEnd;

static void GameOnGameEnd(object? sender, GameEndEventArgs e)
{
    Console.WriteLine("Partita finita");
}

static void GameOnPlayerSet(object? sender, PlayerSetEventArgs e)
{
    Console.WriteLine($"Player {e.User} muove in riga: {e.Row}, colonna: {e.Column}");
}