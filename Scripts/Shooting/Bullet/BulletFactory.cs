using UnityEngine;
using UnityEngine.Pool;

namespace ScrollShooter
{
    public class BulletFactory
    {
        private ShotConfig _currentShot;
        private float _damage;
        private float _speed;

        private ObjectPool<Bullet> _pool;

        public BulletFactory(ShotConfig shot, float damage, float speed)
        {
            _currentShot = shot;
            _damage = damage;
            _speed = speed;

            _pool = new ObjectPool<Bullet>(
                OnCreateBullet,
                OnGetBullet,
                OnReleaseBullet);
        }

        public void CreateOfType(ShotConfig config, in Vector3 position)
        {
            _currentShot = config;
            
            switch (config.Type())
            {
                case ShotType.Forward:
                    CreateForwardType((ShotForward)config, position);
                    break;
                case ShotType.Angle:
                    CreateAngleType((ShotAngle)config, position);
                    break;
            }
        }
        
        #region Object pooling callbacks

        private Bullet OnCreateBullet()
        {
            Bullet instance = GameObject.Instantiate(_currentShot.Prefab, Vector3.zero, Quaternion.identity);
            instance.gameObject.SetActive(false);
            
            return instance;
        }

        private void OnGetBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseBullet(Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        #endregion

        private void CreateAngleType(ShotAngle config, in Vector3 position)
        {
            if (config.Bullets < 2)
                return;

            int middleBullet = (config.Bullets / 2) - 1;
            float bulletAngle = -config.FireAngle / 2f;
            float stepAngle = config.FireAngle / config.Bullets;

            Vector3 direction = Quaternion.AngleAxis(bulletAngle, Vector3.up) * Vector3.forward;

            for (int i = 0; i < config.Bullets; i++)
            {
                direction = Quaternion.AngleAxis(bulletAngle, Vector3.up) * Vector3.forward;
                GetBullet(position, direction);
                bulletAngle += stepAngle;
                
                if (i == middleBullet)
                    bulletAngle += stepAngle;
            }
        }
        
        private void CreateForwardType(ShotForward config, Vector3 position)
        {
            if (config.Bullets <= 1)
            {
                GetBullet(position, Vector3.forward);;
                return;
            }

            float bulletPositionX = position.x;
            float distanceStep = config.BulletsDistance;
            
            if (config.Bullets % 2 == 0)
                bulletPositionX -= (distanceStep / 2f) + (distanceStep * ((config.Bullets - 2) / 2f));
            else
                bulletPositionX -= distanceStep * ((config.Bullets - 1) / 2f);

            for (int i = 0; i < config.Bullets; i++)
            {
                position.x = bulletPositionX;
                GetBullet(position, Vector3.forward);

                bulletPositionX += distanceStep;
            }
        }

        private void GetBullet(Vector3 position, in Vector3 direction)
        {
            Bullet bullet = _pool.Get();
            bullet.Init(
                _damage,
                _speed,
                position,
                direction,
                () => _pool.Release(bullet));
        }
    }
}