using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Tooltip("격자 크기 (X, Y)")]
    public Vector2Int gridSize = new Vector2Int(8, 8);
    [Tooltip("셀 하나의 월드 단위 크기")]
    public float cellSize = 1f;

    // 월드 좌표 (0,0) 셀의 왼쪽 아래 모서리 위치
    private Vector3 origin;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 이 오브젝트 위치를 격자 중앙으로 두고,
        // 왼쪽 아래 원점을 계산
        origin = transform.position
               - new Vector3(gridSize.x * cellSize / 2f,
                             gridSize.y * cellSize / 2f,
                             0f);
    }

    /// <summary>
    /// (0,0) ~ (gridSize.x-1, gridSize.y-1) 좌표를
    /// 월드 좌표로 변환 (셀 중심)
    /// </summary>
    public Vector3 GridToWorld(Vector2Int cell)
    {
        return origin
             + new Vector3(cell.x * cellSize + cellSize / 2f,
                           cell.y * cellSize + cellSize / 2f,
                           0f);
    }
}
