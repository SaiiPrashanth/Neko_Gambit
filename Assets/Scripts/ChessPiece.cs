using UnityEngine;

namespace ChessGame
{
    // Basic class for all chess pieces
    public abstract class ChessPiece : MonoBehaviour
    {
        public int currentX;
        public int currentY;
        public bool isWhite;

        // Set the piece position on the grid
        public void SetPosition(int x, int y)
        {
            currentX = x;
            currentY = y;
        }

        // Overridden by each piece to show where it can go
        public virtual bool[,] PossibleMoves()
        {
            return new bool[8, 8];
        }

        // Helper to check if a move is valid
        public bool Move(int x, int y, ref bool[,] r)
        {
            if (x >= 0 && x < 8 && y >= 0 && y < 8)
            {
                ChessPiece c = GameManager.Instance.Pieces[x, y];
                
                if (c == null)
                {
                    r[x, y] = true;
                }
                else
                {
                    if (isWhite != c.isWhite)
                    {
                        r[x, y] = true;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
