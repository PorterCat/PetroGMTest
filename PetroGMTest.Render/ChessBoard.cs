using PetroGMTest.Core.Pieces;

public enum CellColor
{
    Default,
    Green,
    Yellow,
    Red,
}

public class Cell
{
    public string Content { get; set; } = string.Empty;
    public CellColor Color { get; set; } = CellColor.Default;
}

public class ChessBoard
{
    private readonly Cell[,] _cells = new Cell[8, 8];
    
    public ChessBoard(IEnumerable<Piece> pieces)
    {
        InitializeBoard();
        PlacePieces(pieces);
    }

    private void InitializeBoard()
    {
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
                _cells[y, x] = new Cell();
    }
    
    private void PlacePieces(IEnumerable<Piece> pieces)
    {
        foreach (var piece in pieces)
        {
            var (x, y) = piece.Position;
            _cells[y, x].Content = piece.FullName;
        }
    }
    
    public void HighlightCell((int x, int y) pos, CellColor color)
    {
        if (pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8)
            _cells[pos.y, pos.x].Color = color;
    }

    public void ClearHighlights()
    {
        for (int y = 0; y < 8; y++)
            for (int x = 0; x < 8; x++)
                _cells[y, x].Color = CellColor.Default;   
    }

    public void RenderWithHighlightedAttacks(Piece attacker, IEnumerable<Piece> underAttack)
    {
        _cells[attacker.Position.y, attacker.Position.x].Color = CellColor.Yellow;
        foreach (var piece in underAttack)
        {
            _cells[piece.Position.y, piece.Position.x].Color = CellColor.Red;
        }
        Render();
        ClearHighlights();
    }
    
    public void Render()
    {
        Console.ResetColor();
        Console.WriteLine("  ╔══════╦══════╦══════╦══════╦══════╦══════╦══════╦══════╗");
        
        for (int y = 7; y >= 0; y--)
        {
            Console.Write($"{y + 1} ");
            for (int x = 0; x < 8; x++)
            {
                var cell = _cells[y, x];
                
                var content = string.IsNullOrWhiteSpace(cell.Content) 
                    ? "    " 
                    : cell.Content;
                
                Console.Write("║");
                SetConsoleColor(cell.Color);
                Console.Write($" {content} ");
                Console.ResetColor();
            }
            Console.WriteLine("║");
            
            if(y != 0)
                Console.Write("  ");
            
            for (int x = 0; x < 8 && y > 0; ++x)
            {
                if (x == 7)
                {
                    Console.Write("══════╣");
                    break;
                }
                Console.Write(x == 0 ? "╠══════╬" : "══════╬");   
            }
            if(y != 0)
                Console.WriteLine();
        }

        Console.WriteLine("  ╚══════╩══════╩══════╩══════╩══════╩══════╩══════╩══════╝");
        Console.WriteLine("   a       b      c      d      e      f      g      h     ");
    }
    
    private void SetConsoleColor(CellColor color)
    {
        switch (color)
        {
            case CellColor.Green:
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                break;
            case CellColor.Red:
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Red;
                break;
            case CellColor.Yellow:
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;   
                break;
            case CellColor.Default: default:
                Console.ResetColor();
                break;
        }
    }
}