using Spectre.Console;

namespace TicTacToeConsole;

public class TicTacToe
{
    // Insert Method start() - Done
    // Insert Property for number of games - Done
    // insert 2 events -> inside program.cs
    // -PlayerSet() : player 1 in A2 es
    // -GameEnd() : return who won and print game table
    // Use 8 if to verify if the player won : when someone win call GameEnd()

    public event EventHandler<PlayerSetEventArgs> PlayerSet;
    public event EventHandler<GameEndEventArgs> GameEnd;

    private readonly int[,] GameTable;
    private int GameCount { get; set; }
    private bool User { get; set; }
    
    // private int Row { get; set; }
    // private int Columns { get; set; }

    public TicTacToe()
    {
        this.User = true; //Initialise at true to identify the first player
        this.GameTable = new int[3, 3];
    }
    
    //Start method
    public void Start()
    {
        GetGameCount(); // this method sets the variable GamesCount

       
        // for for the number of GamesCount
        for (int i = 0; i < GameCount; i++)
        {
            var countCell = 0;
            // while until someone wins 
            while (!CheckWin().Item1)
            {
                AddMove();
                //Check if the grid is full and nobody won
                if (countCell++ == 8)
                    break;
            }
            //Check wich player win
            //TODO: Move the code below inside the event handler
            if (CheckWin().Item2 == 1)
            {
                Console.WriteLine("Player1 Wins");
                OnGameEnd();
            } 
            else if (CheckWin().Item2 == 2)
            {
                Console.WriteLine("Player2 Wins");
                OnGameEnd();
            }
            else
                throw new Exception("Nobody Win");
            //TODO: Unleash the event OnGameEnd() to print the game board and the name of the winning player
        }
    }
    
    //Method to check if the current move make the player win the game
    private Tuple<bool, int> CheckWin()
    {
        // TODO: implement 8 if conditions to check the combinations to win  with static values instead of making it inside a double for 
        // the cells of the array are populated with 0s if empty, we can use this to check che state of the cell

        var value = 0;
        //Check Rows
        if ((GameTable[0, 0] == 1 && GameTable[0, 1] == 1 && GameTable[0, 2] == 1) || (GameTable[0, 0] == 2 && GameTable[0, 1] == 2 && GameTable[0, 2] == 2))
        {
            if (GameTable[0, 0] == 1) value = 1;
            else if (GameTable[0, 0] == 2) value = 2;
            return Tuple.Create(true, value);
        } 
        if ((GameTable[1, 0] == 1 && GameTable[1, 1] == 1 && GameTable[1, 2] == 1) || (GameTable[1, 0] == 2 && GameTable[1, 1] == 2 && GameTable[1, 2] == 2))
        {
            if (GameTable[1, 0] == 1) value = 1;
            else if (GameTable[1, 0] == 2) value = 2;
            return Tuple.Create(true, value);
        } 
        if ((GameTable[2, 0] == 1 && GameTable[2, 1] == 1 && GameTable[2, 2] == 1) || (GameTable[2, 0] == 2 && GameTable[2, 1] == 2 && GameTable[2, 2] == 2))
        {
            if (GameTable[2, 0] == 1) value = 1;
            else if (GameTable[2, 0] == 2) value = 2;
            return Tuple.Create(true, value);
        }        
        
        //Check columns
        if ((GameTable[0, 0] == 1 && GameTable[1, 0] == 1 && GameTable[2, 0] == 1) || (GameTable[0, 0] == 2 && GameTable[1, 0] == 2 && GameTable[2, 0] == 2))
        {
            if (GameTable[0, 0] == 1) value = 1;
            else if (GameTable[0, 0] == 2) value = 2;
            return Tuple.Create(true, value);
        }
        if ((GameTable[0, 1] == 1 && GameTable[1, 1] == 1 && GameTable[2, 1] == 1) || (GameTable[0, 1] == 2 && GameTable[1, 1] == 2 && GameTable[2, 1] == 2))
        {
            if (GameTable[0, 1] == 1) value = 1;
            else if (GameTable[0, 1] == 2) value = 2;
            return Tuple.Create(true, value);
        }
        if ((GameTable[0, 2] == 1 && GameTable[1, 2] == 1 && GameTable[2, 2] == 1) || (GameTable[0, 2] == 2 && GameTable[1, 2] == 2 && GameTable[2, 2] == 2))
        {
            if (GameTable[0, 2] == 1) value = 1;
            else if (GameTable[0, 2] == 2) value = 2;
            return Tuple.Create(true, value);
        }

        //Check Diagonal
        if ((GameTable[0, 0] == 1 && GameTable[1, 1] == 1 && GameTable[2, 2] == 1) || (GameTable[0, 0] == 2 && GameTable[1, 1] == 2 && GameTable[2, 2] == 2))
        {
            if (GameTable[0, 0] == 1) value = 1;
            else if (GameTable[0, 0] == 2) value = 2;
            return Tuple.Create(true, value);
        }
        if ((GameTable[0, 2] == 1 && GameTable[1, 1] == 1 && GameTable[2, 0] == 1) || (GameTable[0, 2] == 2 && GameTable[1, 1] == 2 && GameTable[2, 0] == 2))
        {
            if (GameTable[0, 2] == 1) value = 1;
            else if (GameTable[0, 2] == 2) value = 2;
            return Tuple.Create(true, value);
        }
        return Tuple.Create(false, value);
    }

