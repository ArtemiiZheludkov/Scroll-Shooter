using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ScrollShooter
{
    public class WinPanel : UIPanel
    {
        [SerializeField] private Text _coins;
        [SerializeField] private Button _doubleCoins;
        
        [Header("TO MENU")]
        [SerializeField] private float _showMenuDelay;
        [SerializeField] private Button _menu;
        
        private GameManager _gameManager;

        private void OnDisable()
        {
            _doubleCoins.onClick.RemoveAllListeners();
            _menu.onClick.RemoveAllListeners();
        }

        public override void Activate()
        {
            if (_gameManager == null)
                _gameManager = GameManager.Instance;

            UpdatePanel();
            _menu.gameObject.SetActive(false);
            gameObject.SetActive(true);
            
            _doubleCoins.interactable = true;
            //_doubleCoins.onClick.AddListener(OnDoubleCoinsClick);
            _menu.onClick.AddListener(OnMenuClick);

            StartCoroutine(Delay());
        }

        public override void UpdatePanel()
        {
            _coins.text = _gameManager.Bank.LevelBalance.ToString();
        }
        
        private IEnumerator Delay()
        {
            yield return new WaitForSecondsRealtime(_showMenuDelay);
            _menu.gameObject.SetActive(true);
        }

        private void OnDoubleCoinsClick()
        {
            _doubleCoins.onClick.RemoveListener(OnDoubleCoinsClick);
            _doubleCoins.interactable = false;
            _gameManager.Bank.DoubleCoins();
            UpdatePanel();
        }
        
        private void OnMenuClick()
        {
            gameObject.SetActive(false);
            _gameManager.ReturnToMenu();
        }
    }
}