using PetroGMTest.Console;
using PetroGMTest.Core.Pieces;

internal static class Program
{
    public static void Main(string[] args)
    {
        bool detailedOutput = false;
        
        if (args.Length < 1 || args.Length > 2)
        {
            Console.WriteLine("Usage: PetroGMTest.Console.exe <input-file> [-detailed]");
            return;
        }

        if(args.Length == 2)
            detailedOutput = args[1] == "-d" || args[1] == "--detailed";

        var (pieces, errors) = ChessParser.ParsePieces(args[0]);
        if (errors.Count > 0)
        {
            Console.WriteLine($"Errors: [{errors.Count}]");
            foreach (string error in errors) WriteError($"- {error}");
        }
        
        var board = new ChessBoard(pieces);
        board.Render();
        
        var result = CheckAttacks(pieces);
        foreach (var (attacker, attacked) in result)
        {
            if(attacked.Count == 0)
                continue;
            
            Console.WriteLine($"{attacker.FullName} attacks:");
            foreach (var attackedPiece in attacked)
            {
                Console.WriteLine($" {attackedPiece.FullName}");   
            }

            if (detailedOutput)
                board.RenderWithHighlightedAttacks(attacker, attacked);
            Console.WriteLine();
        }
    }

    public static Dictionary<Piece, List<Piece>> CheckAttacks(List<Piece> pieces)
    {
        var attackMap = new Dictionary<Piece, List<Piece>>();
        var positionMap = pieces.ToDictionary(p => (p.Position.x, p.Position.y));
        
        foreach (var attacker in pieces)
        {
            var attacked = new List<Piece>();
            
            foreach (var vector in attacker.AttackVectors)
            {
                int steps = 0;
                var (x, y) = attacker.Position;
                
                while (steps < attacker.Distance)
                {
                    x += vector.dx;
                    y += vector.dy;
                    
                    if (x < 0 || x >= 8 || y < 0 || y >= 8) break;
                    
                    if (positionMap.TryGetValue((x, y), out var target))
                    {
                        attacked.Add(target);
                        if (!attacker.CanJump) break;
                    }

                    steps++;
                }
            }
            
            attackMap[attacker] = attacked;
        }
        
        return attackMap;
    }
    
    public static void WriteError(string err)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(err);
        Console.ResetColor();
    }
}