using UnityEngine;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    public Button leftButton;
    public Button rightButton;
    public Button mainMenuButton;

    public Board board { get; private set; }
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public Vector3Int position { get; private set; }

    public void Initialize(Board board, Vector3Int position, TetrominoData data)
    {
        this.board = board;
        this.position = position;
        this.data = data;

        if (this.cells == null)
        {
            this.cells = new Vector3Int[data.cells.Length];
        }
        for (int i = 0; i < data.cells.Length; i++)
        {
            this.cells[i] = (Vector3Int)data.cells[i];
        }
    }

    private void Start()
    {
        leftButton.onClick.AddListener(() => Move(Vector2Int.left));
        rightButton.onClick.AddListener(() => Move(Vector2Int.right));
    }

    private void Update()
    {
        this.board.Clear(this);
        this.board.Set(this);
    }

    private bool Move(Vector2Int translate)
    {
        Vector3Int newPosition = this.position;
        newPosition.x += translate.x;
        newPosition.y += translate.y;


        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid)
        {
            this.position = newPosition;
        }

        return valid;
    }
}
