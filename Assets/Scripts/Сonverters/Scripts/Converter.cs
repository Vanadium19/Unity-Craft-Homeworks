using System;
using System.Collections.Generic;
using UnityEngine;

namespace Converters
{
    public class Converter
    {
        private readonly Queue<Resource> _loadingArea;
        private readonly Queue<Product> _unloadingArea;
        private readonly List<Resource> _conversionResources;

        private readonly int _loadingAreaCapacity;
        private readonly int _unloadingAreaCapacity;
        private readonly int _takenResourcesCount;
        private readonly int _givenResourcesCount;
        public readonly float _conversionTime;
        private readonly float _proportion;

        private bool _enabled;
        private float _elapsedTime;

        public Converter(ConverterArguments arguments, bool enabled = false)
        {
            _loadingArea = new Queue<Resource>(arguments.LoadingAreaCapacity);
            _unloadingArea = new Queue<Product>(arguments.UnloadingAreaCapacity);
            _conversionResources = new List<Resource>(arguments.TakenResourcesCount);
            _enabled = enabled;
            _elapsedTime = 0f;

            _loadingAreaCapacity = arguments.LoadingAreaCapacity;
            _unloadingAreaCapacity = arguments.UnloadingAreaCapacity;
            _takenResourcesCount = arguments.TakenResourcesCount;
            _givenResourcesCount = arguments.GivenResourcesCount;
            _conversionTime = arguments.ConversionTime;
            _proportion = (float)_givenResourcesCount / _takenResourcesCount;
        }

        public bool Enabled => _enabled;
        public float ElapsedTime => _elapsedTime;
        public int LoadingAreaCount => _loadingArea.Count;
        public int LoadingAreaCapacity => _loadingAreaCapacity;
        public int UnloadingAreaCapacity => _unloadingAreaCapacity;
        public int UnloadingAreaCount => _unloadingArea.Count;
        public int TakenResourcesCount => _takenResourcesCount;
        public int GivenResourcesCount => _givenResourcesCount;
        public float ConversionTime => _conversionTime;
        public int ConversionResourcesCount => _conversionResources.Count;

        public void Add(IEnumerable<Resource> resources, out IReadOnlyList<Resource> extraResources)
        {
            CheckResources(resources);

            List<Resource> extra = new();

            foreach (var resource in resources)
            {
                if (_loadingArea.Count < _loadingAreaCapacity)
                    _loadingArea.Enqueue(resource);
                else
                    extra.Add(resource);
            }

            extraResources = extra;
        }

        public void Add(IEnumerable<Resource> resources)
        {
            CheckResources(resources);

            foreach (var resource in resources)
            {
                if (_loadingArea.Count == _loadingAreaCapacity)
                    break;

                _loadingArea.Enqueue(resource);
            }
        }

        public void SetActive(bool value)
        {
            _enabled = value;

            if (!value)
            {
                Add(_conversionResources);
                _conversionResources.Clear();
                _elapsedTime = 0f;
            }
        }

        public void Update(float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentException();

            if (!_enabled)
                return;

            if (_unloadingArea.Count == _unloadingAreaCapacity)
                return;

            if (_loadingArea.Count == 0 && _conversionResources.Count == 0)
                return;

            if (_conversionResources.Count == 0)
                StartConvert();

            _elapsedTime += deltaTime;

            if (_elapsedTime >= _conversionTime)
            {
                EndConvert();
                _elapsedTime = 0;
            }
        }

        private void StartConvert()
        {
            int takenResourcesCount = _takenResourcesCount;
            int freeProductSpace = _unloadingAreaCapacity - _unloadingArea.Count;

            if (takenResourcesCount > freeProductSpace)
                takenResourcesCount = (int)Mathf.Ceil(freeProductSpace / _proportion);

            for (int i = 0; i < takenResourcesCount; i++)
            {
                if (_loadingArea.TryDequeue(out Resource resource))
                {
                    _conversionResources.Add(resource);
                }
            }
        }

        private void EndConvert()
        {
            int productCount = (int)(_conversionResources.Count * _proportion);

            for (int i = 0; i < productCount; i++)
            {
                if (_unloadingArea.Count == _unloadingAreaCapacity)
                    break;

                _unloadingArea.Enqueue(new Product());
            }

            _conversionResources.Clear();
        }

        private void CheckResources(IEnumerable<Resource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException();

            foreach (var resource in resources)
            {
                if (resource == null)
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}