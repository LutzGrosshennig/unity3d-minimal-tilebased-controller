using UnityEngine;

public class MinimalisticTilebasedController : MonoBehaviour
{
    [Header("Start location")]  
    public Vector2Int StartTile = new(0, 0);
    public Directions ViewDirection = Directions.North;

    [Header("Level settings")]
    public TileArray Level = new TileArray(16, 16, 3);

    private Vector2Int _currentTile = new(0, 0);

    void Start()
    {
        _currentTile = StartTile;
        SetPositionAndRotation();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ViewDirection = ViewDirection.RotateLeft();
            SetPositionAndRotation();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ViewDirection = ViewDirection.RotateRight();
            SetPositionAndRotation();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            TryWalk(_currentTile + ViewDirection.ToVector2Int());
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            TryWalk(_currentTile - ViewDirection.ToVector2Int());
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TryWalk(_currentTile + ViewDirection.RotateRight().ToVector2Int());
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TryWalk(_currentTile + ViewDirection.RotateLeft().ToVector2Int());
        }
    }

    void TryWalk(Vector2Int newPosition)
    {
        if (IsWalkable(newPosition))
        {
            _currentTile = newPosition;
            SetPositionAndRotation();
        }
    }

    bool IsWalkable(Vector2Int targetTile)
    {
        bool isWalkable = Level.GetElement(targetTile);
        return isWalkable;
    }

    void SetPositionAndRotation()
    {
        float cameraHeight = transform.position.y;
        Vector3 newPosition = new Vector3(_currentTile.x * Level.tileSize, cameraHeight, _currentTile.y * Level.tileSize);
        transform.SetPositionAndRotation(newPosition, Quaternion.Euler(0, (int)ViewDirection, 0));
    }
}
