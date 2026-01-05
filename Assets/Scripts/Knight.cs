using UnityEngine;

namespace ChessGame
{
    public class Knight : ChessPiece
    {
        public override bool[,] PossibleMoves()
        {
            bool[,] r = new bool[8, 8];

            // The L shape moves
            Move(currentX - 1, currentY + 2, ref r);
            Move(currentX + 1, currentY + 2, ref r);
            Move(currentX - 1, currentY - 2, ref r);
            Move(currentX + 1, currentY - 2, ref r);
            Move(currentX - 2, currentY - 1, ref r);
            Move(currentX + 2, currentY - 1, ref r);
            Move(currentX - 2, currentY + 1, ref r);
            Move(currentX + 2, currentY + 1, ref r);

            return r;
        }
    }
}
