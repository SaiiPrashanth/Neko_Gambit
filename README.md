# Neko Gambit

**Neko Gambit** is a fully functional 3D Chess implementation built in **Unity**.

## Overview

A stylized 3D Chess game set in a mystical desert kingdom. Command an army of **Cat Pharaohs** and feline warriors on a board surrounded by sands and ancient ruins.

## Features

- **Unique Theme**: Replaces standard chess pieces with stylized Cat Pharaohs and Egyptian-themed feline units.
- **Atmospheric Setting**: A visually rich desert environment that brings the board to life.
- **Complete Chess Ruleset**:
  - Standard piece movements.
  - Special moves: **Castling**, **En Passant**, and **Pawn Promotion** (to Queen).
  - Win condition detection (King Capture).
- **Interactive Board**: Click-to-move interface with dynamic highlighting of valid tiles.
- **3D Graphics**: Fully 3D modeled pieces and board environment.

## Controls

- **Mouse**: 
  - Click a piece to select it.
  - Click a highlighted tile to move.
- **Escape**: Quit application.

## Tech Stack

- **Engine**: Unity 3D
- **Language**: C#

## Project Structure

- `Assets/Scripts/GameManager.cs`: The core engine handling turn logic, board state, and piece management.
- `Assets/Scripts/ChessPiece.cs`: Base class for all pieces (Rook, Knight, Bishop, Queen, King, Pawn).
- `Assets/Scripts/HighlightManager.cs`: Visualizes valid moves on the board grid.

## Script Reference

### Core Logic
- **`GameManager.cs`**: The brain of the chess engine. Stores the board state (`ChessPiece[,]`), handles turn swapping, piece selection, move validation, and special rules (En Passant, Promotion).
- **`AIPlayer.cs`**: A Minimax-based AI opponent. Uses Alpha-Beta pruning to calculate the best move by evaluating board state (piece values).
- **`ChessPiece.cs`**: Abstract base class for all pieces. Defines the virtual `PossibleMoves()` method.

### Pieces
- **`King.cs`, `Queen.cs`, `Bishop.cs`, `Knight.cs`, `Rook.cs`, `Pawn.cs`**: Concrete implementations defining the unique movement patterns for each piece type.

### Visuals
- **`HighlightManager.cs`**: Manages the "Allowed Move" highlights on the board tiles.
- **`CameraManager.cs`**: Controls camera positioning (likely for switching views between players or rotating the board).

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
Copyright (c) 2026 ARGUS