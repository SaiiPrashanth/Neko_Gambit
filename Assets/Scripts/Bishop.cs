using UnityEngine;

namespace ChessGame
{
    public class Bishop : ChessPiece
    {
        public override bool[,] PossibleMoves()
        {
            bool[,] r = new bool[8, 8];
            int i, j;

            // Diagonals
            i = currentX; j = currentY;
            while (true) {
                i--; j++;
                if (i < 0 || j >= 8) break;
                if (Move(i, j, ref r)) break;
            }

            i = currentX; j = currentY;
            while (true) {
                i++; j++;
                if (i >= 8 || j >= 8) break;
                if (Move(i, j, ref r)) break;
            }

            i = currentX; j = currentY;
            while (true) {
                i--; j--;
                if (i < 0 || j < 0) break;
                if (Move(i, j, ref r)) break;
            }

            i = currentX; j = currentY;
            while (true) {
                i++; j--;
                if (i >= 8 || j < 0) break;
                if (Move(i, j, ref r)) break;
            }

            return r;
        }
    }
}
