using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Vector2Int cursorPos;

    void Start()
    {
        cursorPos = Vector2Int.zero;
        transform.position = GridManager.Instance.GridToWorld(cursorPos);
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;

        // ����Ű �̵�
        if (Input.GetKeyDown(KeyCode.LeftArrow)) cursorPos += Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) cursorPos += Vector2Int.right;
        else if (Input.GetKeyDown(KeyCode.UpArrow)) cursorPos += Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) cursorPos += Vector2Int.down;

        // �׸��� ���� Ŭ����
        cursorPos.x = Mathf.Clamp(cursorPos.x, 0, GridManager.Instance.gridSize.x - 1);
        cursorPos.y = Mathf.Clamp(cursorPos.y, 0, GridManager.Instance.gridSize.y - 1);

        transform.position = GridManager.Instance.GridToWorld(cursorPos);

        // �����̽� �� ������ ��ġ (�ִ� 3��)
        if (Input.GetKeyDown(KeyCode.Space) && ItemManager.Instance.CanSpawn())
        {
            ItemManager.Instance.SpawnItem(cursorPos);
        }
    }
}
