using UnityEngine;
using UnityEngine.UI;

namespace ScrollShooter
{
    public class GamePanel : UIPanel
    {
        [SerializeField] private Text _coins;
        [SerializeField] private Text _level;
        [SerializeField] private Slider _levelBar;
        
        [Header("PAUSE")]
        [SerializeField] private RectTransform _pauseWindow;
        [SerializeField] private Button _pause;
        [SerializeField] private Button _unPause;
        [SerializeField] private Button _menu;

        private GameManager _gameManager;

        private void OnDisable()
        {
            _pause.onClick.RemoveAllListeners();
            _unPause.onClick.RemoveAllListeners();
            _menu.onClick.RemoveAllListeners();
        }
        
        public override void Activate()
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;
            
            gameObject.SetActive(true);
            _pauseWindow.gameObject.SetActive(false);

            _level.text = _gameManager.LevelID.ToString();
            _levelBar.minValue = 0;
            _levelBar.maxValue = 100;
            UpdatePanel();

            _pause.onClick.AddListener(OnPauseClick);
            _menu.onClick.AddListener(OnMenuClick);
        }

        public override void UpdatePanel()
        {
            _levelBar.value = _gameManager.LevelCompletedOn;
            _coins.text = _gameManager.Bank.LevelBalance.ToString();
        }

        private void OnPauseClick()
        {
            _gameManager.OnPauseClicked();
            
            _pauseWindow.gameObject.SetActive(true);
            _pause.onClick.RemoveListener(OnPauseClick);
            _unPause.onClick.AddListener(OnUnPauseClick);
        }
        
        private void OnUnPauseClick()
        {
            _gameManager.OnUnPauseClicked();

            _pauseWindow.gameObject.SetActive(false);
            _unPause.onClick.RemoveListener(OnUnPauseClick);
            _pause.onClick.AddListener(OnPauseClick);
        }

        private void OnMenuClick()
        {
            gameObject.SetActive(false);
            _gameManager.ReturnToMenu();
        }
    }
}