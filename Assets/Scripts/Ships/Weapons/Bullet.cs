using System;
using UnityEngine;

namespace ShootEmUp.Ships.Weapons
{
    [RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
    public class Bullet : MonoBehaviour
    {
        private bool _isEnemy;
        private int _damage;

        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody;
        private Transform _transform;

        public event Action<Bullet> Collided;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _transform = transform;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out Ship ship) && ship.IsEnemy != _isEnemy)
                ship.TakeDamage(_damage);

            Collided?.Invoke(this);
        }

        public void SetAttackInfo(bool isEnemy, int damage)
        {
            _isEnemy = isEnemy;
            _damage = damage;
        }

        public void SetColorAndLayer(Color color, int layer)
        {
            _spriteRenderer.color = color;
            gameObject.layer = layer;
        }

        public void SetPositionAndVelocity(Vector3 position, Vector2 velocity)
        {
            _transform.position = position;
            _rigidbody.velocity = velocity;
        }
    }
}