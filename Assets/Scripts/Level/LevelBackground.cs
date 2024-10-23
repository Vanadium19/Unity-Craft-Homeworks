using UnityEngine;

namespace ShootEmUp.Level
{
    public sealed class LevelBackground : MonoBehaviour
    {
        [SerializeField] private float _startPositionY = 19;
        [SerializeField] private float _endPositionY = -38;
        [SerializeField] private float _movingSpeedY = 5;

        private Transform _transform;
        private float _positionX;
        private float _positionZ;

        private void Awake()
        {
            _transform = transform;
            _positionX = _transform.position.x;
            _positionZ = _transform.position.z;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (_transform.position.y <= _endPositionY)
                _transform.position = new Vector3(_positionX, _startPositionY, _positionZ);

            _transform.Translate(Vector3.down * _movingSpeedY * Time.deltaTime);
        }
    }
}