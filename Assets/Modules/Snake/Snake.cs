using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Modules
{
    //Don't modify
    public sealed class Snake : MonoBehaviour, ISnake
    {
        public event Action<Vector2Int> OnMoved;
        public event Action OnSelfCollided;

        [Header("Spine")]
        [FormerlySerializedAs("_bodyPrefab")]
        [SerializeField]
        private Transform _bonePrefab;

        [FormerlySerializedAs("_initialBody")]
        [SerializeField]
        private Transform[] _initialBones;

        [Header("Movement")]
        [SerializeField]
        private float _speed = 2;

        [SerializeField]
        private SnakeDirection _currentDirection = SnakeDirection.UP;

        [Title("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
        private float _movementTick;

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private Vector3 _moveDirection;

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private readonly List<Transform> _currentBones = new();

        private const int HeadIndex = 0;
        private const int SpineIndex = HeadIndex + 1;

        public Vector2Int HeadPosition
        {
            get
            {
                Vector3 position = _currentBones[HeadIndex].position;
                return new Vector2Int((int) position.x, (int) position.y);
            }
        }

        private void Awake()
        {
            _currentBones.AddRange(_initialBones);
            this.SetMoveDirection(_currentDirection);
        }

        private void FixedUpdate()
        {
            _movementTick += Time.fixedDeltaTime;
            float speedRate = 1 / _speed;

            if (_movementTick < speedRate)
                return;

            this.MoveStep();
            _movementTick -= speedRate;

            if (this.CheckSelfCollided())
            {
                this.OnSelfCollided?.Invoke();
                this.enabled = false;
            }
        }

        [Button, HideInEditorMode]
        public void SetSpeed(float speed)
        {
            _speed = Mathf.Max(0, speed);
        }

        public void SetActive(bool active)
        {
            this.enabled = active;
        }

        [Button, HideInEditorMode]
        public void Turn(SnakeDirection direction)
        {
            if (direction == SnakeDirection.NONE)
                return;

            if (_currentDirection == direction)
                return;

            this.SetMoveDirection(direction);
            _currentDirection = direction;
        }

        [Button, HideInEditorMode]
        public void Expand(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (amount == 0)
                return;

            for (int i = 0; i < amount; i++)
            {
                int tailIndex = _currentBones.Count - 1;

                Vector3 tailPosition = _currentBones[tailIndex].position;
                Transform bone = Instantiate(_bonePrefab, tailPosition, Quaternion.identity, transform);

                _currentBones.Insert(tailIndex, bone);

                tailIndex++;
                _currentBones[tailIndex].position = tailPosition - _moveDirection;
            }
        }

        private void SetMoveDirection(SnakeDirection direction)
        {
            _moveDirection = direction switch
            {
                SnakeDirection.UP when _currentDirection != SnakeDirection.DOWN => Vector2.up,
                SnakeDirection.DOWN when _currentDirection != SnakeDirection.UP => Vector2.down,
                SnakeDirection.LEFT when _currentDirection != SnakeDirection.RIGHT => Vector2.left,
                SnakeDirection.RIGHT when _currentDirection != SnakeDirection.LEFT => Vector2.right,
                _ => _moveDirection
            };
        }

        private void MoveStep()
        {
            Transform head = _currentBones[HeadIndex];

            Vector3 previousPosition = head.position;
            head.position += _moveDirection;

            for (int i = SpineIndex, count = _currentBones.Count; i < count; i++)
            {
                Transform bone = _currentBones[i];
                (bone.position, previousPosition) = (previousPosition, bone.position);
            }

            Vector3 newPosition = head.position;
            this.OnMoved?.Invoke(new Vector2Int((int) newPosition.x, (int) newPosition.y));
        }

        private bool CheckSelfCollided()
        {
            Vector3 headPosition = _currentBones[HeadIndex].position;

            for (int i = HeadIndex + 1; i < _currentBones.Count; i++)
            {
                Vector3 bonePosition = _currentBones[i].position;
                if (bonePosition == headPosition)
                    return true;
            }

            return false;
        }
    }
}