using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("실시간 UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("항상 보이는 High Score")]
    public TextMeshProUGUI highScoreText;

    [Header("결과 화면 (초기에는 비활성)")]
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

        // 결과 패널과 버튼은 처음에 숨김
        resultPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // 리스타트 버튼 클릭 리스너
        restartButton.onClick.AddListener(RestartGame);

        // High Score 초기화 (첫 게임은 0)
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
    /// 게임 종료 시 호출
    /// </summary>
    public void ShowResult(int finalScore)
    {
        // 최종 점수만 표시
        finalScoreText.text = $"Your Score: {finalScore}";

        // 새 최고점이면 저장 및 UI 갱신
        int savedHigh = PlayerPrefs.GetInt("HighScore", 0);
        if (finalScore > savedHigh)
        {
            savedHigh = finalScore;
            PlayerPrefs.SetInt("HighScore", savedHigh);
            highScoreText.text = $"High Score: {savedHigh}";
        }

        // 결과 패널/버튼 보이기
        resultPanel.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
