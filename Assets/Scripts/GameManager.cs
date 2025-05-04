using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [HideInInspector] public int score = 0;
    [Tooltip("�� �÷��� �ð� (��)")]
    public float gameTime = 90f;

    /// <summary>���� ���� ����</summary>
    public bool IsGameOver { get; private set; } = false;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        UIManager.Instance.UpdateScore(score);
        UIManager.Instance.UpdateTimer(gameTime);
    }

    void Update()
    {
        if (IsGameOver) return;

        gameTime -= Time.deltaTime;
        UIManager.Instance.UpdateTimer(gameTime);

        if (gameTime <= 0f)
        {
            gameTime = 0f;
            EndGame();
        }
    }

    public void AddScore(int amount)
    {
        if (IsGameOver) return;
        score += amount;
        UIManager.Instance.UpdateScore(score);
    }

    private void EndGame()
    {
        IsGameOver = true;
        Time.timeScale = 0f; // ���� ����
        UIManager.Instance.ShowResult(score);
    }
}
