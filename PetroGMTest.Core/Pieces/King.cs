namespace PetroGMTest.Core.Pieces;

public class King((int x, int y) position) : Piece(position)
{
    public override char Symbol { get; } = 'K';
    public override int Distance => 1;
    public override IEnumerable<(int dx, int dy)> AttackVectors =>
    [
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),          (0, 1),
        (1, -1),  (1, 0), (1, 1)
    ];
}