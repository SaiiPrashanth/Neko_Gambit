using UnityEngine;

namespace ChessGame
{
    public class Queen : ChessPiece
    {
        public override bool[,] PossibleMoves()
        {
            bool[,] r = new bool[8, 8];
            int i, j;

            // Straight lines (like Rook)
            i = currentX;
            while (true) { i++; if (i >= 8 || Move(i, currentY, ref r)) break; }
            i = currentX;
            while (true) { i--; if (i < 0 || Move(i, currentY, ref r)) break; }
            i = currentY;
            while (true) { i++; if (i >= 8 || Move(currentX, i, ref r)) break; }
            i = currentY;
            while (true) { i--; if (i < 0 || Move(currentX, i, ref r)) break; }

            // Diagonals (like Bishop)
            i = currentX; j = currentY;
            while (true) { i--; j++; if (i < 0 || j >= 8 || Move(i, j, ref r)) break; }
            i = currentX; j = currentY;
            while (true) { i++; j++; if (i >= 8 || j >= 8 || Move(i, j, ref r)) break; }
            i = currentX; j = currentY;
            while (true) { i--; j--; if (i < 0 || j < 0 || Move(i, j, ref r)) break; }
            i = currentX; j = currentY;
            while (true) { i++; j--; if (i >= 8 || j < 0 || Move(i, j, ref r)) break; }

            return r;
        }
    }
}
