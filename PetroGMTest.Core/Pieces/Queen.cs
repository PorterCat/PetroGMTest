namespace PetroGMTest.Core.Pieces;

public class Queen((int x, int y) position) : Piece(position)
{
    public override char Symbol { get; } = 'Q';
    
    public override IEnumerable<(int dx, int dy)> AttackVectors =>
    [
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),          (0, 1),
        (1, -1),  (1, 0), (1, 1)
    ];
}