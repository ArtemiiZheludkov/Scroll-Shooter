using UnityEngine;

namespace ScrollShooter
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        private float _speed;
        private bool _move = false;
        private Vector3 _lastMousePosition;
        private Rigidbody _rigidbody;

        public void Init(float speed)
        {
            _speed = speed;
            _rigidbody = GetComponent<Rigidbody>();

            _move = true;
        }
        
        private void Update()
        {
            if (_move == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _lastMousePosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(0))
                {
                    Move();
                }
            }
        }

        private void Move()
        {
            Vector3 deltaMousePos = Input.mousePosition - _lastMousePosition;
            float normalizedX = deltaMousePos.x / Screen.width;
            
            Vector3 playerPos = transform.position;
            Vector3 newPosition = new Vector3(
                Mathf.Clamp(playerPos.x + normalizedX * _speed * Time.deltaTime, -2.5f, 2.5f),
                playerPos.y,
                playerPos.z);

            _rigidbody.MovePosition(newPosition);
            
            _lastMousePosition = Input.mousePosition;
        }
    }
}