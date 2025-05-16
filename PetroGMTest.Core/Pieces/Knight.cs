namespace PetroGMTest.Core.Pieces;

public class Knight((int x, int y) position) : Piece(position)
{
    public override char Symbol => 'N';
    public override int Distance => 1;   
    public override bool CanJump => true;
    
    public override IEnumerable<(int dx, int dy)> AttackVectors =>
    [
        (-2, -1), (-2, 1), 
        (-1, -2), (-1, 2), 
        (1, -2), (1, 2), 
        (2, -1), (2, 1)
    ];
}