using UnityEngine;

namespace ChessGame
{
    public class Pawn : ChessPiece
    {
        public override bool[,] PossibleMoves()
        {
            bool[,] r = new bool[8, 8];
            int[] e = GameManager.Instance.enPassantMove;

            // Direction depends on color
            int dir = isWhite ? -1 : 1;
            int start = isWhite ? 6 : 1;

            // Straight move
            if (currentY + dir >= 0 && currentY + dir <= 7)
            {
                if (GameManager.Instance.Pieces[currentX, currentY + dir] == null)
                {
                    r[currentX, currentY + dir] = true;
                    // Double move from start
                    if (currentY == start && GameManager.Instance.Pieces[currentX, currentY + (dir * 2)] == null)
                        r[currentX, currentY + (dir * 2)] = true;
                }
            }

            // Captures
            if (currentX > 0 && currentY + dir >= 0 && currentY + dir <= 7)
            {
                // normal capture
                ChessPiece p = GameManager.Instance.Pieces[currentX - 1, currentY + dir];
                if (p != null && p.isWhite != isWhite) r[currentX - 1, currentY + dir] = true;
                // en passant
                if (currentX - 1 == e[0] && currentY + dir == e[1]) r[currentX - 1, currentY + dir] = true;
            }
            if (currentX < 7 && currentY + dir >= 0 && currentY + dir <= 7)
            {
                // normal capture
                ChessPiece p = GameManager.Instance.Pieces[currentX + 1, currentY + dir];
                if (p != null && p.isWhite != isWhite) r[currentX + 1, currentY + dir] = true;
                // en passant
                if (currentX + 1 == e[0] && currentY + dir == e[1]) r[currentX + 1, currentY + dir] = true;
            }

            return r;
        }
    }
}
