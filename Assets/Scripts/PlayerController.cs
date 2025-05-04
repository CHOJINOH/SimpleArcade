using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("���� ��ũ ���� (��)")]
    public float moveInterval = 0.5f;

    private Vector2Int currentPos;

    void Start()
    {
        // �׸��� �� ���� ����
        currentPos = new Vector2Int(
            Random.Range(0, GridManager.Instance.gridSize.x),
            Random.Range(0, GridManager.Instance.gridSize.y)
        );
        transform.position = GridManager.Instance.GridToWorld(currentPos);
        StartCoroutine(RandomWalkRoutine());
    }

    IEnumerator RandomWalkRoutine()
    {
        while (true)
        {
            // gameTime ���� ���߷��� Time.timeScale=0 �̱� ������ ���⼭ ����ϴ�
            yield return new WaitForSeconds(moveInterval);
            if (GameManager.Instance.IsGameOver) yield break;
            RandomWalk();
        }
    }

    void RandomWalk()
    {
        var dirs = new[]
        {
            Vector2Int.up, Vector2Int.down,
            Vector2Int.left, Vector2Int.right
        };
        Vector2Int d = dirs[Random.Range(0, dirs.Length)];
        Vector2Int next = currentPos + d;

        if (next.x >= 0 && next.x < GridManager.Instance.gridSize.x &&
            next.y >= 0 && next.y < GridManager.Instance.gridSize.y)
        {
            currentPos = next;
            transform.position = GridManager.Instance.GridToWorld(currentPos);
            ItemManager.Instance.CheckPickup(currentPos);
        }
    }
}
