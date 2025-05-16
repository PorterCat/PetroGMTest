using PetroGMTest.Core.Pieces;

namespace PetroGMTest.Console;

public static class ChessParser
{
    public static (List<Piece> Pieces, List<string> Errors) ParsePieces(string filePath)
    {
        List<Piece> pieces = new();
        List<string> errors = new();
        
        if (!File.Exists(filePath))
        {
            errors.Add($"File not found: {filePath}");
            return (pieces, errors);
        }

        string[] lines = File.ReadAllLines(filePath);
        for(int i = 0; i < lines.Length; ++i)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            
            if (pieces.Count > 64)
            {
                errors.Add($"Line {i + 1}: There's no more space on the Board.");
                return (pieces, errors);   
            }
            
            string[] parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3)
            {
                errors.Add($"Line {i + 1}: is not valid: {lines[i]}");
                continue;
            }
            
            string type = parts[0].ToLower();
            if (!int.TryParse(parts[1], out int x) || !int.TryParse(parts[2], out int y))
            {
                errors.Add($"Line {i + 1}: invalid coordinates: {lines[i]}");
                continue;
            }
            
            if (x < 0 || x > 7 || y < 0 || y > 7)
            {
                errors.Add($"Line {i + 1}: Coordinates ({x}, {y}) out of bounds.");
                continue;
            }
            
            if (pieces.Any(p => p.Position.x == x && p.Position.y == y))
            {
                errors.Add($"Line {i + 1}: Position ({x}, {y}) is occupied.");
                continue;
            }
            
            try
            {
                Piece piece = CreatePiece(type, (x, y));
                pieces.Add(piece);
            }
            catch (Exception ex)
            {
                errors.Add($"Line {i + 1}: {ex.Message}");
            }
        }
        
        if (pieces.Count == 0)
            errors.Add($"No pieces on the board.");
        
        return (pieces, errors);
    }
    
    private static Piece CreatePiece(string type, (int x, int y) position) =>
        type.ToLower() switch
        {
            "king" => new King(position),
            "queen" => new Queen(position),
            "rook" => new Rook(position),
            "bishop" => new Bishop(position),
            "knight" => new Knight(position),
            "shadow" => new Shadow(position),
            _ => throw new ArgumentException($"Unknown piece type '{type}'")
        };
}