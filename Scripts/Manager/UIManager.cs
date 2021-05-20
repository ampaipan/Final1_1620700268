using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private RectTransform startDialog;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI finalScoreText;
        [SerializeField] private Button restartButton;
        [SerializeField] private RectTransform endDialog;
        
        private void Awake()
        {
            Debug.Assert(startButton != null, "startButton cannot be null");
            Debug.Assert(startDialog != null, "startDialog cannot be null");        
            Debug.Assert(scoreText != null, "scoreText cannot null");
            Debug.Assert(finalScoreText != null, "finalScoreText cannot null");
            Debug.Assert(restartButton != null, "restartButton cannot be null");
            Debug.Assert(endDialog != null, "endDialog cannot be null");   
        
            startButton.onClick.AddListener(OnStartButtonClicked);
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        private void Start()
        {
            GameManager.Instance.OnRestarted += RestartUI;
            ScoreManager.Instance.OnScoreUpdated += UpdateScoreUI;
            ShowEndDialog(false);
            ShowScore(false);
            UpdateScoreUI();
        }

        private void OnStartButtonClicked()
        {
            ShowStartDialog(false);
            ShowScore(true);
            GameManager.Instance.StartGame();
        }
    
        private void OnRestartButtonClicked()
        {
            ShowEndDialog(false);
            UpdateScoreUI();
            ShowScore(true);
            GameManager.Instance.StartGame();
        } 
    
        private void UpdateScoreUI()
        {
            scoreText.text = $"Score : {ScoreManager.Instance.GetScore()}";
            finalScoreText.text = $"Player Score : {ScoreManager.Instance.GetScore()}";
        }

        private void RestartUI()
        {
            ShowScore(false);
            ShowEndDialog(true);
        }
    
        public void ShowScore(bool showScore)
        {
            //UpdateScoreUI();
            scoreText.gameObject.SetActive(showScore);
        }

        private void ShowStartDialog(bool showDialog)
        {
            startDialog.gameObject.SetActive(showDialog);
        }

        private void ShowEndDialog(bool showDialog)
        {
            //UpdateScoreUI();
            endDialog.gameObject.SetActive(showDialog);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnRestarted -= RestartUI;
            ScoreManager.Instance.OnScoreUpdated -= UpdateScoreUI;            
        }
    }
}
