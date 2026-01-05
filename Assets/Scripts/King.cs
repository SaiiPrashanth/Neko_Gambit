using UnityEngine;

namespace ChessGame
{
    public class King : ChessPiece
    {
        public override bool[,] PossibleMoves()
        {
            bool[,] r = new bool[8, 8];

            // Moves 1 square in any direction
            Move(currentX + 1, currentY, ref r);
            Move(currentX - 1, currentY, ref r);
            Move(currentX, currentY - 1, ref r);
            Move(currentX, currentY + 1, ref r);
            Move(currentX + 1, currentY - 1, ref r);
            Move(currentX - 1, currentY - 1, ref r);
            Move(currentX + 1, currentY + 1, ref r);
            Move(currentX - 1, currentY + 1, ref r);

            return r;
        }
    }
}
