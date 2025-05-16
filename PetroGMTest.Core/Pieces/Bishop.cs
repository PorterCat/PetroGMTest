namespace PetroGMTest.Core.Pieces;

public class Bishop((int x, int y) position) : Piece(position)
{
    public override char Symbol => 'B';
    public override int Distance => int.MaxValue;

    public override IEnumerable<(int dx, int dy)> AttackVectors =>
    [
        (-1, -1), (-1, 1), 
        (1, -1), (1, 1)
    ];
}