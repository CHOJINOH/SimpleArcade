using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance;

    [Tooltip("1��1 ũ���� ������ ������")]
    public GameObject itemPrefab;

    [Tooltip("�ִ� ��ġ ������ ������ ����")]
    public int maxItems = 3;

    // ���� ���� ��� ������ ���
    private readonly List<GameObject> items = new List<GameObject>();
    private readonly List<Vector2Int> itemPositions = new List<Vector2Int>();

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    /// <summary>���� ��ġ�� ������ ��</summary>
    public int CurrentItemCount => items.Count;

    /// <summary>�� ������ ��ġ ���� ����</summary>
    public bool CanSpawn() => items.Count < maxItems;

    public void SpawnItem(Vector2Int pos)
    {
        if (!CanSpawn() || GameManager.Instance.IsGameOver) return;

        Vector3 worldPos = GridManager.Instance.GridToWorld(pos);
        var go = Instantiate(itemPrefab, worldPos, Quaternion.identity);
        items.Add(go);
        itemPositions.Add(pos);

        Debug.Log($"[ItemManager] Spawned at {pos}. Total = {items.Count}");
    }

    public void CheckPickup(Vector2Int playerPos)
    {
        if (items.Count == 0) return;

        for (int i = 0; i < itemPositions.Count; i++)
        {
            if (playerPos == itemPositions[i])
            {
                Destroy(items[i]);
                items.RemoveAt(i);
                itemPositions.RemoveAt(i);
                GameManager.Instance.AddScore(100);
                break;
            }
        }
    }
}
