using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ScrollShooter
{
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _shotPoint;
        
        private BulletFactory _factory;
        private List<ShotConfig> _modifications;
        private int _fireRate;

        private bool _shoot = false;
        private float _nextFireTime = 0f;
        private Tween _shootAnimation;

        public void Init(PlayerConfig config)
        {
            _fireRate = config.FireRate;
            _modifications = config.Modification;

            _factory = new BulletFactory(_modifications[0], config.BulletDamage, config.BulletSpeed);
            _shoot = true; 
        }

        private void Update()
        {
            if (_shoot == true)
            {
                if (Time.time > _nextFireTime)
                {
                    Shoot();
                    _nextFireTime = Time.time + 1f / _fireRate;
                }
            }
        }
        
        private void Shoot()
        {
            foreach (ShotConfig shotConfig in _modifications)
            {
                _factory.CreateOfType(shotConfig, _shotPoint.position);
            }
            
            _shootAnimation.Kill();
            _prefab.transform.localScale = Vector3.one;
            _shootAnimation = _prefab.transform.DOShakeScale(_fireRate / 5f, Vector3.up / 10f).SetLink(gameObject);
        }
    }
}