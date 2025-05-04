using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    [Tooltip("���� ũ�� (X, Y)")]
    public Vector2Int gridSize = new Vector2Int(8, 8);
    [Tooltip("�� �ϳ��� ���� ���� ũ��")]
    public float cellSize = 1f;

    // ���� ��ǥ (0,0) ���� ���� �Ʒ� �𼭸� ��ġ
    private Vector3 origin;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // �� ������Ʈ ��ġ�� ���� �߾����� �ΰ�,
        // ���� �Ʒ� ������ ���
        origin = transform.position
               - new Vector3(gridSize.x * cellSize / 2f,
                             gridSize.y * cellSize / 2f,
                             0f);
    }

    /// <summary>
    /// (0,0) ~ (gridSize.x-1, gridSize.y-1) ��ǥ��
    /// ���� ��ǥ�� ��ȯ (�� �߽�)
    /// </summary>
    public Vector3 GridToWorld(Vector2Int cell)
    {
        return origin
             + new Vector3(cell.x * cellSize + cellSize / 2f,
                           cell.y * cellSize + cellSize / 2f,
                           0f);
    }
}
