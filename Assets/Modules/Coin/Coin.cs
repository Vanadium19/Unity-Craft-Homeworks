using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules
{
    //Don't modify
    public sealed class Coin : SerializedMonoBehaviour, ICoin
    {
        private const int RandomRangeOffset = 1;
        
        [SerializeField]
        private int _minWeight;

        [SerializeField]
        private int _maxWeight;

        [OdinSerialize]
        private Dictionary<int, Color> _colors;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public int Bones => _weight;
        public int Score => _weight * 10;

        private int _weight;

        public Vector2Int Position
        {
            get
            {
                Vector3 position = this.transform.position;
                return new Vector2Int((int) position.x, (int) position.y);
            }
            set
            {
                Vector3 position = new Vector3(value.x, value.y);
                this.transform.position = position;
            }
        }

        private void OnValidate()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Generate()
        {
            int weight = Random.Range(_minWeight, _maxWeight + RandomRangeOffset);
            _weight = weight;
            _spriteRenderer.color = _colors[weight];
        }
    }
}