namespace PetroGMTest.Core.Pieces;

public class Rook((int x, int y) position) : Piece(position)
{
    public override char Symbol => 'R';
    public override int Distance => int.MaxValue;
    
    public override IEnumerable<(int dx, int dy)> AttackVectors =>
    [
        (-1, 0), (1, 0), (0, -1), (0, 1)
    ];
}