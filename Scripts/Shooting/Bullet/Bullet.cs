using System;
using System.Collections;
using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TrailRenderer))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private float _maxTrailTime;
        [SerializeField] private bool _needChangeSize;
        
        private const float _dieDistance = 8.5f;
        private float _damage;
        private Action _onRelease;
        private Vector3 _standartScale;

        public void Init(float damage, float speed, Vector3 startPos, Vector3 direction, Action onRelease)
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
            
            if (_trail == null)
                _trail = GetComponent<TrailRenderer>();
            
            _damage = damage;
            _onRelease = onRelease;

            SetEffects(in speed);
            
            transform.position = startPos;
            _rigidbody.velocity = direction * speed;
            
            float lifeTime = _dieDistance / speed;
            StartCoroutine(AutoDestroyBullet(lifeTime));
        }

        private void SetEffects(in float speed)
        {
            if (_needChangeSize == true)
            {
                _standartScale = _prefab.transform.localScale;
                _prefab.transform.localScale += _prefab.transform.localScale * (_damage / 50f);
            }
            
            if (_maxTrailTime > 0.0f)
            {
                _trail.startWidth = _prefab.transform.localScale.x;
                _trail.time = _maxTrailTime - _maxTrailTime / (speed / 10f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Cube cube))
            {
                cube.TakeDamage(_damage);
                DestroyBullet();
            }
        }
        
        private IEnumerator AutoDestroyBullet(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            DestroyBullet();
        }

        private void DestroyBullet()
        {
            if (_needChangeSize == true)
                _prefab.transform.localScale = _standartScale;
            
            _trail.Clear();
            _onRelease?.Invoke();
        }
    }
}