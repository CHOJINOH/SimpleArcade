using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("�ǽð� UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("�׻� ���̴� High Score")]
    public TextMeshProUGUI highScoreText;

    [Header("��� ȭ�� (�ʱ⿡�� ��Ȱ��)")]
    public GameObject resultPanel;
    public TextMeshProUGUI finalScoreText;
    public Button restartButton;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // ��� �гΰ� ��ư�� ó���� ����
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // ����ŸƮ ��ư Ŭ�� ������
        restartButton.onClick.AddListener(RestartGame);

        // High Score �ʱ�ȭ (ù ������ 0)
        int savedHigh = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"High Score: {savedHigh}";
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = $"Score: {newScore}";
    }

    public void UpdateTimer(float timeLeft)
    {
        timeLeft = Mathf.Max(0, timeLeft);
        int m = Mathf.FloorToInt(timeLeft / 60f);
        int s = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = $"Time: {m:00}:{s:00}";
    }

    /// <summary>
    /// ���� ���� �� ȣ��
    /// </summary>
    public void ShowResult(int finalScore)
    {
        // ���� ������ ǥ��
        finalScoreText.text = $"Your Score: {finalScore}";

        // �� �ְ����̸� ���� �� UI ����
        int savedHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (finalScore > savedHigh)
        {
            savedHigh = finalScore;
            PlayerPrefs.SetInt("HighScore", savedHigh);
            highScoreText.text = $"High Score: {savedHigh}";
        }

        // ��� �г�/��ư ���̱�
        resultPanel.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
