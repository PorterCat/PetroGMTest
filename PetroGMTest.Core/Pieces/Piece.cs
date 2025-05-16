namespace PetroGMTest.Core.Pieces;

public abstract class Piece
{
    public (int x, int y) Position { get; }
    public string FullName => $"{Symbol}-{GetLetter(Position.x)}{Position.y + 1}";
    
    protected Piece((int x, int y) pos)
    {
        Position = pos;
    }
    public abstract IEnumerable<(int dx, int dy)> AttackVectors { get; }
    public virtual bool CanJump => false;
    public virtual int Distance => int.MaxValue;
    public virtual char Symbol { get; } = '?';

    public static (int dx, int dy) RelyPieces(Piece piece1, Piece piece2)
        => (Math.Abs(piece1.Position.x - piece2.Position.x), Math.Abs(piece1.Position.y - piece2.Position.y));
    
    public static char GetLetter(int x)
    {
        return x switch
        {
            0 => 'a',
            1 => 'b',
            2 => 'c',
            3 => 'd',
            4 => 'e',
            5 => 'f',
            6 => 'g',
            7 => 'h',
            _ => '?'
        };
    }
}