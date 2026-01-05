using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGame
{
    public class AIPlayer : MonoBehaviour
    {
        public float delay = 1.0f;
        public int depth = 2; // how many moves to look ahead

        private bool thinking = false;
        
        void Update()
        {
            // If it's black's turn, the AI moves
            if (!GameManager.Instance.IsWhiteTurn && !thinking)
            {
                thinking = true;
                StartCoroutine(MakeAMove());
            }
        }

        IEnumerator MakeAMove()
        {
            yield return new WaitForSeconds(delay);

            MoveOption best = GetBestMove();

            if (best != null)
            {
                GameManager.Instance.SelectPiece(best.piece.currentX, best.piece.currentY);
                yield return new WaitForSeconds(0.1f);
                GameManager.Instance.MovePiece(best.targetX, best.targetY);
            }

            thinking = false;
        }

        MoveOption GetBestMove()
        {
            List<MoveOption> allMoves = GetMoves(false);
            MoveOption best = null;
            int bestScore = -999999;

            foreach (var move in allMoves)
            {
                ChessPiece captured = Simulate(move);
                int score = -Minimax(depth - 1, -999999, 999999, true);
                Undo(move, captured);

                if (score > bestScore)
                {
                    bestScore = score;
                    best = move;
                }
            }
            return best;
        }

        int Minimax(int d, int alpha, int beta, bool max)
        {
            if (d == 0) return Eval();

            List<MoveOption> moves = GetMoves(max);
            if (moves.Count == 0) return 0;

            if (max)
            {
                int high = -999999;
                foreach (var m in moves)
                {
                    ChessPiece cap = Simulate(m);
                    high = Math.Max(high, Minimax(d - 1, alpha, beta, false));
                    Undo(m, cap);
                    alpha = Math.Max(alpha, high);
                    if (beta <= alpha) break;
                }
                return high;
            }
            else
            {
                int low = 999999;
                foreach (var m in moves)
                {
                    ChessPiece cap = Simulate(m);
                    low = Math.Min(low, Minimax(d - 1, alpha, beta, true));
                    Undo(m, cap);
                    beta = Math.Min(beta, low);
                    if (beta <= alpha) break;
                }
                return low;
            }
        }

        int Eval()
        {
            int s = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ChessPiece p = GameManager.Instance.Pieces[x, y];
                    if (p != null)
                    {
                        int val = 0;
                        if (p is Pawn) val = 10;
                        else if (p is Knight) val = 30;
                        else if (p is Bishop) val = 30;
                        else if (p is Rook) val = 50;
                        else if (p is Queen) val = 90;
                        else if (p is King) val = 900;

                        if (p.isWhite) s -= val;
                        else s += val;
                    }
                }
            }
            return s;
        }

        List<MoveOption> GetMoves(bool white)
        {
            List<MoveOption> list = new List<MoveOption>();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    ChessPiece p = GameManager.Instance.Pieces[x, y];
                    if (p != null && p.isWhite == white)
                    {
                        bool[,] possible = p.PossibleMoves();
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (possible[i, j]) list.Add(new MoveOption(p, i, j));
                            }
                        }
                    }
                }
            }
            return list;
        }

        ChessPiece Simulate(MoveOption m)
        {
            ChessPiece cap = GameManager.Instance.Pieces[m.targetX, m.targetY];
            GameManager.Instance.Pieces[m.piece.currentX, m.piece.currentY] = null;
            GameManager.Instance.Pieces[m.targetX, m.targetY] = m.piece;
            m.piece.SetPosition(m.targetX, m.targetY);
            return cap;
        }

        void Undo(MoveOption m, ChessPiece cap)
        {
            GameManager.Instance.Pieces[m.targetX, m.targetY] = cap;
            GameManager.Instance.Pieces[m.startX, m.startY] = m.piece;
            m.piece.SetPosition(m.startX, m.startY);
        }

        class MoveOption
        {
            public ChessPiece piece;
            public int startX, startY, targetX, targetY;
            public MoveOption(ChessPiece p, int x, int y) {
                piece = p; startX = p.currentX; startY = p.currentY; targetX = x; targetY = y;
            }
        }
    }
}