using DG.Tweening;
using UnityEngine;

namespace ScrollShooter
{
    public abstract class Cube : MonoBehaviour
    {
        [SerializeField] private CubeConfig _config;
        [SerializeField] protected GameObject _prefab;
        [SerializeField] protected CubeService _service;
        
        private float _animationTime, _scaleFactorOnHit, _rotateYFactorOnHit;
        private float _nextAnimationTime;
        
        protected float _healthHit;

        public virtual void Init(int healthHit)
        {
            if (healthHit <= 1)
                _healthHit = 1;
            
            _healthHit = healthHit;
            _animationTime = _config.AnimationTime;
            _scaleFactorOnHit = _config.ScaleFactorOnHit;
            _rotateYFactorOnHit = _config.RotateYFactorOnHit;
            _nextAnimationTime = 0f;
        }
        
        public void TakeDamage(float damage)
        {
            if (damage < 0)
                return;

            _healthHit -= damage;

            if (_healthHit < 1.0f)
                Die();
            else
                OnHit();
        }

        protected virtual void OnHit()
        {
            if (Time.time > _nextAnimationTime)
            {
                _nextAnimationTime = Time.time + _animationTime;

                float rotateY = _prefab.transform.rotation.y <= 0f ? 
                    _rotateYFactorOnHit : -_rotateYFactorOnHit;
                
                DOTween.Sequence()
                    .Append(_prefab.transform.DORotate(new Vector3(0f, rotateY, 0f), _animationTime / 3f))
                    .Append(_prefab.transform.DOScale(Vector3.one * _scaleFactorOnHit, _animationTime / 3f))
                    .Append(_prefab.transform.DOScale(Vector3.one, _animationTime / 3f))
                    .SetLink(gameObject);
            }
        }

        protected virtual void Die()
        {
            _prefab.transform.localScale = Vector3.one;
            
            DOTween.Sequence()
                .Append(_prefab.transform.DOShakePosition(_animationTime / 3f, Vector3.right / 10f))
                .Append(_prefab.transform.DOScale(Vector3.one * _scaleFactorOnHit, _animationTime / 3f))
                .Append(_prefab.transform.DOScale(Vector3.zero, _animationTime / 3f))
                .SetLink(gameObject);
            
            _service.SpawnDestroyParticle(transform);
            GameManager.Instance.CubeDestroyed();
            Destroy(gameObject);
        }
    }
}