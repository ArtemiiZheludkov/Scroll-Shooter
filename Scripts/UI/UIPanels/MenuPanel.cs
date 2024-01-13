using UnityEngine;
using UnityEngine.UI;

namespace ScrollShooter
{
    public class MenuPanel : UIPanel
    {
        [Header("MENU")]
        [SerializeField] private Text _coins;
        [SerializeField] private Text _speed;
        [SerializeField] private Text _damage;
        [SerializeField] private Button _play;

        [Header("UPGRADE PANEL")]
        [SerializeField] private Text _speedPriceText;
        [SerializeField] private Text _damagePriceText;
        [SerializeField] private Button _upSpeed;
        [SerializeField] private Button _upDamage;

        private GameManager _gameManager;
        
        private const int _startPrice = 500;
        private const float _priceIncrease = 0.5f;
        private int _speedPrice, _damagePrice, _increase;

        private void OnDisable()
        {
            _play.onClick.RemoveAllListeners();
            _upSpeed.onClick.RemoveAllListeners();
            _upDamage.onClick.RemoveAllListeners();
        }
        
        public override void Activate()
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;
            
            gameObject.SetActive(true);
            UpdatePanel();
            
            _play.onClick.AddListener(OnPlayClick);
            _upSpeed.onClick.AddListener(OnUpSpeed);
            _upDamage.onClick.AddListener(OnUpDamage);
        }

        public override void UpdatePanel()
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;

            _coins.text = _gameManager.Bank.GameBalance.ToString();
            _speed.text = _gameManager.playerConfig.FireRate.ToString();
            _damage.text = _gameManager.playerConfig.BulletDamage.ToString();

            UpdateUpgradePanel();
        }

        private void UpdateUpgradePanel()
        {
            _speedPrice = _startPrice + (int)((_gameManager.playerConfig.FireRate - 1) * _startPrice * _priceIncrease);
            _damagePrice = _startPrice + (int)((_gameManager.playerConfig.BulletDamage - 1) * _startPrice * _priceIncrease);
            
            _speedPriceText.text = _speedPrice.ToString();
            _damagePriceText.text = _damagePrice.ToString();
            
            if (_speedPrice <= _gameManager.Bank.GameBalance)
                _upSpeed.interactable = true;
            else
                _upSpeed.interactable = false;
            
            if (_damagePrice <= _gameManager.Bank.GameBalance)
                _upDamage.interactable = true;
            else
                _upDamage.interactable = false;
        }
        
        private void OnUpSpeed()
        {
            if (_gameManager.Bank.GetCoins(_speedPrice) == false)
                return;
            
            _gameManager.Player.UpFireRate();

            UpdatePanel();
        }
        
        private void OnUpDamage()
        {
            if (_gameManager.Bank.GetCoins(_damagePrice) == false)
                return;

            _gameManager.Player.UpDamage();

            UpdatePanel();
        }
        
        private void OnPlayClick()
        {
            gameObject.SetActive(false);
            _gameManager.PlayLevel();
        }
    }
}