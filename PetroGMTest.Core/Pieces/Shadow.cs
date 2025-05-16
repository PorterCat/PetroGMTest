namespace PetroGMTest.Core.Pieces;

public class Shadow((int x, int y) position) : Piece(position)
{
    public override char Symbol => 'S';
    
    public override IEnumerable<(int dx, int dy)> AttackVectors =>
    [
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),          (0, 1),
        (1, -1),  (1, 0), (1, 1)
    ];
}