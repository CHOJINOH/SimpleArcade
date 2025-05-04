using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("랜덤 워크 간격 (초)")]
    public float moveInterval = 0.5f;

    private Vector2Int currentPos;

    void Start()
    {
        // 그리드 내 랜덤 시작
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
            // gameTime 으로 멈추려면 Time.timeScale=0 이기 때문에 여기서 멈춥니다
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
