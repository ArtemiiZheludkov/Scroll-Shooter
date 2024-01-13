using System;
using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(PlayerMover))]
    [RequireComponent(typeof(PlayerShooter))]
    [RequireComponent(typeof(BoxCollider))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private ShotConfig[] _standartShotConfigs;
        [SerializeField] private MiniShooters _miniShooters;

        private PlayerMover _mover;
        private PlayerShooter _shooter;
        private Action _died;

        public void Init(Action died)
        {
            _mover = GetComponent<PlayerMover>();
            _shooter = GetComponent<PlayerShooter>();
            _died = died;

            if (_config.Modification.Count <= 1)
            {
                foreach (ShotConfig standart in _standartShotConfigs)
                {
                    _config.PlayerAddShot(standart);
                }
            }

            _mover.Init(_config.MoveSpeed);
            _shooter.Init(_config);
            
            if (_config.MiniShootersActivated == true)
                _miniShooters.Init();

            _miniShooters.gameObject.SetActive(_config.MiniShootersActivated);
        }

        public void PlayerUpdate()
        {
            if (_config.MiniShootersActivated == true)
                _miniShooters.Init();
            
            _miniShooters.gameObject.SetActive(_config.MiniShootersActivated);
            
            if (_config.HasShield == true)
                ShieldActivated();
        }

        public void Pause()
        {
            _mover.enabled = false;
            _shooter.enabled = false;
            _miniShooters.enabled = false;
        }
        
        public void UnPause()
        {
            _mover.enabled = true;
            _shooter.enabled = true;
            _miniShooters.gameObject.SetActive(_config.MiniShootersActivated);
            _miniShooters.enabled = _config.MiniShootersActivated;
        }

        private void ShieldActivated()
        {
            //activate shield animation or model
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cube cube) == true)
            {
                if (_config.HasShield == true)
                    _config.RemovedShield();
                else
                    _died.Invoke();
            }
        }
    }
}