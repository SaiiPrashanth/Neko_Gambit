using UnityEngine;

namespace ChessGame
{
    public class Rook : ChessPiece
    {
        public override bool[,] PossibleMoves()
        {
            bool[,] r = new bool[8, 8];
            int i;

            // Right
            i = currentX;
            while (true)
            {
                i++;
                if (i >= 8) break;
                if (Move(i, currentY, ref r)) break;
            }

            // Left
            i = currentX;
            while (true)
            {
                i--;
                if (i < 0) break;
                if (Move(i, currentY, ref r)) break;
            }

            // Up
            i = currentY;
            while (true)
            {
                i++;
                if (i >= 8) break;
                if (Move(currentX, i, ref r)) break;
            }

            // Down
            i = currentY;
            while (true)
            {
                i--;
                if (i < 0) break;
                if (Move(currentX, i, ref r)) break;
            }

            return r;
        }
    }
}
