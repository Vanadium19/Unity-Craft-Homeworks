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

            if (_unloadingAreaCapacity - _unloadingArea.Count < _givenResourcesCount)
                return;

            if (_loadingArea.Count < _takenResourcesCount && _conversionResources.Count == 0)
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
            for (int i = 0; i < _takenResourcesCount; i++)
            {
                if (_loadingArea.TryDequeue(out Resource resource))
                {
                    _conversionResources.Add(resource);
                }
            }
        }

        private void EndConvert()
        {
            for (int i = 0; i < _givenResourcesCount; i++)
                _unloadingArea.Enqueue(new Product());

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