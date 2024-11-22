using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UI.DefaultControls;

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

        public Converter(int loadingAreaCapacity,
                         int unloadingAreaCapacity,
                         int takenResourcesCount,
                         int givenResourcesCount,
                         float conversionTime,
                         bool enabled = false)
        {
            if (loadingAreaCapacity <= 0
                || unloadingAreaCapacity <= 0
                || takenResourcesCount <= 0
                || givenResourcesCount <= 0
                || conversionTime <= 0)
            {
                throw new ArgumentException();
            }

            _loadingArea = new Queue<Resource>(loadingAreaCapacity);
            _unloadingArea = new Queue<Product>(unloadingAreaCapacity);
            _conversionResources = new List<Resource>(takenResourcesCount);
            _enabled = enabled;
            _elapsedTime = 0f;

            _loadingAreaCapacity = loadingAreaCapacity;
            _unloadingAreaCapacity = unloadingAreaCapacity;
            _takenResourcesCount = takenResourcesCount;
            _givenResourcesCount = givenResourcesCount;
            _conversionTime = conversionTime;
            _proportion = (float)givenResourcesCount / takenResourcesCount;
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
            if (resources == null)
                throw new ArgumentNullException();

            foreach (var resource in resources)
            {
                if (resource == null)
                {
                    throw new ArgumentException();
                }
            }

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
            if (resources == null)
                throw new ArgumentNullException();

            foreach (var resource in resources)
            {
                if (resource == null)
                {
                    throw new ArgumentException();
                }
            }

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
            }
        }

        public void Update(float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentException();

            if (!_enabled)
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
            for (int i = 0; i < TakenResourcesCount; i++)
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
    }
}