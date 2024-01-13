using UnityEngine;
using UnityEngine.UI;

namespace ScrollShooter
{
    public class LosePanel : UIPanel
    {
        [SerializeField] private Text _coins;
        [SerializeField] private Button _respawn;
        [SerializeField] private Button _menu;
        
        private GameManager _gameManager;
        
        private void OnDisable()
        {
            _respawn.onClick.RemoveAllListeners();
            _menu.onClick.RemoveAllListeners();
        }
        
        public override void Activate()
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;
            
            _coins.text = _gameManager.Bank.LevelBalance.ToString();
            gameObject.SetActive(true);

            if (_gameManager.playerConfig.HasRespawn == true)
            {
                //_respawn.onClick.AddListener(OnRespawnClick);
                _respawn.interactable = true;
            }
            else
            {
                _respawn.interactable = false;
            }
            
            _menu.onClick.AddListener(OnMenuClick);
        }

        public override void UpdatePanel()
        {
            _coins.text = _gameManager.Bank.LevelBalance.ToString();
        }

        private void OnRespawnClick()
        {
            _respawn.onClick.RemoveListener(OnRespawnClick);
            _respawn.interactable = false;
            _gameManager.OnRespawnClicked();
        }
        
        private void OnMenuClick()
        {
            gameObject.SetActive(false);
            _gameManager.ReturnToMenu();
        }
    }
}