    //Method to ask the ser the number of games1
    private void GetGameCount()
    {
        GameCount = AnsiConsole.Prompt(
            new TextPrompt<int>("Inserisci il numero di round")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Numero non valido[/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        <= 0 => ValidationResult.Error("[red]Devi giocare almeno un round[/]"),
                        >= 10 => ValidationResult.Error("[red]Puoi giocare massimo 10 round alla volta[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
    }

    //Method to add the move of the player to the board
    private void AddMove()
    {
        if (User)
        {
            InsertValue(1);
            DrawTable();
            //TODO: Unleash the event OnPlayerSet() to print the player move
            User = false;
        }
        else
        {
            InsertValue(2);
            DrawTable();
            //TODO: Unleash the event OnPlayerSet() to print the player move
            User = true;
        }
    }

    private int AskRow()
    {
        int row = AnsiConsole.Prompt(
            new TextPrompt<int>("Inserisci Le cooridnate della cella")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Input non valido[/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        <= -1 => ValidationResult.Error("[red]I valori vanno da 0 a 2[/]"),
                        >= 3 => ValidationResult.Error("[red]I valori vanno da 0 a 2[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
        return row;
    }
    
    private int AskColumn()
    {
        int column = AnsiConsole.Prompt(
            new TextPrompt<int>("Inserisci Le coordinate della colonna")
                .PromptStyle("green")
                .ValidationErrorMessage("[red]Input non valido[/]")
                .Validate(age =>
                {
                    return age switch
                    {
                        <= -1 => ValidationResult.Error("[red]I valori vanno da 0 a 2[/]"),
                        >= 3 => ValidationResult.Error("[red]I valori vanno da 0 a 2[/]"),
                        _ => ValidationResult.Success(),
                    };
                }));
        return column;
    }
    
    //Method to insert the value based on the player that is making the move
    private void InsertValue(int value) // value == 1 || value == 2
    {
        if(value == 1)
            Console.WriteLine("Player1: ");
        else if (value == 2)
            Console.WriteLine("Player2: ");
        else
            throw new Exception("Something gone wrong");
        
        var row = AskRow();
        var column = AskColumn();
        if (CheckCell(row, column))
        {
            OnPlayerSet(User, row, column);
            GameTable[row, column] = value;
            // action.Invoke(User, row, column);
        }
        else
        {
            while (!CheckCell(row, column))
            {
                Console.WriteLine("The selected cell is not available");
                row = AskRow();
                column = AskColumn();
            }
            OnPlayerSet(User, row, column);
            GameTable[row, column] = value;
            
            // action.Invoke(User, row, column);
        }
    }

    //Method to draw the game table
    public void DrawTable()
    {
        
        char[,] tableChar = new char[3,3];
        ConvertTable(tableChar);
        var table = new Table();
        //Add rows and columns
        table.AddColumn("");
        table.AddColumn("C0");
        table.AddColumn("C1");
        table.AddColumn("C2");
        table.AddRow("R0", tableChar[0,0].ToString(), tableChar[0,1].ToString(), tableChar[0,2].ToString());
        table.AddRow("R1", tableChar[1,0].ToString(), tableChar[1,1].ToString(), tableChar[1,2].ToString());
        table.AddRow("R2", tableChar[2,0].ToString(), tableChar[2,1].ToString(), tableChar[2,2].ToString());

        AnsiConsole.Write(table);

    }

    private void ConvertTable(char[,] table)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (GameTable[i,j] == 1)
                    table[i, j] = 'o';
                else if (GameTable[i, j] == 2)
                    table[i, j] = 'x';
            }
        }
    }
    
    //Method to check the cells. Set false if the cell is occupied and true if the cell is free
    private bool CheckCell(int row, int column)
    {
        bool checkValue;
        if (GameTable[row, column] == 1 || GameTable[row, column] == 2)
        {
            checkValue = false;
        }
        else
        {
            checkValue = true;
        }

        return checkValue;
    }
    
    private void OnPlayerSet(bool user, int row, int column)
    {
        // EventHandler<PlayerSetEventArgs> handler = PlayerSet;
        PlayerSet?.Invoke(this, new PlayerSetEventArgs(user, row, column));
    }
    
    private void OnGameEnd()
    {
        // EventHandler<GameEndEventArgs> handler = GameEnd;
        GameEnd?.Invoke(this, new GameEndEventArgs());
    }
}