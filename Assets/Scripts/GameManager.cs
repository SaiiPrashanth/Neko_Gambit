using System.Collections.Generic;
using UnityEngine;

namespace ChessGame
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        // Board settings
        public float tileSize = 1.0f;
        public float tileOffset = 0.5f;

        // What is currently selected
        private int selectionX = -1;
        private int selectionY = -1;

        public List<GameObject> piecePrefabs; 
        public List<Material> whiteMaterials;

        public ChessPiece[,] Pieces;
        private ChessPiece selectedPiece;
        private bool[,] allowedMoves;

        public bool IsWhiteTurn = true;
        
        private List<GameObject> activePieces = new List<GameObject>();
        private Material previousMat;
        public Material selectedMat;

        public int[] enPassantMove = {-1, -1};

        void Start()
        {
            Instance = this;
            SpawnAllPieces();
        }

        void Update()
        {
            UpdateSelection();
            
            // Left click to select or move
            if (Input.GetMouseButtonDown(0))
            {
                if (selectionX >= 0 && selectionY >= 0)
                {
                    if (selectedPiece == null)
                    {
                        SelectPiece(selectionX, selectionY);
                    }
                    else
                    {
                        MovePiece(selectionX, selectionY);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }

        public void SelectPiece(int x, int y)
        {
            if (Pieces[x, y] == null) return;
            if (Pieces[x, y].isWhite != IsWhiteTurn) return;

            allowedMoves = Pieces[x, y].PossibleMoves();
            
            // Check if we can move at all
            bool canMove = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (allowedMoves[i, j]) canMove = true;
                }
            }

            if (!canMove) return;

            selectedPiece = Pieces[x, y];
            previousMat = selectedPiece.GetComponent<MeshRenderer>().material;
            selectedMat.mainTexture = previousMat.mainTexture;
            selectedPiece.GetComponent<MeshRenderer>().material = selectedMat;

            HighlightManager.Instance.HighlightAllowedMoves(allowedMoves);
        }

        public void MovePiece(int x, int y)
        {
            if (allowedMoves[x, y])
            {
                ChessPiece c = Pieces[x, y];

                // If there is an enemy, destroy it
                if (c != null && c.isWhite != IsWhiteTurn)
                {
                    if (c is King)
                    {
                        EndGame();
                        return;
                    }
                    activePieces.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }

                // Check for en passant
                if (x == enPassantMove[0] && y == enPassantMove[1])
                {
                    if (IsWhiteTurn) c = Pieces[x, y - 1];
                    else c = Pieces[x, y + 1];
                    
                    activePieces.Remove(c.gameObject);
                    Destroy(c.gameObject);
                }

                enPassantMove[0] = -1;
                enPassantMove[1] = -1;

                // Pawn specific stuff
                if (selectedPiece is Pawn)
                {
                    if (y == 7 || y == 0)
                    {
                        activePieces.Remove(selectedPiece.gameObject);
                        Destroy(selectedPiece.gameObject);
                        SpawnPiece(1, x, y, IsWhiteTurn); // promote to queen
                        selectedPiece = Pieces[x, y];
                    }

                    if (selectedPiece.currentY == 1 && y == 3) enPassantMove = new int[] { x, 2 };
                    else if (selectedPiece.currentY == 6 && y == 4) enPassantMove = new int[] { x, 5 };
                }

                // Update the grid
                Pieces[selectedPiece.currentX, selectedPiece.currentY] = null;
                selectedPiece.transform.position = GetTileCenter(x, y);
                selectedPiece.SetPosition(x, y);
                Pieces[x, y] = selectedPiece;

                IsWhiteTurn = !IsWhiteTurn;
            }

            // Clean up selection
            if (selectedPiece != null)
                selectedPiece.GetComponent<MeshRenderer>().material = previousMat;

            HighlightManager.Instance.HideHighlights();
            selectedPiece = null;
        }

        private void UpdateSelection()
        {
            if (!Camera.main) return;

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("ChessPlane")))
            {
                selectionX = (int)hit.point.z;
                selectionY = (int)hit.point.x;
            }
            else
            {
                selectionX = -1;
                selectionY = -1;
            }
        }

        public void SpawnPiece(int index, int x, int y, bool white)
        {
            Vector3 pos = GetTileCenter(x, y);
            Quaternion rot = white ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            
            GameObject go = Instantiate(piecePrefabs[index], pos, rot);
            go.transform.SetParent(transform);

            ChessPiece p = go.GetComponent<ChessPiece>();
            Pieces[x, y] = p;
            p.SetPosition(x, y);
            p.isWhite = white;

            if (white && whiteMaterials.Count > index)
                go.GetComponent<MeshRenderer>().material = whiteMaterials[index];

            activePieces.Add(go);
        }

        private Vector3 GetTileCenter(int x, int y)
        {
            return new Vector3(y + tileOffset, 0, x + tileOffset);
        }

        private void SpawnAllPieces()
        {
            Pieces = new ChessPiece[8, 8];

            // Spawn White
            SpawnPiece(0, 3, 7, true); 
            SpawnPiece(1, 4, 7, true); 
            SpawnPiece(2, 0, 7, true); 
            SpawnPiece(2, 7, 7, true);
            SpawnPiece(3, 2, 7, true); 
            SpawnPiece(3, 5, 7, true);
            SpawnPiece(4, 1, 7, true); 
            SpawnPiece(4, 6, 7, true);
            for (int i = 0; i < 8; i++) SpawnPiece(5, i, 6, true);

            // Spawn Black
            SpawnPiece(0, 4, 0, false); 
            SpawnPiece(1, 3, 0, false); 
            SpawnPiece(2, 0, 0, false); 
            SpawnPiece(2, 7, 0, false);
            SpawnPiece(3, 2, 0, false); 
            SpawnPiece(3, 5, 0, false);
            SpawnPiece(4, 1, 0, false); 
            SpawnPiece(4, 6, 0, false);
            for (int i = 0; i < 8; i++) SpawnPiece(5, i, 1, false);
        }

        private void EndGame()
        {
            if (IsWhiteTurn) Debug.Log("White wins!");
            else Debug.Log("Black wins!");

            foreach (GameObject go in activePieces) Destroy(go);
            
            IsWhiteTurn = true;
            HighlightManager.Instance.HideHighlights();
            SpawnAllPieces();
        }
    }
}