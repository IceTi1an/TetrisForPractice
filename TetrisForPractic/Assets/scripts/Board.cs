using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap {  get; private set; }

    public Piece acitvePiece { get; private set; }

    public TetrominoData[] tetrominos;

    public Vector3Int spawnPositon;

    public Vector2Int boardSize = new Vector2Int(10,20);

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(this.boardSize.x / 2, this.boardSize.y / 2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void Awake()
    {
        this.tilemap = GetComponentInChildren<Tilemap>();
        this.acitvePiece = GetComponentInChildren<Piece>();
        for(int i = 0;i<this.tetrominos.Length;i++)
        {
            this.tetrominos[i].Initialize();
        }
    }

    private void Start()
    {
        SpawnPiece();
    }

    private void SpawnPiece()
    {
        int random = Random.Range(0, this.tetrominos.Length);
        TetrominoData data = this.tetrominos[random];

        this.acitvePiece.Initialize(this, this.spawnPositon, data);
        Set(this.acitvePiece);
    }

    public void Set(Piece piece)
    {
        for(int i = 0;i<piece.cells.Length;i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;

        for(int i = 0;i<piece.cells.Length;i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;

            if (!bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }

            if (this.tilemap.HasTile(tilePosition))
            {
                return false;
            }
        }

        return true;
    }

}
